﻿using Microsoft.EntityFrameworkCore;
using ShippingDocuments.Data;
using ShippingDocuments.Domain;
using ShippingDocuments.Infrastructure.OData;
using ShippingDocuments.Infrastructure.OData.Models;
using ShippingDocuments.Infrastructure.Whs.Models;

namespace ShippingDocuments.Application
{
    public interface ISaleDocService
    {
        Task CreateAsync(string mngrOrderString);
        Task UpdateAsync(SaleDoc saleDoc);
        Task<List<SaleDoc>> GetList();
        Task<List<SaleDoc>?> GetList(string invoiceRefKey);
        Task<SaleDoc?> Get(Guid id);
        Task DeleteUserAsync(string userId);
    }

    public class SaleDocService(
        ApplicationDbContext dbContext,
        IODataService oDataService,
        AuthService authService,
        ILogger<SaleDocService> logger) : ISaleDocService
    {
        public async Task CreateAsync(string mngrOrderString)
        {
            var mngrDocuments = MngrOrder.From(mngrOrderString);

            if (mngrDocuments is null)
            {
                logger.LogWarning("{Source} baseDocuments is null", nameof(CreateAsync));
                return;
            }

            var saleDocs = mngrDocuments
                .Where(e => e.Распоряжение_Name != null &&
                            e.Распоряжение_Name.Contains(Document_РеализацияТоваровУслуг.DocumentName))
                .Select(e => SaleDoc.From(e));

            foreach (var doc in saleDocs)
            {
                if (!dbContext.SaleDocs.Any(e => e.Id == doc.Id))
                    await dbContext.SaleDocs.AddAsync(doc);
            }

            await dbContext.SaveChangesAsync();

            logger.LogDebug("{Source} {@SaleDocs}", nameof(CreateAsync), saleDocs);
        }

        public async Task UpdateAsync(SaleDoc item)
        {
            item.UserId = await authService.GetCurrentUserIdAsync();

            dbContext.Update(item);

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<SaleDoc>> GetList()
        {
            var result = await dbContext.SaleDocs
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<SaleDoc?> Get(Guid id)
        {
            var item = await dbContext.SaleDocs
                .Include(e => e.PaperworkErrors)
                .Include(e => e.QuantityErrors)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (item is null)
                return null;

            item.User = await authService.FindByIdAsync(item.UserId);

            return item;
        }

        public async Task<List<SaleDoc>?> GetList(string invoiceRefKey)
        {
            var baseDocuments = await oDataService.GetDocument_СчетФактураВыданный_ДокументыОснования(invoiceRefKey);

            if (baseDocuments is null)
                return null;

            var baseDocumentsStr = string.Join(" ", baseDocuments.Select(d => d.ДокументОснование));

            var saleDocs = await dbContext.SaleDocs
                 .AsNoTracking()
                 .Where(saleDoc => baseDocumentsStr.Contains(saleDoc.Id.ToString()))
                 .ToListAsync();

            if (saleDocs is null || saleDocs.Count == 0)
                saleDocs = await TryLoadAndCreate(baseDocuments, saleDocs);

            return saleDocs;
        }

        private async Task<List<SaleDoc>?> TryLoadAndCreate(Document_СчетФактураВыданный_ДокументыОснования[] baseDocuments, List<SaleDoc>? saleDocs)
        {
            saleDocs ??= [];

            foreach (var baseDocument in baseDocuments)
            {
                var document = await oDataService.GetDocument_РеализацияТоваровУслуг(baseDocument.ДокументОснование);
                if (document is null)
                    continue;

                saleDocs.Add(SaleDoc.From(document));
            }

            if (saleDocs is not null && saleDocs.Count > 0)
            {
                await dbContext.SaleDocs.AddRangeAsync(saleDocs);

                await dbContext.SaveChangesAsync();
            }

            return saleDocs;
        }

        public async Task DeleteUserAsync(string userId)
        {
            await dbContext.SaleDocs
                .Where(e => e.UserId == userId)
                .ExecuteDeleteAsync();
        }
    }
}

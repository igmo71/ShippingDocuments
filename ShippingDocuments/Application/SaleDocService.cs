using Microsoft.EntityFrameworkCore;
using ShippingDocuments.Data;
using ShippingDocuments.Domain;
using ShippingDocuments.Infrastructure.OData.Models;
using ShippingDocuments.Infrastructure.Whs.Models;

namespace ShippingDocuments.Application
{
    public interface ISaleDocService
    {
        Task CreateAsync(string mngrOrderString);
        Task UpdateAsync(SaleDoc saleDoc);
        Task<List<SaleDoc>> GetList();
        Task<SaleDoc?> Get(Guid id);
    }

    public class SaleDocService(ApplicationDbContext dbContext, ILogger<SaleDocService> logger) : ISaleDocService
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

            return item;
        }
    }
}

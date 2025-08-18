using ShippingDocuments.Data;
using ShippingDocuments.Domain;
using ShippingDocuments.Infrastructure.OData.Models;
using ShippingDocuments.Infrastructure.Whs.Models;

namespace ShippingDocuments.Application
{
    public interface ISaleDocService
    {
        Task CreateAsync(string mngrOrderString);
    }

    public class SaleDocService(ApplicationDbContext dbContext, ILogger<SaleDocService> logger) : ISaleDocService
    {
        public async Task CreateAsync(string mngrOrderString)
        {
            var baseDocuments = MngrOrder.From(mngrOrderString);

            if (baseDocuments is null)
            {
                logger.LogWarning("{Source} baseDocuments is null", nameof(CreateAsync));
                return;
            }

            var saleDocs = baseDocuments
                .Where(e => e.Распоряжение_Name != null &&
                            e.Распоряжение_Name.Contains(Document_РеализацияТоваровУслуг.DocumentName))
                .Select(e => SaleDoc.From(e));

            foreach (var doc in saleDocs)
            {
                if (dbContext.SaleDocs.Any(e => e.Id == doc.Id))
                    continue;

                await dbContext.SaleDocs.AddAsync(doc);
            }

            await dbContext.SaveChangesAsync();

            logger.LogDebug("{Source} {@SaleDocs}", nameof(CreateAsync), saleDocs);
        }
    }
}

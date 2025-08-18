using ShippingDocuments.Infrastructure.OData.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShippingDocuments.Infrastructure.OData
{
    public interface IODataService
    {
        Task<Document_РеализацияТоваровУслуг?> GetDocument_РеализацияТоваровУслуг(string refKey);
    }

    public class ODataService(ODataClient oDataClient, ILogger<IODataService> logger) : IODataService
    {
        public async Task<Document_РеализацияТоваровУслуг?> GetDocument_РеализацияТоваровУслуг(string refKey)
        {
            var uri = BuildUri(Document_РеализацияТоваровУслуг.ODataParams, refKey);

            var result = await oDataClient.GetDataAsync<Document_РеализацияТоваровУслуг>(uri);

            return result;
        }

        private string BuildUri(ODataParams oDataParams, string? refKey)
        {
            var uri = $"{oDataParams.ODataObjectName}?$format=json";

            if (!string.IsNullOrEmpty(oDataParams.Inlinecount))
                uri += $"&$inlinecount={oDataParams.Inlinecount}";

            if (!string.IsNullOrEmpty(oDataParams.Select))
                uri += $"&$select={oDataParams.Select}";

            if (!string.IsNullOrEmpty(oDataParams.Expand))
                uri += $"&$expand={oDataParams.Expand}";

            if (!string.IsNullOrEmpty(refKey))
                uri += $"Ref_Key eq guid'{refKey}'";

            return uri;
        }
    }
}

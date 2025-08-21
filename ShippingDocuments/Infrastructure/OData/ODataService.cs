using ShippingDocuments.Infrastructure.OData.Models;

namespace ShippingDocuments.Infrastructure.OData
{
    public interface IODataService
    {
        Task<Document_РеализацияТоваровУслуг?> GetDocument_РеализацияТоваровУслуг(string refKey);
        Task<Document_РеализацияТоваровУслуг_Товары[]?> GetDocument_РеализацияТоваровУслуг_Товары(string refKey);
        Task<Document_СчетФактураВыданный_ДокументыОснования[]?> GetDocument_СчетФактураВыданный_ДокументыОснования(string refKey);
    }

    public class ODataService(ODataClient oDataClient) : IODataService
    {
        public async Task<Document_РеализацияТоваровУслуг?> GetDocument_РеализацияТоваровУслуг(string refKey)
        {
            var uri = BuildUri(Document_РеализацияТоваровУслуг.ODataParams, refKey);

            var result = await oDataClient.GetDataAsync<Document_РеализацияТоваровУслуг>(uri);

            return result;
        }

        public async Task<Document_РеализацияТоваровУслуг_Товары[]?> GetDocument_РеализацияТоваровУслуг_Товары(string refKey)
        {
            var uri = BuildUri(Document_РеализацияТоваровУслуг_Товары.ODataParams, refKey);

            var rootobject = await oDataClient.GetDataAsync<Rootobject<Document_РеализацияТоваровУслуг_Товары>>(uri);

            var result = rootobject?.Value;

            return result;
        }

        public async Task<Document_СчетФактураВыданный_ДокументыОснования[]?> GetDocument_СчетФактураВыданный_ДокументыОснования(string refKey)
        {
            var uri = BuildUri(Document_СчетФактураВыданный_ДокументыОснования.ODataParams, refKey);

            var rootobject = await oDataClient.GetDataAsync<Rootobject<Document_СчетФактураВыданный_ДокументыОснования>>(uri);

            var result = rootobject?.Value;

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
                uri += $"&$filter=Ref_Key eq guid'{refKey}'";

            return uri;
        }
    }
}

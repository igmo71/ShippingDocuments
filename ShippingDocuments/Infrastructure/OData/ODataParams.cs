namespace ShippingDocuments.Infrastructure.OData
{
    public class ODataParams
    {
        public  string? ODataObjectName { get; set; }
        public string? Select { get; set; }
        public string? Expand { get; set; }
        public string? OrderBy { get; set; }
        public string? Inlinecount { get; set; }
    }
}

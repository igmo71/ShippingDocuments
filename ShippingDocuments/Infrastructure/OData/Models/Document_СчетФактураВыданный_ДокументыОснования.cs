namespace ShippingDocuments.Infrastructure.OData.Models
{
    public class Document_СчетФактураВыданный_ДокументыОснования
    {
        public string? Ref_Key { get; set; }
        public int LineNumber { get; set; }
        public string? ДокументОснование { get; set; }

        public static ODataParams ODataParams => new()
        {
            ODataObjectName = nameof(Document_СчетФактураВыданный_ДокументыОснования),
            Select = "Ref_Key,LineNumber,ДокументОснование"
        };


        //public string ДокументОснование_Type { get; set; }
        //public string ХозяйственнаяОперация { get; set; }
        //public string СчетФактураПолученныйОтПродавца_Key { get; set; }
        //public string ПорядковыеНомераСтрок { get; set; }
    }
}

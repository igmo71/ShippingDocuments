using ShippingDocuments.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ShippingDocuments.Domain
{
    public class SaleDocLog
    {
        public SaleDocLog()
        { }

        public SaleDocLog(SaleDoc saleDoc)
        {
            DateTime = DateTime.Now;
            SaleDocId = saleDoc.Id;
            Log = JsonSerializer.Serialize(saleDoc, AppSettings.JsonSerializerOptions);
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public Guid SaleDocId { get; set; }

        public string? Log { get; set; }

        [NotMapped]
        public SaleDoc? SaleDoc
        {
            get
            {
                if (Log is null)
                    return null;

                return JsonSerializer.Deserialize<SaleDoc>(Log);
            }
        }
    }
}

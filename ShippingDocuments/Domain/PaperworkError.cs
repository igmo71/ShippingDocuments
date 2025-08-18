using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShippingDocuments.Domain
{
    public class PaperworkError
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SaleDocId { get; set; }

        [JsonIgnore]
        public SaleDoc? SaleDoc { get; set; } 

        public PaperworkErrorType Type { get; set; }

        [MaxLength(450)]
        public string? Message { get; set; }
    }

    public enum PaperworkErrorType
    {
        [Description("Прочее")]
        Other,

        [Description("Печать")]
        Stamp,

        [Description("Дата получения")]
        ReceiptCargoDate,

        [Description("Должность грузополучателя")]
        ConsigneeJobTitle,

        [Description("Подпись грузополучателя")]
        ConsigneeSignature,

        [Description("ФИО грузополучателя")]
        ConsigneeFullName,

        [Description("Доверенность на право получения")]
        ConsigneePowerOfAttorney,

        [Description("Должность ответственного")]
        ResponsibleJobTitle,

        [Description("Подпись ответственного")]
        ResponsibleSignature,

        [Description("ФИО ответственного")]
        ResponsibleFullName,

        [Description("Доверенность на право оформления")]
        ResponsiblePowerOfAttorney
    }
}

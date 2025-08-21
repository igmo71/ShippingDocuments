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

        [Description("Отсутствует Печать")]
        Stamp,

        [Description("Отсутствует или не правильная Дата получения")]
        ReceiptCargoDate,

        [Description("Отсутствует Должность грузополучателя")]
        ConsigneeJobTitle,

        [Description("Отсутствует Подпись грузополучателя")]
        ConsigneeSignature,

        [Description("Отсутствует ФИО грузополучателя")]
        ConsigneeFullName,

        [Description("Отсутствует Доверенность на право получения")]
        ConsigneePowerOfAttorney,

        [Description("Отсутствует Должность ответственного")]
        ResponsibleJobTitle,

        [Description("Отсутствует Подпись ответственного")]
        ResponsibleSignature,

        [Description("Отсутствует ФИО ответственного")]
        ResponsibleFullName,

        [Description("Отсутствует Доверенность на право оформления")]
        ResponsiblePowerOfAttorney
    }
}

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


        [Description("Нет документа")]
        NoDocument,


        // Печать
        [Description("нет Печати (нужна)")]
        NoStamp,

        [Description("не та Печать")]
        WrongStamp,


        // Грузополучатель
        [Description("нет Должности")]
        ConsigneeJobTitle,

        [Description("нет/не та Подпись")]
        ConsigneeSignature,

        [Description("нет/не то ФИО")]
        ConsigneeFullName,

        [Description("нет/не та Дата получения")]
        ReceiptCargoDate,

        [Description("нет Доверенности на право получения")]
        ConsigneePowerOfAttorney,

        [Description("нет/не тот Адрес получения")]
        ReceiptAddress,


        // Ответственный за оформление
        [Description("нет Должности")]
        ResponsibleJobTitle,

        [Description("нет/не та Подпись")]
        ResponsibleSignature,

        [Description("нет/не то ФИО")]
        ResponsibleFullName,

        [Description("нет Доверенности на право оформления")]
        ResponsiblePowerOfAttorney        
    }
}

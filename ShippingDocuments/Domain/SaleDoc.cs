using ShippingDocuments.Data;
using ShippingDocuments.Infrastructure.Whs.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShippingDocuments.Domain
{
    public class SaleDoc //Document_РеализацияТоваровУслуг
    {        
        [Key]
        public Guid Id { get; set; } // Ref_Key

        [MaxLength(36)]
        public string? Number { get; set; }

        public DateTime? Date { get; set; }

        public string? UserId { get; set; }

        [JsonIgnore]
        public ApplicationUser? User { get; set; }

        public Position? Position { get; set; } = Domain.Position.ForDispatch;

        public Status? Status { get; set; } = Domain.Status.New;

        public List<PaperworkError>? PaperworkErrors { get; set; }

        public List<QuantityError>? QuantityErrors { get; set; }

        public int Redispatch { get; set; } // Повторная отправка

        public static string ODataSShortName => "Реализация";

        public static SaleDoc From(MngrOrder? mngrOrder)
        {// Реализация товаров и услуг КСУТ-006288 от 16.04.2025 10:46:59
            if (mngrOrder == null)
                throw new ArgumentNullException(nameof(mngrOrder));
            
            var saleDoc = new SaleDoc
            {
                Id = Guid.Parse(mngrOrder.Распоряжение_Id),
                Number = mngrOrder.Распоряжение_Name?.Substring(27, 11)
            };

            if (DateTime.TryParse(mngrOrder.Распоряжение_Name?[42..], out DateTime dateTime))
                saleDoc.Date = dateTime;

            return saleDoc;
        }
    }

    [Flags]
    public enum Status
    {
        [Description("None")]
        None = 0b0000,

        [Description("Новый")]
        New = 0b0001,

        [Description("Корректен")]
        Correct = 0b0010,

        [Description("Перепечатать")]
        PaperworkError = 0b0100,

        [Description("Требует корректировки состава товаров")]
        QuantityError = 0b1000
    }

    public enum Position
    {
        [Description("К отправке")]
        ForDispatch,

        [Description("Операторы РЦ")]
        Operator,

        [Description("Менеджеры")]
        Manager,

        [Description("Бухгалтерия к приемке")]
        Accounting,

        [Description("Закрыт")]
        Closed
    }
}

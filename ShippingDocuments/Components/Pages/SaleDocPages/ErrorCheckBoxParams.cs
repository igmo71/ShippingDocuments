using ShippingDocuments.Domain;

namespace ShippingDocuments.Components.Pages.SaleDocPages
{
    public record ErrorCheckBoxParams(PaperworkErrorType ErrorType, bool IsChecked);
}

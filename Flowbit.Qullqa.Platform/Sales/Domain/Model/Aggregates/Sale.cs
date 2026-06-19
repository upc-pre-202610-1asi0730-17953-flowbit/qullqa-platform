using Qullqa.Platform.Sales.Domain.Model.Enums;
using Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Qullqa.Platform.Sales.Domain.Model.Aggregates;

public class Sale : IAuditableEntity
{
    public Sale() { Details = new List<SaleDetail>(); }
    public Sale(int businessId, int? customerId, string description, string currency)
    {
        BusinessId = businessId; CustomerId = customerId; Description = description; Currency = currency;
        Status = SaleStatus.Open; Date = DateTimeOffset.UtcNow; Details = new List<SaleDetail>();
    }

    public int Id { get; private set; }
    public int BusinessId { get; private set; }
    public int? CustomerId { get; private set; }
    public SaleStatus Status { get; private set; } = SaleStatus.Open;
    public decimal TotalAmount { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string Currency { get; private set; } = "PEN";
    public ICollection<SaleDetail> Details { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Sale Pay(PaymentMethod paymentMethod, decimal totalAmount)
    {
        Status = SaleStatus.Paid; PaymentMethod = paymentMethod; TotalAmount = totalAmount; return this;
    }

    public Sale Cancel() { Status = SaleStatus.Cancelled; return this; }
}

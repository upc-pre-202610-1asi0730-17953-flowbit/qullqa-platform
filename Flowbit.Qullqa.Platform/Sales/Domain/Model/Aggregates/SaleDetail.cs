using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;

public class SaleDetail : IAuditableEntity
{
    public SaleDetail() { }
    public SaleDetail(int saleId, int productId, int quantity, decimal unitPrice, decimal discount)
    {
        SaleId = saleId; ProductId = productId; Quantity = quantity; UnitPrice = unitPrice; Discount = discount;
    }

    public int Id { get; private set; }
    public int SaleId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public decimal LineTotal => Math.Round(Math.Max(0, UnitPrice - Discount) * Quantity, 2);
}

using Flowbit.Qullqa.Platform.Product.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Product.Domain.Model.Aggregates;

public class Product : IAuditableEntity
{
    public Product() { }
    public Product(int businessId, string name, string description, ProductCategory category, decimal basePrice)
    {
        BusinessId = businessId;
        Name = name;
        Description = description;
        Category = category;
        BasePrice = basePrice;
        Status = ProductStatus.Active;
    }

    public int Id { get; private set; }
    public int BusinessId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ProductCategory Category { get; private set; }
    public decimal BasePrice { get; private set; }
    public ProductStatus Status { get; private set; } = ProductStatus.Active;
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Product Update(string name, string description, ProductCategory category, decimal basePrice, ProductStatus status)
    {
        Name = name; Description = description; Category = category; BasePrice = basePrice; Status = status;
        return this;
    }
}

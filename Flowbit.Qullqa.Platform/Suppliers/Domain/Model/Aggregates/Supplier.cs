using Qullqa.Platform.Shared.Domain.Model.Entities;
using Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Qullqa.Platform.Suppliers.Domain.Model.Aggregates;

public class Supplier : IAuditableEntity
{
    public int Id { get; private set; }
    public int BusinessId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Ruc { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string ContactPerson { get; private set; } = string.Empty;
    public SupplierCategory Category { get; private set; }
    public SupplierStatus Status { get; private set; } = SupplierStatus.Active;
    public DateTimeOffset Since { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    protected Supplier() { }

    public Supplier(int businessId, string name, string lastName, string ruc, string email, string phone,
        string address, string contactPerson, SupplierCategory category)
    {
        BusinessId = businessId;
        Name = name;
        LastName = lastName;
        Ruc = ruc;
        Email = email;
        Phone = phone;
        Address = address;
        ContactPerson = contactPerson;
        Category = category;
        Since = DateTimeOffset.UtcNow;
    }

    public void Update(string name, string lastName, string email, string phone, string address, string contactPerson, SupplierCategory category)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        ContactPerson = contactPerson;
        Category = category;
    }

    public void Deactivate() => Status = SupplierStatus.Inactive;
    public void Activate() => Status = SupplierStatus.Active;
}

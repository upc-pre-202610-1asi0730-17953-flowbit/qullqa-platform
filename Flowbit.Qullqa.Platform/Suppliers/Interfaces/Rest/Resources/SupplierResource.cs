using Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record SupplierResource(int Id, int BusinessId, string Name, string LastName, string Ruc, string Email,
    string Phone, string Address, string ContactPerson, SupplierCategory Category, SupplierStatus Status, DateTimeOffset Since);

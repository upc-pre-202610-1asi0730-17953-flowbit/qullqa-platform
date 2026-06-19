using Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record CreateSupplierResource(int BusinessId, string Name, string LastName, string Ruc, string Email,
    string Phone, string Address, string ContactPerson, SupplierCategory Category);

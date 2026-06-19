using Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record UpdateSupplierResource(string Name, string LastName, string Email, string Phone,
    string Address, string ContactPerson, SupplierCategory Category);

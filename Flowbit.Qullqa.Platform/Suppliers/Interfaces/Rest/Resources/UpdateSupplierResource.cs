using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record UpdateSupplierResource(string Name, string LastName, string Email, string Phone,
    string Address, string ContactPerson, SupplierCategory Category);

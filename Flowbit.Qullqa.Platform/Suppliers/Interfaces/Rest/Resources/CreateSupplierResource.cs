using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record CreateSupplierResource(int BusinessId, string Name, string LastName, string Ruc, string Email,
    string Phone, string Address, string ContactPerson, SupplierCategory Category);

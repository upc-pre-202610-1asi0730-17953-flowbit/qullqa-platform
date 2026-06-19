using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier s) => new(
        s.Id, s.BusinessId, s.Name, s.LastName, s.Ruc, s.Email,
        s.Phone, s.Address, s.ContactPerson, s.Category, s.Status, s.Since);
}

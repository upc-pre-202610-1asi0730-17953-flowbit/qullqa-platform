using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier supplier)
    {
        return new SupplierResource(supplier.Id, supplier.BusinessId, supplier.Name, supplier.LastName, supplier.Ruc,
            supplier.Email, supplier.Phone, supplier.Address, supplier.ContactPerson, supplier.Category, supplier.Status,
            supplier.Since);
    }
}

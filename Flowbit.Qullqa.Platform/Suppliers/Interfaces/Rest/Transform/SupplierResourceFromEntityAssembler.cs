using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Suppliers.Interfaces.Rest.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier supplier)
    {
        return new SupplierResource(supplier.Id, supplier.BusinessId, supplier.Name, supplier.LastName, supplier.Ruc,
            supplier.Email, supplier.Phone, supplier.Address, supplier.ContactPerson, supplier.Category, supplier.Status,
            supplier.Since);
    }
}

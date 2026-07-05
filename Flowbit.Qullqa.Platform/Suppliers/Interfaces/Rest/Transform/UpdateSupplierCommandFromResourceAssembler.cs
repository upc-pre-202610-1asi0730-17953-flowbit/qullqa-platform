using Qullqa.Platform.v2.Suppliers.Domain.Model.Commands;
using Qullqa.Platform.v2.Suppliers.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Suppliers.Interfaces.Rest.Transform;

public static class UpdateSupplierCommandFromResourceAssembler
{
    public static UpdateSupplierCommand ToCommandFromResource(UpdateSupplierResource resource, int supplierId)
    {
        return new UpdateSupplierCommand(supplierId, resource.Name, resource.LastName, resource.Ruc, resource.Email,
            resource.Phone, resource.Address, resource.ContactPerson, resource.Category);
    }
}

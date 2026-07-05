using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Transform;

public static class UpdateSupplierCommandFromResourceAssembler
{
    public static UpdateSupplierCommand ToCommandFromResource(UpdateSupplierResource resource, int supplierId)
    {
        return new UpdateSupplierCommand(supplierId, resource.Name, resource.LastName, resource.Ruc, resource.Email,
            resource.Phone, resource.Address, resource.ContactPerson, resource.Category);
    }
}

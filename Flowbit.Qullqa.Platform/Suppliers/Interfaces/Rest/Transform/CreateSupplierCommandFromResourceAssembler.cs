using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Transform;

public static class CreateSupplierCommandFromResourceAssembler
{
    public static CreateSupplierCommand ToCommandFromResource(CreateSupplierResource resource, int businessId)
    {
        return new CreateSupplierCommand(businessId, resource.Name, resource.LastName, resource.Ruc, resource.Email,
            resource.Phone, resource.Address, resource.ContactPerson, resource.Category);
    }
}

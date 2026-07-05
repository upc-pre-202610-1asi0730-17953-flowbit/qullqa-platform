using Qullqa.Platform.v2.Sales.Domain.Model.Commands;
using Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Sales.Interfaces.Rest.Transform;

public static class CreateCustomerCommandFromResourceAssembler
{
    public static CreateCustomerCommand ToCommandFromResource(CreateCustomerResource resource, int businessId)
    {
        return new CreateCustomerCommand(businessId, resource.FullName, resource.DocumentNumber, resource.PhoneNumber,
            resource.Email);
    }
}

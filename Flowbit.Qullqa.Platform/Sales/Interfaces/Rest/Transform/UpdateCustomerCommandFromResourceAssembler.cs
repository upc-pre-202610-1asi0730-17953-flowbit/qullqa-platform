using Qullqa.Platform.v2.Sales.Domain.Model.Commands;
using Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Sales.Interfaces.Rest.Transform;

public static class UpdateCustomerCommandFromResourceAssembler
{
    public static UpdateCustomerCommand ToCommandFromResource(UpdateCustomerResource resource, int customerId)
    {
        return new UpdateCustomerCommand(customerId, resource.FullName, resource.DocumentNumber, resource.PhoneNumber,
            resource.Email);
    }
}

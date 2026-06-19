using Flowbit.Qullqa.Platform.Sales.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Transform;

public static class CreateSaleCommandFromResourceAssembler
{
    public static CreateSaleCommand ToCommandFromResource(CreateSaleResource resource) => new(
        resource.BusinessId, resource.CustomerId, resource.Description, resource.Currency);
}

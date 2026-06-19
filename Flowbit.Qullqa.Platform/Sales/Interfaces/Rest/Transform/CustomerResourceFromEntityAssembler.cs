using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Interfaces.Rest.Resources;

namespace Qullqa.Platform.Sales.Interfaces.Rest.Transform;

public static class CustomerResourceFromEntityAssembler
{
    public static CustomerResource ToResourceFromEntity(Customer c) => new(
        c.Id, c.BusinessId, c.FullName, c.DocumentNumber, c.PhoneNumber, c.RegisteredAt);
}

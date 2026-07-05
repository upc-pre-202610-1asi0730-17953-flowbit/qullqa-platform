using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Sales.Interfaces.Rest.Transform;

public static class CustomerResourceFromEntityAssembler
{
    public static CustomerResource ToResourceFromEntity(Customer customer)
    {
        return new CustomerResource(customer.Id, customer.BusinessId, customer.FullName, customer.DocumentNumber,
            customer.PhoneNumber, customer.Email, customer.RegisteredAt);
    }
}

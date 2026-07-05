using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Transform;

public static class CustomerResourceFromEntityAssembler
{
    public static CustomerResource ToResourceFromEntity(Customer customer)
    {
        return new CustomerResource(customer.Id, customer.BusinessId, customer.FullName, customer.DocumentNumber,
            customer.PhoneNumber, customer.Email, customer.RegisteredAt);
    }
}

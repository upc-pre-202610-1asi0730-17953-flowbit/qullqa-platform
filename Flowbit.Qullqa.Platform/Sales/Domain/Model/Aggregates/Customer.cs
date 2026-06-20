using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;

public class Customer : IAuditableEntity
{
    public Customer() { }
    public Customer(int businessId, string fullName, string documentNumber, string phoneNumber)
    {
        BusinessId = businessId; FullName = fullName; DocumentNumber = documentNumber; PhoneNumber = phoneNumber;
        RegisteredAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int BusinessId { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string DocumentNumber { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public DateTimeOffset RegisteredAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Customer Update(string fullName, string documentNumber, string phoneNumber)
    {
        FullName = fullName; DocumentNumber = documentNumber; PhoneNumber = phoneNumber; return this;
    }
}

using Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Qullqa.Platform.Iam.Domain.Model.Aggregates;

public class Business : IAuditableEntity
{
    public Business() { }
    public Business(string name, string ruc, string email, string phone, string address)
    {
        Name = name;
        Ruc = ruc;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Ruc { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Business Update(string name, string ruc, string email, string phone, string address)
    {
        Name = name;
        Ruc = ruc;
        Email = email;
        Phone = phone;
        Address = address;
        return this;
    }
}

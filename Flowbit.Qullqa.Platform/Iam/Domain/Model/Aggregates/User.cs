using System.Text.Json.Serialization;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;

public class User : IAuditableEntity
{
    public User() { }
    public User(string email, string passwordHash, string firstName, string lastName, int? businessId = null, int? roleId = null)
    {
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        BusinessId = businessId;
        RoleId = roleId;
        Status = UserStatus.Active;
    }

    public int Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    [JsonIgnore] public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public int? BusinessId { get; private set; }
    public int? RoleId { get; private set; }
    public UserStatus Status { get; private set; } = UserStatus.Active;
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public User UpdatePasswordHash(string passwordHash) { PasswordHash = passwordHash; return this; }
    public User UpdateStatus(UserStatus status) { Status = status; return this; }
    public User UpdateProfile(string firstName, string lastName) { FirstName = firstName; LastName = lastName; return this; }
    public User AssignBusiness(int businessId) { BusinessId = businessId; return this; }
    public User AssignRole(int roleId) { RoleId = roleId; return this; }
}

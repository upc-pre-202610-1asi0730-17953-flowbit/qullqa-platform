using Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Qullqa.Platform.Suppliers.Domain.Model.Commands;

public record UpdateSupplierCommand(int Id, string Name, string LastName, string Email, string Phone,
    string Address, string ContactPerson, SupplierCategory Category);

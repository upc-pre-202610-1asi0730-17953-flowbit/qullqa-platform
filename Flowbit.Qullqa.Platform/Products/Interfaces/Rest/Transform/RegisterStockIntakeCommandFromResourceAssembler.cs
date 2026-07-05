using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class RegisterStockIntakeCommandFromResourceAssembler
{
    public static RegisterStockIntakeCommand ToCommandFromResource(RegisterStockIntakeResource resource, int productId,
        int businessId)
    {
        return new RegisterStockIntakeCommand(productId, businessId, resource.WarehouseId, resource.Quantity,
            resource.PurchasePrice, resource.Expiration, resource.Supplier, resource.Note, resource.MinimumStock);
    }
}

using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class WarehouseResourceFromEntityAssembler
{
    public static WarehouseResource ToResourceFromEntity(Warehouse warehouse)
    {
        return new WarehouseResource(warehouse.Id, warehouse.BusinessId, warehouse.Name, warehouse.Code,
            warehouse.Address, warehouse.Status, warehouse.Capacity);
    }
}

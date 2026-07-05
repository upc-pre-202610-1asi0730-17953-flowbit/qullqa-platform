using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Transform;

public static class BusinessKpisResourceFromResultAssembler
{
    public static BusinessKpisResource ToResourceFromResult(BusinessKpisResult result)
    {
        return new BusinessKpisResource(result.TotalProducts, result.LowStockCount, result.ExpiringSoonCount,
            result.InventoryValue, result.TotalSales, result.StockHealthPercentage);
    }
}

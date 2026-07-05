using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Events;
using Flowbit.Qullqa.Platform.Shared.Application.Internal.EventHandlers;
using Flowbit.Qullqa.Platform.Shared.Domain.Model.Services;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Application.Internal.EventHandlers;

/// <summary>
///     Reactive half of the alerts engine (§5.4): re-evaluates low-stock /
///     out-of-stock for the exact InventoryItem that just changed —
///     covers the 90% of cases where a concrete action (sale, intake,
///     adjustment) is what changed the stock.
/// </summary>
public class StockLevelChangedEventHandler(
    IAlertRepository alertRepository,
    IAlertRuleRepository alertRuleRepository,
    IUnitOfWork unitOfWork)
    : IEventHandler<StockLevelChangedEvent>
{
    public async Task Handle(StockLevelChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        var outOfStockRule = await alertRuleRepository.FindByBusinessIdAndTypeAsync(domainEvent.BusinessId,
            AlertType.OutOfStock, cancellationToken);
        var lowStockRule = await alertRuleRepository.FindByBusinessIdAndTypeAsync(domainEvent.BusinessId,
            AlertType.LowStock, cancellationToken);
        var outOfStockEnabled = outOfStockRule?.Enabled ?? true;
        var lowStockEnabled = lowStockRule?.Enabled ?? true;

        var existingLowStock = await alertRepository.FindActiveByProductAndTypeAsync(domainEvent.ProductId, AlertType.LowStock,
            null, cancellationToken);
        var existingOutOfStock = await alertRepository.FindActiveByProductAndTypeAsync(domainEvent.ProductId,
            AlertType.OutOfStock, null, cancellationToken);

        var isOutOfStock = StockRules.IsOutOfStock(domainEvent.NewQuantity);
        var isLowStock = StockRules.IsLowStock(domainEvent.NewQuantity, domainEvent.MinimumStock);

        if (isOutOfStock && outOfStockEnabled)
        {
            var message = $"{domainEvent.ProductName} está agotado.";
            if (existingOutOfStock != null) existingOutOfStock.RefreshStockInfo(AlertSeverity.High, message, domainEvent.NewQuantity);
            else await alertRepository.AddAsync(
                new Alert(domainEvent.BusinessId, domainEvent.ProductId, null, domainEvent.ProductName, AlertType.OutOfStock,
                    AlertSeverity.High, message, domainEvent.NewQuantity, domainEvent.MinimumStock, null),
                cancellationToken);

            existingLowStock?.Resolve();
        }
        else if (isLowStock && lowStockEnabled)
        {
            var message = $"{domainEvent.ProductName} tiene stock bajo ({domainEvent.NewQuantity} unidades, mínimo {domainEvent.MinimumStock}).";
            if (existingLowStock != null) existingLowStock.RefreshStockInfo(AlertSeverity.Medium, message, domainEvent.NewQuantity);
            else await alertRepository.AddAsync(
                new Alert(domainEvent.BusinessId, domainEvent.ProductId, null, domainEvent.ProductName, AlertType.LowStock,
                    AlertSeverity.Medium, message, domainEvent.NewQuantity, domainEvent.MinimumStock, null),
                cancellationToken);

            existingOutOfStock?.Resolve();
        }
        else
        {
            // Stock is healthy again — clear any previously active alert for this product.
            existingLowStock?.Resolve();
            existingOutOfStock?.Resolve();
        }

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}

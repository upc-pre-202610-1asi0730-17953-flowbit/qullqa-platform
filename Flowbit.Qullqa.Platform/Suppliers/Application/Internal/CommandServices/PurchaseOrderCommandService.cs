using Qullqa.Platform.Suppliers.Application.CommandServices;
using Qullqa.Platform.Shared.Application.Model;
using Qullqa.Platform.Shared.Domain.Repositories;
using Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.Suppliers.Domain.Model.Commands;
using Qullqa.Platform.Suppliers.Domain.Model.Enums;
using Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Qullqa.Platform.Suppliers.Application.Internal.CommandServices;

public class PurchaseOrderCommandService(IPurchaseOrderRepository purchaseOrderRepository, IUnitOfWork unitOfWork) : IPurchaseOrderCommandService
{
    public async Task<Result<PurchaseOrder>> Handle(CreatePurchaseOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = new PurchaseOrder(command.BusinessId, command.SupplierId, command.SupplierName,
                command.ExpectedDate, command.Description, command.Currency);
            await purchaseOrderRepository.AddAsync(order, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PurchaseOrder>.Success(order);
        }
        catch (Exception ex)
        {
            return Result<PurchaseOrder>.Failure(ex.Message);
        }
    }

    public async Task<Result<PurchaseOrder>> Handle(AddPurchaseOrderDetailCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await purchaseOrderRepository.FindByIdWithDetailsAsync(command.PurchaseOrderId, cancellationToken);
            if (order is null) return Result<PurchaseOrder>.Failure("Purchase order not found");

            var detail = new PurchaseOrderDetail(command.PurchaseOrderId, command.ProductId, command.ProductName,
                command.Quantity, command.UnitPrice, command.Discount);
            order.Details.Add(detail);
            purchaseOrderRepository.Update(order);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PurchaseOrder>.Success(order);
        }
        catch (Exception ex)
        {
            return Result<PurchaseOrder>.Failure(ex.Message);
        }
    }

    public async Task<Result<PurchaseOrder>> Handle(UpdatePurchaseOrderStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await purchaseOrderRepository.FindByIdAsync(command.Id, cancellationToken);
            if (order is null) return Result<PurchaseOrder>.Failure("Purchase order not found");

            switch (command.Status)
            {
                case PurchaseOrderStatus.Received: order.Receive(); break;
                case PurchaseOrderStatus.Delayed: order.MarkDelayed(); break;
                case PurchaseOrderStatus.Cancelled: order.Cancel(); break;
            }

            purchaseOrderRepository.Update(order);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PurchaseOrder>.Success(order);
        }
        catch (Exception ex)
        {
            return Result<PurchaseOrder>.Failure(ex.Message);
        }
    }
}

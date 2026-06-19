using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Domain.Model.Commands;
using Qullqa.Platform.Sales.Domain.Model.Enums;
using Qullqa.Platform.Sales.Domain.Repositories;
using Qullqa.Platform.Shared.Application.Model;
using Qullqa.Platform.Sales.Application.CommandServices;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Sales.Application.Internal.CommandServices;

public class SaleCommandService(ISaleRepository saleRepository, ISaleDetailRepository saleDetailRepository, IUnitOfWork unitOfWork) : ISaleCommandService
{
    public async Task<Result<Sale>> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var sale = new Sale(command.BusinessId, command.CustomerId, command.Description, command.Currency);
            await saleRepository.AddAsync(sale, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Sale>.Success(sale);
        }
        catch (Exception ex)
        {
            return Result<Sale>.Failure(ex.Message);
        }
    }

    public async Task<Result<Sale>> Handle(AddSaleDetailCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await saleRepository.FindByIdWithDetailsAsync(command.SaleId, cancellationToken);
            if (sale is null) return Result<Sale>.Failure("Sale not found");
            if (sale.Status != SaleStatus.Open) return Result<Sale>.Failure("Sale is already closed");

            var detail = new SaleDetail(command.SaleId, command.ProductId, command.Quantity, command.UnitPrice, command.Discount);
            await saleDetailRepository.AddAsync(detail, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Sale>.Success(sale);
        }
        catch (Exception ex)
        {
            return Result<Sale>.Failure(ex.Message);
        }
    }

    public async Task<Result<Sale>> Handle(PaySaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await saleRepository.FindByIdAsync(command.SaleId, cancellationToken);
            if (sale is null) return Result<Sale>.Failure("Sale not found");

            sale.Pay(command.PaymentMethod, command.TotalAmount);
            saleRepository.Update(sale);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Sale>.Success(sale);
        }
        catch (Exception ex)
        {
            return Result<Sale>.Failure(ex.Message);
        }
    }

    public async Task<Result<Sale>> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await saleRepository.FindByIdAsync(command.SaleId, cancellationToken);
            if (sale is null) return Result<Sale>.Failure("Sale not found");

            sale.Cancel();
            saleRepository.Update(sale);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Sale>.Success(sale);
        }
        catch (Exception ex)
        {
            return Result<Sale>.Failure(ex.Message);
        }
    }
}

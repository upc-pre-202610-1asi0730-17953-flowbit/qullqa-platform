using Flowbit.Qullqa.Platform.Suppliers.Application.CommandServices;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.Internal.CommandServices;

public class SupplierCommandService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork) : ISupplierCommandService
{
    public async Task<Result<Supplier>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (await supplierRepository.ExistsByRucAsync(command.Ruc, cancellationToken))
                return Result<Supplier>.Failure("RUC already registered");

            var supplier = new Supplier(command.BusinessId, command.Name, command.LastName, command.Ruc,
                command.Email, command.Phone, command.Address, command.ContactPerson, command.Category);
            await supplierRepository.AddAsync(supplier, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Supplier>.Success(supplier);
        }
        catch (Exception ex)
        {
            return Result<Supplier>.Failure(ex.Message);
        }
    }

    public async Task<Result<Supplier>> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var supplier = await supplierRepository.FindByIdAsync(command.Id, cancellationToken);
            if (supplier is null) return Result<Supplier>.Failure("Supplier not found");

            supplier.Update(command.Name, command.LastName, command.Email, command.Phone,
                command.Address, command.ContactPerson, command.Category);
            supplierRepository.Update(supplier);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Supplier>.Success(supplier);
        }
        catch (Exception ex)
        {
            return Result<Supplier>.Failure(ex.Message);
        }
    }
}

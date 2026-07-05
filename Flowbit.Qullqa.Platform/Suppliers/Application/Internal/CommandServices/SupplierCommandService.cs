using Microsoft.Extensions.Localization;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Application.CommandServices;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Errors;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Resources;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.Internal.CommandServices;

public class SupplierCommandService(
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<SuppliersMessages> localizer)
    : ISupplierCommandService
{
    public async Task<Result<Supplier>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
    {
        var supplier = new Supplier(command.BusinessId, command.Name, command.LastName, command.Ruc, command.Email,
            command.Phone, command.Address, command.ContactPerson, command.Category, DateOnly.FromDateTime(DateTime.UtcNow));
        await supplierRepository.AddAsync(supplier, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Supplier>.Success(supplier);
    }

    public async Task<Result<Supplier>> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindByIdAsync(command.SupplierId, cancellationToken);
        if (supplier == null)
            return Result<Supplier>.Failure(SuppliersError.SupplierNotFound, localizer[nameof(SuppliersError.SupplierNotFound)]);

        supplier.UpdateDetails(command.Name, command.LastName, command.Ruc, command.Email, command.Phone, command.Address,
            command.ContactPerson, command.Category);
        supplierRepository.Update(supplier);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Supplier>.Success(supplier);
    }

    public async Task<Result<Supplier>> Handle(DeactivateSupplierCommand command, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindByIdAsync(command.SupplierId, cancellationToken);
        if (supplier == null)
            return Result<Supplier>.Failure(SuppliersError.SupplierNotFound, localizer[nameof(SuppliersError.SupplierNotFound)]);

        supplier.Deactivate();
        supplierRepository.Update(supplier);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Supplier>.Success(supplier);
    }
}

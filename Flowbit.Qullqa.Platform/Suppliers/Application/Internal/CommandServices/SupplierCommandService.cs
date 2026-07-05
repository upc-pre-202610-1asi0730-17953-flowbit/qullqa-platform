using Microsoft.Extensions.Localization;
using Qullqa.Platform.v2.Shared.Application.Model;
using Qullqa.Platform.v2.Shared.Domain.Repositories;
using Qullqa.Platform.v2.Suppliers.Application.CommandServices;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Commands;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Errors;
using Qullqa.Platform.v2.Suppliers.Domain.Repositories;
using Qullqa.Platform.v2.Suppliers.Resources;

namespace Qullqa.Platform.v2.Suppliers.Application.Internal.CommandServices;

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

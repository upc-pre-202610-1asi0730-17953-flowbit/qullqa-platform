using Flowbit.Qullqa.Platform.Alerts.Application.CommandServices;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Application.Internal.CommandServices;

public class AlertCommandService(IAlertRepository alertRepository, IUnitOfWork unitOfWork) : IAlertCommandService
{
    public async Task<Result<Alert>> Handle(CreateAlertCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var alert = new Alert(command.BusinessId, command.ProductId, command.ProductName, command.Type,
                command.Severity, command.Message, command.BatchId, command.CurrentStock, command.MinStock, command.DaysToExpiry);
            await alertRepository.AddAsync(alert, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Alert>.Success(alert);
        }
        catch (Exception ex) { return Result<Alert>.Failure(ex.Message); }
    }

    public async Task<Result<Alert>> Handle(UpdateAlertStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var alert = await alertRepository.FindByIdAsync(command.Id, cancellationToken);
            if (alert is null) return Result<Alert>.Failure("Alert not found");

            switch (command.Status)
            {
                case AlertStatus.Acknowledged: alert.Acknowledge(); break;
                case AlertStatus.Sent: alert.MarkNotified(); break;
                case AlertStatus.Resolved: alert.Resolve(); break;
            }

            alertRepository.Update(alert);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Alert>.Success(alert);
        }
        catch (Exception ex) { return Result<Alert>.Failure(ex.Message); }
    }
}

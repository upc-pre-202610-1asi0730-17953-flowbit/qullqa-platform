namespace Qullqa.Platform.v2.Subscription.Domain.Model.Commands;

public record UpdatePlanCommand(int PlanId, string Name, string Description, decimal Price, string Currency, string TimeLength,
    IReadOnlyCollection<string> Features);

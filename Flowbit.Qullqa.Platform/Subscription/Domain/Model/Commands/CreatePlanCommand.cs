namespace Qullqa.Platform.v2.Subscription.Domain.Model.Commands;

/// <summary>Admin/seed only — the frontend has no plan-creation UI (§6.9).</summary>
public record CreatePlanCommand(string Name, string Description, decimal Price, string Currency, string TimeLength,
    IReadOnlyCollection<string> Features);

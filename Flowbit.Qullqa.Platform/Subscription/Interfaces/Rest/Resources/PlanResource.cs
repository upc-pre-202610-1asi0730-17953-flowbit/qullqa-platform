namespace Qullqa.Platform.v2.Subscription.Interfaces.Rest.Resources;

public record PlanResource(int Id, string Name, string Description, decimal Price, string Currency, string TimeLength,
    string Status, IReadOnlyCollection<string> Features);

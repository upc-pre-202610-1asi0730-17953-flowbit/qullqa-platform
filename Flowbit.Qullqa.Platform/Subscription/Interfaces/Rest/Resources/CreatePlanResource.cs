namespace Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest.Resources;

public record CreatePlanResource(string Name, string Description, decimal Price, string Currency, string TimeLength,
    IReadOnlyCollection<string> Features);

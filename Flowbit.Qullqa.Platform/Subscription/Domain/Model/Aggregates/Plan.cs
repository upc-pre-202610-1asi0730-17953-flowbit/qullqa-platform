namespace Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;

public static class PlanStatus
{
    public const string Active = "ACTIVE";
    public const string Inactive = "INACTIVE";
}

public static class PlanTimeLength
{
    public const string Monthly = "MONTHLY";
    public const string Yearly = "YEARLY";
}

/// <summary>
///     A subscription tier a Business can be enrolled in. Read-mostly catalog
///     — no Stripe/payment concerns here (deferred, see architecture doc §15);
///     changing a business's plan today is a direct field update
///     (IAM's Business.PlanId), same as the current frontend.
///
///     Features is free-text, display-only content (verified against the
///     real data — see architecture doc §6.9): Spanish marketing strings
///     like "Inventario básico", not machine-readable capability keys.
///     Feature-gating (Plan.Limits) is explicitly out of scope (§5.5/§8.2).
/// </summary>
public class Plan(string name, string description, decimal price, string currency, string timeLength, IReadOnlyCollection<string> features)
{
    public Plan() : this(string.Empty, string.Empty, 0, "PEN", PlanTimeLength.Monthly, [])
    {
    }

    public int Id { get; }
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public decimal Price { get; private set; } = price;
    public string Currency { get; private set; } = currency;
    public string TimeLength { get; private set; } = timeLength;
    public string Status { get; private set; } = PlanStatus.Active;
    public IReadOnlyCollection<string> Features { get; private set; } = features;

    public bool IsActive => Status == PlanStatus.Active;

    public Plan UpdateDetails(string name, string description, decimal price, string currency, string timeLength,
        IReadOnlyCollection<string> features)
    {
        Name = name;
        Description = description;
        Price = price;
        Currency = currency;
        TimeLength = timeLength;
        Features = features;
        return this;
    }
}

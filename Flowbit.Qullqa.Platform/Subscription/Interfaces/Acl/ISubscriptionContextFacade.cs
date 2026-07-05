namespace Flowbit.Qullqa.Platform.Subscription.Interfaces.Acl;

/// <summary>
///     The only way another bounded context may reach into Subscription
///     (Plan Management) — never direct repository/DbContext access.
///     Consumed today only by IAM, to show the plan currently assigned to a
///     business. GetPlanLimits/HasCapability stay as future reference design
///     only (feature-gating is out of scope — §5.5/§8.2).
/// </summary>
public interface ISubscriptionContextFacade
{
    Task<PlanInfo?> GetPlanById(int planId, CancellationToken cancellationToken);
}

public record PlanInfo(int Id, string Name, decimal Price, string Currency);

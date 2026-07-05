using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Alerts.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Alerts.Interfaces.Rest.Transform;

public static class AlertRuleResourceFromEntityAssembler
{
    public static AlertRuleResource ToResourceFromEntity(AlertRule rule)
    {
        return new AlertRuleResource(rule.Id, rule.BusinessId, rule.AlertType, rule.ThresholdValue, rule.Enabled);
    }
}

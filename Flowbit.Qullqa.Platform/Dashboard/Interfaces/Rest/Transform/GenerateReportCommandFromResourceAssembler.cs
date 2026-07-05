using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Transform;

public static class GenerateReportCommandFromResourceAssembler
{
    public static GenerateReportCommand ToCommandFromResource(GenerateReportResource resource, int businessId)
    {
        return new GenerateReportCommand(businessId, resource.Type, resource.DateFrom, resource.DateTo);
    }
}

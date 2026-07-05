namespace Qullqa.Platform.v2.Sales.Domain.Model.Queries;

public record GetAllSalesByBusinessIdQuery(int BusinessId, DateOnly? DateFrom = null, DateOnly? DateTo = null);

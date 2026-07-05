namespace Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Queries;

/// <summary>Consumed by Suppliers (via a facade, symmetric to IProductContextFacade) to show real delivery status on a purchase order view.</summary>
public record GetDeliveryStatusByPurchaseDetailIdQuery(int PurchaseDetailId);

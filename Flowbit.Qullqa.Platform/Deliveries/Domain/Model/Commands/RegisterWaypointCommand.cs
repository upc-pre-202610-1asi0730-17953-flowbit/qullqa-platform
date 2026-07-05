using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.ValueObjects;

namespace Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;

public record RegisterWaypointCommand(int DeliveryId, string Label, string District, GeoCoordinate Location, int SequenceOrder);

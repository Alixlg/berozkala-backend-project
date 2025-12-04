using berozkala_backend.Entities.OrderEntities;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs.OrderDTOs;

public class OrderAddDto
{
    public required Guid ReceiverAddressId { get; set; }
    public required string ReceiverFullName { get; set; }
    public required Guid PaymentMethodId { get; set; }
    public required Guid ShipmentMethodId { get; set; }
    public Guid? DiscountCodeId { get; set; }
}

namespace berozkala_backend.DTOs.ShippingMethodDTOs;

public class ShippingMethodGetDto
{
    public Guid Id { get; set; }
    public required bool IsActive { get; set; }
    public required string MethodName { get; set; }
    public required string MethodDescription { get; set; }
    public decimal ShipmentCost { get; set; }
}

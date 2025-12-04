namespace berozkala_backend.DTOs.PaymentMethodDTOs;

public class PaymentMethodGetDto
{
    public Guid Id { get; set; }
    public string? MethodName { get; set; }
    public string? MethodDescription { get; set; }
}

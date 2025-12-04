using berozkala_backend.Entities.AccountsEntities;
using berozkala_backend.Entities.CommonEntities;
using berozkala_backend.Entities.OtherEntities;
using berozkala_backend.Enums;

namespace berozkala_backend.Entities.OrderEntities
{
    public class Order : DbBaseProps
    {
        public DateTime DateToAdd { get; set; } = DateTime.Now;
        public required OrderStatus OrderStatus { get; set; }
        public required string OrderNumber { get; set; }
        public required List<OrderItem> OrderItems { get; set; }
        public int CustomerId { get; set; }
        public required UserAccount Customer { get; set; }
        public required string SenderAddress { get; set; }
        public int ReceiverAddressId { get; set; }
        public required Address ReceiverAddress { get; set; }
        public required string ReceiverFullName { get; set; }
        public int PaymentInformaationId { get; set; }
        public required PaymentInformaation? PaymentInformaation { get; set; }
        public int ShipmentMethodId { get; set; }
        public required ShippingMethod ShippingMethod { get; set; }
        public int DiscountCodeId { get; set; }
        public DiscountCode? DiscountCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal BasketTotalPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
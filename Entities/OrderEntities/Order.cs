using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public required long OrderNumber { get; set; }
        public required List<OrderItem> OrderItems { get; set; }
        public int CustomerId { get; set; }
        public required UserAccount Customer { get; set; }
        public required string SenderAddress { get; set; }
        public int ReceiverAddressId { get; set; }
        public Address? ReceiverAddress { get; set; }
        public required string ReceiverFullName { get; set; }
        public int PaymentInformaationId { get; set; }
        public PaymentInformaation? PaymentInformaation { get; set; }
        public required ShippingMethod ShipmentMethod { get; set; }
        public DiscountCode? DiscountCode { get; set; }
        public decimal BasketTotalPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
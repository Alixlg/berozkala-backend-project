using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.OrderDTOs;
using berozkala_backend.Entities.OrderEntities;
using berozkala_backend.Entities.OtherEntities;
using berozkala_backend.Enums;
using berozkala_backend.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints;

public static class OrderEndPoints
{
    public static void MapCreateOrder(this WebApplication app)
    {
        app.MapPost("api/v1/orders/create", async ([FromBody] OrderAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
        {
            try
            {
                var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                var user = await db.Users
                    .Include(x => x.Addresses)
                    .Include(x => x.BasketsProducts)
                        .ThenInclude(b => b.Product)
                    .Include(x => x.BasketsProducts)
                        .ThenInclude(p => p.SelectedGarranty)
                    .Include(x => x.SubmitedDiscountCodes)
                    .FirstOrDefaultAsync(u => u.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما لاگین نکرده اید");

                if (!user.BasketsProducts.Any())
                {
                    throw new Exception("سبد خرید شما خالی است");
                }

                var userAddress = user.Addresses?
                    .FirstOrDefault(x => x.Guid == dto.ReceiverAddressId) ?? throw new Exception("آدرس وارد شده یافت نشد");

                var paymentMethod = await db.PaymentMethods
                    .FirstOrDefaultAsync(x => x.Guid == dto.PaymentMethodId) ?? throw new Exception("روش پرداخت وارد شده یافت نشد");

                var shipmentMethod = await db.ShippingMethods
                    .Where(x => x.IsActive)
                    .FirstOrDefaultAsync(x => x.Guid == dto.ShipmentMethodId) ?? throw new Exception("روش ارسال وارد شده یافت نشد");

                decimal basketPrice = 0;
                decimal totalPrice = 0;
                user.BasketsProducts.ForEach(b => basketPrice += b.Product.Price);

                DiscountCode discountCode = null!;

                if (dto.DiscountCodeId is not null)
                {
                    discountCode = await db.DiscountCodes
                        .FirstOrDefaultAsync(x => x.Guid == dto.DiscountCodeId) ?? throw new Exception("کد تخفیف وارد شده یافت نشد");

                    var submitedDiscountCode = user.SubmitedDiscountCodes?.Select(x => x.Id == discountCode.Id);

                    await DiscountTools.DiscountCodeValidatorAsync(discountCode, basketPrice, (submitedDiscountCode?.Count()) ?? 0);
                    totalPrice = basketPrice - discountCode.DiscountAmount;
                }

                var paymentInformation = new PaymentInformaation()
                {
                    PaymentMethod = paymentMethod,
                    PaymentStatus = PaymentStatus.Pending,
                    Amount = basketPrice,
                    IsApproved = false
                };

                List<OrderItem> orderItems = [];
                user.BasketsProducts.ForEach(b =>
                {
                    orderItems.Add(new OrderItem()
                    {
                        ProductBrand = b.Product.Brand,
                        ProductTitle = b.Product.Title,
                        Product = b.Product,
                        ProductGarranty = b.SelectedGarranty,
                        ProductCount = b.ProductCount,
                        Price = b.Product.Price,
                    });
                });

                var order = new Order()
                {
                    OrderStatus = OrderStatus.PendingToPay,
                    OrderNumber = "",
                    OrderItems = orderItems,
                    Customer = user,
                    SenderAddress = "شرکت بروزکالا",
                    ReceiverAddress = userAddress,
                    ReceiverFullName = dto.ReceiverFullName,
                    PaymentInformaation = paymentInformation,
                    ShippingMethod = shipmentMethod,
                    DiscountCode = discountCode,
                    DiscountAmount = discountCode?.DiscountAmount ?? 0,
                    BasketTotalPrice = basketPrice,
                    TotalPrice = totalPrice,
                };

                await db.Orders.AddAsync(order);
                order.OrderNumber = order.Id.ToString("D6");

                await db.SaveChangesAsync();

                db.BasketsProducts.RemoveRange(user.BasketsProducts);
                await db.SaveChangesAsync();

                return new RequestResultDto<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "در حال انتقال به صفحه پرداخت"
                };
            }
            catch (Exception ex)
            {
                return new RequestResultDto<string>()
                {
                    IsSuccess = false,
                    StatusCode = context.Response.StatusCode,
                    Message = ex.ToString()
                };
            }
        }).RequireAuthorization();
    }
}
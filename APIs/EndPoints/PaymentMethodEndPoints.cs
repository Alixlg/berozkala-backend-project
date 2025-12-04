using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.PaymentMethodDTOs;
using berozkala_backend.DTOs.ShippingMethodDTOs;
using berozkala_backend.Entities.OtherEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class PaymentMethodEndPoints
    {
        public static void MapPaymentMethodCreate(this WebApplication app)
        {
            app.MapPost("api/v1/paymentmethod/create", async ([FromBody] PaymentMethodAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guidClaim = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";
                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guidClaim))
                        ?? throw new Exception("شما ادمین نیستید");

                    var payment = new PaymentMethod()
                    {
                        MethodName = dto.MethodName,
                        MethodDescription = dto.MethodDescription
                    };

                    await db.PaymentMethods.AddAsync(payment);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفقیت روش پرداخت را اضافه کردید"
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message
                    };
                }
            }).RequireAuthorization();
        }

        public static void MapPaymentMethodEdit(this WebApplication app)
        {
            app.MapPut("api/v1/paymentmethod/edit/{id:Guid}", async ([FromRoute] Guid id, [FromBody] PaymentMethodAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guidClaim = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";
                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guidClaim))
                        ?? throw new Exception("شما ادمین نیستید");

                    var payment = await db.PaymentMethods.FirstOrDefaultAsync(x => x.Guid == id)
                        ?? throw new Exception("روش پرداخت مورد نظر یافت نشد");

                    payment.MethodName = dto.MethodName;
                    payment.MethodDescription = dto.MethodDescription;

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفقیت روش پرداخت را ویرایش کردید"
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message
                    };
                }
            }).RequireAuthorization();
        }

        public static void MapPaymentMethodList(this WebApplication app)
        {
            app.MapGet("api/v1/paymentmethod/list", async ([FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var payments = await db.PaymentMethods
                        .Select(x => new PaymentMethodGetDto()
                        {
                            Id = x.Guid,
                            MethodName = x.MethodName,
                            MethodDescription = x.MethodDescription
                        }).ToListAsync();

                    return new RequestResultDto<IEnumerable<PaymentMethodGetDto>>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "عملیات موفقیت آمیز بود",
                        Body = payments
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<IEnumerable<PaymentMethodGetDto>>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message
                    };
                }
            }).RequireAuthorization();
        }

        public static void MapPaymentMethodGet(this WebApplication app)
        {
            app.MapGet("api/v1/paymentmethod/get/{id:Guid}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var payment = await db.PaymentMethods.FirstOrDefaultAsync(x => x.Guid == id)
                        ?? throw new Exception("روش پرداخت یافت نشد");

                    var dto = new PaymentMethodGetDto()
                    {
                        Id = payment.Guid,
                        MethodName = payment.MethodName,
                        MethodDescription = payment.MethodDescription
                    };

                    return new RequestResultDto<PaymentMethodGetDto>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "عملیات موفقیت آمیز بود",
                        Body = dto
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<PaymentMethodGetDto>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message
                    };
                }
            }).RequireAuthorization();
        }

        public static void MapPaymentMethodDelete(this WebApplication app)
        {
            app.MapDelete("api/v1/paymentmethod/delete/{id:Guid}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guidClaim = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";
                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guidClaim))
                        ?? throw new Exception("شما ادمین نیستید");

                    var payment = await db.PaymentMethods.FirstOrDefaultAsync(x => x.Guid == id)
                        ?? throw new Exception("روش پرداخت مورد نظر یافت نشد");

                    db.PaymentMethods.Remove(payment);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "روش پرداخت با موفقیت حذف شد"
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message
                    };
                }
            }).RequireAuthorization();
        }
    }
}
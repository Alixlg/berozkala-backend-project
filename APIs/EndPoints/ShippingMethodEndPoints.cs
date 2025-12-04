using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.ShippingMethodDTOs;
using berozkala_backend.Entities.OtherEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints;

public static class ShippingMethodEndPoints
{
    public static void MapShippingMethodCreate(this WebApplication app)
    {
        app.MapPost("api/v1/shippingmethod/create", async ([FromBody] ShippingMethodAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
        {
            try
            {
                var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";
                var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                    ?? throw new Exception("شما ادمین نیستید");

                var shipping = new ShippingMethod()
                {
                    IsActive = dto.IsActive,
                    MethodName = dto.MethodName,
                    MethodDescription = dto.MethodDescription,
                    ShipmentCost = dto.ShipmentCost
                };

                await db.ShippingMethods.AddAsync(shipping);
                await db.SaveChangesAsync();

                return new RequestResultDto<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "شما با موفقیت روش ارسال را اضافه کردید"
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

    public static void MapShippingMethodEdit(this WebApplication app)
    {
        app.MapPut("api/v1/shippingmethod/edit/{id:Guid}", async ([FromRoute] Guid id, [FromBody] ShippingMethodAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
        {
            try
            {
                var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";
                var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                    ?? throw new Exception("شما ادمین نیستید");

                var shipping = await db.ShippingMethods.FirstOrDefaultAsync(x => x.Guid == id)
                    ?? throw new Exception("روش ارسال مورد نظر یافت نشد");

                shipping.IsActive = dto.IsActive;
                shipping.MethodName = dto.MethodName;
                shipping.MethodDescription = dto.MethodDescription;
                shipping.ShipmentCost = dto.ShipmentCost;

                await db.SaveChangesAsync();

                return new RequestResultDto<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "شما با موفقیت روش ارسال را ویرایش کردید"
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

    public static void MapShippingMethodList(this WebApplication app)
    {
        app.MapGet("api/v1/shippingmethod/list", async ([FromServices] BerozkalaDb db, HttpContext context) =>
        {
            try
            {
                var shippingMethods = await db.ShippingMethods
                    .Select(x => new ShippingMethodGetDto()
                    {
                        Id = x.Guid,
                        IsActive = x.IsActive,
                        MethodName = x.MethodName,
                        MethodDescription = x.MethodDescription,
                        ShipmentCost = x.ShipmentCost
                    }).ToListAsync();

                return new RequestResultDto<IEnumerable<ShippingMethodGetDto>>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "عملیات موفقیت آمیز بود",
                    Body = shippingMethods
                };
            }
            catch (Exception ex)
            {
                return new RequestResultDto<IEnumerable<ShippingMethodGetDto>>()
                {
                    IsSuccess = false,
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                };
            }
        }).RequireAuthorization();
    }

    public static void MapShippingMethodGet(this WebApplication app)
    {
        app.MapGet("api/v1/shippingmethod/get/{id:Guid}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
        {
            try
            {
                var shipping = await db.ShippingMethods.FirstOrDefaultAsync(x => x.Guid == id)
                    ?? throw new Exception("روش ارسال یافت نشد");

                var dto = new ShippingMethodGetDto()
                {
                    Id = shipping.Guid,
                    IsActive = shipping.IsActive,
                    MethodName = shipping.MethodName,
                    MethodDescription = shipping.MethodDescription,
                    ShipmentCost = shipping.ShipmentCost
                };

                return new RequestResultDto<ShippingMethodGetDto>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "عملیات موفقیت آمیز بود",
                    Body = dto
                };
            }
            catch (Exception ex)
            {
                return new RequestResultDto<ShippingMethodGetDto>()
                {
                    IsSuccess = false,
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                };
            }
        }).RequireAuthorization();
    }

    public static void MapShippingMethodDelete(this WebApplication app)
    {
        app.MapDelete("api/v1/shippingmethod/delete/{id:Guid}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
        {
            try
            {
                var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";
                var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                    ?? throw new Exception("شما ادمین نیستید");

                var shipping = await db.ShippingMethods.FirstOrDefaultAsync(x => x.Guid == id)
                    ?? throw new Exception("روش ارسال مورد نظر یافت نشد");

                db.ShippingMethods.Remove(shipping);
                await db.SaveChangesAsync();

                return new RequestResultDto<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "روش ارسال با موفقیت حذف شد"
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
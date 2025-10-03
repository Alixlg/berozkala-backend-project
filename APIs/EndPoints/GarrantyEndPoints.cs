using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.ProductSubDTOs;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class GarrantyEndPoints
    {
        public static void MapAddGarrantysProduct(this WebApplication app)
        {
            app.MapPost("api/v1/products/add-garrantys", async ([FromBody] GarrantyAddToProductDto dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products
                        .Include(x => x.Garrantys)
                        .FirstOrDefaultAsync(P => P.Guid == dto.ProductId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    var garrantys = dto.Garrantys.Select(x => new ProductGarranty()
                    {
                        Product = product,
                        Name = x.Name,
                        GarrantyCode = x.GarrantyCode
                    });

                    await db.ProductGarrantys.AddRangeAsync(garrantys);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "گارانتی های مورد نظر با موفقیت اظافه شدند"
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

        public static void MapDeleteGarrantysProduct(this WebApplication app)
        {
            app.MapDelete("api/v1/products/delete-garrantys", async ([FromBody] GarrantyDeleteToProductDto dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products
                        .Include(x => x.Garrantys)
                        .FirstOrDefaultAsync(P => P.Guid == dto.ProductId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    if (product.Garrantys == null || !product.Garrantys.Any())
                    {
                        throw new Exception("این محصول گارانتی برای حذف ندارد");
                    }

                    var garrantys = product.Garrantys
                        .Where(x => dto.GarrantyIds.Contains(x.Guid))
                        .Select(x => x);

                    if (!garrantys.Any())
                        throw new Exception("گارانتی های مورد نظر وجود ندارند");

                    db.ProductGarrantys.RemoveRange(garrantys);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "گارانتی های مورد نظر با موفقیت حذف شد"
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
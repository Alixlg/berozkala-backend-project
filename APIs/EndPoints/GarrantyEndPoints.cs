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
            app.MapPost("api/v1/products/add-garrantys", async ([FromBody] GeneticToProductDto<GarrantyDto> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products
                        .Include(x => x.Garrantys)
                        .FirstOrDefaultAsync(P => P.Guid == dto.EntityId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    var garrantys = dto.Items.Select(x => new ProductGarranty()
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
            app.MapDelete("api/v1/products/delete-garrantys", async ([FromBody] GeneticToProductDto<Guid> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products
                        .Include(x => x.Garrantys)
                        .FirstOrDefaultAsync(P => P.Guid == dto.EntityId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    if (product.Garrantys == null || !product.Garrantys.Any())
                    {
                        throw new Exception("این محصول گارانتی برای حذف ندارد");
                    }

                    var garrantys = product.Garrantys
                        .Where(x => dto.Items.Contains(x.Guid))
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

        public static void MapEditGarrantysProduct(this WebApplication app)
        {
            app.MapPut("api/v1/products/edit-garrantys", async ([FromBody] List<GarrantyEditDto> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var garrantys = db.ProductGarrantys
                        .Where(x => dto.Select(d => d.Id).Contains(x.Guid));

                    if (!garrantys.Any())
                        throw new Exception("گارانتی های مورد نظر وجود ندارند");

                    dto.ForEach(d =>
                    {
                        var g = garrantys.FirstOrDefault(g => g.Guid == d.Id);
                        if (g != null)
                        {
                            g.Name = d.Name;
                            g.GarrantyCode = d.GarrantyCode;
                        }
                    });

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "گارانتی های مورد نظر با موفقیت ویرایش شد"
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
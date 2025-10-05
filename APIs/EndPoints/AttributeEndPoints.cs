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
    public static class AttributeEndPoints
    {
        public static void MapAddAttributesProduct(this WebApplication app)
        {
            app.MapPost("api/v1/products/add-attribute", async ([FromBody] GeneticToProductDto<AttributeDto> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products
                        .FirstOrDefaultAsync(P => P.Guid == dto.EntityId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    var attributes = dto.Items.Select(x => new ProductAttribute()
                    {
                        Product = product,
                        TitleName = x.TitleName,
                        Subsets = x.Subsets.Select(i => new AttributeSubset()
                        {
                            Name = i.Name,
                            Value = i.Value
                        }).ToList()
                    });

                    await db.ProductAttributes.AddRangeAsync(attributes);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "اتربیوت های مورد نظر با موفقیت اظافه شدند"
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

        public static void MapDeleteAttributesProduct(this WebApplication app)
        {
            app.MapDelete("api/v1/products/delete-attribute", async ([FromBody] GeneticToProductDto<Guid> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products
                        .Include(x => x.Attributes)
                        .FirstOrDefaultAsync(P => P.Guid == dto.EntityId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    if (product.Attributes == null || !product.Attributes.Any())
                    {
                        throw new Exception("این محصول اتربیوتی برای حذف ندارد");
                    }

                    var attributes = product.Attributes
                        .Where(x => dto.Items.Contains(x.Guid))
                        .Select(x => x);

                    if (attributes == null || !attributes.Any())
                    {
                        throw new Exception("اتربیوت های مورد نظر وجود ندارند");
                    }

                    db.ProductAttributes.RemoveRange(attributes);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "اتربیوت های مورد نظر با موفقیت حذف شدند"
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

        public static void MapEditAttributesProduct(this WebApplication app)
        {
            app.MapPut("api/v1/products/edit-attributes", async ([FromBody] List<AttributeEditDto> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var attributes = db.ProductAttributes
                        .Where(x => dto.Select(d => d.Id).Contains(x.Guid));

                    if (!attributes.Any())
                        throw new Exception("اتربیوت های مورد نظر وجود ندارند");

                    dto.ForEach(d =>
                    {
                        var g = attributes.FirstOrDefault(g => g.Guid == d.Id);
                        if (g != null)
                        {
                            g.TitleName = d.TitleName;
                        }
                    });

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "اتربیوت های مورد نظر با موفقیت ویرایش شد"
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
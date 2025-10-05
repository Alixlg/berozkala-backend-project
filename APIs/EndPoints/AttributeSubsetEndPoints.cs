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
    public static class AttributeSubsetEndPoints
    {
        public static void MapAddSubsetAttributesProduct(this WebApplication app)
        {
            app.MapPost("api/v1/products/add-subsetattribute", async ([FromBody] GeneticToProductDto<AttributeSubsetDto> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var attribute = await db.ProductAttributes
                        .FirstOrDefaultAsync(P => P.Guid == dto.EntityId)
                            ?? throw new Exception("اتربیوت مورد نظر یافت نشد");

                    var subsetAttributes = dto.Items.Select(x => new AttributeSubset()
                    {
                        Name = x.Name,
                        Value = x.Value,
                        Attribute = attribute
                    });

                    await db.AddRangeAsync(subsetAttributes);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "زیر مجموعه های مورد نظر با موفقیت اظافه شدند"
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

        public static void MapDeleteSubsetAttributesProduct(this WebApplication app)
        {
            app.MapDelete("api/v1/products/delete-subsetattribute", async ([FromBody] GeneticToProductDto<Guid> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var attribute = await db.ProductAttributes
                        .Include(x => x.Subsets)
                        .FirstOrDefaultAsync(P => P.Guid == dto.EntityId)
                            ?? throw new Exception("اتربیوت مورد نظر یافت نشد");

                    if (attribute.Subsets == null || !attribute.Subsets.Any())
                    {
                        throw new Exception("این محصول زیر مجموعه ای برای حذف ندارد");
                    }

                    var attributes = attribute.Subsets
                        .Where(x => dto.Items.Contains(x.Guid))
                        .Select(x => x);

                    if (attributes == null || !attributes.Any())
                    {
                        throw new Exception("زیر مجموعه های مورد نظر وجود ندارند");
                    }

                    db.RemoveRange(attributes);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "زیر مجموعه های مورد نظر با موفقیت حذف شدند"
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

        public static void MapEditSubsetAttributesProduct(this WebApplication app)
        {
            app.MapPut("api/v1/products/edit-subsetattribute", async ([FromBody] List<AttributeSubsetEditDto> dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var subsetAttributes = db.ProductSubsetAttributes
                        .Where(x => dto.Select(d => d.Id).Contains(x.Guid));

                    if (!subsetAttributes.Any())
                        throw new Exception("زیر مجموعه های مورد نظر وجود ندارند");

                    dto.ForEach(d =>
                    {
                        var s = subsetAttributes.FirstOrDefault(g => g.Guid == d.Id);
                        if (s != null)
                        {
                            s.Name = d.Name;
                            s.Value = d.Value;
                        }
                    });

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "زیر مجموعه های مورد نظر با موفقیت ویرایش شد"
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
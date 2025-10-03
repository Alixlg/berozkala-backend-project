using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.ProductSubDtos;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class ImageEndPoints
    {
        public static void MapAddImagesProduct(this WebApplication app)
        {
            app.MapPost("api/v1/products/add-images", async ([FromBody] ImageAddToProductDto dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products
                        .Include(x => x.ImagesUrls)
                        .FirstOrDefaultAsync(P => P.Guid == dto.ProductId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    var images = dto.Images.Select(x => new ProductImage
                    {
                        Product = product,
                        ImageName = x.ImageName,
                        ImagePath = x.ImagePath
                    });

                    await db.ProductImages.AddRangeAsync(images);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "عکس های مورد نظر با موفقیت اظافه شدند"
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

        public static void MapDeleteImagesProduct(this WebApplication app)
        {
            app.MapDelete("api/v1/products/delete-images", async ([FromBody] ImageDeleteToProductDto dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products
                        .Include(x => x.ImagesUrls)
                        .FirstOrDefaultAsync(P => P.Guid == dto.ProductId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    if (product.ImagesUrls == null || !product.ImagesUrls.Any())
                    {
                        throw new Exception("این محصول عکسی برای حذف ندارد");
                    }

                    var images = product.ImagesUrls
                        .Where(x => dto.ImageIds.Contains(x.Guid))
                        .Select(x => x);

                    if (!images.Any())
                        throw new Exception("تصاویر مورد نظر وجود ندارند");

                    db.ProductImages.RemoveRange(images);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "عکس های مورد نظر با موفقیت حذف شدند"
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
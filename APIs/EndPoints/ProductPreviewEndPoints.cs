using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class ProductPreviewEndPoints
    {
        public static void MapProductPreviewGet(this WebApplication app)
        {
            app.MapGet("api/v1/productsprevirw/get/{id}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var product = await db.Products.FirstOrDefaultAsync(p => p.Guid == id);

                if (product == null)
                {
                    return new RequestResultDTO<ProductPreviewDTO>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول مورد نظر یافت نشد !"
                    };
                }

                var productPreviewDTO = new ProductPreviewDTO()
                {
                    Id = product.Guid,
                    IsAvailable = product.IsAvailable,
                    Brand = product.Brand,
                    Title = product.Title,
                    Price = product.Price,
                    MaxCount = product.MaxCount,
                    ScoreRank = product.ScoreRank,
                    DiscountPercent = product.DiscountPercent,
                    PreviewImageUrl = product.PreviewImageUrl,
                    Category = product.Category,
                };

                return new RequestResultDTO<ProductPreviewDTO>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "محصول مورد نظر یافت شد",
                    Body = productPreviewDTO
                };
            }).RequireAuthorization();
        }

        public static void MapProductPreviewList(this WebApplication app)
        {
            app.MapGet("api/v1/productsprevirw/list", async ([FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var products = db.Products
                    .Select(p => new ProductPreviewDTO()
                    {
                        Id = p.Guid,
                        IsAvailable = p.IsAvailable,
                        Brand = p.Brand,
                        Title = p.Title,
                        Price = p.Price,
                        MaxCount = p.MaxCount,
                        ScoreRank = p.ScoreRank,
                        DiscountPercent = p.DiscountPercent,
                        PreviewImageUrl = p.PreviewImageUrl,
                        Category = p.Category
                    });

                return new RequestResultDTO<IEnumerable<ProductPreviewDTO>>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "لیست محصولات با موفقبت یافت شد",
                    Body = products
                };
            }).RequireAuthorization();
        }
    }
}
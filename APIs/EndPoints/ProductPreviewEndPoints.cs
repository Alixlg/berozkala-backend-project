using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.DbContextes;
using berozkala_backend.DTOs;
using berozkala_backend.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class ProductPreviewEndPoints
    {
        public static void MapProductPreviewGet(this WebApplication app)
        {
            app.MapGet("api/v1/productsprevirw/get/{id}", async ([FromRoute] string id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var product = await db.Products.FirstOrDefaultAsync(p => p.GuId == id);

                if (product == null)
                {
                    return new RequestResultDTO<ProductPreviewDTO>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول مورد نظر یافت نشد !"
                    };
                }
                else
                {
                    var productPreviewDTO = new ProductPreviewDTO()
                    {
                        Id = product.GuId,
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
                }
            }).RequireAuthorization();
        }
        public static void MapProductPreviewList(this WebApplication app)
        {
            app.MapGet("api/v1/productsprevirw/list", async ([FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var products = await db.Products
                    .Select(p => new ProductPreviewDTO()
                    {
                        Id = p.GuId,
                        IsAvailable = p.IsAvailable,
                        Brand = p.Brand,
                        Title = p.Title,
                        Price = p.Price,
                        MaxCount = p.MaxCount,
                        ScoreRank = p.ScoreRank,
                        DiscountPercent = p.DiscountPercent,
                        PreviewImageUrl = p.PreviewImageUrl,
                        Category = p.Category,
                    }).ToListAsync();

                return new RequestResultDTO<List<ProductPreviewDTO>>()
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
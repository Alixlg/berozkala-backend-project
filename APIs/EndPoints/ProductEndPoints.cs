using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.DbContextes;
using berozkala_backend.DTOs;
using berozkala_backend.DTOs.Common;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class ProductEndPoints
    {
        public static void MapProductCreate(this WebApplication app)
        {
            app.MapPost("api/v1/products/create", async ([FromBody] ProductDTO p, [FromServices] BerozkalaDb db) =>
            {
                await db.Products.AddAsync(new Product()
                {
                    IsInvisible = false,
                    IsAvailable = p.IsAvailable,
                    Brand = p.Brand,
                    Title = p.Title,
                    Price = p.Price,
                    MaxCount = p.MaxCount,
                    ScoreRank = p.ScoreRank,
                    DiscountPercent = p.DiscountPercent,
                    PreviewImageUrl = p.PreviewImageUrl,
                    ImagesUrl = p.ImagesUrl,
                    Category = p.Category,
                    Description = p.Description,
                    Review = p.Review
                });

                await db.SaveChangesAsync();

                return new RequestResultDTO<string>()
                {
                    IsSuccess = true,
                    Message = "محصول با موفقیت اظافه شد !"
                };
            });
        }
        public static void MapProductList(this WebApplication app)
        {
            app.MapGet("api/v1/products/list", async ([FromServices] BerozkalaDb db) =>
            {
                var products = await db.Products
                    .Where(p => !p.IsInvisible)
                    .Select(p => new ProductDTO()
                    {
                        Id = p.GuId,
                        DateToAdd = p.DateToAdd,
                        IsAvailable = p.IsAvailable,
                        Brand = p.Brand,
                        Title = p.Title,
                        Price = p.Price,
                        MaxCount = p.MaxCount,
                        ScoreRank = p.ScoreRank,
                        DiscountPercent = p.DiscountPercent,
                        PreviewImageUrl = p.PreviewImageUrl,
                        ImagesUrl = p.ImagesUrl,
                        Category = p.Category,
                        Description = p.Description,
                        Review = p.Review
                    }).ToListAsync();

                return new RequestResultDTO<List<ProductDTO>>()
                {
                    IsSuccess = true,
                    Message = "لیست محصولات با موفقبت یافت شد",
                    Body = products
                };
            });
        }
        public static void MapProductGet(this WebApplication app)
        {
            app.MapGet("api/v1/products/getproduct/{id}", async ([FromRoute] string id, [FromServices] BerozkalaDb db) =>
            {
                var product = await db.Products.FirstOrDefaultAsync(p => p.GuId == id);

                if (product == null || product.IsInvisible)
                {
                    return new RequestResultDTO<ProductDTO>()
                    {
                        IsSuccess = false,
                        Message = "محصول مورد نظر یافت نشد !"
                    };
                }
                else
                {
                    var productDto = new ProductDTO()
                    {
                        Id = product.GuId,
                        DateToAdd = product.DateToAdd,
                        IsAvailable = product.IsAvailable,
                        Brand = product.Brand,
                        Title = product.Title,
                        Price = product.Price,
                        MaxCount = product.MaxCount,
                        ScoreRank = product.ScoreRank,
                        DiscountPercent = product.DiscountPercent,
                        PreviewImageUrl = product.PreviewImageUrl,
                        ImagesUrl = product.ImagesUrl,
                        Category = product.Category,
                        Description = product.Description,
                        Review = product.Review
                    };

                    return new RequestResultDTO<ProductDTO>()
                    {
                        IsSuccess = true,
                        Message = "محصول مورد نظر یافت شد",
                        Body = productDto
                    };
                }
            });
        }
        public static void MapProductDelete(this WebApplication app)
        {
            app.MapPut("api/v1/products/delete/{id}", async ([FromRoute] string id, [FromServices] BerozkalaDb db) =>
            {
                var p = await db.Products.FirstOrDefaultAsync(p => p.GuId == id);

                if (p == null || p.IsInvisible)
                {
                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = false,
                        Message = "محصول مورد نظر یافت نشد !"
                    };
                }
                else
                {
                    p.IsInvisible = true;
                }

                await db.SaveChangesAsync();

                return new RequestResultDTO<string>()
                {
                    IsSuccess = true,
                    Message = "محصول مورد نظر با موفقیت حذف شد !"
                };
            });
        }
        public static void MapProductEdit(this WebApplication app)
        {
            app.MapPut("api/v1/products/edit/{id}", async ([FromRoute] string id, [FromBody] ProductDTO newProduct, [FromServices] BerozkalaDb db) =>
            {
                var p = await db.Products.FirstOrDefaultAsync(p => p.GuId == id);

                if (p == null)
                {
                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = false,
                        Message = "محصول مورد نظر یافت نشد !"
                    };
                }

                p.IsAvailable = newProduct.IsAvailable;
                p.Brand = newProduct.Brand;
                p.Title = newProduct.Title;
                p.Category = newProduct.Category;
                p.Price = newProduct.Price;
                p.MaxCount = newProduct.MaxCount;
                p.ScoreRank = newProduct.ScoreRank;
                p.DiscountPercent = newProduct.DiscountPercent;
                p.PreviewImageUrl = newProduct.PreviewImageUrl;
                p.ImagesUrl = newProduct.ImagesUrl;
                p.Description = newProduct.Description;
                p.Review = newProduct.Review;
                p.Garrantys = newProduct.Garrantys;
                p.Attributes = newProduct.Attributes;
                await db.SaveChangesAsync();

                return new RequestResultDTO<string>()
                {
                    IsSuccess = true,
                    Message = "محصول با موفقیت ویرایش شد !"
                };
            });
        }
    }
}
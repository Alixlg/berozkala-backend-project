using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.ProductDTOs;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class ProductEndPoints
    {
        public static void MapProductCreate(this WebApplication app)
        {
            app.MapPost("api/v1/products/create", async ([FromBody] ProductAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                        ?? throw new Exception("شما ادمین نیستید");

                    dto.SubCategorys?.ForEach(c =>
                    {
                        if (!db.SubCategorys.Any(x => x.Guid == c))
                        {
                            throw new Exception($"ساب کتگوری {c} وجود ندارد");
                        }
                    });

                    var product = new Product()
                    {
                        IsAvailable = dto.IsAvailable,
                        Brand = dto.Brand,
                        Title = dto.Title,
                        Price = dto.Price,
                        MaxCount = dto.MaxCount,
                        ScoreRank = dto.ScoreRank,
                        DiscountPercent = dto.DiscountPercent,
                        PreviewImageUrl = dto.PreviewImageUrl,
                        ImagesUrls = dto.ToProductImageListEntitie(),
                        Description = dto.Description,
                        Review = dto.Review,
                        Attributes = dto.ToProductAttributeListEntitie(),
                        Garrantys = dto.ToProductGarrantyListEntitie()
                    };

                    await db.Products.AddAsync(product);

                    if (dto.SubCategorys != null && dto.SubCategorys.Any())
                    {
                        var subCategorys = db.SubCategorys
                            .Where(x => (dto.SubCategorys).Contains(x.Guid))
                            .Select(s => new { s.Id });

                        var productsSubCategorys = subCategorys.Select(x => new ProductsSubCategorys()
                        {
                            Product = product,
                            SubCategoryId = x.Id
                        });

                        await db.ProductsSubCategorys.AddRangeAsync(productsSubCategorys);
                    }

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول با موفقیت اظافه شد !"
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.ToString()
                    };
                }
            }).RequireAuthorization();
        }

        // public static void MapProductList(this WebApplication app)
        // {
        //     app.MapGet("api/v1/products/list", async ([FromServices] BerozkalaDb db, HttpContext context) =>
        //     {
        //         var products = db.Products
        //             .Include(p => p.SubCategorys)
        //             .Include(p => p.ImagesUrls)
        //             .Include(p => p.Garrantys)
        //             .Include(p => p.Attributes)
        //             .ThenInclude(p => p.Subsets)
        //             .Select(p => new ProductGetDTO()
        //             {
        //                 Id = p.Guid,
        //                 DateToAdd = p.DateToAdd,
        //                 IsAvailable = p.IsAvailable,
        //                 Brand = p.Brand,
        //                 Title = p.Title,
        //                 Price = p.Price,
        //                 MaxCount = p.MaxCount,
        //                 ScoreRank = p.ScoreRank,
        //                 DiscountPercent = p.DiscountPercent,
        //                 PreviewImageUrl = p.PreviewImageUrl,
        //                 ImagesUrls = p.ImagesUrls,
        //                 Category = p.SubCategorys,
        //                 Description = p.Description,
        //                 Review = p.Review,
        //                 Attributes = p.Attributes,
        //                 Garrantys = p.Garrantys
        //             });

        //         return new RequestResultDTO<IEnumerable<ProductGetDTO>>()
        //         {
        //             IsSuccess = true,
        //             StatusCode = context.Response.StatusCode,
        //             Message = "لیست محصولات با موفقبت یافت شد",
        //             Body = products
        //         };
        //     }).RequireAuthorization();
        // }
        // public static void MapProductGet(this WebApplication app)
        // {
        //     app.MapGet("api/v1/products/getproduct/{id:Guid}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
        //     {
        //         var product = await db.Products
        //             .Where(p => p.Guid == id)
        //             .Include(p => p.SubCategorys)
        //             .Include(p => p.ImagesUrls)
        //             .Include(p => p.Garrantys)
        //             .Include(p => p.Attributes)
        //             .ThenInclude(p => p.Subsets)
        //             .Select(p => new ProductGetDTO()
        //             {
        //                 Id = p.Guid,
        //                 DateToAdd = p.DateToAdd,
        //                 IsAvailable = p.IsAvailable,
        //                 Brand = p.Brand,
        //                 Title = p.Title,
        //                 Price = p.Price,
        //                 MaxCount = p.MaxCount,
        //                 ScoreRank = p.ScoreRank,
        //                 DiscountPercent = p.DiscountPercent,
        //                 PreviewImageUrl = p.PreviewImageUrl,
        //                 ImagesUrls = p.ImagesUrls,
        //                 Category = p.SubCategorys,
        //                 Description = p.Description,
        //                 Review = p.Review,
        //                 Attributes = p.Attributes,
        //                 Garrantys = p.Garrantys
        //             }).FirstOrDefaultAsync();

        //         if (product == null)
        //         {
        //             return new RequestResultDTO<ProductGetDTO>()
        //             {
        //                 IsSuccess = false,
        //                 StatusCode = context.Response.StatusCode,
        //                 Message = "محصول مورد نظر یافت نشد !"
        //             };
        //         }
        //         else
        //         {
        //             return new RequestResultDTO<ProductGetDTO>()
        //             {
        //                 IsSuccess = true,
        //                 StatusCode = context.Response.StatusCode,
        //                 Message = "محصول مورد نظر یافت شد",
        //                 Body = product
        //             };
        //         }
        //     }).RequireAuthorization();
        // }

        // public static void MapProductDelete(this WebApplication app)
        // {
        //     app.MapDelete("api/v1/products/delete/{id:Guid}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
        //     {
        //         var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

        //         var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid));

        //         if (admin == null)
        //         {
        //             return new RequestResultDTO<string>()
        //             {
        //                 IsSuccess = false,
        //                 StatusCode = context.Response.StatusCode,
        //                 Message = "شما ادمین نیستید"
        //             };
        //         }

        //         var p = await db.Products.FirstOrDefaultAsync(p => p.Guid == id);

        //         if (p == null)
        //         {
        //             return new RequestResultDTO<string>()
        //             {
        //                 IsSuccess = false,
        //                 StatusCode = context.Response.StatusCode,
        //                 Message = "محصول مورد نظر یافت نشد !"
        //             };
        //         }
        //         else
        //         {
        //             db.Products.Remove(p);
        //         }

        //         await db.SaveChangesAsync();

        //         return new RequestResultDTO<string>()
        //         {
        //             IsSuccess = true,
        //             StatusCode = context.Response.StatusCode,
        //             Message = "محصول مورد نظر با موفقیت حذف شد !"
        //         };
        //     }).RequireAuthorization();
        // }

        // public static void MapProductEdit(this WebApplication app)
        // {
        //     app.MapPut("api/v1/products/edit/{id:Guid}", async ([FromRoute] Guid id, [FromBody] ProductAddDto newProduct, [FromServices] BerozkalaDb db, HttpContext context) =>
        //     {
        //         var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

        //         var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid));

        //         if (admin == null)
        //         {
        //             return new RequestResultDTO<string>()
        //             {
        //                 IsSuccess = false,
        //                 StatusCode = context.Response.StatusCode,
        //                 Message = "شما ادمین نیستید"
        //             };
        //         }

        //         var p = await db.Products
        //             .Where(p => p.Guid == id)
        //             .Include(p => p.SubCategorys)
        //             .Include(p => p.ImagesUrls)
        //             .Include(p => p.Garrantys)
        //             .Include(p => p.Attributes)
        //             .ThenInclude(p => p.Subsets)
        //             .FirstOrDefaultAsync();

        //         if (p == null)
        //         {
        //             return new RequestResultDTO<string>()
        //             {
        //                 IsSuccess = false,
        //                 StatusCode = context.Response.StatusCode,
        //                 Message = "محصول مورد نظر یافت نشد !"
        //             };
        //         }

        //         p.IsAvailable = newProduct.IsAvailable;
        //         p.Brand = newProduct.Brand;
        //         p.Title = newProduct.Title;
        //         p.SubCategorys = newProduct.SubCategorys;
        //         p.Price = newProduct.Price;
        //         p.MaxCount = newProduct.MaxCount;
        //         p.ScoreRank = newProduct.ScoreRank;
        //         p.DiscountPercent = newProduct.DiscountPercent;
        //         p.PreviewImageUrl = newProduct.PreviewImageUrl;
        //         p.ImagesUrls = newProduct.ImagesUrls;
        //         p.Description = newProduct.Description;
        //         p.Review = newProduct.Review;
        //         p.Garrantys = newProduct.Garrantys;
        //         p.Attributes = newProduct.Attributes;

        //         await db.SaveChangesAsync();

        //         return new RequestResultDTO<string>()
        //         {
        //             IsSuccess = true,
        //             StatusCode = context.Response.StatusCode,
        //             Message = "محصول با موفقیت ویرایش شد !"
        //         };
        //     }).RequireAuthorization();
        // }
    }
}
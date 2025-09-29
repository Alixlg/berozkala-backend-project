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
            app.MapPost("api/v1/products/create", async ([FromBody] ProductAddDTO p, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid));

                if (admin == null)
                {
                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما ادمین نیستید"
                    };
                }

                await db.Products.AddAsync(new Product()
                {
                    IsAvailable = p.IsAvailable,
                    Brand = p.Brand,
                    Title = p.Title,
                    Price = p.Price,
                    MaxCount = p.MaxCount,
                    ScoreRank = p.ScoreRank,
                    DiscountPercent = p.DiscountPercent,
                    PreviewImageUrl = p.PreviewImageUrl,
                    ImagesUrls = p.ImagesUrls,
                    Category = p.Category,
                    Description = p.Description,
                    Review = p.Review,
                    Attributes = p.Attributes,
                    Garrantys = p.Garrantys
                });

                await db.SaveChangesAsync();

                return new RequestResultDTO<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "محصول با موفقیت اظافه شد !"
                };
            }).RequireAuthorization();
        }

        public static void MapProductList(this WebApplication app)
        {
            app.MapGet("api/v1/products/list", async ([FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var products = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.ImagesUrls)
                    .Include(p => p.Garrantys)
                    .Include(p => p.Attributes)
                    .ThenInclude(p => p.Subsets)
                    .Select(p => new ProductGetDTO()
                    {
                        Id = p.Guid,
                        DateToAdd = p.DateToAdd,
                        IsAvailable = p.IsAvailable,
                        Brand = p.Brand,
                        Title = p.Title,
                        Price = p.Price,
                        MaxCount = p.MaxCount,
                        ScoreRank = p.ScoreRank,
                        DiscountPercent = p.DiscountPercent,
                        PreviewImageUrl = p.PreviewImageUrl,
                        ImagesUrls = p.ImagesUrls,
                        Category = p.Category,
                        Description = p.Description,
                        Review = p.Review,
                        Attributes = p.Attributes,
                        Garrantys = p.Garrantys
                    });

                return new RequestResultDTO<IEnumerable<ProductGetDTO>>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "لیست محصولات با موفقبت یافت شد",
                    Body = products
                };
            }).RequireAuthorization();
        }
        public static void MapProductGet(this WebApplication app)
        {
            app.MapGet("api/v1/products/getproduct/{id}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var product = await db.Products
                    .Where(p => p.Guid == id)
                    .Include(p => p.Category)
                    .Include(p => p.ImagesUrls)
                    .Include(p => p.Garrantys)
                    .Include(p => p.Attributes)
                    .ThenInclude(p => p.Subsets)
                    .Select(p => new ProductGetDTO()
                    {
                        Id = p.Guid,
                        DateToAdd = p.DateToAdd,
                        IsAvailable = p.IsAvailable,
                        Brand = p.Brand,
                        Title = p.Title,
                        Price = p.Price,
                        MaxCount = p.MaxCount,
                        ScoreRank = p.ScoreRank,
                        DiscountPercent = p.DiscountPercent,
                        PreviewImageUrl = p.PreviewImageUrl,
                        ImagesUrls = p.ImagesUrls,
                        Category = p.Category,
                        Description = p.Description,
                        Review = p.Review,
                        Attributes = p.Attributes,
                        Garrantys = p.Garrantys
                    }).FirstOrDefaultAsync();

                if (product == null)
                {
                    return new RequestResultDTO<ProductGetDTO>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول مورد نظر یافت نشد !"
                    };
                }
                else
                {
                    return new RequestResultDTO<ProductGetDTO>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول مورد نظر یافت شد",
                        Body = product
                    };
                }
            }).RequireAuthorization();
        }

        public static void MapProductDelete(this WebApplication app)
        {
            app.MapDelete("api/v1/products/delete/{id}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid));

                if (admin == null)
                {
                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما ادمین نیستید"
                    };
                }

                var p = await db.Products.FirstOrDefaultAsync(p => p.Guid == id);

                if (p == null)
                {
                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول مورد نظر یافت نشد !"
                    };
                }
                else
                {
                    db.Products.Remove(p);
                }

                await db.SaveChangesAsync();

                return new RequestResultDTO<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "محصول مورد نظر با موفقیت حذف شد !"
                };
            }).RequireAuthorization();
        }

        public static void MapProductEdit(this WebApplication app)
        {
            app.MapPut("api/v1/products/edit/{id}", async ([FromRoute] Guid id, [FromBody] ProductAddDTO newProduct, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid));

                if (admin == null)
                {
                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما ادمین نیستید"
                    };
                }

                var p = await db.Products
                    .Where(p => p.Guid == id)
                    .Include(p => p.Category)
                    .Include(p => p.ImagesUrls)
                    .Include(p => p.Garrantys)
                    .Include(p => p.Attributes)
                    .ThenInclude(p => p.Subsets)
                    .FirstOrDefaultAsync();

                if (p == null)
                {
                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
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
                p.ImagesUrls = newProduct.ImagesUrls;
                p.Description = newProduct.Description;
                p.Review = newProduct.Review;
                p.Garrantys = newProduct.Garrantys;
                p.Attributes = newProduct.Attributes;

                await db.SaveChangesAsync();

                return new RequestResultDTO<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "محصول با موفقیت ویرایش شد !"
                };
            }).RequireAuthorization();
        }
    }
}
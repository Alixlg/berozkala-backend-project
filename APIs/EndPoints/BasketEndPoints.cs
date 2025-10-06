using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.BasketDTOs;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class BasketEndPoints
    {
        public static void MapAddProductToBasket(this WebApplication app)
        {
            app.MapPost("api/v1/basket/add-product", async ([FromBody] ProductToBasketDto dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var user = await db.Users.FirstOrDefaultAsync(u => u.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما لاگین نکرده اید");

                    var product = await db.Products
                        .Include(x => x.Garrantys)
                        .FirstOrDefaultAsync(P => P.Guid == dto.ProductId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    var exists = db.BasketsProducts
                        .Where(x => x.User.Id == user.Id && x.Product.Id == product.Id);

                    if (await exists.AnyAsync())
                    {
                        await exists.ForEachAsync(x =>
                        {
                            if (x.SelectedGarranty == null)
                            {
                                if (dto.SelectedGarranty == null)
                                {
                                    x.ProductCount += dto.ProductCount;
                                }
                                else if (dto.SelectedGarranty != null)
                                {

                                }
                            }
                        });

                        throw new Exception("این محصول قبلاً در سبد شما وجود دارد");
                    }

                    var basket = new BasketProduct()
                    {
                        Product = product,
                        User = user
                    };

                    if (dto.SelectedGarranty != null)
                    {
                        if (product.Garrantys != null && product.Garrantys.Any())
                        {
                            var garranty = product.Garrantys
                                .FirstOrDefault(x => x.Guid == dto.SelectedGarranty)
                                    ?? throw new Exception("گارانتی های انتخابی وجود ندارند");

                            basket.SelectedGarranty = garranty;
                        }
                        else
                        {
                            throw new Exception("این محصول هیچ گارانتی برای ثبت ندارد");
                        }
                    }

                    await db.BasketsProducts.AddAsync(basket);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول مورد نظر با موفقیت به سبد خرید شما اضافه شد"
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

        // public static void MapRemoveCategorysProduct(this WebApplication app)
        // {
        //     app.MapDelete("api/v1/products/remove-categorys", async ([FromBody] SubCategoryToProductDto dto,
        //         [FromServices] BerozkalaDb db, HttpContext context) =>
        //     {
        //         try
        //         {
        //             var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

        //             var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
        //                 ?? throw new Exception("شما ادمین نیستید");

        //             var product = await db.Products.FirstOrDefaultAsync(P => P.Guid == dto.ProductId)
        //                 ?? throw new Exception("محصول مورد نظر یافت نشد");

        //             var subCategorys = db.SubCategorys
        //                 .Where(x => dto.SubCategoryIds.Contains(x.Guid))
        //                 .Select(s => s.Id);

        //             var currentRelations = await db.ProductsSubCategorys
        //                 .Where(x => x.ProductId == product.Id && subCategorys.Contains(x.SubCategoryId))
        //                 .ToListAsync();

        //             if (!currentRelations.Any())
        //                 throw new Exception("کتگوری ها قبلا از پروداکت حذف شده اند");

        //             db.ProductsSubCategorys.RemoveRange(currentRelations);
        //             await db.SaveChangesAsync();

        //             return new RequestResultDto<string>()
        //             {
        //                 IsSuccess = true,
        //                 StatusCode = context.Response.StatusCode,
        //                 Message = "کتگوری ها با موفقیت از پروداکت حذف شدند"
        //             };
        //         }
        //         catch (Exception ex)
        //         {
        //             return new RequestResultDto<string>()
        //             {
        //                 IsSuccess = false,
        //                 StatusCode = context.Response.StatusCode,
        //                 Message = ex.Message
        //             };
        //         }
        //     }).RequireAuthorization();
        // }
    }
}
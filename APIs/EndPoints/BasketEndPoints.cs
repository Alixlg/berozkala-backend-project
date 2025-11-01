using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.BasketDTOs;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.ProductDTOs;
using berozkala_backend.DTOs.ProductSubDTOs;
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
                        .FirstOrDefaultAsync(p => p.Guid == dto.ProductId)
                            ?? throw new Exception("محصول مورد نظر یافت نشد");

                    ProductGarranty garranty;

                    if (dto.SelectedGarranty != null)
                    {
                        if (product.Garrantys != null && product.Garrantys.Any())
                        {
                            garranty = product.Garrantys
                                .FirstOrDefault(x => x.Guid == dto.SelectedGarranty)
                                    ?? throw new Exception("گارانتی انتخابی وجود ندارد");
                        }
                        else
                        {
                            throw new Exception("این محصول هیچ گارانتی برای ثبت ندارد");
                        }
                    }
                    else
                    {
                        garranty = null!;
                    }

                    BasketProduct? exist;

                    if (dto.SelectedGarranty != null)
                    {
                        exist = await db.BasketsProducts
                            .FirstOrDefaultAsync(x => x.User.Id == user.Id && x.Product.Id == product.Id && x.SelectedGarrantyId == (garranty != null ? garranty.Id : 0));
                    }
                    else
                    {
                        exist = await db.BasketsProducts
                            .FirstOrDefaultAsync(x => x.User.Id == user.Id && x.Product.Id == product.Id && x.SelectedGarrantyId == null);
                    }

                    var exists = db.BasketsProducts
                        .Where(x => x.User.Id == user.Id && x.Product.Id == product.Id);

                    if (exist != null)
                    {
                        int count = 1;
                        await exists.ForEachAsync(x =>
                        {
                            count += x.ProductCount;
                        });

                        if (count > product.MaxCount)
                        {
                            throw new Exception("شما نمیتوانید بیش از حد معین محصول به سبد خود اضافه کنید");
                        }

                        exist.ProductCount++;
                    }
                    else
                    {
                        int count = 1;
                        await exists.ForEachAsync(x =>
                        {
                            count += x.ProductCount;
                        });

                        if (count > product.MaxCount)
                        {
                            throw new Exception("شما نمیتوانید بیش از حد معین محصول به سبد خود اضافه کنید");
                        }

                        var basket = new BasketProduct()
                        {
                            Product = product,
                            User = user,
                            ProductCount = 1
                        };

                        if (garranty != null)
                        {
                            basket.SelectedGarranty = garranty;
                        }

                        await db.BasketsProducts.AddAsync(basket);
                    }

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

        public static void MapRemoveProductFromBasket(this WebApplication app)
        {
            app.MapDelete("api/v1/basket/remove-product", async ([FromQuery] Guid basketProductId,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var user = await db.Users.Include(x => x.BasketsProducts)
                        .FirstOrDefaultAsync(u => u.Guid == Guid.Parse(guid))
                            ?? throw new Exception("شما لاگین نکرده اید");

                    var exist = user.BasketsProducts?
                        .FirstOrDefault(p => p.Guid == basketProductId)
                            ?? throw new Exception("محصول مورد نظر در سبد شما وجود ندارد");

                    if (exist.ProductCount > 1)
                    {
                        exist.ProductCount--;
                    }
                    else
                    {
                        db.BasketsProducts.Remove(exist);
                    }

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول مورد نظر از سبد خرید شما حذف شد"
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

        public static void MapClearProductFromBasket(this WebApplication app)
        {
            app.MapDelete("api/v1/basket/clear-product", async ([FromQuery] Guid basketProductId,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var user = await db.Users.Include(x => x.BasketsProducts)
                        .FirstOrDefaultAsync(u => u.Guid == Guid.Parse(guid))
                            ?? throw new Exception("شما لاگین نکرده اید");

                    var exist = user.BasketsProducts?
                        .FirstOrDefault(p => p.Guid == basketProductId)
                            ?? throw new Exception("محصول مورد نظر در سبد شما وجود ندارد");

                    db.BasketsProducts.Remove(exist);

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول مورد نظر از سبد خرید شما حذف شد"
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

        public static void MapGetBasketProducts(this WebApplication app)
        {
            app.MapGet("api/v1/basket/get-products", async ([FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var user = await db.Users
                        .Include(x => x.BasketsProducts).ThenInclude(x => x.SelectedGarranty)
                        .Include(x => x.BasketsProducts).ThenInclude(x => x.Product)
                            .FirstOrDefaultAsync(u => u.Guid == Guid.Parse(guid)) ?? throw new Exception("شما لاگین نکرده اید");

                    List<GetBasketProductDto> basketProducts = [];

                    if (user.BasketsProducts != null)
                    {
                        basketProducts = user.BasketsProducts.Select(p => new GetBasketProductDto
                        {
                            Id = p.Guid,
                            ProductCount = p.ProductCount,
                            Product = new ProductPreviewDto()
                            {
                                Id = p.Product.Guid,
                                IsAvailable = p.Product.IsAvailable,
                                Brand = p.Product.Brand,
                                Title = p.Product.Title,
                                Price = p.Product.Price,
                                MaxCount = p.Product.MaxCount,
                                ScoreRank = p.Product.ScoreRank,
                                DiscountPercent = p.Product.DiscountPercent,
                                PreviewImageUrl = p.Product.PreviewImageUrl
                            },
                            SelectedGarranty = new GarrantyDto()
                            {
                                Id = p.SelectedGarranty?.Guid,
                                GarrantyCode = p.SelectedGarranty?.GarrantyCode ?? 0,
                                Name = p.SelectedGarranty?.Name ?? "بدون گارانتی"
                            }
                        }).ToList();
                    }

                    return new RequestResultDto<List<GetBasketProductDto>>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "عملیات موفقیت آمیز بود",
                        Body = basketProducts
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<List<GetBasketProductDto>>()
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
using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CategoryDTOs;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.ProductDTOs;
using berozkala_backend.DTOs.ProductSubDtos;
using berozkala_backend.DTOs.ProductSubDTOs;
using berozkala_backend.Entities.ProductEntities;
using berozkala_backend.Enums;
using berozkala_backend.Tools;
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

                    dto.SubCategoryIds?.ForEach(c =>
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

                    if (dto.SubCategoryIds != null && dto.SubCategoryIds.Any())
                    {
                        var subCategorys = db.SubCategorys
                            .Where(x => dto.SubCategoryIds.Contains(x.Guid))
                            .Select(s => s);

                        var productsSubCategorys = subCategorys.Select(x => new ProductSubCategory()
                        {
                            Product = product,
                            SubCategory = x
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

        public static void MapProductList(this WebApplication app)
        {
            app.MapPost("api/v1/products/list", async ([FromQuery] string? searchQuery,
                [FromBody] ProductGetListDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var querys = db.Products.AsQueryable();

                    if (dto.IsAvailable != null)
                    {
                        querys = querys.Where(x => x.IsAvailable == dto.IsAvailable);
                    }

                    if (!string.IsNullOrWhiteSpace(dto.SubCategoryName))
                    {
                        querys = querys.Where(p =>
                            p.ProductsSubCategorys!.Any(ps =>
                                EF.Functions.Like(ps.SubCategory!.SubCategoryName, $"%{dto.SubCategoryName}%")));
                    }

                    var skipCount = AppTools.PageSkipCount(dto.PageId, dto.PageCount);

                    if (!string.IsNullOrWhiteSpace(searchQuery) && Guid.TryParse(searchQuery, out var guid))
                    {
                        querys = querys.Where(x => x.Guid == guid);
                    }
                    else if (!string.IsNullOrWhiteSpace(searchQuery))
                    {
                        querys = querys.Where(p =>
                            EF.Functions.Like(p.Title, $"%{searchQuery}%") ||
                            EF.Functions.Like(p.Brand, $"%{searchQuery}%"));

                        querys = querys.Skip(skipCount).Take(dto.PageCount);
                    }
                    else
                    {
                        querys = querys.Skip(skipCount).Take(dto.PageCount);
                    }

                    switch (dto.Fillter)
                    {
                        case ProductFillter.FilterByPriceHighToLow:
                            querys = querys.OrderBy(x => x.Price);
                            break;
                        case ProductFillter.FilterByPriceLowToHigh:
                            querys = querys.OrderByDescending(x => x.Price);
                            break;
                        case ProductFillter.NewProducts:
                            querys = querys.OrderBy(x => x.Id);
                            break;
                        case ProductFillter.OldProducts:
                            querys = querys.OrderByDescending(x => x.Id);
                            break;
                    }

                    var prdoucts = querys
                        .Include(p => p.ProductsSubCategorys)
                            .ThenInclude(p => p.SubCategory)
                        .Include(p => p.ImagesUrls)
                        .Include(p => p.Garrantys)
                        .Include(p => p.Attributes)
                            .ThenInclude(p => p.Subsets)
                        .Select(p => new ProductGetDto()
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
                            ImagesUrls = p.ImagesUrls!.Select(i => new ImageDto()
                            {
                                Id = i.Guid,
                                ImagePath = i.ImagePath,
                                ImageName = i.ImageName
                            }).ToList(),
                            Category = p.ProductsSubCategorys!.Select(s => new SubCategoryGetDto()
                            {
                                Id = s.Guid,
                                SubCategoryName = s.SubCategory!.SubCategoryName
                            }).ToList(),
                            Description = p.Description,
                            Review = p.Review,
                            Attributes = p.Attributes!.Select(a => new AttributeDto()
                            {
                                Id = a.Guid,
                                TitleName = a.TitleName,
                                Subsets = a.Subsets.Select(s => new AttributeSubsetDto
                                {
                                    Id = s.Guid,
                                    Name = s.Name,
                                    Value = s.Value
                                }).ToList()
                            }).ToList(),
                            Garrantys = p.Garrantys!.Select(s => new GarrantyDto()
                            {
                                Id = s.Guid,
                                Name = s.Name,
                                GarrantyCode = s.GarrantyCode
                            }).ToList()
                        });

                    return new RequestResultDto<IEnumerable<ProductGetDto>>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "لیست محصولات با موفقبت یافت شد",
                        Body = prdoucts
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<IEnumerable<ProductGetDto>>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message
                    };
                }
            }).RequireAuthorization();
        }

        public static void MapProductEdit(this WebApplication app)
        {
            app.MapPut("api/v1/products/edit/{id:Guid}", async ([FromRoute] Guid id, [FromBody] ProductEditDto newProduct, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var p = await db.Products
                        .Where(p => p.Guid == id)
                        .FirstOrDefaultAsync() ?? throw new Exception("محصول مورد نظر یافت نشد");

                    p.IsAvailable = newProduct.IsAvailable;
                    p.Brand = newProduct.Brand;
                    p.Title = newProduct.Title;
                    p.Price = newProduct.Price;
                    p.MaxCount = newProduct.MaxCount;
                    p.DiscountPercent = newProduct.DiscountPercent;
                    p.ScoreRank = newProduct.ScoreRank == 0 ? p.ScoreRank : newProduct.ScoreRank;
                    p.PreviewImageUrl = newProduct.PreviewImageUrl ?? p.PreviewImageUrl;
                    p.Description = newProduct.Description ?? p.Description;
                    p.Review = newProduct.Review ?? p.Review;

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "محصول با موفقیت ویرایش شد !"
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

        public static void MapAddCategorysToProduct(this WebApplication app)
        {
            app.MapPost("api/v1/products/add-categorys", async ([FromBody] SubCategoryToProductDto dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products.FirstOrDefaultAsync(P => P.Guid == dto.ProductId)
                        ?? throw new Exception("محصول مورد نظر یافت نشد");

                    var subCategorys = db.SubCategorys
                        .Where(x => dto.SubCategoryIds.Contains(x.Guid))
                        .Select(s => s.Id);

                    var productsSubCategorys = subCategorys
                        .Select(x => new ProductSubCategory()
                        {
                            Product = product,
                            SubCategoryId = x
                        })
                        .ToList();

                    if (!productsSubCategorys.Any())
                        throw new Exception("کتگوری ها وجود ندارند");

                    var currentRelations = await db.ProductsSubCategorys
                        .Where(x => x.ProductId == product.Id && subCategorys.Contains(x.SubCategoryId))
                        .ToListAsync();

                    if (currentRelations.Any())
                        throw new Exception("کتگوری ها قبلا به پروداکت اظافه شده اند");

                    await db.ProductsSubCategorys.AddRangeAsync(productsSubCategorys);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "کتگوری با موفقیت به پروداکت اظافه شد"
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

        public static void MapRemoveCategorysProduct(this WebApplication app)
        {
            app.MapDelete("api/v1/products/remove-categorys", async ([FromBody] SubCategoryToProductDto dto,
                [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var product = await db.Products.FirstOrDefaultAsync(P => P.Guid == dto.ProductId)
                        ?? throw new Exception("محصول مورد نظر یافت نشد");

                    var subCategorys = db.SubCategorys
                        .Where(x => dto.SubCategoryIds.Contains(x.Guid))
                        .Select(s => s.Id);

                    var currentRelations = await db.ProductsSubCategorys
                        .Where(x => x.ProductId == product.Id && subCategorys.Contains(x.SubCategoryId))
                        .ToListAsync();

                    if (!currentRelations.Any())
                        throw new Exception("کتگوری ها قبلا از پروداکت حذف شده اند");

                    db.ProductsSubCategorys.RemoveRange(currentRelations);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "کتگوری ها با موفقیت از پروداکت حذف شدند"
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
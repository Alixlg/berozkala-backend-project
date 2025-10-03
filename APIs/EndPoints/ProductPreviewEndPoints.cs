using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.ProductDTOs;
using berozkala_backend.Entities.ProductEntities;
using berozkala_backend.Enums;
using berozkala_backend.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class ProductPreviewEndPoints
    {
        public static void MapProductPreviewList(this WebApplication app)
        {
            app.MapPost("api/v1/productsprevirw/list", async ([FromQuery] string? searchQuery,
                [FromBody] ProductGetListDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var querys = db.Products.AsQueryable();

                    if (dto.IsAvailable != null)
                    {
                        querys = querys.Where(x => x.IsAvailable == dto.IsAvailable);
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
                        .Select(p => new ProductPreviewDto()
                        {
                            Id = p.Guid,
                            IsAvailable = p.IsAvailable,
                            Brand = p.Brand,
                            Title = p.Title,
                            Price = p.Price,
                            MaxCount = p.MaxCount,
                            ScoreRank = p.ScoreRank,
                            DiscountPercent = p.DiscountPercent,
                            PreviewImageUrl = p.PreviewImageUrl
                        });

                    return new RequestResultDto<IEnumerable<ProductPreviewDto>>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "لیست محصولات با موفقبت یافت شد",
                        Body = prdoucts
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<IEnumerable<ProductPreviewDto>>()
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
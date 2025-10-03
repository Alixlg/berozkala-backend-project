using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CategoryDTOs;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace berozkala_backend.APIs.EndPoints
{
    public static class CategoryEndPoints
    {
        public static void MapCategoryCreate(this WebApplication app)
        {
            app.MapPost("api/v1/category/create", async ([FromBody] CategoryAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var category = new Category()
                    {
                        CategoryName = dto.CategoryName
                    };

                    category.SubCategorys = dto.SubCategorys?.Select(s => s.ToProductSubCategory(category))
                        .ToList();

                    await db.Categorys.AddAsync(category);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفقیت کتگوری را اظافه کردید"
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

        public static void MapCategoryEdit(this WebApplication app)
        {
            app.MapPut("api/v1/category/edit/{id:Guid}", async ([FromRoute] Guid id, [FromBody] CategoryAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var category = await db.Categorys.FirstOrDefaultAsync(x => x.Guid == id)
                        ?? throw new Exception("کتگوری مورد نظر یافت نشد");

                    category.CategoryName = dto.CategoryName;
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفقیت کتگوری را ویرایش کردید"
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

        public static void MapCategoryList(this WebApplication app)
        {
            app.MapGet("api/v1/category/list", async ([FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var categorys = db.Categorys.Include(x => x.SubCategorys)
                        .Select(x => new CategoryGetDto()
                        {
                            Id = x.Guid,
                            CategoryName = x.CategoryName,
                            SubCategorys = x.SubCategorys!.Select(s => new SubCategoryAddDto()
                            {
                                Id = s.Guid,
                                SubCategoryName = s.SubCategoryName
                            }).ToList()
                        });

                    return new RequestResultDto<IEnumerable<CategoryGetDto>>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Body = categorys
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<IEnumerable<CategoryGetDto>>()
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
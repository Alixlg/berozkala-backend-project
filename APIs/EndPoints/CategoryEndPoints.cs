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
            app.MapPost("api/v1/category/create", async ([FromBody] AddCategoryDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid))
                        ?? throw new Exception("شما ادمین نیستید");

                    await db.ProductCategorys.AddAsync(new ProductCategory()
                    {
                        CategoryName = dto.CategoryName,
                        SubCategorys = dto.SubCategorys
                    });

                    await db.SaveChangesAsync();

                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفقیت کتگوری را اظافه کردید"
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDTO<string>()
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
            app.MapPut("api/v1/category/Edit{id}", async ([FromRoute] Guid id, [FromBody] AddCategoryDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var category = await db.ProductCategorys.FirstOrDefaultAsync(x => x.Guid == id)
                        ?? throw new Exception("کتگوری مورد نظر یافت نشد");

                    category.CategoryName = dto.CategoryName;
                    category.SubCategorys = dto.SubCategorys;

                    await db.SaveChangesAsync();

                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفقیت کتگوری را ویرایش کردید"
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDTO<string>()
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
                    var categorys = db.ProductCategorys.Include(x => x.SubCategorys)
                        .Select(x => new GetCategoryDto()
                        {
                            Id = x.Guid,
                            CategoryName = x.CategoryName,
                            SubCategorys = x.SubCategorys
                        });

                    return new RequestResultDTO<IEnumerable<GetCategoryDto>>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Body = categorys
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDTO<IEnumerable<GetCategoryDto>>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message
                    };
                }
            }).RequireAuthorization();
        }

        public static void MapCategoryDelete(this WebApplication app)
        {
            app.MapDelete("api/v1/category/delete/{id}", async (Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var category = db.ProductCategorys.Include(x => x.SubCategorys)
                        .FirstOrDefaultAsync(x => x.Guid == id) ?? throw new Exception("کتگوری مورد نظر وجود ندارد");

                    db.Remove(category);
                    await db.SaveChangesAsync();

                    return new RequestResultDTO<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "کتگوری مورد نظر با موفقیت حذف شد"
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDTO<string>()
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
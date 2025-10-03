using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CategoryDTOs;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class SubCategoryEndPoints
    {
        public static void MapSubCategoryCreate(this WebApplication app)
        {
            app.MapPost("api/v1/subcategory/create/{categoryId:Guid}", async (Guid categoryId, [FromBody] SubCategoryAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var category = await db.Categorys.FirstOrDefaultAsync(x => x.Guid == categoryId)
                        ?? throw new Exception("کتگوری مورد نظر پیدا نشد");

                    await db.AddAsync(new SubCategory()
                    {
                        SubCategoryName = dto.SubCategoryName,
                        ProductCategory = category
                    });

                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفقیت ساب کتگوری را اظافه کردید"
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

        public static void MapSubCategoryEdit(this WebApplication app)
        {
            app.MapPut("api/v1/subcategory/edit/{subCategoryId:Guid}", async ([FromRoute] Guid subCategoryId, [FromBody] SubCategoryAddDto dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var subCategory = await db.SubCategorys.FirstOrDefaultAsync(x => x.Guid == subCategoryId)
                        ?? throw new Exception("ساب کتگوری مورد نظر یافت نشد");

                    subCategory.SubCategoryName = dto.SubCategoryName;
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفقیت ساب کتگوری را ویرایش کردید"
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

        public static void MapSubCategoryGet(this WebApplication app)
        {
            app.MapGet("api/v1/subcategory/get/{id:Guid}", async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var subCategory = await db.SubCategorys
                        .Include(x => x.ProductCategory)
                        .FirstOrDefaultAsync(x => x.Guid == id)
                            ?? throw new Exception("ساب کتگوری مورد نظر یافت نشد");

                    var dto = new SubCategoryGetDto()
                    {
                        Id = subCategory.Guid,
                        SubCategoryName = subCategory.SubCategoryName
                    };

                    return new RequestResultDto<SubCategoryGetDto>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Body = dto
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<SubCategoryGetDto>()
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
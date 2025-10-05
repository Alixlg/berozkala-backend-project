using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class GenericEndPoints
    {
        public static void MapGenericDeleteList<T1>(this WebApplication app, string apiRoute) where T1 : class, IGuid
        {
            app.MapDelete(apiRoute, async ([FromBody] List<Guid> dto, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var entitys = db.Set<T1>().Where(x => dto.Contains(x.Guid))
                        .Select(x => x);

                    if (entitys == null || !entitys.Any())
                    {
                        throw new Exception("دیتا های مورد نظر وجود ندارند");
                    }

                    db.RemoveRange(entitys);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = $"دیتا های مورد نظر با موفقیت حذف شدند"
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
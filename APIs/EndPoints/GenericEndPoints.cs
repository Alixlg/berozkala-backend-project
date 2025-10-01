using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class GenericEndPoints
    {
        public static void MapGenericDelete<T1>(this WebApplication app, string tabelName) where T1 : class, IGuid
        {
            app.MapDelete("api/v1/tabel/delete/{id:Guid}".Replace("tabel", tabelName), async ([FromRoute] Guid id, [FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var userGuid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins.FirstOrDefaultAsync(a => a.Guid == Guid.Parse(userGuid))
                        ?? throw new Exception("شما ادمین نیستید");

                    var entity = await db.Set<T1>().FirstOrDefaultAsync(x => x.Guid == id)
                        ?? throw new Exception($"مورد نظر یافت نشد {tabelName}");

                    db.Remove(entity);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = $"مورد نظر با موفقیت حذف شد {tabelName}"
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
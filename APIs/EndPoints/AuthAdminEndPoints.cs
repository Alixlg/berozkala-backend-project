using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.MemberDTOs;
using berozkala_backend.Enums;
using berozkala_backend.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class AuthAdminEndPoints
    {
        public static void MapAuthAdminLogin(this WebApplication app)
        {
            app.MapPost("api/v1/auth/admin/login", async ([FromServices] BerozkalaDb db, [FromBody] LoginWithUsernameDto dto,
                HttpContext context) =>
            {
                if (string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.PassWord))
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "یوزر نیم یا پسورد خالی است"
                    };
                }

                var admin = await db.Admins.Where(x => x.Status == AccountStatus.Active)
                    .FirstOrDefaultAsync(x => x.UserName == dto.UserName);

                if (admin == null)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "یوزر نیم اشتباه است"
                    };
                }

                if (admin.PassWord != dto.PassWord)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "پسورد اشتباه است"
                    };
                }

                var token = JwtTools.GenerateJwtToken(admin.UserName, admin.Guid.ToString(), context.Request.Headers.UserAgent, DateTime.Now.AddMinutes(300));

                return new RequestResultDto<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "شما با موفقیت وارد پنل ادمین شدید",
                    Body = token
                };
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.MemberDTOs;
using berozkala_backend.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class AuthCheckEndPoints
    {
        public static void MapAuthValidToken(this WebApplication app)
        {
            app.MapGet("api/v1/auth/valid-token", async ([FromServices] BerozkalaDb db,
                HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    AuthCheckDto response;

                    var admin = await db.Admins
                        .Where(x => x.Status == AccountStatus.Active)
                        .FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid));

                    var user = await db.Users
                        .Where(x => x.Status == AccountStatus.Active)
                        .FirstOrDefaultAsync(u => u.Guid == Guid.Parse(guid));

                    if (admin != null)
                    {
                        response = new AuthCheckDto()
                        {
                            AccountRole = AccountRole.Admin,
                            IsSingIn = true,
                        };
                    }
                    else if (user != null)
                    {
                        response = new AuthCheckDto()
                        {
                            AccountRole = AccountRole.User,
                            IsSingIn = true
                        };
                    }
                    else
                    {
                        response = new AuthCheckDto()
                        {
                            AccountRole = AccountRole.None,
                            IsSingIn = false
                        };
                    }

                    return new RequestResultDto<AuthCheckDto>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "عملیات شما موفقیت آمیز بود",
                        Body = response
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<AuthCheckDto>()
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
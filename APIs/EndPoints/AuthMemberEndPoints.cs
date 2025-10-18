using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.MemberDTOs;
using berozkala_backend.Entities.AccountsEntities;
using berozkala_backend.Enums;
using berozkala_backend.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class AuthMemberEndPoints
    {
        public static void MapAuthMemberSingUp(this WebApplication app)
        {
            app.MapPost("api/v1/auth/member/singup", async ([FromServices] BerozkalaDb db, [FromBody] MemberSingUpDto dto,
                HttpContext context) =>
            {
                if (string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.PhoneNumber) || string.IsNullOrWhiteSpace(dto.PassWord))
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "یوزر نیم یا پسورد یا شماره تلفن خالی است"
                    };
                }

                var result = await db.Users.AnyAsync(u => u.UserName == dto.UserName || u.PhoneNumber == dto.PhoneNumber);
                if (result)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "این یوزر نیم یا شماره تلفن قبلا استفاده شده است"
                    };
                }

                var user = new UserAccount()
                {
                    UserName = dto.UserName,
                    PhoneNumber = dto.PhoneNumber,
                    PassWord = dto.PassWord,
                    LastIp = context.Connection.RemoteIpAddress?.ToString() ?? "",
                    Status = AccountStatus.InActive,
                    AccountRole = AccountRole.User,
                    DateOfSingup = DateTime.Now
                };

                var token = JwtTools.GenerateJwtToken(user.UserName, user.Guid.ToString(), context.Request.Headers.UserAgent, DateTime.Now.AddMinutes(300));

                user.Status = AccountStatus.Active;

                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();

                return new RequestResultDto<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "شما با موفقیت ثبت نام کردید",
                    Body = token
                };
            });
        }

        public static void MapAuthMemberLoginWithUserName(this WebApplication app)
        {
            app.MapPost("api/v1/auth/member/login-with-username", async ([FromServices] BerozkalaDb db, [FromBody] LoginWithUsernameDto dto,
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

                var user = await db.Users.Where(x => x.Status == AccountStatus.Active)
                    .FirstOrDefaultAsync(x => x.UserName == dto.UserName);

                if (user == null)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "یوزر نیم اشتباه است"
                    };
                }

                if (user.PassWord != dto.PassWord)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "پسورد اشتباه است"
                    };
                }

                var token = JwtTools.GenerateJwtToken(user.UserName, user.Guid.ToString(), context.Request.Headers.UserAgent, DateTime.Now.AddMinutes(300));

                return new RequestResultDto<string>()
                {
                    IsSuccess = true,
                    StatusCode = context.Response.StatusCode,
                    Message = "شما با موفقیت وارد شدید",
                    Body = token
                };
            });
        }

        public static void MapAuthMemberLoginWithCode(this WebApplication app)
        {
            app.MapPost("api/v1/auth/member/login-with-code", async ([FromServices] BerozkalaDb db, [FromBody] MemberLoginWithCodeDto dto,
                HttpContext context) =>
            {
                if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                {
                    return new RequestResultDto<int>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "شماره تلفن خالی است"
                    };
                }

                var user = await db.Users.Where(x => x.Status == AccountStatus.Active)
                    .FirstOrDefaultAsync(x => x.PhoneNumber == dto.PhoneNumber);

                if (user == null)
                {
                    return new RequestResultDto<int>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "کاربری با این شماره تلفن وجود ندارد"
                    };
                }

                if (!user.OptLifeTime.HasValue || DateTime.Now >= user.OptLifeTime.Value)
                {
                    var rnd = new Random();
                    var code = rnd.Next(100000, 999999);

                    Console.WriteLine($"User : {user.UserName} Opt Code : {code}");

                    user.OptCode = code;
                    user.OptLifeTime = DateTime.Now.AddMinutes(2);
                    await db.SaveChangesAsync();

                    return new RequestResultDto<int>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "کد با موفقیت برای شما ارسال شد"
                    };
                }
                else
                {
                    var left = user.OptLifeTime - DateTime.Now;
                    return new RequestResultDto<int>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = $"شما به تازگی درخواست دادید و نمیتوانید دوباره درخواست دهید تا {(int)left.Value.TotalSeconds} ثانیه دیگر"
                    };
                }
            });
        }

        public static void MapAuthMemberLoginSubmitCode(this WebApplication app)
        {
            app.MapPost("api/v1/auth/member/login-submit-code", async ([FromServices] BerozkalaDb db, [FromBody] MemberSubmitCodeDto dto,
                HttpContext context) =>
            {
                if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "شماره تلفن خالی است"
                    };
                }

                var user = await db.Users.Where(x => x.Status == AccountStatus.Active)
                    .FirstOrDefaultAsync(x => x.PhoneNumber == dto.PhoneNumber);

                if (user == null)
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "کاربری با این شماره تلفن وجود ندارد"
                    };
                }

                if (user.OptLifeTime.HasValue && DateTime.Now < user.OptLifeTime.Value)
                {
                    if (user.OptCode == dto.OptCode)
                    {
                        var token = JwtTools.GenerateJwtToken(user.UserName, user.Guid.ToString(), context.Request.Headers.UserAgent, DateTime.Now.AddMinutes(300));

                        return new RequestResultDto<string>()
                        {
                            IsSuccess = true,
                            StatusCode = context.Response.StatusCode,
                            Message = "شما با موفقیت وارد شدید",
                            Body = token
                        };
                    }
                    else
                    {
                        return new RequestResultDto<string>()
                        {
                            IsSuccess = false,
                            StatusCode = context.Response.StatusCode,
                            Message = "کد وارد شده اشتباه است لطفا دوباره تلاش کنید"
                        };
                    }
                }
                else
                {
                    return new RequestResultDto<string>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "زمان تایید کد شما منقضی شده لطفا دوباره درخواست بدهید"
                    };
                }
            });
        }
    }
}
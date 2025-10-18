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
    public static class ProfileEndPoints
    {
        public static void MapAuthUserGetInfo(this WebApplication app)
        {
            app.MapGet("api/v1/auth/member/get-info", async ([FromServices] BerozkalaDb db, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    AccountDto response;

                    var admin = await db.Admins
                        .Where(x => x.Status == AccountStatus.Active)
                        .Include(x => x.Addresses)
                        .FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid));

                    var user = await db.Users
                        .Where(x => x.Status == AccountStatus.Active)
                        .Include(x => x.Addresses)
                        .FirstOrDefaultAsync(u => u.Guid == Guid.Parse(guid));

                    if (admin != null)
                    {
                        response = new AccountDto()
                        {
                            DateOfSingup = admin.DateOfSingup,
                            AccountRole = admin.AccountRole,
                            PhoneNumber = admin.PhoneNumber,
                            FullName = admin.FullName,
                            UserName = admin.UserName,
                            Addresses = admin.Addresses?.Select(x => new AddressDto()
                            {
                                Id = x.Guid,
                                AddressBody = x.AddressBody,
                                PostalCode = x.PostalCode,
                                PhoneNumber = x.PhoneNumber
                            }).ToList(),
                            Email = admin.Email,
                            Status = admin.Status,
                            Gender = admin.Gender,
                            NationalCode = admin.NationalCode,
                            DateOfBirth = admin.DateOfBirth
                        };
                    }
                    else if (user != null)
                    {
                        response = new AccountDto()
                        {
                            DateOfSingup = user.DateOfSingup,
                            AccountRole = user.AccountRole,
                            PhoneNumber = user.PhoneNumber,
                            FullName = user.FullName,
                            UserName = user.UserName,
                            Addresses = user.Addresses?.Select(x => new AddressDto()
                            {
                                Id = x.Guid,
                                AddressBody = x.AddressBody,
                                PostalCode = x.PostalCode,
                                PhoneNumber = x.PhoneNumber
                            }).ToList(),
                            Email = user.Email,
                            Status = user.Status,
                            Gender = user.Gender,
                            NationalCode = user.NationalCode,
                            DateOfBirth = user.DateOfBirth
                        };
                    }
                    else
                    {
                        throw new Exception("کاربر مورد نظر وجود ندارد");
                    }

                    return new RequestResultDto<AccountDto>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "کاربر با موفقیت یافت شد",
                        Body = response
                    };
                }
                catch (Exception ex)
                {
                    return new RequestResultDto<AccountDto>()
                    {
                        IsSuccess = false,
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message
                    };
                }
            }).RequireAuthorization();
        }
        public static void MapUserEditProfile(this WebApplication app)
        {
            app.MapPut("api/v1/user/edit-profile", async ([FromServices] BerozkalaDb db, [FromBody] ProfileEditDto dto, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

                    var admin = await db.Admins
                        .Where(x => x.Status == AccountStatus.Active)
                        .FirstOrDefaultAsync(a => a.Guid == Guid.Parse(guid));

                    var user = await db.Users
                        .Where(x => x.Status == AccountStatus.Active)
                        .FirstOrDefaultAsync(u => u.Guid == Guid.Parse(guid));

                    if (admin != null)
                    {
                        admin.FullName = !string.IsNullOrWhiteSpace(dto.FullName) ? dto.FullName : admin.FullName;
                        admin.Email = !string.IsNullOrWhiteSpace(dto.Email) ? dto.Email : admin.Email;
                        admin.PhoneNumber = !string.IsNullOrWhiteSpace(dto.PhoneNumber) ? dto.PhoneNumber : admin.PhoneNumber;
                        admin.UserName = !string.IsNullOrWhiteSpace(dto.UserName) ? dto.UserName : admin.UserName;
                        admin.Gender = dto.Gender;
                        admin.NationalCode = !string.IsNullOrWhiteSpace(dto.NationalCode) ? dto.NationalCode : admin.NationalCode;
                        admin.DateOfBirth = dto.DateOfBirth != null ? dto.DateOfBirth : admin.DateOfBirth;

                        await db.SaveChangesAsync();
                    }
                    else if (user != null)
                    {
                        user.FullName = !string.IsNullOrWhiteSpace(dto.FullName) ? dto.FullName : user.FullName;
                        user.Email = !string.IsNullOrWhiteSpace(dto.Email) ? dto.Email : user.Email;
                        user.PhoneNumber = !string.IsNullOrWhiteSpace(dto.PhoneNumber) ? dto.PhoneNumber : user.PhoneNumber;
                        user.UserName = !string.IsNullOrWhiteSpace(dto.UserName) ? dto.UserName : user.UserName;
                        user.Gender = dto.Gender;
                        user.NationalCode = !string.IsNullOrWhiteSpace(dto.NationalCode) ? dto.NationalCode : user.NationalCode;
                        user.DateOfBirth = dto.DateOfBirth != null ? dto.DateOfBirth : user.DateOfBirth;

                        await db.SaveChangesAsync();
                    }

                    else
                    {
                        throw new Exception("کاربر مورد نظر وجود ندارد");
                    }

                    return new RequestResultDto<string>()
                    {
                        IsSuccess = true,
                        StatusCode = context.Response.StatusCode,
                        Message = "شما با موفیقت پروفایل خود را ویرایش کردید",
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.DbContextes;
using berozkala_backend.DTOs.CommonDTOs;
using berozkala_backend.DTOs.MemberDTOs;
using berozkala_backend.Entities.OtherEntities;
using berozkala_backend.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.APIs.EndPoints
{
    public static class AddressEndPoints
    {
        public static void MapAddressEditList(this WebApplication app)
        {
            app.MapPut("api/v1/addresses/edit", async ([FromServices] BerozkalaDb db, List<AddressDto> dto, HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

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
                        var addresses = admin.Addresses?
                            .Where(x => dto.Select(d => d.Id).Contains(x.Guid)) ?? [];

                        if (!addresses.Any())
                            throw new Exception("گارانتی های مورد نظر وجود ندارند");

                        dto.ForEach(d =>
                        {
                            var address = addresses.FirstOrDefault(g => g.Guid == d.Id);
                            if (address != null)
                            {
                                address.AddressBody = d.AddressBody;
                                address.PhoneNumber = d.PhoneNumber;
                                address.PostalCode = d.PostalCode;
                            }
                        });

                        await db.SaveChangesAsync();
                    }
                    else if (user != null)
                    {
                        var addresses = user.Addresses?
                            .Where(x => dto.Select(d => d.Id).Contains(x.Guid)) ?? [];

                        if (!addresses.Any())
                            throw new Exception("آدرس های مورد نظر وجود ندارند");

                        dto.ForEach(d =>
                        {
                            var address = addresses.FirstOrDefault(g => g.Guid == d.Id);
                            if (address != null)
                            {
                                address.AddressBody = d.AddressBody;
                                address.PhoneNumber = d.PhoneNumber;
                                address.PostalCode = d.PostalCode;
                            }
                        });

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
                        Message = "آدرس های شما با موفقیت ویراش شد"
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

        public static void MapAddressAddList(this WebApplication app)
        {
            app.MapPost("api/v1/addresses/add", async ([FromServices] BerozkalaDb db, List<AddressDto> dto, HttpContext context) =>
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
                        var addresses = dto.Select(x => new Address()
                        {
                            AddressBody = x.AddressBody,
                            PostalCode = x.PostalCode,
                            PhoneNumber = x.PhoneNumber,
                            Admin = admin
                        });

                        await db.Addresses.AddRangeAsync(addresses);
                        await db.SaveChangesAsync();
                    }
                    else if (user != null)
                    {
                        var addresses = dto.Select(x => new Address()
                        {
                            AddressBody = x.AddressBody,
                            PostalCode = x.PostalCode,
                            PhoneNumber = x.PhoneNumber,
                            User = user
                        });

                        await db.Addresses.AddRangeAsync(addresses);
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
                        Message = "آدرس های شما با موفقیت اظافه شد"
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

        public static void MapAddressDelete(this WebApplication app)
        {
            app.MapDelete("api/v1/address/delete/{id:guid}", async ([FromServices] BerozkalaDb db, [FromRoute] Guid id,
                HttpContext context) =>
            {
                try
                {
                    var guid = context.User.Claims.FirstOrDefault(x => x.Type == "guid")?.Value ?? "";

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
                        var address = admin.Addresses?.FirstOrDefault(x => x.Guid == id)
                            ?? throw new Exception("آدرس مورد نظر یافت نشد");

                        db.Addresses.Remove(address);
                        await db.SaveChangesAsync();
                    }
                    else if (user != null)
                    {
                        var address = user.Addresses?.FirstOrDefault(x => x.Guid == id)
                            ?? throw new Exception("آدرس مورد نظر یافت نشد");

                        db.Addresses.Remove(address);
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
                        Message = "آدرس شما با موفقیت حذف شد"
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
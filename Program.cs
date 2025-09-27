using System.Text;
using berozkala_backend.APIs.EndPoints;
using berozkala_backend.DbContextes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<BerozkalaDb>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ClockSkew = TimeSpan.Zero,
      ValidIssuer = "BerozKala.ir",
      ValidAudience = "BerozKala.ir",
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yWCyWsAJwshIf5ZXTw1HTdV9SCyXojkz2L9mvQUqbQy6fzu4WgtDvY15rHcq6EqXxzVFa5qeTs3SVx09VLAWiGaKcxBdBelIIbQKZl4ZIhwkkxmMrUxcIfV32r7dITOqPxfbO14eDFGO7TbdfR8CWjwpsMWqd9SMBXJ46U3DkbPql110LqZZU983IjyxjBhJBY9QAmls2A2WxedziuJlUl5dytRTQxZsGxl1skexi5g22lYOodbJCSi0J383lVK"))
    };
  });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors(policy =>
{
  policy.AllowAnyHeader();
  policy.AllowAnyMethod();
  policy.AllowAnyOrigin();
});

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<BerozkalaDb>();
  await db.Database.MigrateAsync();
}

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapProductList();
app.MapProductCreate();
app.MapProductGet();
app.MapProductDelete();
app.MapProductEdit();

app.MapProductPreviewGet();
app.MapProductPreviewList();

app.MapAuthMemberSingUp();
app.MapAuthMemberLoginWithUserName();
app.MapAuthMemberLoginWithCode();
app.MapAuthMemberLoginSubmitCode();

app.MapAuthAdminLogin();

app.Run();
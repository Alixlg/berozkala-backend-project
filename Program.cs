using System.Text;
using berozkala_backend.APIs.EndPoints;
using berozkala_backend.DbContextes;
using berozkala_backend.Entities.ProductEntities;
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

await using var scope = app.Services.CreateAsyncScope();
var db = scope.ServiceProvider.GetRequiredService<BerozkalaDb>();
await db.Database.MigrateAsync();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

#region Product Apis
app.MapProductList();
app.MapProductCreate();
app.MapProductEdit();
app.MapProductPreviewList();
app.MapGenericDeleteList<Product>("api/v1/products/delete");
#endregion


#region Product Category Apis
app.MapAddCategorysToProduct();
app.MapRemoveCategorysProduct();
#endregion


#region Product Image Apis
app.MapAddImagesProduct();
app.MapDeleteImagesProduct();
app.MapEditImagesProduct();
#endregion


#region Product Garranty Apis
app.MapDeleteGarrantysProduct();
app.MapAddGarrantysProduct();
app.MapEditGarrantysProduct();
#endregion


#region Product Attribute Apis
app.MapAddAttributesProduct();
app.MapDeleteAttributesProduct();
app.MapEditAttributesProduct();
#endregion


#region Product SubsetAttribute Apis
app.MapAddSubsetAttributesProduct();
app.MapEditSubsetAttributesProduct();
app.MapDeleteSubsetAttributesProduct();
#endregion


#region Auth Apis
app.MapAuthMemberSingUp();
app.MapAuthMemberLoginWithUserName();
app.MapAuthMemberLoginWithCode();
app.MapAuthMemberLoginSubmitCode();
app.MapAuthAdminLogin();
app.MapAuthValidToken();
#endregion


#region Category Apis
app.MapCategoryCreate();
app.MapGenericDeleteList<Category>("api/v1/categorys/delete");
app.MapCategoryEdit();
app.MapCategoryList();
#endregion


#region Sub Category Apis
app.MapSubCategoryCreate();
app.MapSubCategoryEdit();
app.MapSubCategoryGet();
app.MapGenericDeleteList<SubCategory>("api/v1/subcategorys/delete");
#endregion


#region Profile Apis
app.MapUserEditProfile();
app.MapUserChangePassword();
app.MapUserGetInfo();
#endregion


#region Address Apis
app.MapAddressEditList();
app.MapAddressAddList();
app.MapAddressDelete();
#endregion


#region Basket Apis
app.MapAddProductToBasket();
app.MapRemoveProductFromBasket();
app.MapClearProductFromBasket();
app.MapGetBasketProducts();
#endregion


#region ShipingMethod Apis
app.MapShippingMethodCreate();
app.MapShippingMethodDelete();
app.MapShippingMethodEdit();
app.MapShippingMethodList();
app.MapShippingMethodGet();
#endregion

#region PaymentMethod Apis
app.MapPaymentMethodCreate();
app.MapPaymentMethodEdit();
app.MapPaymentMethodDelete();
app.MapPaymentMethodList();
app.MapPaymentMethodGet();
#endregion

#region Order Apis
app.MapCreateOrder();
#endregion

app.Run();
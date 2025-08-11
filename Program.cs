using berozkala_backend.APIs.EndPoints;
using berozkala_backend.DbContextes;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<BerozkalaDb>();

var app = builder.Build();

app.UseCors(policy =>
{
  policy.AllowAnyHeader();
  policy.AllowAnyMethod();
  policy.AllowAnyOrigin();
});

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapProductList();
app.MapProductCreate();
app.MapProductGet();


// app.MapPut("api/v1/products/delete{id}", ([FromRoute] string id, [FromServices] BerozkalaDb db) =>
// {
//   var p = db.Products.FirstOrDefault(p => p.GuId == id);

//   if (p == null || p.IsInvisible)
//   {
//     return new RequestResultDTO()
//     {
//       IsSuccess = false,
//       Message = "محصول مورد نظر یافت نشد !"
//     };
//   }

//   p.IsInvisible = true;
//   db.SaveChanges();

//   return new RequestResultDTO()
//   {
//     IsSuccess = true,
//     Message = "محصول مورد نظر با موفقیت حذف شد !"
//   };
// });

// app.MapPut("api/v1/products/edit{id}", ([FromRoute] string id, [FromBody] ProductDTO newProduct, [FromServices] BerozkalaDb db) =>
// {
//   var p = db.Products.FirstOrDefault(p => p.GuId == id);

//   if (p == null)
//   {
//     return new RequestResultDTO()
//     {
//       IsSuccess = false,
//       Message = "محصول مورد نظر یافت نشد !"
//     };
//   }

//   p.IsAvailable = newProduct.IsAvailable;
//   p.Brand = newProduct.Brand;
//   p.Title = newProduct.Title;
//   p.Price = newProduct.Price;
//   p.MaxCount = newProduct.MaxCount;
//   p.ScoreRank = newProduct.ScoreRank;
//   p.DiscountPercent = newProduct.DiscountPercent;
//   p.PreviewImageUrl = newProduct.PreviewImageUrl;
//   p.ImagesUrl = newProduct.ImagesUrl;
//   p.Category = newProduct.Category;
//   p.Description = newProduct.Description;
//   p.Review = newProduct.Review;
//   db.SaveChanges();

//   return new RequestResultDTO()
//   {
//     IsSuccess = true,
//     Message = "محصول با موفقیت ویرایش شد !"
//   };
// });

app.Run();
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
app.MapProductDelete();
app.MapProductEdit();

app.MapProductPreviewGet();
app.MapProductPreviewList();

app.Run();
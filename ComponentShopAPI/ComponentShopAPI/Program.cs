using ComponentShopAPI.Repositories;
using ComponentShopAPI.Services.GpuSearch;
using ComponentShopAPI.Services.Image;
using ComponentShopAPI.Services.MonitorSearch;
using ComponentShopAPI.Services.Pagination;
using ComponentShopAPI.Services.ProductDtoFactory;
using ComponentShopAPI.Services.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<ComponentShopDBContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ComponentShopDBContext>();

builder.Services.AddScoped<IMonitorService, MonitorService>();
builder.Services.AddScoped<IGpuService, GpuService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IPaginationService, PaginationService>();
builder.Services.AddScoped<IProductDtoFactory, ProductDtoFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseCors(options =>
options.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();

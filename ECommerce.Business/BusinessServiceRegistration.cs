using ECommerce.Business.Interfaces;
using ECommerce.Business.Mappings.AutoMapper;
using ECommerce.Business.Services;
using ECommerce.Core.Interfaces;
using ECommerce.DataAccess.Context;
using ECommerce.DataAccess.Repositories.Concrete;
using ECommerce.Entities.Entities.Concrete;
using ECommerce.Models.ViewModels.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Business;

public class BusinessServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.Configure<JwtSettings>(configuration.GetSection("JWT"));


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGenericRepo<Category>, GenericRepo<Category>>();
        services.AddScoped<IGenericRepo<Product>, GenericRepo<Product>>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        // AutoMapper konfigürasyonu
        services.AddAutoMapper(typeof(ProductProfile), typeof(CategoryProfile));

        return services;
    }
}
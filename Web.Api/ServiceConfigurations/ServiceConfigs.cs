using Application.IServices;
using Application.Services;
using Domain.IRepositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Web.Api.Middleware;

namespace Web.Api.ServiceConfigurations;

public static class ServiceConfigs
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService,StorageService>();
        services.AddScoped<ICategoryService,CategoryService>();
        services.AddScoped<IImageService,ImageService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();

        services.AddScoped<CustomExceptionHandler>();
        
        return services;
    }
}
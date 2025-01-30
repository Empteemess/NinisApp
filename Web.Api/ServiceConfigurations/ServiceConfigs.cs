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
        services.AddScoped<CustomExceptionHandler>();
        
        
        services.AddScoped<IStorageService,StorageService>();
        services.AddScoped<IImageService,ImageService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    
        services.AddScoped<IImageRepository, ImageRepository>();
        
        return services;
    }
}
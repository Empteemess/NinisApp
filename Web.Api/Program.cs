using Application;
using Application.IServices;
using Application.Services;
using DotNetEnv;
using Web.Api.Middleware;
using Web.Api.ServiceConfigurations;

namespace Web.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddDbConfigs(builder.Configuration);
        builder.Services.AddServices();

        builder.Services.AddCors();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors();
        
        app.UseHttpsRedirection();

        app.UseMiddleware<CustomExceptionHandler>();
        
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
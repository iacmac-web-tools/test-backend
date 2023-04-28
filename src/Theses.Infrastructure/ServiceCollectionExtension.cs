using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Theses.Application.Common.Interfaces;
using Theses.Infrastructure.Persistence;

namespace Theses.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetSection("UseInMemoryDatabase").Get<bool>())
        {
            services.AddDbContext<IApplicationContext, ApplicationContext>(builder => builder.UseInMemoryDatabase("Theses"));
        }
        else
        {
            services.AddDbContext<IApplicationContext, ApplicationContext>(builder =>
                builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        return services;
    }
}

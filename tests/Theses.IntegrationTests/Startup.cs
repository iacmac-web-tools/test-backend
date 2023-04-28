using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theses.Application;
using Theses.Infrastructure;

namespace Theses.IntegrationTests;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureHostConfiguration(builder => builder.AddJsonFile("appsettings.json", false));

        hostBuilder.ConfigureServices((context, services) => {
            services.AddApplicationServices();
            services.AddInfrastructureServices(context.Configuration);
        });
    }
}

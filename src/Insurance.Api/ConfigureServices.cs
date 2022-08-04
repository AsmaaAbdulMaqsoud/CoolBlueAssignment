using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers();
        serviceCollection.AddSwaggerGen();

        return serviceCollection;
    }
}

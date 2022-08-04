using Insurance.Application.Common.Interfaces;
using Insurance.Application.Common.Interfaces.Repositories;
using Insurance.Application.Common.Interfaces.Services;
using Insurance.Infrastructure.Persistence;
using Insurance.Infrastructure.Repositories;
using Insurance.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
           options.UseInMemoryDatabase("CoolBlueDb"));
        serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        serviceCollection.AddScoped<ApplicationDbContextInitialiser>();
        serviceCollection.AddHttpClient<IHttpRequestService, HttpRequestService>(x => x.BaseAddress = new Uri(configuration.GetSection("HttpEndpoint").Value));
        serviceCollection.AddTransient<IProductInsuranceService, ProductInsuranceService>();
        serviceCollection.AddTransient<IInsuranceCalculationService, InsuranceCalculationService>();
        serviceCollection.AddTransient<IOrderInsuranceService, OrderInsuranceService>();
        serviceCollection.AddTransient<IProductRepository, ProductRepository>();
        serviceCollection.AddTransient<IProducTypeRepository, ProducTypeRepository>();
        serviceCollection.AddTransient<IProductTypeService, ProductTypeService>();
        return serviceCollection;
    }
}

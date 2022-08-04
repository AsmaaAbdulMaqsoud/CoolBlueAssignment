using Insurance.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Insurance.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IHttpRequestService _httpRequestService;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        IHttpRequestService httpRequestService)
    {
        _logger = logger;
        _context = context;
        _httpRequestService = httpRequestService;
    }
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {

        if (!_context.ProductList.Any())
        {
            var products = await _httpRequestService.GetProducts();
            if (products != null)
            {
                _context.ProductList.AddRange(products);
            }
            await _context.SaveChangesAsync();
        }
        if (!_context.ProductTypeList.Any())
        {
            var productTypes = await _httpRequestService.GetProductsTypes();
            if (productTypes != null)
            {
                _context.ProductTypeList.AddRange(productTypes);
            }
            await _context.SaveChangesAsync();
        }
    }
}

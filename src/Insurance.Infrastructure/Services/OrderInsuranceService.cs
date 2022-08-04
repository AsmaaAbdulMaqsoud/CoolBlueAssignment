using Insurance.Application.Common.Interfaces.Services;
using Insurance.Application.DTO;
using Microsoft.Extensions.Logging;

namespace Insurance.Infrastructure.Services;

public class OrderInsuranceService : IOrderInsuranceService
{
    private readonly IProductInsuranceService _productInsuranceService;
    private readonly ILogger<OrderInsuranceService> _logger;

    public OrderInsuranceService(IProductInsuranceService productInsuranceService, ILogger<OrderInsuranceService> logger)
    {
        _productInsuranceService = productInsuranceService;
        _logger = logger;
    }

    public async Task<decimal> GetOrderInsurance(OrderDto order)
    {
        var productsInsurance = await _productInsuranceService.GetProductsInsurance(order.Products);

        if (productsInsurance.HasAdditionalInsurance)
            productsInsurance.TotalProductsInsurance += 500;

        var productsJson = Newtonsoft.Json.JsonConvert.SerializeObject(order.Products);
        _logger.LogInformation($"Total insurance for products: {productsJson} is: {productsInsurance.TotalProductsInsurance}");

        return productsInsurance.TotalProductsInsurance;
    }

}

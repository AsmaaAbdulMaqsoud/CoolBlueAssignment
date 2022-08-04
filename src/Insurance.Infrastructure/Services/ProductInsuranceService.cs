using Insurance.Application.Common.Interfaces.Repositories;
using Insurance.Application.Common.Interfaces.Services;
using Insurance.Application.DTO;
using Insurance.Application.Models;
using Microsoft.Extensions.Logging;

namespace Insurance.Infrastructure.Services;

public class ProductInsuranceService : IProductInsuranceService
{
    private readonly IHttpRequestService _httpService;
    private readonly IInsuranceCalculationService _insuranceCalculationService;
    private readonly ILogger<ProductInsuranceService> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IProducTypeRepository _productTypeRepository;

    public ProductInsuranceService(IHttpRequestService httpService
        , IInsuranceCalculationService insuranceCalculationService, ILogger<ProductInsuranceService> logger
        , IProductRepository productRepository, IProducTypeRepository productTypeRepository)
    {
        _httpService = httpService;
        _insuranceCalculationService = insuranceCalculationService;
        _logger = logger;
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
    }

    public async Task<decimal> GetProductInsurance(int productId)
    {
        decimal insuranceValue = 0;

        _logger.LogInformation($"Getting product by product id: {productId}");

        var product = _productRepository.GetProductById(productId);
        if (product == null)
            product = await _httpService.GetProduct(productId);

        if (product == null)
        {
            _logger.LogInformation($"The product with id: {productId} is null");
            return insuranceValue;
        }

        _logger.LogInformation($"Getting product type by id: {product.ProductTypeId}");

        var productType = _productTypeRepository.GetProductTypeById(product.ProductTypeId);
        if (productType == null)
            productType = await _httpService.GetProductType(product.ProductTypeId);

        if (productType == null)
        {
            _logger.LogInformation($"The product type with id: {product.ProductTypeId} is null");
            return insuranceValue;
        }
        if (!productType.CanBeInsured)
        {
            _logger.LogInformation($"The product type : {productType.Name} can't be insured!");
            return insuranceValue;
        }
        insuranceValue = _insuranceCalculationService.GetInsuranceValue(product.SalesPrice);
        insuranceValue += productType.SurchargeRate;
        insuranceValue = _insuranceCalculationService.GetSpecialProductsInsurance(productType.Name, insuranceValue);

        _logger.LogInformation("The insurance value for product id: " + productId + " is: " + insuranceValue);
        return insuranceValue;
    }

    public async Task<OrderInsurance> GetProductsInsurance(List<ProductDto>? products)
    {
        var insurance = new OrderInsurance();
        if (products != null)
        {
            foreach (var product in products)
            {
                var productData = _productRepository.GetProductById(product.ProductId);
                if (productData == null)
                    productData = await _httpService.GetProduct(product.ProductId);

                if (productData != null)
                {
                    var productType = _productTypeRepository.GetProductTypeById(productData.ProductTypeId);
                    if (productType == null)
                         productType = await _httpService.GetProductType(productData.ProductTypeId);

                    if (productType != null && productType.CanBeInsured)
                    {
                        if (productType.Name == "Digital cameras")
                            insurance.HasAdditionalInsurance = true;

                        var productInsurance = _insuranceCalculationService.GetInsuranceValue(productData.SalesPrice);
                        productInsurance += productType.SurchargeRate;
                        productInsurance = _insuranceCalculationService.GetSpecialProductsInsurance(productType.Name, productInsurance);

                        insurance.TotalProductsInsurance += (productInsurance * product.Quantity);
                    }
                }
            }
        }


        return insurance;
    }


}

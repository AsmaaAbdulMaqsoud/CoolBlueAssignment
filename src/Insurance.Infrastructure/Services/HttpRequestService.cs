using Insurance.Application.Common.Interfaces.Services;
using Insurance.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Insurance.Infrastructure.Services;

public class HttpRequestService : IHttpRequestService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpRequestService> _logger;
    public HttpRequestService(HttpClient httpClient, ILogger<HttpRequestService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Product?> GetProduct(int productId)
    {
        var productData = await _httpClient.GetAsync($"/products/{productId}");
        string productJson = await productData.Content.ReadAsStringAsync();

        _logger.LogInformation($"The product object for product id {productId} is:{productJson}");

        var product = JsonConvert.DeserializeObject<Product>(productJson);
        return product;
    }

    public async Task<List<Product>?> GetProducts()
    {
        var productsData = await _httpClient.GetAsync("/products");
        string productsJson = await productsData.Content.ReadAsStringAsync();

        _logger.LogInformation($"All products object is: {productsJson}");

        var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);
        return products;
    }

    public async Task<List<ProductType>?> GetProductsTypes()
    {
        var productTypesData = await _httpClient.GetAsync("/product_types");
        string productTypesJson = await productTypesData.Content.ReadAsStringAsync();

        _logger.LogInformation($"The product types json object is: {productTypesJson}");

        var productTypes = JsonConvert.DeserializeObject<List<ProductType>>(productTypesJson);
        return productTypes;
    }

    public async Task<ProductType?> GetProductType(int productTypeId)
    {
        var productTypeData = await _httpClient.GetAsync($"/product_types/{productTypeId}");
        string productTypeJson = await productTypeData.Content.ReadAsStringAsync();

        _logger.LogInformation($"The product type object for product type id :{productTypeId} is: {productTypeJson}");

        var productType = JsonConvert.DeserializeObject<ProductType>(productTypeJson);
        return productType;
    }
}

using Insurance.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Contrib.HttpClient;
using Newtonsoft.Json;

namespace Insurance.Test.Services;

[TestClass]
public class HttpRequestServiceTests
{
    private Mock<HttpMessageHandler> _handlerMock = new Mock<HttpMessageHandler>();
    [TestInitialize]
    public void Setup()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
    }
    [TestMethod]
    public async Task WhenProductIdIsValid_GetProduct_ShoudFetchProductData()
    {
        var product = new Domain.Entities.Product
        {
            Id = 725435,
            Name = "Cowon Plenue D Gold",
            SalesPrice = 299.99M,
            ProductTypeId = 12
        };
        var httpClient = _handlerMock.CreateClient();
        httpClient.BaseAddress = new Uri("http://localhost:5002");
        _handlerMock.SetupRequest(HttpMethod.Get, "http://localhost:5002/products/725435")
.ReturnsResponse(JsonConvert.SerializeObject(product), "application/json");

        var subject = new HttpRequestService(httpClient, new Mock<ILogger<HttpRequestService>>().Object);
        var result = await subject.GetProduct(725435);

        Assert.AreEqual(result.ProductTypeId, product.ProductTypeId);
        Assert.AreEqual(result.Id, product.Id);
        Assert.AreEqual(result.Name, product.Name);
        Assert.AreEqual(result.SalesPrice, product.SalesPrice);
    }

    [TestMethod]
    public async Task GetProductType_GetAsync_shouldGetTheData()
    {
        var productType = new Domain.Entities.ProductType
        {
            Id = 572,
            Name = "Samsung WW80J6400CW EcoBubble",
            CanBeInsured = true
        };

        var httpClient = _handlerMock.CreateClient();

        httpClient.BaseAddress = new Uri("http://localhost:5002");
        _handlerMock.SetupRequest(HttpMethod.Get, "http://localhost:5002/product_types/572")
.ReturnsResponse(JsonConvert.SerializeObject(productType), "application/json");

        var subject = new HttpRequestService(httpClient, new Mock<ILogger<HttpRequestService>>().Object);
        var result = await subject.GetProductType(572);

        Assert.AreEqual(result.Id, productType.Id);
        Assert.AreEqual(result.Name, productType.Name);
        Assert.AreEqual(result.CanBeInsured, productType.CanBeInsured);
    }

    [TestMethod]
    public async Task GetProducts_GetAsync_shouldGetTheData()
    {
        //Arrange
        var products = new List<Domain.Entities.Product>
           {
               new Domain.Entities.Product
               {
               Id = 572770,
               Name = "Samsung WW80J6400CW EcoBubble",
               SalesPrice = 475,
               ProductTypeId = 124
               },
               new Domain.Entities.Product
               {
               Id = 572775,
               Name = "Mobile",
               SalesPrice = 250,
               ProductTypeId = 125
               },

           };
        var httpclient = _handlerMock.CreateClient();
        httpclient.BaseAddress = new Uri("http://localhost:5002");
        _handlerMock.SetupRequest(HttpMethod.Get, "http://localhost:5002/products")
 .ReturnsResponse(JsonConvert.SerializeObject(products), "application/json");

        var subject = new HttpRequestService(httpclient, new Mock<ILogger<HttpRequestService>>().Object);
        var result = await subject.GetProducts();

        Assert.AreEqual(result.Count, products.Count);
        Assert.AreEqual(result[0].ProductTypeId, products[0].ProductTypeId);
        Assert.AreEqual(result[0].Id, products[0].Id);
        Assert.AreEqual(result[0].Name, products[0].Name);
        Assert.AreEqual(result[0].SalesPrice, products[0].SalesPrice);
        Assert.AreEqual(result[1].ProductTypeId, products[1].ProductTypeId);
        Assert.AreEqual(result[1].Id, products[1].Id);
        Assert.AreEqual(result[1].Name, products[1].Name);
        Assert.AreEqual(result[1].SalesPrice, products[1].SalesPrice);
    }

    [TestMethod]
    public async Task GetProductTypes_GetAsync_shouldGetTheData()
    {
        var productTypes = new List<Domain.Entities.ProductType>
           {
               new Domain.Entities.ProductType
           {
               Id = 572,
               Name = "Samsung WW80J6400CW EcoBubble",
               CanBeInsured = true
           },
                new Domain.Entities.ProductType
           {
               Id = 570,
               Name = "Samsung ",
               CanBeInsured = true
           },
       };
        var httpclient = _handlerMock.CreateClient();
        httpclient.BaseAddress = new Uri("http://localhost:5002");
        _handlerMock.SetupRequest(HttpMethod.Get, "http://localhost:5002/product_types")
     .ReturnsResponse(JsonConvert.SerializeObject(productTypes), "application/json");

        var subject = new HttpRequestService(httpclient, new Mock<ILogger<HttpRequestService>>().Object);
        var result = await subject.GetProductsTypes();

        Assert.AreEqual(result.Count, productTypes.Count);
        Assert.AreEqual(result[0].Id, productTypes[0].Id);
        Assert.AreEqual(result[0].Name, productTypes[0].Name);
        Assert.AreEqual(result[0].CanBeInsured, productTypes[0].CanBeInsured);
        Assert.AreEqual(result[1].Id, productTypes[1].Id);
        Assert.AreEqual(result[1].Name, productTypes[1].Name);
        Assert.AreEqual(result[1].CanBeInsured, productTypes[1].CanBeInsured);
    }
}
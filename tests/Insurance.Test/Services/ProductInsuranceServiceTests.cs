using Insurance.Application.Common.Interfaces.Repositories;
using Insurance.Application.Common.Interfaces.Services;
using Insurance.Application.DTO;
using Insurance.Application.Models;
using Insurance.Domain.Entities;
using Insurance.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Insurance.Test.Services;

[TestClass]
public class ProductInsuranceServiceTests
{
    [TestMethod]
    [DataRow(420, 0)]
    [DataRow(500, 1000)]
    [DataRow(600, 1000)]
    [DataRow(1000, 1000)]
    [DataRow(2000, 2000)]
    public async Task WhensalesPrice_Exist_CalculateInsurance(float salesPrice, float insurance)
    {
        var product = new Product
        {
            Id = 725435,
            Name = "Cowon Plenue D Gold",
            SalesPrice = (decimal)salesPrice,
            ProductTypeId = 12
        };
        var productType = new ProductType
        {
            Id = 12,
            Name = "Cowon",
            CanBeInsured = true
        };
        var httpRequestServiceMock = new Mock<IHttpRequestService>();
        var insuranceCalculationServiceMock = new Mock<IInsuranceCalculationService>();
        var loggerMock = new Mock<ILogger<ProductInsuranceService>>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var productTypeRepositoryMock = new Mock<IProducTypeRepository>();
        httpRequestServiceMock.Setup(x => x.GetProduct(product.Id)).ReturnsAsync(product);
        httpRequestServiceMock.Setup(x => x.GetProductType(productType.Id)).ReturnsAsync(productType);
        insuranceCalculationServiceMock.Setup(x => x.GetInsuranceValue((decimal)salesPrice)).Returns((decimal)insurance);
        insuranceCalculationServiceMock.Setup(x => x.GetSpecialProductsInsurance(productType.Name, (decimal)insurance)).Returns((decimal)insurance);
        productRepositoryMock.Setup(x => x.GetProductById(product.Id)).Returns(product);
        productTypeRepositoryMock.Setup(x => x.GetProductTypeById(productType.Id)).Returns(productType);

        //Arrange
        var productService = new ProductInsuranceService(httpRequestServiceMock.Object, insuranceCalculationServiceMock.Object
            , loggerMock.Object, productRepositoryMock.Object, productTypeRepositoryMock.Object);
        //Act
        var result = await productService.GetProductInsurance(product.Id);
        //Assert
        Assert.AreEqual(result, (decimal)insurance);
    }


    [TestMethod]
    [DataRow("Digital cameras", true)]
    [DataRow("Cowon", false)]
    public async Task WhenOrderDto_Exist_CalculateInsuranceForList(string productTypeName, bool hasAdditionalInsurance)
    {
        var product = new Product
        {
            Id = 725435,
            Name = "Cowon Plenue D Gold",
            SalesPrice = 1500,
            ProductTypeId = 12
        };
        var productType = new ProductType
        {
            Id = 12,
            Name = productTypeName,
            CanBeInsured = true
        };
        var productsDto = new List<ProductDto> { new ProductDto { ProductId = 725435, Quantity = 1 } };
        var orderInsuranceModel = new OrderInsurance { HasAdditionalInsurance = hasAdditionalInsurance, TotalProductsInsurance = 1000 };
        var httpRequestServiceMock = new Mock<IHttpRequestService>();
        var insuranceCalculationServiceMock = new Mock<IInsuranceCalculationService>();
        var loggerMock = new Mock<ILogger<ProductInsuranceService>>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var productTypeRepositoryMock = new Mock<IProducTypeRepository>();
        httpRequestServiceMock.Setup(x => x.GetProduct(product.Id)).ReturnsAsync(product);
        httpRequestServiceMock.Setup(x => x.GetProductType(productType.Id)).ReturnsAsync(productType);
        insuranceCalculationServiceMock.Setup(x => x.GetInsuranceValue(product.SalesPrice)).Returns(orderInsuranceModel.TotalProductsInsurance);
        insuranceCalculationServiceMock.Setup(x => x.GetSpecialProductsInsurance(productType.Name, orderInsuranceModel.TotalProductsInsurance)).Returns(orderInsuranceModel.TotalProductsInsurance);
        productRepositoryMock.Setup(x => x.GetProductById(product.Id)).Returns(product);
        productTypeRepositoryMock.Setup(x => x.GetProductTypeById(productType.Id)).Returns(productType);

        //Arrange
        var productService = new ProductInsuranceService(httpRequestServiceMock.Object, insuranceCalculationServiceMock.Object
            , loggerMock.Object, productRepositoryMock.Object, productTypeRepositoryMock.Object);
        //Act
        var result = await productService.GetProductsInsurance(productsDto);
        //Assert
        Assert.AreEqual(result.TotalProductsInsurance, orderInsuranceModel.TotalProductsInsurance);
        Assert.AreEqual(result.HasAdditionalInsurance, orderInsuranceModel.HasAdditionalInsurance);
    }
}

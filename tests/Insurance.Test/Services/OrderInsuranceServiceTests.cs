using Insurance.Application.Common.Interfaces.Services;
using Insurance.Application.DTO;
using Insurance.Application.Models;
using Insurance.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Insurance.Test.Services;

[TestClass]
public class OrderInsuranceServiceTests
{
    [TestMethod]
    public async Task WhenOrderProductsDto_Exist_CalculateInsurance()
    {
        //Arrange
        var productsDto = new List<ProductDto> { new ProductDto { ProductId = 1, Quantity = 1 } };
        decimal insurance = 1000;
        var orderInsuranceModel = new OrderInsurance { HasAdditionalInsurance = false, TotalProductsInsurance = insurance };
        var ProductInsuranceMock = new Mock<IProductInsuranceService>();
        var loggerMock = new Mock<ILogger<OrderInsuranceService>>();
        ProductInsuranceMock.Setup(x => x.GetProductsInsurance(productsDto)).ReturnsAsync(orderInsuranceModel);
        var orderInsuranceService = new OrderInsuranceService(ProductInsuranceMock.Object, loggerMock.Object);
        //Act
        var result = await orderInsuranceService.GetOrderInsurance(new OrderDto { Products = productsDto });
        //Assert
        Assert.AreEqual(result, insurance);
    }
    [TestMethod]
    public async Task WhenOrde_HasAdditionalInsurance_ShouldHaveMore500Insurance()
    {
        //Arrange
        var productsDto = new List<ProductDto> { new ProductDto { ProductId = 1, Quantity = 1 } };
        decimal insurance = 1500;
        var orderInsuranceModel = new OrderInsurance { HasAdditionalInsurance = true, TotalProductsInsurance = 1000 };
        var ProductInsuranceMock = new Mock<IProductInsuranceService>();
        var loggerMock = new Mock<ILogger<OrderInsuranceService>>();
        ProductInsuranceMock.Setup(x => x.GetProductsInsurance(productsDto)).ReturnsAsync(orderInsuranceModel);
        var orderInsuranceService = new OrderInsuranceService(ProductInsuranceMock.Object, loggerMock.Object);
        //Act
        var result = await orderInsuranceService.GetOrderInsurance(new OrderDto { Products = productsDto });
        //Assert
        Assert.AreEqual(result, insurance);
    }
}

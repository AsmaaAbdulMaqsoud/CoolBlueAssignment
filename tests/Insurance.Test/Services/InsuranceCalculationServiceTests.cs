using Insurance.Infrastructure.Services;

namespace Insurance.Test.Services;

[TestClass]
public class InsuranceCalculationServiceTests
{
    [TestMethod]
    [DataRow(420, 0)]
    [DataRow(500, 1000)]
    [DataRow(600, 1000)]
    [DataRow(1000, 1000)]
    [DataRow(2000, 2000)]
    public void WhenSalesPrics_Exist_CalculateBasibInsurance(float salesPrice, float insurance)
    {
        //Arrange
        var insuranceService = new InsuranceCalculationService();
        //Act
        var result = insuranceService.GetInsuranceValue((decimal)salesPrice);
        //Assert
        Assert.AreEqual(result, (decimal)insurance);
    }

    [TestMethod]
    [DataRow("test", 0)]
    [DataRow("Laptops", 500)]
    [DataRow("Smartphones", 500)]
    public void WhenProductTypeName_Exist_CalculateIfHasInsurance(string ProductType, float insurance)
    {
        //Arrange
        var insuranceService = new InsuranceCalculationService();
        //Act
        var result = insuranceService.GetSpecialProductsInsurance(ProductType, 0);
        //Assert
        Assert.AreEqual(result, (decimal)insurance);
    }
}

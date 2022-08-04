using Insurance.Application.Common.Interfaces.Services;

namespace Insurance.Infrastructure.Services;

public class InsuranceCalculationService : IInsuranceCalculationService
{
    private readonly List<string> _specialProducts;

    public InsuranceCalculationService()
    {
        _specialProducts = new List<string>
        {
            "Laptops",
            "Smartphones"
        };
    }
    public decimal GetInsuranceValue(decimal salesPrice)
    {
        Link _isSalesPriceBetween500And2000 = new IsSalesPriceBetween500And2000();
        Link _isSalesPriceMoreThaneOrEqual2000 = new IsSalesPriceMoreThaneOrEqual2000();

        _isSalesPriceBetween500And2000.SetSuccessor(_isSalesPriceMoreThaneOrEqual2000);
        return _isSalesPriceBetween500And2000.Execute(salesPrice);
    }

    public decimal GetSpecialProductsInsurance(string? productTypeName, decimal insuranceValue)
    {

        if (productTypeName != null && _specialProducts.Contains(productTypeName))
            insuranceValue += 500;

        return insuranceValue;
    }
}

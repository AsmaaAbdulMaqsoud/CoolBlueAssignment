namespace Insurance.Application.Common.Interfaces.Services;

public interface IInsuranceCalculationService
{
    public decimal GetInsuranceValue(decimal salesPrice);
    public decimal GetSpecialProductsInsurance(string? productTypeName, decimal insuranceValue);
}

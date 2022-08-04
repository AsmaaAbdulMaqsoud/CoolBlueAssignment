using Insurance.Application.DTO;
using Insurance.Application.Models;

namespace Insurance.Application.Common.Interfaces.Services;

public interface IProductInsuranceService
{
    public Task<decimal> GetProductInsurance(int productId);
    public Task<OrderInsurance> GetProductsInsurance(List<ProductDto>? products);

}

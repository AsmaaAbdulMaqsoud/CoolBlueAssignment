using Insurance.Application.DTO;

namespace Insurance.Application.Common.Interfaces.Services;

public interface IOrderInsuranceService
{
    Task<decimal> GetOrderInsurance(OrderDto order);
}

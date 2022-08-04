using Insurance.Application.DTO;

namespace Insurance.Application.Common.Interfaces.Services;

public interface IProductTypeService
{
    public Task<bool> AddSurcharge(SurchargeRateDto surchargeRate);
}

using Insurance.Application.Common.Interfaces.Repositories;
using Insurance.Application.Common.Interfaces.Services;
using Insurance.Application.DTO;
using Insurance.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Insurance.Infrastructure.Services;

public class ProductTypeService : IProductTypeService
{
    private readonly IProducTypeRepository _producTypeRepository;
    private readonly ILogger<ProductTypeService> _logger;
    public ProductTypeService(IProducTypeRepository producTypeRepository, ILogger<ProductTypeService> logger)
    {
        _producTypeRepository = producTypeRepository;
        _logger = logger;
    }

    public async Task<bool> AddSurcharge(SurchargeRateDto surchargeRateObj)
    {
        _logger.LogInformation($"add surchage of value :{surchargeRateObj.SurchargeRate} for product type:{surchargeRateObj.ProductTypeId}");
        ProductType? productType = _producTypeRepository.GetProductTypeById(surchargeRateObj.ProductTypeId);
        productType.SurchargeRate = surchargeRateObj.SurchargeRate;
        return await _producTypeRepository.UpdateProductType(productType);
    }
}

using Insurance.Domain.Entities;

namespace Insurance.Application.Common.Interfaces.Repositories;

public interface IProducTypeRepository
{
    ProductType? GetProductTypeById(int productTypeId);
    public Task<bool> UpdateProductType(ProductType newProductType);
}

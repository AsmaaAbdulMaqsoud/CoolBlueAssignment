using Insurance.Application.Common.Interfaces.Repositories;
using Insurance.Domain.Entities;
using Insurance.Infrastructure.Persistence;

namespace Insurance.Infrastructure.Repositories;

public class ProducTypeRepository : IProducTypeRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public ProducTypeRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public ProductType? GetProductTypeById(int productTypeId)
    {
        return _applicationDbContext.ProductTypeList.FirstOrDefault(x => x.Id == productTypeId);
    }

    public async Task<bool> UpdateProductType(ProductType newProductType)
    {
        _applicationDbContext.Update(newProductType);
        var result = await _applicationDbContext.SaveChangesAsync();

        return result > 0;
    }
}

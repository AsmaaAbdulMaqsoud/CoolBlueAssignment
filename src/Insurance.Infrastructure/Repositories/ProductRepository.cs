using Insurance.Application.Common.Interfaces.Repositories;
using Insurance.Domain.Entities;
using Insurance.Infrastructure.Persistence;

namespace Insurance.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public ProductRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public Product? GetProductById(int productId)
    {
        return _applicationDbContext.ProductList.FirstOrDefault(x => x.Id == productId);
    }
}

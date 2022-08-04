using Insurance.Domain.Entities;

namespace Insurance.Application.Common.Interfaces.Repositories;

public interface IProductRepository
{
    Product? GetProductById(int productId);
}

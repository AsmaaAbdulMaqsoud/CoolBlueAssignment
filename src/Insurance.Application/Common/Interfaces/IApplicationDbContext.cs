using Insurance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> ProductList { get; }
    DbSet<ProductType> ProductTypeList { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

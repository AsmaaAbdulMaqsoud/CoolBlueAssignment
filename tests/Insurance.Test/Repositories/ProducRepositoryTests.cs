using Insurance.Domain.Entities;
using Insurance.Infrastructure.Persistence;
using Insurance.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Test.Repositories;

[TestClass]
public class ProducRepositoryTests
{
    [TestMethod]
    public void WhenProductId_ExistsInDB_GetProduct()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: "cooolblueDb")
       .Options;
        var product = new Product { Name = "digital Cameras", Id = 2 };
        var _context = new ApplicationDbContext(options);
        _context.ProductList.Add(product);
        _context.SaveChanges();

        var productRepository = new ProductRepository(_context);

        //Act
        var result = productRepository.GetProductById(product.Id);
        //Assert
        Assert.AreEqual(result.Name, product.Name);
        Assert.AreEqual(result.Id, product.Id);
    }
}

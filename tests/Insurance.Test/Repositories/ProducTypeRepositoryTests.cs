using Insurance.Domain.Entities;
using Insurance.Infrastructure.Persistence;
using Insurance.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Test.Repositories;

[TestClass]
public class ProducTypeRepositoryTests
{
    [TestMethod]
    public void WhenProductTypeId_ExistsInDB_GetProductType()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: "cooolblueDb")
       .Options;
        var productType = new ProductType { CanBeInsured = true, Name = "digital Cameras", Id = 2 };
        var _context = new ApplicationDbContext(options);
        _context.ProductTypeList.Add(productType);
        _context.SaveChanges();

        var producTypeRepository = new ProducTypeRepository(_context);

        //Act
        var result = producTypeRepository.GetProductTypeById(productType.Id);
        //Assert
        Assert.AreEqual(result.Name, productType.Name);
        Assert.AreEqual(result.CanBeInsured, productType.CanBeInsured);
        Assert.AreEqual(result.Id, productType.Id);
    }
}

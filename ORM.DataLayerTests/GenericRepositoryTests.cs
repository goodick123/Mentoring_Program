using ORM.DataLayer.DbContexts;
using ORM.DataLayer.Enums;
using ORM.DataLayer.Models;
using ORM.DataLayer.Repositories;
using Xunit;

namespace ORM.DataLayerTests
{
    public class GenericRepositoryTests
    {
        private OrmDbContext _dbContext;
        #region Order

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task ValidId_CalledRepository_ReturnedCorrectOrders(int id)
        {
            _dbContext = new OrmDbContext(TestData.GetUnitTestDbOptions());
            //Arrange
            var repository = new GenericRepository<Order>(_dbContext);

            var expected = _dbContext.Orders.FirstOrDefault(g => g.Id == id);

            //Act
            var actual = (await repository.GetWithIncludeAndTrackingAsync(g => g.Id == id)).FirstOrDefault();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateOrder_CalledRepository_SuccessfullyCreatedOrder()
        {
            this._dbContext = new OrmDbContext(TestData.GetUnitTestDbOptions());
            var repository = new GenericRepository<Order>(_dbContext);

            await repository.AddAsync(new Order
                { CreateDate = DateTime.UtcNow, Status = Status.Arrived, UpdateDate = DateTime.UtcNow });

            await repository.SaveChangesAsync();

            var result = (await repository.GetAllAsync()).Count;

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteOrder_CalledRepository_OrderRemoved()
        {
            this._dbContext = new OrmDbContext(TestData.GetUnitTestDbOptions());
            var testOrder = new Order
                { Id = 1, CreateDate = DateTime.UtcNow, Status = Status.Arrived, UpdateDate = DateTime.UtcNow };

            var repository = new GenericRepository<Order>(_dbContext);
            await repository.AddAsync(testOrder);
            await repository.SaveChangesAsync();

            repository.Delete(testOrder);
            await repository.SaveChangesAsync();

            Assert.Empty(await repository.GetAllAsync());
        }

        [Fact]
        public async Task UpdateOrder_CalledRepository_OrderUpdated()
        {
            this._dbContext = new OrmDbContext(TestData.GetUnitTestDbOptions());
            var testOrder = new Order
                { Id = 1, CreateDate = DateTime.UtcNow, Status = Status.Arrived, UpdateDate = DateTime.UtcNow };
            var repository = new GenericRepository<Order>(_dbContext);
            await repository.AddAsync(testOrder);
            await repository.SaveChangesAsync();

            testOrder.Status = Status.Done;
            repository.Update(testOrder);
            await repository.SaveChangesAsync();

            var result = await repository.GetByIdAsync(testOrder.Id);

            Assert.Equal(testOrder.Status, result.Status);
        }

        #endregion

        #region Product

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task ValidId_CalledRepository_ReturnedCorrectProducts(int id)
        {
            this._dbContext = new OrmDbContext(TestData.GetUnitTestDbOptions());
            //Arrange
            var repository = new GenericRepository<Product>(_dbContext);

            var expected = _dbContext.Products.FirstOrDefault(g => g.Id == id);

            //Act
            var actual = (await repository.GetByIdAsync(id));

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateProduct_CalledRepository_SuccessfullyCreatedProduct()
        {
            this._dbContext = new OrmDbContext(TestData.GetUnitTestDbOptions());
            var repository = new GenericRepository<Product>(_dbContext);

            await repository.AddAsync(new Product {Name = "123", Description = "123", Height = 1, Length = 1, Weight = 1, Width = 1});
            await repository.SaveChangesAsync();

            var result = (await repository.GetAllAsync()).Count;

            Assert.Equal(3, result);
        }

        [Fact]
        public async Task DeleteProduct_CalledRepository_ProductRemoved()
        {
            this._dbContext = new OrmDbContext(TestData.GetUnitTestDbOptions());
            var testProduct = new Product
            { Id = 100, Name = "test", Description = "test" };

            var repository = new GenericRepository<Product>(_dbContext);
            await repository.AddAsync(testProduct);
            await repository.SaveChangesAsync();

            repository.Delete(testProduct);
            await repository.SaveChangesAsync();

            Assert.Null(await repository.GetByIdAsync(testProduct.Id));
        }

        [Fact]
        public async Task UpdateProduct_CalledRepository_ProductUpdated()
        {
            this._dbContext = new OrmDbContext(TestData.GetUnitTestDbOptions());
            var testProduct = new Product
            { Id = 101, Name = "123", Description = "123"};
            var repository = new GenericRepository<Product>(_dbContext);
            await repository.AddAsync(testProduct);
            await repository.SaveChangesAsync();

            testProduct.Height = 1;
            repository.Update(testProduct);
            await repository.SaveChangesAsync();

            var result = await repository.GetByIdAsync(testProduct.Id);

            Assert.Equal(testProduct.Height, result.Height);
        }

        #endregion
    }
}

using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using ORM.DataLayer.DbContexts;
using ORM.DataLayer.Enums;
using ORM.DataLayer.Models;

namespace ORM.DataLayerTests
{
    internal static class TestData
    {
        public static DbContextOptions<OrmDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<OrmDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            using (var context = new OrmDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static void SeedData(OrmDbContext context)
        {
            context.Set<Order>().Add(new Order
                { Id = 1, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow, Status = Status.Done });
            context.Set<Order>().Add(new Order
                { Id = 2, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow, Status = Status.Arrived });


            context.Set<Product>().Add(new Product
                { Id = 1, Weight = 1, Name = "test1", Description = "test1", Height = 1, Length = 1, Width = 1 });
            context.Set<Product>().Add(new Product
                { Id = 2, Weight = 2, Name = "test2", Description = "test2", Height = 2, Length = 2, Width = 2 });
        }

    }
}
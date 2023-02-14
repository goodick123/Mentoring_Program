using Microsoft.EntityFrameworkCore;
using ORM.DataLayer.Models;

namespace ORM.DataLayer.DbContexts
{
    public class OrmDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public OrmDbContext()
        {
              Database.EnsureCreated();
        }

        public OrmDbContext(DbContextOptions<OrmDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasOne(x => x.Product);

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "test1",
                    Description = "test1",
                    Height = 1.0,
                    Length = 1,
                    Weight = 1,
                    Width = 1.0
                },
                new Product
                {
                    Id = 2,
                    Name = "test2",
                    Description = "test2",
                    Height = 2.0,
                    Length = 2,
                    Weight = 2.0,
                    Width = 2.0
                });
        }

        public override int SaveChanges()
        {
            SetInitialData();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetInitialData();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetInitialData()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;

                if (entity is Order && entry.State == EntityState.Added)
                {
                    entity.GetType().GetProperty("CreateDate")?.SetValue(entity, DateTime.UtcNow);
                    entity.GetType().GetProperty("UpdateDate")?.SetValue(entity, DateTime.UtcNow);
                }
            }

        }
    }
}

using ADO.NET.DataLayer.Repositories;
using ADO.NET.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ADO.NET.DataLayer
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddFileRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(BaseRepository<>))
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IProductRepository, ProductRepository>();
        }
    }
}

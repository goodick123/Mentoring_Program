using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.RepositoriesExtensions
{
    public static class RepositoriesServiceExtension
    {
        public static IServiceCollection AddFileRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IGenericDataAccess<>), typeof(FileSystemRepository<>));
        }
    }
}

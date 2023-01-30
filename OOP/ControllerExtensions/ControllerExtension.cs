using Microsoft.Extensions.DependencyInjection;
using OOP.Interfaces;

namespace OOP.LibraryFileSystem.ServicesExtensions
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddFileControllers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IFileStorageController, FileStorageController>();
        }
    }
}

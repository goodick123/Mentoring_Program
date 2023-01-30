using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Helpers;
using BLL.LibraryFileSystem.Interfaces;
using BLL.LibraryFileSystem.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.LibraryFileSystem.ServicesExtensions
{
    public static class FileServicesExtension
    {
        public static IServiceCollection AddFileServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IFileSystemService<BookDTO>, BookService>()
                .AddScoped<IFileSystemService<LocalizedBookDTO>, LocalizedBookService>()
                .AddScoped<IFileSystemService<PatentDTO>, PatentService>()
                .AddScoped<IFileSystemService<MagazineDTO>, MagazineService>()
                .AddScoped(typeof(ICrudHelper<,>), typeof(CrudHelper<,>));
        }
    }
}

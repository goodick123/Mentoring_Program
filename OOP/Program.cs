using BLL.LibraryFileSystem.MappingProfiles;
using BLL.LibraryFileSystem.Options;
using BLL.LibraryFileSystem.ServicesExtensions;
using DAL.RepositoriesExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OOP.Interfaces;
using OOP.LibraryFileSystem.ServicesExtensions;

namespace OOP
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var fileController = host.Services.GetService<IFileStorageController>() 
                                 ?? throw new ArgumentException("null");
            fileController.RunProgram();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var _hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddFileRepositories();
                    services.Configure<CachingExpirationTimeOptions>(hostContext.Configuration.GetSection(nameof(CachingExpirationTimeOptions)));
                    services.AddFileServices();
                    services.AddMemoryCache();
                    services.AddFileControllers();
                    services.AddAutoMapper(config => config.AddProfile<MappingProfiles>());
                    services.AddLogging();
                });

            return _hostBuilder;
        }
    }
}


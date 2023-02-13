using System.Data;
using System.Data.SqlClient;
using ADO.NET.DataLayer;
using ADO.NET.DataLayer.Enums;
using ADO.NET.DataLayer.Models;
using ADO.NET.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ADO.NET.Program
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var fileController = host.Services.GetService<IOrderRepository>()
                                 ?? throw new ArgumentException("File storage controller is null.");

            fileController.Create(GetFakeOrders().First());
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var _hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddFileRepositories();
                    services.AddSingleton<IDbConnection>(x => new SqlConnection(hostContext.Configuration.GetConnectionString("LocalDb")));
                    services.AddMemoryCache();
                    services.AddLogging();
                });

            return _hostBuilder;
        }

        private static List<Order> GetFakeOrders()
        {
            return new List<Order>
            {
                new()
                {
                    CreateDate = DateTime.Today.AddDays(-10),
                    UpdateDate = DateTime.Today,
                    Status = Status.InProgress,
                    ProductId = 3
                },
                new()
                {
                    CreateDate = DateTime.Today.AddDays(-30),
                    UpdateDate = DateTime.Today.AddDays(-1),
                    Status = Status.Done,
                    ProductId = 4
                }
            };
        }
    }
}
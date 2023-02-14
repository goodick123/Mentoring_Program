using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ORM.DataLayer.DbContexts;
using ORM.DataLayer.Enums;
using ORM.DataLayer.Models;
using ORM.DataLayer.Repositories;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var repositoryOrder = host.Services.GetService<IGenericRepository<Order>>();

        await repositoryOrder.AddAsync(new Order
        {
            Status = Status.NotStarted,
            Product = new Product
            {
                Description = "123",
                Name = "123",
                Height = 1,
                Length = 1,
                Weight = 1,
                Width = 1

            }
        });
        await repositoryOrder.SaveChangesAsync();

        var result = await repositoryOrder.GetWithIncludeAsync(x => x.Status == Status.NotStarted, x => x.Product);
        var parameters = ConvertFilterToParams(new Filter
        {
            Month = 2,
            ProductId = 3,
            Year = 2023,
            Status = Status.NotStarted
        }).ToArray();

        var result2 = await repositoryOrder.ExecuteSqlWithReturningModel(AddParametersListToProcedure("dbo.GetOrders", parameters), parameters);
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var _hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddDbContext<OrmDbContext>(opt =>
                    opt.UseSqlServer(hostContext.Configuration.GetConnectionString("LocalDb")));

                services.AddLogging();
            });

        return _hostBuilder;
    }

    private static List<SqlParameter> ConvertFilterToParams(Filter filter)
    {
        var parameters = new List<SqlParameter>();

        var props = filter.GetType().GetProperties();

        foreach (var property in props)
        {
            parameters.Add(new SqlParameter
            {
                ParameterName = $"@{property.Name}",
                Value = property.GetValue(filter)
            });
        }

        return parameters;
    }

    private static string AddParametersListToProcedure(string procedureName, IEnumerable<SqlParameter> parameters)
    {
        return procedureName + " " + string.Join(',', parameters.Select(x => x.ParameterName).ToArray());
    }
}
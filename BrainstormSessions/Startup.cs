using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using BrainstormSessions.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.Email;

namespace BrainstormSessions
{
    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var emailSmtpOptions = configuration.GetSection("EmailLoggingOptions");

            Log.Logger = new LoggerConfiguration()
               .WriteTo.Log4Net()
               .WriteTo.Console()
               .WriteTo.Email(new EmailConnectionInfo
               {
                   FromEmail = emailSmtpOptions.GetValue<string>("FromEmail"),
                   ToEmail = emailSmtpOptions.GetValue<string>("ToEmail"),
                   MailServer = emailSmtpOptions.GetValue<string>("MailServer"),
                   NetworkCredentials = new NetworkCredential
                   {
                       UserName = emailSmtpOptions.GetValue<string>("Username"),
                       Password = emailSmtpOptions.GetValue<string>("Password")
                   },
                   EnableSsl = true,
                   Port = 587,
                   EmailSubject = emailSmtpOptions.GetValue<string>("Subject")
               },
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                   batchPostingLimit: 1,
                   restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug
               )
               .MinimumLevel.Debug()
               .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));

            services.AddControllersWithViews();

            services.AddScoped<IBrainstormSessionRepository,
                EFStormSessionRepository>();
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                var repository = serviceProvider.GetRequiredService<IBrainstormSessionRepository>();

                InitializeDatabaseAsync(repository).Wait();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public async Task InitializeDatabaseAsync(IBrainstormSessionRepository repo)
        {
            var sessionList = await repo.ListAsync();
            if (!sessionList.Any())
            {
                Log.Logger.Warning("Sessions are not exist!");
                await repo.AddAsync(GetTestSession());
            }
        }

        public static BrainstormSession GetTestSession()
        {
            var session = new BrainstormSession()
            {
                Name = "Test Session 1",
                DateCreated = new DateTime(2016, 8, 1)
            };
            var idea = new Idea()
            {
                DateCreated = new DateTime(2016, 8, 1),
                Description = "Totally awesome idea",
                Name = "Awesome idea"
            };
            session.AddIdea(idea);
            return session;
        }
    }
}

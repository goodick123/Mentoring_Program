using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Email;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(new LoggerConfiguration()
                    .WriteTo.Log4Net()
                    .WriteTo.Console()
                    /*.WriteTo.Email(new EmailConnectionInfo
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
                    )*/
                    .MinimumLevel.Debug()
                    .CreateLogger())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

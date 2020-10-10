using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Core;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace NotificationService
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            CreateHostBuilder(args).Build().Run();
        }        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IEmailService, EmailService>();

                    var mailkitOptions = Configuration.GetSection("Email").Get<MailKitOptions>();
                    services.AddMailKit(config =>
                    {
                        config.UseMailKit(mailkitOptions);
                    });

                    services.AddHostedService<Worker>();
                })
                .UseWindowsService();;
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Module3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: setup the logger first to catch all errors
            ILogger logger = null;
            IHostingEnvironment hostingEnvironment;

            try
            {
                var webHost = CreateWebHostBuilder(args).Build();

                using (var scope = webHost.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    hostingEnvironment = services.GetService<IHostingEnvironment>();
                    logger = webHost.Services.GetService<ILogger<Program>>();
                }

                logger.LogInformation($"Application started. Path: {hostingEnvironment.WebRootPath}");
                webHost.Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.LogError(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
            .UseNLog();

    }
}

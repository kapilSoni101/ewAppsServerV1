using System;
using ewApps.Core.SerilogLoggingService;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace ewApps.Payment.WebApi {
    public class Program {
        public static void Main(string[] args) {

            // Init Serilog 
            SerilogLogger.InitLogger();
            try {
                Log.Information("Starting web host for Payment App");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch(Exception ex) {
                Log.Fatal(ex, "Payment App Host terminated unexpectedly");
            }
            finally {
                //As logger may be using some heavy resources it must be disposed on exit
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

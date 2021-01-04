using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ewApps.Core.SerilogLoggingService;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AppPortal.WebApi {

    public class Program {

        public static void Main(string[] args) {
            // Init Serilog 
            SerilogLogger.InitLogger();
            try {
                Log.Information("Starting web host for App Portal");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch(Exception ex) {
                Log.Fatal(ex, "App Portal Host terminated unexpectedly");
            }
            finally {
                //As logger may be using some heavy resources it must be disposed on exit
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
       .UseSerilog();


    }
}

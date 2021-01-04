using System;
using System.Diagnostics;
using ewApps.Core.SerilogLoggingService;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace BusinessEntity.WebApi {

  public class Program {

    public static void Main(string[] args) {

            // Init Serilog 
            SerilogLogger.InitLogger();
            // SendTestEmail();
            try {
        Log.Information("Starting web host for Business Entity App");
        CreateWebHostBuilder(args).Build().Run();
      }
      catch(Exception ex) {
        Log.Fatal(ex, "Business Entity App Host terminated unexpectedly");
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

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

namespace ewApps.Report.WebApi {

  public class Program {

    public static void Main(string[] args) {
      // Init Serilog 
      SerilogLogger.InitLogger();
      try {
        Log.Information("Starting web host for Report App");
        CreateWebHostBuilder(args).Build().Run();
      }
      catch(Exception ex) {
        Log.Fatal(ex, "Report App Host terminated unexpectedly");
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

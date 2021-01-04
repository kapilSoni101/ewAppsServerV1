/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Asha Sharda
 * Date: 4 Sept 2019
 *
 */

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using System.Diagnostics;

namespace  ewApps.ServiceRegistration.WebApi
{

  public class Program {

    public static void Main(string[] args) {
      // Init Serilog '
      Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
   
      try
      {
        Log.Information("Starting web host for SAPB1 Connector App");
        CreateWebHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Registration Service Host terminated unexpectedly");
      }
      finally
      {
        //As logger may be using some heavy resources it must be disposed on exit
        Log.CloseAndFlush();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            // this replaces default logger with Serilog logging
            //  As serilog uses older version of ASp.netcore.app library 
            // this function can not be shifted to the dll.
            .UseSerilog();
  }
}

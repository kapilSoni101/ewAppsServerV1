using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ewApps.Core.SerilogLoggingService {
  public static class LoggerFactory {
    public static void SetDefaultAppLogger() {
      // Seri Log Settings Start
      string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      var configuration = new ConfigurationBuilder()
                  .AddJsonFile($"AppSettings/appsettings.{envName}.json")
                  .Build();
      string appName = configuration.GetValue<string>("AppName");
      string appVersion = configuration.GetValue<string>("AppVersion");
      string deployment = configuration.GetValue<string>("Deployment");
      LoggerModel model = new LoggerModel { AppName = appName, AppVersion = appVersion, DeploymentName = deployment, SeqURL = "http://ewp-dev22.eworkplaceapps.com:5341" };
      Log.Logger = SerilogLogger.Configure(model, null);
      // Seri Log Settings End
    }
  }
}

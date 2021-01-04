using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace ewApps.Core.SerilogLoggingService {
    /// <summary>
    /// Static class to define different type of logger
    /// with the predefined Enricher and configurations
    /// </summary>
    static public class SerilogLogger {
        private static int _rollingFileSizeInBytes = 5242880;

        /// <summary>
        /// Gets Logger
        /// It logs to Console, RolloingFile define with Date, and Seq
        /// Properties : Application Name,Application Version,Context Name, Thread Id
        /// It also overides the Microsoft default Information Logging
        /// Values will be set only if it isavailable in the model
        /// </summary>
        /// <param name="model">Parameters to define application and other arguments</param>
        /// <param name="lConfiguration">Any extra configuration that need to be added with the logger</param>
        /// <returns>Ilogger</returns>

        public static ILogger Configure(LoggerModel model, LoggerConfiguration lConfiguration) {
            //get Confoguration based on defined LogginveLEvel
            LoggerConfiguration lc = new LoggerConfiguration();
            if(lConfiguration != null)
                lc = lConfiguration;
            if(model.DeploymentName == "Development" && string.IsNullOrEmpty(model.LoggingLevel))
                model.LoggingLevel = "Debug";
            MapStringToLoggingLevel(model.LoggingLevel, lc);

            //Configure Console Sink
            if(model.ConsoleSinkRequired) {
                string consoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] ({SourceContext}) {Message}{NewLine}{Exception}";
                if(!string.IsNullOrEmpty(model.ConsoleOutputTemplate))
                    consoleOutputTemplate = model.ConsoleOutputTemplate;
                lc.WriteTo.Console(outputTemplate: consoleOutputTemplate);
            }
            //Configure Rollingfile Sink
            if(model.RollingFileSinkRequired) {
                string rollingFileoutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] ({SourceContext}) {Message}{NewLine}{Exception}";
                string outputFileLocation = @"Serilog/Logs/Log-{Date}.txt";
                if(!string.IsNullOrEmpty(model.RollingFileOutputTemplat))
                    rollingFileoutputTemplate = model.ConsoleOutputTemplate;
                if(!string.IsNullOrEmpty(model.RollingFileLocation))
                    outputFileLocation = model.RollingFileLocation;
                lc.WriteTo.RollingFile(outputFileLocation, outputTemplate: rollingFileoutputTemplate, fileSizeLimitBytes: _rollingFileSizeInBytes, retainedFileCountLimit: 100);
            }

            //Configure Seq Sing 
            if(model.SeqSinkRequired && !string.IsNullOrEmpty(model.SeqURL)) {
                lc.WriteTo.Seq(model.SeqURL);
            }
            //Add enrichers
            if(!string.IsNullOrEmpty(model.AppName)) {
                lc.Enrich.WithProperty("Application", model.AppName);
            }
            if(!string.IsNullOrEmpty(model.AppVersion)) {
                lc.Enrich.WithProperty("ApplicationVersion", model.AppVersion);
            }
            if(!string.IsNullOrEmpty(model.DeploymentName)) {
                lc.Enrich.WithProperty("Deployment", model.DeploymentName);
            }

            //Create Logger
            ILogger logger = lc.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                             .MinimumLevel.Override("System", LogEventLevel.Error)
                             .Enrich.FromLogContext()
                             .Enrich.WithThreadId()
                             .CreateLogger();
            return logger;
        }
        /// <summary>
        /// Returns Email logger
        /// </summary>
        /// <param name="model">Email Logger model</param>
        /// <param name="lConfiguration">Logger configuration object</param>
        /// <returns></returns>
        public static Logger GetEmailLogger(EmailLoggerModel model, LoggerConfiguration lConfiguration) {
            LoggerConfiguration lc = new LoggerConfiguration();
            if(lConfiguration != null)
                lc = lConfiguration;
            ConfigureEmailSink(model, lc);

            Logger logger = lc.CreateLogger();
            return logger;
        }

        #region supportive methods
        /// <summary>
        /// Maps string to Logging level
        /// </summary>
        /// <param name="loggerLevel">string value of logging level</param>
        /// <param name="lc">Loffer configuration</param>
        private static void MapStringToLoggingLevel(string loggerLevel, LoggerConfiguration lc) {
            lc.MinimumLevel.Debug();
            if(loggerLevel == "Debug")
                lc.MinimumLevel.Debug();
            if(loggerLevel == "Information")
                lc.MinimumLevel.Information();
            if(loggerLevel == "Error")
                lc.MinimumLevel.Error();
            if(loggerLevel == "Warning")
                lc.MinimumLevel.Warning();
            if(loggerLevel == "Verbose")
                lc.MinimumLevel.Verbose();

        }

        private static void ConfigureEmailSink(EmailLoggerModel model, LoggerConfiguration lc) {
            if(model == null)
                return;
            lc.WriteTo.Email(new EmailConnectionInfo {
                FromEmail = model.SenderEmail,
                ToEmail = model.ReceiverEmail,
                MailServer = model.EmailServer, //"smtp.gmail.com",
                NetworkCredentials = new NetworkCredential {
                    UserName = model.UserName,
                    Password = model.Password,

                },
                EnableSsl = model.EmailServerSSLEnabled,
                Port = model.EmailServerPort,
                EmailSubject = model.EmailSubject,
                IsBodyHtml = false

            },
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}",
                   batchPostingLimit: 1,
                  restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
                  mailSubject: model.EmailSubject
                 );
        }

        public static void InitLogger() {
            // Seri Log Settings Start
            string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile($"AppSettings/appsettings.{envName}.json")
                        .Build();
            string appName = configuration.GetValue<string>("AppName");
            string appVersion = configuration.GetValue<string>("AppVersion");
            string deployment = configuration.GetValue<string>("Deployment");
            string logPortalUrl = configuration.GetValue<string>("LogPortalUrl");
            string minimumLogLevel = configuration.GetValue<string>("MinimumLoggingLevel");
            //LoggerModel model = new LoggerModel { AppName = appName, AppVersion = appVersion, DeploymentName = deployment, SeqURL = "http://ewp-dev22.eworkplaceapps.com:5341" };
            LoggerModel model = new LoggerModel { AppName = appName, AppVersion = appVersion, DeploymentName = deployment, SeqURL = logPortalUrl, LoggingLevel = minimumLogLevel };
            Log.Logger = SerilogLogger.Configure(model, null);
            // Seri Log Settings End
        }



        public static void LogInfo(string message) {
            Log.Information(message);
        }

        public static void LogDebug(string message) {
            Log.Debug(message);
        }

        public static void LogError(string message) {
            Log.Error(message);
        }

        #endregion

        #region unused Section
        /*  /// <summary>
          /// Email Logger, to send email 
          /// </summary>
          /// <param name="model">Parameters to define the Email Server</param>
          /// <returns></returns>
          public static ILogger GetEmailLogger(EmailLoggerModel model) {
            ILogger logger = new LoggerConfiguration()
                             .MinimumLevel.Error()
                             .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                             .Enrich.FromLogContext()
                             .Enrich.WithThreadId()
                             .Enrich.WithProperty("Application", model.AppName)
                             .Enrich.WithProperty("ApplicationVersion", model.AppVersion)
                             .Enrich.WithProperty("DeploymentName", model.DeploymentName)
                             .WriteTo.Email(new EmailConnectionInfo {
                               FromEmail = model.SenderEmail,
                               ToEmail = model.ReceiverEmail,
                               MailServer = model.EmailServer, //"smtp.gmail.com",
                               NetworkCredentials = new NetworkCredential {
                                 UserName = model.UserName,
                                 Password = model.Password
                               },
                               EnableSsl = true,
                               Port = 465,
                               EmailSubject = model.EmailSubject
                             },
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}",
                    batchPostingLimit: 10
                  , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
                  )
              .CreateLogger();
            return logger;
          }*/

        /// <summary>
        /// Default logger with Seq
        /// It logs to Console, RolloingFile define with Date, and Seq
        /// Properties : Application Name,Application Version,Context Name, Thread Id
        /// It also overides the Microsoft default Information Logging
        /// </summary>
        /// <param name="model">Parameters to define application and other arguments</param>
        /// <returns>Ilogger</returns>
        //Note - Obselete, use other method Configure
        public static ILogger GetDefaultSeqLogger(LoggerModel model) {
            //get Confoguration based on defined LogginveLEvel
            LoggerConfiguration lc = new LoggerConfiguration();
            MapStringToLoggingLevel(model.LoggingLevel, lc);
            //Configure email sink
            ILogger logger = lc.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                             .MinimumLevel.Override("System", LogEventLevel.Error)
                             .Enrich.FromLogContext()
                             .Enrich.WithThreadId()
                             .Enrich.WithProperty("Application", model.AppName)
                             .Enrich.WithProperty("ApplicationVersion", model.AppVersion)
                             .Enrich.WithProperty("Deployment", model.DeploymentName)
                             .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] ({SourceContext}) {Message}{NewLine}{Exception}")
                             .WriteTo.RollingFile(@"Serilog/Logs/Log-{Date}.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] ({SourceContext}) {Message}{NewLine}{Exception}")
                             .WriteTo.Seq(model.SeqURL)
                             .CreateLogger();
            return logger;
        }


        #endregion

    }
}

namespace ewApps.Core.SerilogLoggingService {
  /// <summary>
  /// Model class for Email Logging defination
  /// </summary>
  public class LoggerModel {
    /// <summary>
    /// Application Name
    /// </summary>
    public string AppName {
      get; set;
    }

    /// <summary>
    /// Application Version
    /// </summary>
    public string AppVersion {
      get; set;
    }

    /// <summary>
    /// Deployment Name
    /// </summary>
    public string DeploymentName {
      get; set;
    }

    /// <summary>
    /// Minimum Logging Level
    /// </summary>
    public string LoggingLevel {
      get; set;
    }

    /// <summary>
    /// Email Sink is Required or not
    /// </summary>
    public bool SeqSinkRequired {
      get;
      set;
    } = false;
    /// <summary>
    /// Seq installed URL
    /// </summary>
    public string SeqURL {
      get;
      set;
    } = "http://localhost:5341";

    
    /// <summary>
    /// Email Sink is Required or not
    /// </summary>
    public bool ConsoleSinkRequired {
      get;
      set;
    } = true;

    /// <summary>
    /// All properties required to defined Email Sink
    /// </summary>
    public string ConsoleOutputTemplate {
      get;
      set;
    }
    /// <summary>
    /// Email Sink is Required or not
    /// </summary>
    public bool RollingFileSinkRequired {
      get;
      set;
    } = true;

    /// <summary>
    /// All properties required to defined Email Sink
    /// </summary>
    public string RollingFileOutputTemplat{
      get;
      set;
    }
    /// <summary>
    /// All properties required to defined Email Sink
    /// </summary>
    public string RollingFileLocation {
      get;
      set;
    }


  }
}

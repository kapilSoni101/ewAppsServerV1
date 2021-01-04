namespace ewApps.Core.SerilogLoggingService {
  /// <summary>
  /// Model class for Email Logging defination
  /// </summary>
  public class EmailLoggerModel {
    
    /// <summary>
    /// Deployment Name
    /// </summary>
    public string DeploymentName {
      get; set;
    }

    /// <summary>
    /// Sender Email Address
    /// </summary>
    public string SenderEmail {
      get; set;
    }
    /// <summary>
    /// Receiver Email Address
    /// </summary>
    public string ReceiverEmail {
      get; set;
    }
    /// <summary>
    /// Email Server
    /// </summary>
    public string EmailServer {
      get; set;
    }

    /// <summary>
    /// Email Server port
    /// </summary>
    public int EmailServerPort
    {
      get; set;
    }

    /// <summary>
    /// Email Server port
    /// </summary>
    public bool EmailServerSSLEnabled
    {
      get; set;
    } = false;
    /// <summary>
    /// Network User Name 
    /// </summary>
    public string UserName {
      get; set;
    }

    /// <summary>
    /// Network USer Password
    /// </summary>
    public string Password {
      get; set;
    }
    /// <summary>
    /// Email Subject
    /// </summary>
    public string EmailSubject {
      get; set;
    } = "Application Error";

  }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.Webhook.Server {
  public static class WebhookServerConstants {
  
//Status
    public const string StatusInProgress = "InProgress";
    public const string StatusSuccess = "Success";
    public const string StatusFailure = "Failure";
    //Delivery Attempts
    public const int MaxDeliveryAttempts = 5;
    public const int MaxDeliveryallowedHours = 24;
    //Events
    public const string EventServerStart = "Server.Start";
        public const int DispatchServiceExecutionDelay = 600;// Seconds;
  }
}

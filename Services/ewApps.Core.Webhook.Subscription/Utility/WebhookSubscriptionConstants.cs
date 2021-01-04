using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.Webhook.Subscriber {
  public static class WebhookSubscriptionConstants {
    //End Points at server
    public const string AddSubscriptionServerEndPoint = "/add/subscription";
    public const string UpdateSubscriptionServerEndPoint = "/update/subscription";
    public const string RemoveSubscriptionServerEndPoint = "/remove/subscription";
    public const string SynchronizationServerEndPoint = "/sync/subscription";

    //EndPoints ar Subscriber
    public const string SubscriptionCallbackEndPoint = "/receiveevents";
    public const string ServerShutDownCallbackEndPoint = "/servershutdown";

    //Events
    public const string ServerStopEvent = "Server.ShutDown";
    public const int DispatchServiceExecutionDelay = 120; //Seconds
  }
}

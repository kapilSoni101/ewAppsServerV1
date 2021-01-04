using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ewApps.Core.Webhook.Server {
/// <summary>
/// This class handles Webhook Server subscriptions.
/// Hari: More?
/// </summary>
/// 
//Hari: Not clear. Discuss
  public class WebhookSubscriber {

    #region Constructor and property

    private WebhookDBContext _webhookDBContext;
    public WebhookSubscriber(WebhookDBContext dbContext) {
      _webhookDBContext = dbContext;
    }
    //will be called by client to subscribe the events and by the calling endpoint the 
    //server name will be identified and tracked
    public Task AddSubscription(WebhookSubscription subscription, string serverName) {
      //Get Server Name from Endpoint
      return null;
    }
    public List<WebhookEventDefinition> GetEvents() {
      //Get Server Name from Endpoint
      //Get list of events from DB
      return null;
    }

    public List<WebhookEventDefinition> GetSubscribedEvents(string clientName) {
      //Get Server Name from Endpoint
      //GetList of events from Db
      return null;
    }

    public List<WebhookEventPayload> GetPendingEvents(string clientName) {
      //Get Server Name from Endpoint
      //GetList of events from Db
      return null;
    }

    public void MarkPendingEventsExecuted(DateTime dt) {
      //Get Server Name from Endpoint
      //Remove all the events from db till that time
      return ;
    }

    #endregion
  }
}

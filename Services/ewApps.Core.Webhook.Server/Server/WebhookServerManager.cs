/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ewApps.Core.Webhook.Server {
  /// <summary>
  /// WebhookServerManager class manages all Webhook Servers in the app.
  /// It is a singleton class. It provides support for:
  /// (1) Manage all Webhook servers.
  /// (2) Manage subscriptions.  
  /// (3) Handle all events raised by Webhook servers
  /// (4) Raises Server Start Event on Initialization.
  /// </summary>
  public class WebhookServerManager {

    #region Constructor and property
    private WebhookDBContext _webhookDBContext;
    private IOptions<WebhookServerAppSettings> _appSettings;
    private ILogger<WebhookServerManager> _logger;
    // This is a cache to store List<WebhookServer> items.
   // private List<WebhookServer> _webhookServerCache;
    // This is a cache to store List<WebhookSubscription> items. 
   // private List<WebhookSubscription> _webhookSubscriptionCache;

    /// <summary>
    /// Webhook Server Manager Constructor
    /// </summary>
    /// <param name="dbContext">Database Context</param>
    /// <param name="appSetting">App setting Options</param>
    /// <param name="logger">DI for logger</param>
    public WebhookServerManager(WebhookDBContext dbContext, IOptions<WebhookServerAppSettings> appSetting, ILogger<WebhookServerManager> logger) {
      IsInitialized = false;
      _webhookDBContext = dbContext;
      _appSettings = appSetting;
      _logger = logger;
      _logger.LogInformation("[{Method}] Service initialized", "WebhookServerManager");
     // InitializeLocalServerCache();

     // InitializeLocalSubscriptionCache();
      //Raise Server start event for all stored Subscriptions
      //Assumption all the Subscription should have configured for this event to Get notified    
     // ProcessServerStart();
     // IsInitialized = true;
    }
    /// <summary>
    /// Checks webhook server manager initialization is completed or not.
    /// </summary>
    public bool IsInitialized {
      get; set;
    }
    #endregion

    #region  Webhook Server methods

    /// <summary>
    /// Initialize the local cache from Database
    /// </summary>
    //public void InitializeLocalServerCache() {
    //  _logger.LogTrace("[{Method}] Initializes the server cache", "InitializeLocalServerCache");
    //  _webhookServerCache = _webhookDBContext.GetAllWebhookServers();
    //  _logger.LogTrace("[{Method}] Initializes the server cache {@servercache}", "InitializeLocalServerCache", _webhookServerCache);
    //  //_webhookDBContext.RemoveRange(_webhookDBContext.WebhookServers.ToList());    
    //  //_webhookDBContext.SaveChanges();
    //}

    /// <summary>
    /// Gets all the WebhookServers defined by host
    /// </summary>
    /// <param name="token"> cancellation Token</param>
    /// <returns></returns>
    public async Task<List<WebhookServer>> GetAllWebhookServersAsync(CancellationToken token = default(CancellationToken)) {

      List<WebhookServer> servers = await _webhookDBContext.GetAllWebhookServersAsync(token);
      _logger.LogTrace("[{Method}] Gets all defined webhook servers {@servers}", "GetAllWebhookServersAsync", servers);
      return servers;
    }
    /// <summary>
    /// Adds Webhook Server
    /// </summary>
    /// <param name="webhookServer">Webhook server defination that need to be added</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>Added webhookServer Entity</returns>
    public async Task<WebhookServer> AddWebhookServerAsync(WebhookServer webhookServer, CancellationToken token = default(CancellationToken)) {
      //Check Server in the Cache with givenName for duplicacy 
      _logger.LogDebug("[{Method}] Add webhook server - Starts", "AddWebhookServerAsync");
      _logger.LogTrace("[{Method}] Add webhook server {@server}", "AddWebhookServerAsync", webhookServer);
      WebhookServer webhookServerFromCache = _webhookDBContext.WebhookServers.FirstOrDefault<WebhookServer>(x => x.ServerName == webhookServer.ServerName);
      //If exists get it
      if (webhookServerFromCache != null) {
        _logger.LogDebug("[{Method}] webhook server already exists", "AddWebhookServerAsync");
        return webhookServerFromCache;
      }
      //Else Add Server to Database
      webhookServer.ID = new Guid();
      //Add webhookserver defination to the DB
      _webhookDBContext.Add<WebhookServer>(webhookServer);
      await _webhookDBContext.SaveChangesAsync();
      //Add new object to Cache;
     // _webhookServerCache.Add(webhookServer);
      _logger.LogDebug("[{Method}] Add webhook server - Ends", "AddWebhookServerAsync");
      return webhookServer;
    }

    /// <summary>
    /// Updates webhook Server
    /// </summary>
    /// <param name="webhookServer">Webhook server defination that need to be updated</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>Updated webhookServer Entity</returns>
    public async Task<WebhookServer> UpdateWebhookServerAsync(WebhookServer webhookServer, CancellationToken token = default(CancellationToken)) {
      //Check Server in the Database with givenName
      _logger.LogDebug("[{Method}] Update webhook server - Starts", "UpdateWebhookServerAsync");
      _logger.LogTrace("[{Method}] Update webhook server {@server}", "UpdateWebhookServerAsync", webhookServer);

      WebhookServer webhookServerFromCache = _webhookDBContext.WebhookServers.FirstOrDefault<WebhookServer>(x => x.ServerName == webhookServer.ServerName);//await _webhookDBContext.GetWebhookServerAsync(webhookServer.ServerName, token);
                                                                                                                     //If not exists 
      if (webhookServerFromCache == null) {
        _logger.LogDebug("[{Method}] Update webhook server object not exists ", "UpdateWebhookServerAsync");
        return null;
      }                             //Confirm with Sanjeev Else update Server to Database
                                    //Note - Subscription may have different event set from the Server fater update, so the new events which are not subscribed  
                                    //or events which are no longer available to server will not be handled by this server anymore.
      _webhookDBContext.Update<WebhookServer>(webhookServer);
      await _webhookDBContext.SaveChangesAsync();
      _logger.LogDebug("[{Method}] Update webhook server - ends", "UpdateWebhookServerAsync");
      //Update0 local Cache
      //_webhookServerCache.Remove(webhookServerFromCache);
      //_webhookServerCache.Add(webhookServer);
      return webhookServer;
    }

    /// <summary>
    /// Deletes webhook Server along with all ites subscriptions
    /// </summary>
    /// <param name="webhookServer">Webhook server defination that need to be deleted</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>void</returns>
    public async Task DeleteWebhookServerAsync(WebhookServer webhookServer, CancellationToken token = default(CancellationToken)) {
      _logger.LogDebug("[{Method}] Delete webhook server - Starts", "DeleteWebhookServerAsync");
      _logger.LogTrace("[{Method}] Delete webhook server {@server}", "DeleteWebhookServerAsync", webhookServer);
      _webhookDBContext.Remove<WebhookServer>(webhookServer);
      await _webhookDBContext.SaveChangesAsync(token);
      List<WebhookSubscription> serverSubscriptions = _webhookDBContext.WebhookSubscriptions.Where<WebhookSubscription>(x => x.ServerName == webhookServer.ServerName).ToList();
      _logger.LogTrace("[{Method}] Allsubscriptions from cache for server {@subscriptions}", "DeleteWebhookServerAsync", serverSubscriptions);
      _webhookDBContext.RemoveRange(serverSubscriptions);
      await _webhookDBContext.SaveChangesAsync(token);
    //  _webhookServerCache.Remove(webhookServer);
     // _webhookSubscriptionCache.RemoveAll(x => x.ServerName == webhookServer.ServerName);
      await _webhookDBContext.SaveChangesAsync();
      _logger.LogDebug("[{Method}] Delete webhook server - Ends", "DeleteWebhookServerAsync");
    }
    /// <summary>
    /// Process raised event by adding it to the event queue.
    /// </summary>
    /// <param name="eventName">Event name </param>
    /// <param name="payload">Object involve in the action</param>
    /// <param name="token">cancellation Token</param>
    /// <returns>void</returns>
    public async Task RaiseWebhookEventAsync(string eventName, object payload, CancellationToken token = default(CancellationToken)) {
      //Cretae Event object
      _logger.LogDebug("[{Method}] Raise webhook event {event} - Starts", "RaiseWebhookEventAsync", eventName);
      WebhookEvent eventQueue = new WebhookEvent {
        ID = new Guid(),
        EventName = eventName,
        Payload = JsonConvert.SerializeObject(payload),
        CreatedDate = DateTime.UtcNow,
      };
      //Add event to EventQueue for processing
      _webhookDBContext.Add<WebhookEvent>(eventQueue);
      await _webhookDBContext.SaveChangesAsync();
      _logger.LogDebug("[{Method}] Raise webhook event ends", "RaiseWebhookEventAsync");
    }

    /// <summary>
    /// Process raised event by adding it to the event queue.
    /// </summary>
    /// <param name="eventName">Event name </param>
    /// <param name="payload">Object involve in the action</param>
    /// <returns>Void</returns>
    /// <remarks>Synchronous version of RaiseWebhook Event</remarks>
    public void RaiseWebhookEvent(string eventName, object payload) {
      //Create Event Object
      _logger.LogDebug("[{Method}] Raise webhook event-Sync version {event} starts", "RaiseWebhookEvent", eventName);
      WebhookEvent eventQueue = new WebhookEvent {
        ID = new Guid(),
        EventName = eventName,
        Payload = JsonConvert.SerializeObject(payload),
        CreatedDate = DateTime.UtcNow,
      };
      //Save chnages to DB
      _webhookDBContext.Add<WebhookEvent>(eventQueue);
      _webhookDBContext.SaveChanges();
      _logger.LogDebug("[{Method}] Raise webhook event ends", "RaiseWebhookEvent");

    }
    #endregion

    #region  Webhook Subscriber methods
    /// <summary>
    /// Initialize the local cache from Database
    /// </summary>
    public void InitializeLocalSubscriptionCache() {
      _logger.LogDebug("[{Method}] Initializes webhook subscription cache", "InitializeLocalSubscriptionCache");
     // _webhookSubscriptionCache = _webhookDBContext.GetAllWebhookSubscriptions();
    }
    /// <summary>
    /// Gets all the WebhookSubscriptions 
    /// </summary>
    /// <returns></returns>
    public async Task<List<WebhookSubscription>> GetAllWebhookSubscriptionsAsync(CancellationToken token = default(CancellationToken)) {
      List<WebhookSubscription> subscriptions = await _webhookDBContext.GetAllWebhookSubscriptionsAsync(token);
      _logger.LogTrace("[{Method}] Gets all webhook subscription {@subscription}", "GetAllWebhookSubscriptionsAsync", subscriptions);
      return subscriptions;
    }
    /// <summary>
    /// Adds Webhook Subscription
    /// </summary>
    /// <param name="webhookSubscription"> Webhook Subscription object that need to be added</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Added Subscription Entity</returns>
    /// <remarks>Subscription Name is suppose to be unique and if Subscription already exist for the Server Existing Subscription will be used.</remarks>
    public async Task<WebhookSubscription> AddWebhookSubscriptionAsync(WebhookSubscription webhookSubscription, CancellationToken token = default(CancellationToken)) {
      _logger.LogDebug("[{Method}] Adds webhook subscription starts", "AddWebhookSubscriptionAsync");
      _logger.LogTrace("[{Method}] Adds webhook subscription {@subscription} starts", "AddWebhookSubscriptionAsync", webhookSubscription);
      WebhookServer webhookServerFromCache = _webhookDBContext.WebhookServers.FirstOrDefault<WebhookServer>(x => x.ServerName == webhookSubscription.ServerName);
      //If exists get it
      if (webhookServerFromCache == null) {
        _logger.LogDebug("[{Method}] Webhook Server for subscription not exists", "AddWebhookSubscriptionAsync");
        return null;
      }
      //Check Subscription already exists or not
      WebhookSubscription webhookSubscriptionFromCache = _webhookDBContext.WebhookSubscriptions.FirstOrDefault<WebhookSubscription>(x => x.ServerName == webhookSubscription.ServerName && x.SubscriptionName == webhookSubscription.SubscriptionName);
      //If exists get it
      if (webhookSubscriptionFromCache != null) {
        _logger.LogDebug("[{Method}] Webhook subscription already exists", "AddWebhookSubscriptionAsync");
        return webhookSubscriptionFromCache;
      }

      // Note - Not used now Validate Subscription events and remove events not available from Webhookserver
      // webhookSubscription = ValidateSubscriptionEvents(webhookSubscription, webhookServerFromCache);
      //Else, Add Subscription to DB
      webhookSubscription.ID = new Guid();
      _webhookDBContext.Add<WebhookSubscription>(webhookSubscription);
      await _webhookDBContext.SaveChangesAsync();

      //Add new object to Cache;
     // _webhookSubscriptionCache.Add(webhookSubscription);
      _logger.LogDebug("[{Method}] Add Webhook subscription ends", "AddWebhookSubscriptionAsync");
      return webhookSubscription;

    }

    /// <summary>
    /// Updates webhook Subscription
    /// </summary>
    /// <param name="webhookSubscription"> Webhook Subscription object that need to be updated</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Updated subscription Entity</returns>
    public async Task<WebhookSubscription> UpdateWebhookSubscriptionAsync(WebhookSubscription webhookSubscription, CancellationToken token = default(CancellationToken)) {
      _logger.LogDebug("[{Method}] Update Webhook subscription starts", "UpdateWebhookSubscriptionAsync");
      _logger.LogTrace("[{Method}] Update Webhook subscription {@subscription}", "UpdateWebhookSubscriptionAsync", webhookSubscription);
      WebhookServer webhookServerFromCache = _webhookDBContext.WebhookServers.FirstOrDefault<WebhookServer>(x => x.ServerName == webhookSubscription.ServerName);
      //If exists get it
      if (webhookServerFromCache == null) {
        _logger.LogDebug("[{Method}] Webhook server for subscription not exists", "UpdateWebhookSubscriptionAsync");
        return null;
      }
      //Check Subscription already exists or not
      WebhookSubscription webhookSubscriptionFromCache = _webhookDBContext.WebhookSubscriptions.FirstOrDefault<WebhookSubscription>(x => x.ServerName == webhookSubscription.ServerName && x.SubscriptionName == webhookSubscription.SubscriptionName);
      //If exists get it
      if (webhookSubscriptionFromCache == null) {
        _logger.LogDebug("[{Method}] Webhook subscription for subscription not exists", "UpdateWebhookSubscriptionAsync");
        return null;
      }
      //Else update Subscriber to Database
      _webhookDBContext.Update<WebhookSubscription>(webhookSubscription);
      await _webhookDBContext.SaveChangesAsync();

      //Update local Cache
      //_webhookSubscriptionCache.Remove(webhookSubscriptionFromCache);
     // _webhookSubscriptionCache.Add(webhookSubscription);
      _logger.LogDebug("[{Method}] Update Webhook subscription ends", "UpdateWebhookSubscriptionAsync");
      return webhookSubscription;
    }

    /// <summary>
    /// Deletes webhook Subscription
    /// </summary>
    /// <param name="serverName"> Server NAme</param>
    /// <param name="SubscriptionName"> Subscription NAme</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>void</returns>
    public async Task DeleteWebhookSubscriptionAsync(string serverName, string subscriptionName, CancellationToken token = default(CancellationToken)) {
      //Get  Subscription 
      //Check Subscription already exists or not
      _logger.LogDebug("[{Method}] Delete Webhook subscription starts", "DeleteWebhookSubscriptionAsync");
      _logger.LogTrace("[{Method}] Delete Webhook subscription for server {@server} and subscription {@suscription}", "DeleteWebhookSubscriptionAsync", serverName, subscriptionName);
      WebhookSubscription webhookSubscription;
      webhookSubscription = _webhookDBContext.WebhookSubscriptions.FirstOrDefault<WebhookSubscription>(x => x.ServerName == serverName && x.SubscriptionName == subscriptionName);      //Remove from DB
      if (webhookSubscription != null) {
        _webhookDBContext.Remove<WebhookSubscription>(webhookSubscription);
        await _webhookDBContext.SaveChangesAsync();
        //Remove from Cache
//        _webhookSubscriptionCache.Remove(webhookSubscription);
      }
      _logger.LogDebug("[{Method}] Delete Webhook subscription ends", "DeleteWebhookSubscriptionAsync");
    }
    #endregion

    #region EventQueue
    /// <summary>
    /// Gets all the WebhookServers for the Event
    /// </summary>
    /// <returns></returns>
    public List<WebhookServer> GetWebhookServersByEvent(string eventName) {
      //Gets all the Servers listing to the given Event  
      List<WebhookServer> servers = _webhookDBContext.WebhookServers.Where<WebhookServer>(x => x.ServerEvents.Contains(eventName)).ToList();
      _logger.LogDebug("[{Method}] Got Webhook servers for Event {@event} and ServerList {@servers}", "GetWebhookServersByEvent", eventName, servers);
      return servers;
    }

    /// <summary>
    /// Gets all the WebhookSubscriptions of Event for given server
    /// </summary>
    /// <returns></returns>
    public List<WebhookSubscription> GetWebhookSubscriptionsByEvent(string eventName, string serverName) {
      //Gets list of all subscription which are subscribed for the given event on the given server.
      List<WebhookSubscription> subscriptions = _webhookDBContext.WebhookSubscriptions.Where<WebhookSubscription>(x => x.SubscribeEvents.Contains(eventName) && x.ServerName == serverName).ToList();
      _logger.LogDebug("[{Method}] Got Webhook servers for Event {@event} and subscriptionList {@subscriptions}", "GetWebhookServersByEvent", eventName, subscriptions);
      return subscriptions;
    }

    /// <summary>
    /// Gets all the WebhookServers for the Event
    /// </summary>
    /// <returns></returns>
    public async Task<List<WebhookEvent>> GetWebhookEventQueueAsync(CancellationToken token) {
      //Gets all the Servers listing to the given Event  
      List<WebhookEvent> events = await _webhookDBContext.GetWebhookEventQueueAsync(token);
      _logger.LogDebug("[{Method}] Got Webhook events  {@event} ", "GetWebhookEventQueueAsync", events);
      return events;
    }

    /// <summary>
    /// Deletes webhook Event
    /// </summary>

    public async Task DeleteWebhookEventAsync(WebhookEvent webhookEvent, CancellationToken token = default(CancellationToken)) {
      //Get  Event 
      //Check Subscription already exists or not
      _logger.LogDebug("[{Method}] Delete Webhook Event starts", "DeleteWebhookEventAsync");
      _logger.LogTrace("[{Method}] Delete Webhook Event {@event}", "DeleteWebhookEventAsync", webhookEvent);
      await _webhookDBContext.DeleteWebhookEventAsync(webhookEvent, token);
      await _webhookDBContext.SaveChangesAsync();

      _logger.LogDebug("[{Method}] Delete Webhook event ends", "DeleteWebhookEventAsync");
    }
    #endregion

    #region DeliveryLog

    /// <summary>
    /// Gets all the pending event by subscription
    /// </summary>
    /// <returns>List<WebhookEvent></WebhookEvent></returns>
    public async Task<List<WebhookEventDeliveryLog>> GetPendingDeliveryLogAsync(string subscriptionName, string serverName, CancellationToken token) {
      List<WebhookEventDeliveryLog> pendingDeliveryLog = await _webhookDBContext.GetPendingWebhookDeliveryLogAsync(subscriptionName, serverName, token);
      _logger.LogDebug("[{Method}] Got Webhook servers pending events {@event} and subscription {@subscription}", "GetPendingDeliveryLogAsync", pendingDeliveryLog, subscriptionName);
      return pendingDeliveryLog;
    }

    /// <summary>
    /// Gets all the pending event by subscription
    /// </summary>
    /// <returns>List<WebhookEvent></WebhookEvent></returns>
    public async Task<List<WebhookEventDeliveryLog>> GetWebhookDeliveryLogForDispatchAsync( CancellationToken token) {
      List<WebhookEventDeliveryLog> pendingDeliveryLog = await _webhookDBContext.GetWebhookDeliveryLogForDispatchAsync(token);
      _logger.LogDebug("[{Method}] Got pending events {@event} ", "GetWebhookDeliveryLogForDispatchAsync", pendingDeliveryLog);
      return pendingDeliveryLog;
    }

    /// <summary>
    /// Mark the list of events success by Client - Not used
    /// </summary>
    /// <param name="eventLogIds">List of Delivery log id </param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>void</returns>
    public async Task MarkPendingDeliveryLog(List<Guid> eventLogIds, CancellationToken token) {
      foreach (Guid id in eventLogIds) {
        WebhookEventDeliveryLog log = _webhookDBContext.WebhookEventDeliveryLog.Find(id);
        if (log != null) {
          log = WebhookEventHelper.SetDeilveryLogForSucess(log);
          _webhookDBContext.WebhookEventDeliveryLog.Update(log);
          //Save all the Delivery log chnages to DB
          await _webhookDBContext.SaveChangesAsync(token);
        }
      }


    }

    /// <summary>
    /// Adds Event to WebhookEventDeliveryLog
    /// <para>1. Generate DeliveryLogobject from EventQueue</para>
    /// <para>2. Add to Delivery Log</para>
    /// <para>3. Remove/Dequeue the Event from EventQueue</para>
    /// </summary>
    /// <param name="subscription">Webhook Subscription for the Event</param>
    /// <param name="webhookEvent">Webhook Event to Deliver</param>
    /// <returns>Void</returns>
    public async Task AddWebhookEventToDeliveryLogAsync(WebhookSubscription subscription, WebhookEvent webhookEvent) {
      //1. Generate DeliveryLogobject from EventQueue
      _logger.LogDebug("[{Method}] Add Deilivery log - starts ", "AddWebhookEventToDeliveryLogAsync");
      WebhookEventDeliveryLog deliveryObject = WebhookEventHelper.GetDeliveryLogObject();
      deliveryObject.ServerName = subscription.ServerName;
      deliveryObject.SubscriptionName = subscription.SubscriptionName;
      deliveryObject.EventName = webhookEvent.EventName;
      deliveryObject.SubscriptionCallBack = subscription.CallbackEndPoint;
      deliveryObject.Payload = webhookEvent.Payload;
      deliveryObject.EventQueueTime = DateTime.UtcNow;
     
      // 2. Add to Delivery Log
      _logger.LogTrace("[{Method}] Add Deilivery log {@deliveryObject}", "AddWebhookEventToDeliveryLogAsync", deliveryObject);
      await _webhookDBContext.AddEventToDeliveryLogAsync(deliveryObject);
      await _webhookDBContext.SaveChangesAsync();
      _logger.LogDebug("[{Method}] Add Deilivery log ends", "AddWebhookEventToDeliveryLogAsync");
    }

    /// <summary>
    /// Update the Delivery Log item for Subscription Initialization, 
    /// So that It can be processed again by scheduler
    /// </summary>
    /// <param name="log">Delivery Log Object to be updated</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>void</returns>
    public async Task UpdateDeliveryLogForInitAsync(WebhookEventDeliveryLog log, CancellationToken token) {
      if (log == null) return;
      _logger.LogDebug("[{Method}] Update Deilivery log for init starts", "UpdateDeliveryLogForInitAsync");
      _logger.LogDebug("[{Method}] Update Deilivery log for init log {@deliverylog}", "UpdateDeliveryLogForInitAsync", log);
      //Set the Deliverylog object to Initial state
      log = WebhookEventHelper.SetDeilveryLogForInit(log);
      //Update to DB
      _webhookDBContext.WebhookEventDeliveryLog.Update(log);
      await _webhookDBContext.SaveChangesAsync();
      _logger.LogDebug("[{Method}] Update Deilivery log for init ends", "UpdateDeliveryLogForInitAsync");
    }

    /// <summary>
    /// Update events to Delivery log for Success/Failure
    /// </summary>
    /// <param name="deliveryLog">List of delivery log to be updated</param>
    /// <param name="isSuccess">Mark the Delivery Log as Sucess or failure</param>
    /// <returns>void</returns>
    public async Task UpdateEventToDeliveryLogAsync(List<WebhookEventDeliveryLog> deliveryLog, bool isSuccess, CancellationToken token) {
      await _webhookDBContext.UpdateEventToDeliveryLogAsync(deliveryLog, isSuccess, token);
      await _webhookDBContext.SaveChangesAsync(token);

    }

    #endregion

    #region Server Processing Start/Shudown
    /// <summary>
    /// Raise Server start Event on Server Initialization
    /// </summary>
    public void ProcessServerStart() {
      //Raise event and Adds tothe EventQueue
      _logger.LogDebug("[{Method}] Server start event raised", "ProcessServerStart");
      RaiseWebhookEvent(WebhookServerConstants.EventServerStart, "");
    }
    /// <summary>
    /// Notify all the subscriptions about the server shutdown
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>void</returns>
    /// <remarks>Should be called from graceful shutdown event
    /// Consider the IhostService StopAsyn iscalled on Shuting down, so called from there</remarks>
    public async Task ProcessServerShutDownASync(CancellationToken token) {
      _logger.LogDebug("[{Method}] Server shut down process starts", "ProcessServerShutDownASync");
      List<WebhookSubscription> subscriptions = await GetAllWebhookSubscriptionsAsync(token);
      List<WebhookServer> servers = await GetAllWebhookServersAsync(token);
      _logger.LogTrace("[{Method}] Server shut down processed servers {@servers} and subscriptions {@subscriptions}", "ProcessServerShutDownASync", servers, subscriptions);
      //Loop for all the servers hosted on this host
      foreach (WebhookServer s in servers) {
        List<WebhookSubscription> ServerSubscriptions = subscriptions.FindAll(x => x.ServerName == s.ServerName);
        foreach (WebhookSubscription subscription in ServerSubscriptions) {
          //Check the subscription has callback end point for shutdown event or not, If it has call it
          if (!string.IsNullOrEmpty(subscription.ServerShutDownCallBackEndPoint)) {
            _logger.LogDebug("[{Method}] Server shut down event send on endpoint {@endpoint}", "ProcessServerShutDownASync", subscription.ServerShutDownCallBackEndPoint);
            await ServerShutDownNotificationAsync(s.ServerName, subscription.SubscriptionName, subscription.ServerShutDownCallBackEndPoint, token);
          }
          else {
            _logger.LogDebug("[{Method}] Server shut down event end point is not defined for subscription {@subscription}", "ProcessServerShutDownASync", subscription.SubscriptionName);
          }
        }
      }
      _logger.LogDebug("[{Method}] Server shut down processed ends", "ProcessServerShutDownASync");
    }
    #endregion

    #region SupportMethods
    //Not used
    private WebhookSubscription ValidateSubscriptionEvents(WebhookSubscription webhookSubscription, WebhookServer webhookServer) {
      List<string> validEvents = new List<string>();
      //Check event exists in server list or not
      foreach (string subscriberEvent in webhookSubscription.SubscribeEventsAsList) {
        if (webhookServer.ServerEvents.Contains(subscriberEvent))
          validEvents.Add(subscriberEvent);
      }
      //Assign valid event back to the object;
      webhookSubscription.SubscribeEventsAsList = validEvents;
      return webhookSubscription;
    }

    /// <summary>
    /// call Subscription's server shutdown Endpoint 
    /// <summary>
    /// <param name="serverName">Name of the Server shuting down</param>
    /// <param name="subscriptionShutDownEndPoint">Subscription End point for Http Call</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>void</returns>
    private async Task ServerShutDownNotificationAsync(string serverName, string subscriptionName, string subscriptionShutDownEndPoint, CancellationToken token) {
      //Create HttpClient
      //Send request to the calbackendpoint 
      //Send List of Event with payload object
      _logger.LogDebug("[{Method}] Server shut down notification for server {@server} and subscription {@subscription} starts", "ServerShutDownNotificationAsync", serverName, subscriptionName);
      HttpClient client = new HttpClient();
      WebhookServerSubscriptionDTO dto = new WebhookServerSubscriptionDTO();
      dto.ServerName = serverName;
      dto.SubscriptionName = subscriptionName;
      HttpRequestProcessor processor = new HttpRequestProcessor(client);
      try {
        _logger.LogDebug("[{Method}] Http Call with body {@body} on endpoint {@endpoint}", "ServerShutDownNotificationAsync", dto, subscriptionName, subscriptionShutDownEndPoint);
        await processor.ExecutePOSTRequestAsync<WebhookServerSubscriptionDTO>(subscriptionShutDownEndPoint, "", AcceptMediaTypeEnum.JSON, null, null, null, dto);
      }
      catch (Exception ex) {
        //Log error and return
        _logger.LogError(ex.Message, ex);
      }
    }

    #endregion
  }
}

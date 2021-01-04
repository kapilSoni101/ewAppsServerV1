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
using ewApps.Core.ServiceProcessor;
using ewApps.Core.Webhook.Subscription;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.Webhook.Subscriber {
    /// <summary>
    /// WebhookSubscriberManager class manages all Webhook Subscriptions in the app.
    /// It is a singleton class. It provides support for:
    /// <para>(1) Callback endpoint for client.</para>
    /// <para>(2) Manage subscriptions. </para> 
    /// <para>(3) Handle all events received by client.</para>
    /// <para>(4) Send Notification to the Webhook Server for the Subscription Initialization, to get all the Pending events.</para>
    /// <para>(5) Server ShutDown event is Handled and Added as the Event to the EventQueue, 
    ///      And will be handled by the Subscribers as other events.</para>
    /// </summary>
    public class WebhookSubscriptionManager {

    #region Constructor and property
    private WebhookSubscriptionDBContext _webhookDBContext;
    private IOptions<WebhookSubscriptionAppSettings> _webhookappSettings;
    private IWebhookEventDelegate _eventDelegate;

    // This is a cache to store List<WebhookSubscription> items. 
    // private List<WebhookSubscription> _webhookSubscriptionCache;
    private ILogger<WebhookSubscriptionManager> _logger;
    //Id will be Key and Delegate will be value

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbContext">DB constant Instance</param>
    /// <param name="appSetting">App Setting Instance</param>
    public WebhookSubscriptionManager(WebhookSubscriptionDBContext dbContext,IWebhookEventDelegate eventDelegate, IOptions<WebhookSubscriptionAppSettings> webhookappSettings, ILogger<WebhookSubscriptionManager> logger) {
     // IsInitialized = false;
      _logger = logger;
      _logger.LogInformation("[{Method}] Service initialized", "WebhookSubscriptionController");
      _webhookDBContext = dbContext;
      _webhookappSettings = webhookappSettings;
      _eventDelegate = eventDelegate;
      //InitializeLocalSubscriptionCache();
      //Initialize Subscriber at Server so it can Send all pending events if Required
      _logger.LogDebug("[{Method}] Notify webhook server for subscription availability", "WebhookSubscriptionController");
    // await NotifyWebhookServerAsync();
     // IsInitialized = true;
    }
    /// <summary>
    /// Checks webhook server manager initialization is completed or not.
    /// </summary>
    //public bool IsInitialized {
    //  get; set;
    //}
    /// <summary>
    /// Controller  Callback endpoint for Subscription
    /// </summary>
    public string SubscriptionCallBack {
      get {
        return WebhookSubscriptionConstants.SubscriptionCallbackEndPoint;
      }
    }

    /// <summary>
    /// Controller  Callback endpoint for Subscription
    /// </summary>
    public string ServerShutDownCallBackEndPoint {
      get {
        return WebhookSubscriptionConstants.ServerShutDownCallbackEndPoint;
      }
    }

    #endregion

    #region  Webhook Subscriber methods
    /// <summary>
    /// Initialize the local cache from Database
    /// </summary>
    //public void InitializeLocalSubscriptionCache() {
    //  _logger.LogDebug("[{Method}] Initializes Cache from DB", "InitializeLocalSubscriptionCache");
    ////  _webhookSubscriptionCache = _webhookDBContext.GetAllWebhookSubscriptions();
    //  _logger.LogTrace("[{Method}] Gets webhook subscription from DB {@subscriptionsCache}", "InitializeLocalSubscriptionCache", _webhookSubscriptionCache);

    //}
    /// <summary>
    /// Gets all the WebhookSubscriptions 
    /// </summary>
    /// <returns>List of Webhook Subscriptions</returns>
    public async Task<List<WebhookSubscription>> GetAllWebhookSubscriptionsAsync(CancellationToken token = default(CancellationToken)) {
      _logger.LogDebug("[{Method}] Gets webhook subscription from DB", "GetAllWebhookSubscriptionsAsync");
      List<WebhookSubscription> subscriber = await _webhookDBContext.GetAllWebhookSubscriptionsAsync(token);
      _logger.LogTrace("[{Method}] Gets webhook subscription from DB {@subscriptions}", "GetAllWebhookSubscriptionsAsync", subscriber);

      return subscriber;
    }

    /// <summary>
    /// Gets  the WebhookSubscription 
    /// </summary>
    /// <returns>Webhook Subscriptions</returns>
    public WebhookSubscription GetWebhookSubscription(string subscriptionName) {
      _logger.LogDebug("[{Method}] Gets webhook subscription from cache", "GetWebhookSubscription");
      
      WebhookSubscription subscription = _webhookDBContext.WebhookSubscriptions.FirstOrDefault<WebhookSubscription>(x => x.SubscriptionName == subscriptionName);
      _logger.LogDebug("[{Method}] Gets webhook subscription from cache {@subscription}", "GetWebhookSubscription", subscription);
      return subscription;
    }

    /// <summary>
    /// Adds Webhook Subscription
    /// </summary>
    /// <returns>Added webhook Subscription Entity</returns>
    public async Task<WebhookSubscription> AddWebhookSubscriptionAsync(WebhookSubscription webhookSubscription, CancellationToken token = default(CancellationToken)) {
      //Check Subscription already exists or not
      _logger.LogDebug("[{Method}] processing start", "AddWebhookSubscriptionAsync");
      _logger.LogTrace("[{Method}] processing start for webhookSubscription {@webhookSubscription}", "AddWebhookSubscriptionAsync", webhookSubscription);
      WebhookSubscription webhookSubscriptionFromCache = _webhookDBContext.WebhookSubscriptions.FirstOrDefault<WebhookSubscription>(x => x.ServerName == webhookSubscription.ServerName && x.SubscriptionName == webhookSubscription.SubscriptionName);
      //If exists get it and reattach the delegate
      if (webhookSubscriptionFromCache != null) {
      //  Dictionary<string, WebhookEventDelegate.WebhookEventHandlerAsync> dic = new Dictionary<string, WebhookEventDelegate.WebhookEventHandlerAsync>();
      //  dic.Add(webhookSubscription.SubscriptionName, webhookSubscription.EventDelegation);
      // // await AttachDelegatesAsync(dic);
        return webhookSubscriptionFromCache;
      }
      webhookSubscription.ID = new Guid(); //Primary Key
      await _webhookDBContext.AddAsync<WebhookSubscription>(webhookSubscription, token);
      _logger.LogDebug("[{Method}] Subscription added to local.", "AddWebhookSubscriptionAsync");
      //Add Subscription to Server
      bool sucess = await AddSubscriptionToServerAsync(webhookSubscription, token);
      if (sucess) {
        _logger.LogDebug("[{Method}] Subscription Added to server", "AddWebhookSubscriptionAsync");
        await _webhookDBContext.SaveChangesAsync();
        //Add new object to Cache;
      //  _webhookSubscriptionCache.Add(webhookSubscription);
        _logger.LogDebug("[{Method}] Subscription added to cache", "AddWebhookSubscriptionAsync");
      }
      else {
        _logger.LogDebug("[{Method}] Subscription Not Added to server", "AddWebhookSubscriptionAsync");
      }
      return webhookSubscription;
    }
    /// <summary>
    /// Updates webhook Subscription
    /// </summary>
    /// <returns>Updated webhook Subscription Entity</returns>
    public async Task<WebhookSubscription> UpdateWebhookSubscriptionAsync(WebhookSubscription webhookSubscription, CancellationToken token = default(CancellationToken)) {
      //Check Subscription already exists or not
      _logger.LogDebug("[{Method}] Update Subscription starts", "UpdateWebhookSubscriptionAsync");
      _logger.LogTrace("[{Method}] Update Subscription for subscription {@subscription}", "UpdateWebhookSubscriptionAsync", webhookSubscription);
      WebhookSubscription webhookSubscriptionFromCache = _webhookDBContext.WebhookSubscriptions.FirstOrDefault<WebhookSubscription>(x => x.ServerName == webhookSubscription.ServerName && x.SubscriptionName == webhookSubscription.SubscriptionName);
      //If exists get it
      if (webhookSubscriptionFromCache == null) {
        _logger.LogDebug("[{Method}] Subscription Not exists in cache", "UpdateWebhookSubscriptionAsync");
        return null;
      }
      //Else update Subscriber to Database
      _webhookDBContext.Update<WebhookSubscription>(webhookSubscription);

      //Update Subscription to Server
      bool sucess = await UpdateSubscriptionToServerAsync(webhookSubscription, token);
      if (sucess) {
        await _webhookDBContext.SaveChangesAsync();
        //Update local Cache
        //_webhookSubscriptionCache.Remove(webhookSubscriptionFromCache);
       // _webhookSubscriptionCache.Add(webhookSubscription);
        _logger.LogDebug("[{Method}] Subscription Updated", "UpdateWebhookSubscriptionAsync");
      }
      else {
        _logger.LogDebug("[{Method}] Subscription Not updated to server", "UpdateWebhookSubscriptionAsync");
      }
      return webhookSubscription;
    }

    /// <summary>
    /// Deletes webhook Subscription
    /// </summary>
    /// <returns>void</returns>
    public async Task DeleteWebhookSubscriptionAsync(WebhookSubscription webhookSubscription, CancellationToken token = default(CancellationToken)) {
      _logger.LogDebug("[{Method}] Subscription delete - start", "DeleteWebhookSubscriptionAsync");
      _webhookDBContext.Remove<WebhookSubscription>(webhookSubscription);
      //TODO :Remove Subscription from the Host server
      await _webhookDBContext.SaveChangesAsync(token);
     // _webhookSubscriptionCache.Remove(webhookSubscription);
      _logger.LogDebug("[{Method}] Subscription delete - ends", "DeleteWebhookSubscriptionAsync");
    }
    /// <summary>
    /// Attach the Delegate to the Subscription, 
    /// It should be called at the Time of Initialization
    /// To make sure all the Subscriptions has Delegates by the implementor.
    /// It is also called on Add Subscription to add Delegate for given Subscription.
    /// </summary>
    /// <param name="eventDelegates">Dictionary - Subscription Name and EventDelegate</param>
    /// <returns>void</returns>
    /// <remarks>It adds Delegates to the given list of subscribtion, but does not alter any other subscription</remarks>
    //public async Task AttachDelegatesAsync(Dictionary<string, WebhookEventDelegate.WebhookEventHandlerAsync> eventDelegates) {
    //  //Loop for all the items in the dictionary (here key is the name of Subscription)
    //  foreach (string s in eventDelegates.Keys) {
    //    //Get Subscription From Cache
    //    WebhookSubscription webhookSubscription = GetWebhookSubscription(s);
    //    //Attache delegate
    //    webhookSubscription.EventDelegation = eventDelegates[s];
    //    //Refresh Subscription in Cache 
    //   // _webhookSubscriptionCache.Remove(webhookSubscription);
    //   // _webhookSubscriptionCache.Add(webhookSubscription);
    //  }
    //}

    #endregion

    #region EventQueue

    /// <summary>
    /// Add Event to Database Event queue received from Server
    /// </summary>
    /// <param name="events">List of WebhookEvents Received</param>

    public async Task AddEventsToEventQueueAsync(List<WebhookEvent> events) {
      _logger.LogDebug("[{Method}] Add Events to event queue start", "AddEventsToEventQueueAsync");
      _logger.LogTrace("[{Method}] Add Events to event queue {@events}", "AddEventsToEventQueueAsync", events);
      //Loop for all events
      foreach (WebhookEvent webhookEvent in events) {
        //Generate New Event
        webhookEvent.ID = new Guid();
        webhookEvent.ReceivedDate = DateTime.UtcNow;
        webhookEvent.Status = "UnProcessed";
        //Add to DB, so that It can be handled by Event handler Scheduler
        _logger.LogTrace("[{Method}] Add Event {@event}", "AddEventsToEventQueueAsync", webhookEvent);
        await _webhookDBContext.AddAsync(webhookEvent);
      }
      //Save Changes to DB
      await _webhookDBContext.SaveChangesAsync();
      _logger.LogDebug("[{Method}] Add Events to event queue ends", "AddEventsToEventQueueAsync");

    }

    /// <summary>
    /// Execute Event delegate 
    /// </summary>
    /// <param name="subscription"> webhook subscription which will handle the Event by Delegate</param>
    /// <param name="events">List of Event that are queued to handle by this Subscription </param>
    /// <param name="token">Cancellation Token</param>
    public async Task ExecuteEventsAsync(WebhookSubscription subscription, List<WebhookEvent> events, CancellationToken token) {
      //Invoke the Method Delegate if Exists.
      _logger.LogDebug("[{Method}] event delegation start", "ExecuteEventsAsync");
      await _eventDelegate.WebhookEventHandlerAsync(subscription, events, token);
      //await subscription.EventDelegation?.Invoke(subscription, events, token);
      _logger.LogDebug("[{Method}] event delegation Stop", "ExecuteEventsAsync");
    }

    /// <summary>
    /// Execute Server ShutDown Event, It calls Same Subscription Delegate with Server.Shutdown Event.
    /// </summary>
    /// <param name="serverName">WebhookServer Name</param>
    /// <param name="subscriptionName">Webhook  supbscription Name</param>
    ///<remarks>This Event will call the Same Subscription Delegate with Server.ShutDown Event Name</remarks>
    public async Task ExecuteServerShutDownEventAsync(string serverName, string subscriptionName, CancellationToken token) {
      //Get Subscription 
      _logger.LogDebug("[{Method}] Server shut down event handler server {@server} and subscription {subscription}", "ExecuteServerShutDownEventAsync", serverName, subscriptionName);
      WebhookSubscription subscription = GetWebhookSubscription(subscriptionName);
      //Get Event Object for ShutDown
      WebhookEvent e = new WebhookEvent();
      e.ServerName = serverName;
      e.SubscriptionName = subscriptionName;
      e.EventName = WebhookSubscriptionConstants.ServerStopEvent;
      List<WebhookEvent> events = new List<WebhookEvent>();
      events.Add(e);
      //Execute Delegate attached for Shutdown
      await _eventDelegate.WebhookEventHandlerAsync(subscription, events, token);
      //await subscription.EventDelegation?.Invoke(subscription, events, token);
    }

    #endregion

    #region Support
    /// <summary>
    /// Add Subscription to Server by Http Client 
    ///  </summary>
    /// <param name="token"></param>
    /// <returns>Boolean for success</returns>
    private async Task<bool> AddSubscriptionToServerAsync(WebhookSubscription subscription, CancellationToken token) {
      //Create HttpClient
      //Send request to the Serverendpoint 

      HttpClient client = new HttpClient();
      HttpRequestProcessor processor = new HttpRequestProcessor(client);
      string serverEndPoint = subscription.ServerHostEndPoint + WebhookSubscriptionConstants.AddSubscriptionServerEndPoint;

      WebhookSubscriptionDTO addSubscription = new WebhookSubscriptionDTO();
      addSubscription.CallbackEndPoint = subscription.CallBackEndPoint;
      addSubscription.IsActive = subscription.IsActive;
      addSubscription.ServerName = subscription.ServerName;
      addSubscription.SubscribeEvents = subscription.SubscribedEvents;
      addSubscription.SubscriptionName = subscription.SubscriptionName;
      //string eventJson = JsonConvert.SerializeObject(addSubscription);
      try {

        await processor.ExecutePOSTRequestAsync<WebhookSubscriptionDTO>(serverEndPoint, "", AcceptMediaTypeEnum.JSON, null, null, null, addSubscription);
        _logger.LogTrace("[{Method}] Add Server subscription {@subscription} on serverEndPoint {@endPoint}", "AddSubscriptionToServerAsync", addSubscription, serverEndPoint);
        return true;
      }
      catch (Exception ex) {
        _logger.LogError(ex.Message, ex);
        return false;
      }
    }

    /// <summary>
    /// Update Subscription to Server by Http Client 
    ///  </summary>
    /// <param name="token"></param>
    /// <returns>Boolean for success</returns>
    private async Task<bool> UpdateSubscriptionToServerAsync(WebhookSubscription subscription, CancellationToken token) {
      //Create HttpClient
      //Send request to the Serverendpoint 
      HttpClient client = new HttpClient();
      HttpRequestProcessor processor = new HttpRequestProcessor(client);
      string serverEndPoint = subscription.ServerHostEndPoint + WebhookSubscriptionConstants.UpdateSubscriptionServerEndPoint;
      //Create WebhookSubscription DTO from webhook Subscription Object
      WebhookSubscriptionDTO updateSubscription = new WebhookSubscriptionDTO();
      updateSubscription.CallbackEndPoint = subscription.CallBackEndPoint;
      updateSubscription.IsActive = subscription.IsActive;
      updateSubscription.ServerName = subscription.ServerName;
      updateSubscription.SubscribeEvents = subscription.SubscribedEvents;
      updateSubscription.SubscriptionName = subscription.SubscriptionName;
      try {
        await processor.ExecutePOSTRequestAsync<WebhookSubscriptionDTO>(serverEndPoint, "", AcceptMediaTypeEnum.JSON, null, null, null, updateSubscription);
        _logger.LogTrace("[{Method}] update Server subscription {@subscription} on serverEndPoint {@endPoint}", "UpdateSubscriptionToServerAsync", updateSubscription, serverEndPoint);
        return true;
      }
      catch (Exception ex) {
        _logger.LogError(ex.Message, ex);
        return false;
      }
    }

    /// <summary>
    /// Delete Subscription to Server by Http Client 
    ///  </summary>
    /// <param name="token"></param>
    /// <returns>Boolean for success</returns>
    private async Task<bool> RemoveSubscriptionFromServerAsync(WebhookSubscription subscription, CancellationToken token) {
      //Create HttpClient
      //Send request to the Serverendpoint 
      HttpClient client = new HttpClient();
      HttpRequestProcessor processor = new HttpRequestProcessor(client);
      string serverEndPoint = subscription.ServerHostEndPoint + WebhookSubscriptionConstants.RemoveSubscriptionServerEndPoint;
      //Create WebhookSubscription DTO from webhook Subscription Object
      WebhookServerSubscriberDTO subscriptionDTO = new WebhookServerSubscriberDTO();
      subscriptionDTO.ServerName = subscription.ServerName;
      subscriptionDTO.SubscriptionName = subscription.SubscriptionName;
      try {
        await processor.ExecutePOSTRequestAsync<WebhookServerSubscriberDTO>(serverEndPoint, "", AcceptMediaTypeEnum.JSON, null, null, null, subscriptionDTO);
        _logger.LogTrace("[{Method}] Remove Server subscription {@subscription} on serverEndPoint {@endPoint}", "RemoveSubscriptionFromServerAsync", subscriptionDTO, serverEndPoint);
        return true;
      }
      catch (Exception ex) {
        _logger.LogError(ex.Message, ex);
        return false;
      }
    }
    /// <summary>
    /// Call same server end point as of Add, Server will check and reatatch the subscription
    /// </summary>
    /// <returns>void</returns>
    public async Task NotifyWebhookServerAsync(CancellationToken token = default(CancellationToken)) {
      List<WebhookSubscription> subscriptions = await GetAllWebhookSubscriptionsAsync(token);
      List<WebhookServerSubscriberDTO> list = new List<WebhookServerSubscriberDTO>();

      foreach (WebhookSubscription s in subscriptions) {
        HttpClient client = new HttpClient();
        HttpRequestProcessor processor = new HttpRequestProcessor(client);
        string serverEndPoint = s.ServerHostEndPoint + WebhookSubscriptionConstants.SynchronizationServerEndPoint;
        WebhookServerSubscriberDTO dto = new WebhookServerSubscriberDTO();
        dto.ServerName = s.ServerName;
        dto.SubscriptionName = s.SubscriptionName;
        list.Add(dto);
        try {
          await processor.ExecutePOSTRequestAsync<WebhookServerSubscriberDTO>(serverEndPoint, "", AcceptMediaTypeEnum.JSON, null, null, null, dto);
          _logger.LogTrace("[{Method}] Notify Server for subscription availability {@subscription} on serverEndPoint {@endPoint}", "NotifyWebhookServerAsync", dto, serverEndPoint);

        }
        catch (Exception ex) {
          _logger.LogError(ex.Message, ex);
        }

      }

    }
    #endregion
  }

}

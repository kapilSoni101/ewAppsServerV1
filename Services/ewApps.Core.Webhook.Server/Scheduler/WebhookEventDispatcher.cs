/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ewApps.Core.Webhook.Server {
  /// <summary>
  /// Scheduler service running in background to dequeue Webhook Server's raised events 
  /// <para>1. Dequeue Events from EventQueue</para>
  /// <para>2. Add Event the Dispatch log for Subscriber</para>
  /// <para>3. Deliver Events to subscribers</para>
  /// 4. Maintain Event Delivery Status
  /// </summary>

  public class WebhookEventDispatcher :IHostedService, IDisposable {
    private readonly ILogger _logger;
    private IServiceScopeFactory _serviceScopeFactory;
    private WebhookServerAppSettings _appSettings;
    private CancellationTokenSource _stoppingCts = null;
    private Timer _timer;
    //WebhookServerManager _webhookServerManager;
    long iteration = 0;
    protected bool _inProgress = false;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Default microsoft Logger DI</param>
    /// <param name="scopeFactory">System Defined Factory class to get the dependencies</param>
    public WebhookEventDispatcher( ILogger<WebhookEventDispatcher> logger, IServiceScopeFactory scopeFactory, IOptions<WebhookServerAppSettings> appSetting) {
      _logger = logger;
     
     
      _serviceScopeFactory = scopeFactory;
      _appSettings = appSetting.Value;
      _logger.LogInformation("[{method}] Webhook Event Dispatcher Service Initialized", "WebhookEventDispatcher");
    }

    /// <summary>
    /// Starts the dispatch service on application start
    /// </summary>
    /// <param name="token">Cancellation token</param>
    /// <returns>void</returns>
    public Task StartAsync(CancellationToken token) {
      _logger.LogInformation("[{method}] Starting Webhook Server background Event Dispatcher Service", "StartAsync");
      _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(token);
      using (IServiceScope scope = _serviceScopeFactory.CreateScope())
      {
        WebhookServerManager webhookServerManager = scope.ServiceProvider.GetRequiredService<WebhookServerManager>();
        webhookServerManager.ProcessServerStart();
      }
      _timer = new Timer(OnTimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(WebhookServerConstants.DispatchServiceExecutionDelay));
      return Task.CompletedTask;
    }

    /// <summary>
    /// Stop triggers when application is shutdown gracefully
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StopAsync(CancellationToken cancellationToken) {
      _logger.LogInformation("[{method}] Stoping Webhook Server background Event Dispatcher Service", "StopAsync");
      // Stop called without start
      _timer?.Change(Timeout.Infinite, 0);

      _logger.LogDebug("[{method}] called ProcessServerShutDown event ", "StopAsync");
      using (IServiceScope scope = _serviceScopeFactory.CreateScope())
      {
        WebhookServerManager webhookServerManager = scope.ServiceProvider.GetRequiredService<WebhookServerManager>();
        //Call Server shutdown before ending the current process.
        await webhookServerManager.ProcessServerShutDownASync(cancellationToken);//Fire and forgot
      }
    }

    public virtual void Dispose() {
      _logger.LogInformation("[{method}] disposed Webhook Server background Event Dispatcher Service", "Dispose");
      _stoppingCts.Cancel();
    }

    private void OnTimerCallback(object state) {
      try {
        if (_inProgress == false) {
          iteration++;
          _inProgress = true;
          _logger.LogDebug("[{method}] Webhook Server Timer call back - Start, Execution Cycle {@cycle}", "OnTimerCallback",iteration);
          var task = Task.Run(async () => await ExecuteDispatcherAsync(_stoppingCts.Token));
          task.Wait();
         _inProgress = false;
          _logger.LogDebug("[{method}] Webhook Server Timer call back - Stop, Execution Cycle {@cycle}", "OnTimerCallback", iteration);

        }
      }
      catch (Exception ex) {
        _logger.LogError(ex.Message, ex);
        _inProgress = false;
      }
    }
    #region Support
    /// <summary>
    /// Execute operations on the scheduled time
    /// </summary>
    private async Task ExecuteDispatcherAsync(CancellationToken stoppingToken) {
      _logger.LogDebug("[{method}] started at {@time} ", "ExecuteDispatcherAsync", DateTime.UtcNow);
      // Create scope for ServerManager  to run under the singleton method  
      using (IServiceScope scope = _serviceScopeFactory.CreateScope()) {
        WebhookServerManager webhookServerManager = scope.ServiceProvider.GetRequiredService<WebhookServerManager>();

        //Check for WebhookServerInitialization
       // if (webhookServerManager.IsInitialized) {
          _logger.LogDebug("[{method}] AddEventsToDeliveryLog {@time} ", "ExecuteDispatcherAsync", DateTime.UtcNow);
          //Adds event to delivery Log and deletes from EventQueue
          await AddEventsToDeliveryLogAsync(stoppingToken, webhookServerManager);
          _logger.LogDebug("[{method}] Dispatch Events at {@time} ", "ExecuteDispatcherAsync", DateTime.UtcNow);
          //DispatchEvents to Subscibers
          await DispatchEventsAsync(stoppingToken, webhookServerManager);
       // }
      }
    }

    /// <summary>
    /// <para>1. It dequeue the events from EventQueue</para>
    /// <para>2. Gets event Server/Subscribers</para>
    /// <para>3. Add the Events to the Delivery Log</para>
    /// </summary>
    /// <param name="stoppingToken">Cancellation Token</param>
    /// <returns>void</returns>
    private async Task AddEventsToDeliveryLogAsync(CancellationToken stoppingToken,WebhookServerManager webhookServerManager) {
      //Get Events from EventQueue 
      List<WebhookEvent> events = await webhookServerManager.GetWebhookEventQueueAsync(stoppingToken);
      _logger.LogTrace("[{method}] collected events from Queue, Events - {@webhookEvents}", "AddEventsToDeliveryLogAsync", events);
      //Loopfor each event 
      int i = 1;
      foreach (WebhookEvent webhookEvent in events) {
        _logger.LogTrace("[{method}] For each loop For Event Dispatch:{eventName} ,Event Number{@number}", "AddEventsToDeliveryLogAsync", webhookEvent.EventName, i);
        //Gets all server which are listening to the Event
        List<WebhookServer> webhookServers = webhookServerManager.GetWebhookServersByEvent(webhookEvent.EventName);
        _logger.LogTrace("[{method}]  Webhook Server for Event {event} got servers {@servers}", "AddEventsToDeliveryLogAsync", webhookEvent.EventName, webhookServers);
        foreach (WebhookServer server in webhookServers) {
          _logger.LogTrace(" [{method}]  For each loop For server in event Dispatch:", "AddEventsToDeliveryLogAsync", server.ServerName);
          //For Each Event gets the Subscribers
          List<WebhookSubscription> webhookSubscribers = webhookServerManager.GetWebhookSubscriptionsByEvent(webhookEvent.EventName, server.ServerName);
          _logger.LogTrace("[{method}]  Webhook subscriptions for Event {event} and server {server} got subscriptions {@subscription}", "AddEventsToDeliveryLogAsync", webhookEvent.EventName, server.ServerName, webhookSubscribers);
          foreach (WebhookSubscription subscription in webhookSubscribers) {
            _logger.LogTrace(" [{method}]  For each loop For subscriber {subscriber}", "AddEventsToDeliveryLogAsync", subscription.SubscriptionName);
            //Add the Event in DispatcherEventLog and also Delete from Queue
            await webhookServerManager.AddWebhookEventToDeliveryLogAsync(subscription, webhookEvent);
            _logger.LogTrace(" [{method}]  added webhook events to delivery log for server {server} and subscription {subscriber} and event {event}", "AddEventsToDeliveryLogAsync", server.ServerName, subscription.SubscriptionName, webhookEvent.EventName);
          }

        }
        //3.Remove / Dequeue the Event from EventQueue
        await webhookServerManager.DeleteWebhookEventAsync(webhookEvent);
        i++;
      }
    }

    /// <summary>
    /// Send events to subscribers in the batch
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>void</returns>
    private async Task DispatchEventsAsync(CancellationToken token,WebhookServerManager webhookServerManager) {
      _logger.LogDebug("[{method}] starts ", "DispatchEventsAsync");
      //Get event from DispatcherLog ordered by Subscriber Name
      List<WebhookEventDeliveryLog> events = await webhookServerManager.GetWebhookDeliveryLogForDispatchAsync(token);
      if (events == null || events.Count == 0) {
        _logger.LogTrace("[{method}] Gets events for dispatch,Event count 0, No event to Dispatch ", "DispatchEventsAsync");
        return;
      }
      _logger.LogTrace("[{method}] Gets events for dispatch,Event count {@count} - EventsList {@events}", "DispatchEventsAsync",events?.Count, events);
      //Group Events for a client
      string clientEndPoint = "";
      int eventCount = events.Count;
      List<WebhookEventDTO> dispatchEvents = new List<WebhookEventDTO>();
      List<WebhookEventDeliveryLog> dispatchEventsDeliveryLog = new List<WebhookEventDeliveryLog>();
      int i = 0;
      //Foreach event
      while (i < eventCount) {
        WebhookEventDTO webhookEvent = new WebhookEventDTO();
        webhookEvent.EventName = events[i].EventName;
        webhookEvent.Payload = events[i].Payload;
        webhookEvent.ServerName = events[i].ServerName;
        webhookEvent.SubscriptionName = events[i].SubscriptionName;
        dispatchEvents.Add(webhookEvent);
        dispatchEventsDeliveryLog.Add(events[i]);

        //Group Events for the Subscription
        //If last event in list group it,
        //or if the next event has different subscription Endpoint Group it.
        clientEndPoint = events[i].SubscriptionCallBack;
        if (i == eventCount - 1 || clientEndPoint != events[i + 1].SubscriptionCallBack) {
          _logger.LogDebug("[{method}] group event dispatch start ", "DispatchEventsAsync");
          bool isSuccess = await DispatchEventToSubscriptionAsync(dispatchEvents, clientEndPoint, token);
          _logger.LogTrace("[{method}] group events updated in delivery log for disptach with status {@status}, Eventlist {@events}", "DispatchEventsAsync", isSuccess, dispatchEventsDeliveryLog);
          await webhookServerManager.UpdateEventToDeliveryLogAsync(dispatchEventsDeliveryLog, isSuccess, token);
          dispatchEvents = new List<WebhookEventDTO>();//Reinitialize
          dispatchEventsDeliveryLog = new List<WebhookEventDeliveryLog>();//Reinitialize
        }
        i++;
      }

    }
    /// <summary>
    /// Dispatch Events by Http Client to Subscribers in batch
    /// </summary>
    /// <param name="events">List of webhook Event DTOs</param>
    /// <param name="subscriptionEndPoint">Subscription End point for Http Call</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Boolean for success</returns>
    private async Task<bool> DispatchEventToSubscriptionAsync(List<WebhookEventDTO> events, string subscriptionEndPoint, CancellationToken token) {
      _logger.LogTrace("[{method}] events groupped by subscription for dispatch, subscription End Point - {subscription} and events - {@event}", "DispatchEventToSubscriptionAsync", subscriptionEndPoint, events);
      //Create HttpClient
      //Send request to the calbackendpoint 
      //Send List of Event with payload object 
      HttpClient client = new HttpClient();
      HttpRequestProcessor processor = new HttpRequestProcessor(client);
      string eventJson = JsonConvert.SerializeObject(events);
      try {
        _logger.LogDebug("[{method}] DispatchEventToSubscription - start", "DispatchEventToSubscriptionAsync");
        await processor.ExecutePOSTRequestAsync<List<WebhookEventDTO>>(subscriptionEndPoint, "", AcceptMediaTypeEnum.JSON, null, null, null, events);
        _logger.LogDebug("[{method}] DispatchEventToSubscription - End", "DispatchEventToSubscriptionAsync");
        return true;
      }
      catch (Exception ex) {
        //Log error and return
        _logger.LogError(ex.Message, ex);
        return false;
      }
    }

    #endregion
  }
}

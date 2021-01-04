/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ewApps.Core.Webhook.Subscriber {
  /// <summary>
  /// Scheduler service running in background to handle the webhook events received by the server. 
  /// <para>1. Gets Events from ClientEventQueue</para>
  ///<para> 2. Process Events and call Delegates</para>
  /// <para>3. Dequeue events from the ClientEventQueue</para>
  /// </summary>

  public class WebhookEventHandler :IHostedService, IDisposable {
    private readonly ILogger _logger;
    private IServiceScopeFactory _serviceScopeFactory;
    private CancellationTokenSource _stoppingCts = null;
    private Timer _timer;

  //  private WebhookSubscriptionManager _webhookSubscriptionManager;
   // private WebhookSubscriptionDBContext _webhookDBContext;
    protected bool _inProgress = false;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Logger DI</param>
    /// <param name="scopeFactory">System Defined Factory class to get the dependencies</param>
    public WebhookEventHandler(ILogger<WebhookEventHandler> logger, IServiceScopeFactory scopeFactory) {
      _logger = logger;
      _serviceScopeFactory = scopeFactory;
      _logger.LogInformation("[{method}] Initialized", "WebhookEventHandler");
    }

    /// <summary>
    /// Start the dispatch service when application starts
    /// </summary>
    /// <param name="token">Cancellation token</param>
    /// <returns>void</returns>
    public Task StartAsync(CancellationToken token) {
      _logger.LogInformation("[{method}] Starting Webhook subscription background Event handler Service", "StartAsync");
      _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(token);
      using (IServiceScope scope = _serviceScopeFactory.CreateScope())
      {
        WebhookSubscriptionManager webhookSubscriptionManager = scope.ServiceProvider.GetRequiredService<WebhookSubscriptionManager>();
        webhookSubscriptionManager.NotifyWebhookServerAsync(token);
      }
      _timer = new Timer(OnTimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(WebhookSubscriptionConstants.DispatchServiceExecutionDelay));
      return Task.CompletedTask;

    }

    /// <summary>
    /// Stop triggers when application is shutdown gracefully
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken) {
      // Stop called without start
      _logger.LogInformation("[{method}] Stopping Webhook subscription background Event handler Service", "StopAsync");
      return Task.CompletedTask;
    }

    /// <summary>
    /// When the class is collected by GC
    /// </summary>
    public virtual void Dispose() {
      _stoppingCts.Cancel();
    }

    /// <summary>
    /// Function call on timer tick
    /// </summary>
    /// <param name="state"></param>
    private void OnTimerCallback(object state) {
      try {
        if (_inProgress == false) {
          _inProgress = true;
          _logger.LogTrace("[{method}] Webhook Subscription Timer call back - Start", "OnTimerCallback");
          var task = Task.Run(async () => await ExecuteAsync(_stoppingCts.Token));
          task.Wait();
          _inProgress = false;
        }
      }
      catch (Exception ex) {
        _logger.LogError(ex.Message, ex);
        _inProgress = false;
      }
    }

    /// <summary>
    /// <para>1. Dequeue the Events from the ClientEventqueue </para>
    /// <para>2. Group them to handle by single delegate </para>
    ///<para> 3. Call Delegate to handle those events. </para>
    /// </summary> 
    /// <param name="token">cancellation Token</param>
    /// <returns>void</returns>
    private async Task ExecuteAsync(CancellationToken token) {
       _logger.LogTrace("[{method}] started at {@time} ", "ExecuteAsync", DateTime.UtcNow);
      //Create Scope for DBContext as Manager as Handler is a background process
      using (IServiceScope scope = _serviceScopeFactory.CreateScope()) {
        WebhookSubscriptionDBContext webhookDBContext = scope.ServiceProvider.GetRequiredService<WebhookSubscriptionDBContext>();
        WebhookSubscriptionManager webhookSubscriptionManager = scope.ServiceProvider.GetRequiredService<WebhookSubscriptionManager>();

        //Check for WebhookSubscriptionInitialization
       // if (_webhookSubscriptionManager.IsInitialized) {
          //Get event from ClientEventQueue ordered by Subscriber Name

          List<WebhookEvent> events = await webhookDBContext.GetWebhookEventQueueAsync(token);
          _logger.LogTrace("[{method}] collected events from EventQueue, Events - {@events}", "ExecuteAsync", events);
          //Group Events for Delegate
          int i = 0;
          int eventCount = events.Count;
          List<WebhookEvent> subscriptionEvents = new List<WebhookEvent>();
          //while loop for subscriptionEvents
          while (i < eventCount) {
            subscriptionEvents.Add(events[i]);
            //Group Events for the Subscription
            //If last event in list group it,
            //or if the next event has different subscription Name Group it.
            if (i == eventCount - 1 || events[i].SubscriptionName != events[i + 1].SubscriptionName) {
              WebhookSubscription subscription = webhookSubscriptionManager.GetWebhookSubscription(events[i].SubscriptionName);
              //CALL Event Delagate  
              bool isSuccess = false;
              try {
                isSuccess = await HandleEventsAsync(webhookSubscriptionManager,subscriptionEvents, subscription, token);
                _logger.LogTrace("[{method}] Handled Events with status {@status} for events {@events} on subscription {@subscription}", "ExecuteAsync", isSuccess, subscriptionEvents, subscription);
              }
              catch (Exception ex) {
                _logger.LogError(ex.Message, ex);
              }
              finally {
                if (isSuccess) {
                  //Dequeue the event from eventlog on ssuccess
                  webhookDBContext.DequeueWebhookEvents(subscriptionEvents, token);
                  _logger.LogTrace("[{method}] dequeues Events {@events} on subscription {@subscription}", "ExecuteAsync", subscriptionEvents, subscription);
                }
                //Reinitialize the list for next subscription  
                subscriptionEvents = new List<WebhookEvent>();//Reinitialize
              }

            }
            i++;
          }
          //Save changes to Database
          _logger.LogTrace("[{method}] Save changes", "ExecuteAsync");
          await webhookDBContext.SaveChangesAsync(token);
      //  }
      }
    }

    /// <summary>
    /// Handle events and pass to the manager to call the Delegate
    /// </summary>
    /// <param name="events">List ofEvents</param>
    /// <param name="subscription">Subscription</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Boolean for Success</returns>
    private async Task<bool> HandleEventsAsync(WebhookSubscriptionManager webhookSubscriptionManager,List<WebhookEvent> events, WebhookSubscription subscription, CancellationToken token) {
      try {
        //Call Subscription Manager   
        _logger.LogTrace("[{method}] Handle event Execute starts", "HandleEventsAsync");
        _logger.LogTrace("[{method}] Handle event Execute for events {@events}", "HandleEventsAsync", events);

        await webhookSubscriptionManager.ExecuteEventsAsync(subscription, events, token);
        _logger.LogTrace("[{method}] Handle event Execute ends", "HandleEventsAsync");
        return true;
      }
      catch (Exception ex) {
        _logger.LogError(ex.Message, ex);
        return false;
      }
    }
  }
}


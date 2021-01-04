/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ewApps.Core.CommonService;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using ewApps.Core.Webhook.Server;
using ewApps.Core.Webhook.Subscription;
using System;

namespace ewApps.Core.Webhook.Subscriber
{
  /// <summary>
  /// Database context for Webhook Operations  for
  /// Webhook Subscriptions
  /// WebhookEventQueue
  /// </summary>
  public class WebhookSubscriptionDBContext : DbContext
  {

    #region Constructor and Veriable
    //private WebhookAppSettings _connOptions;
    private string _connString;
    private ILogger<WebhookSubscriptionDBContext> _loggingService;

    /// <summary>
    /// Constructor with Context Options
    /// </summary>
    /// <param name="context"></param>
    /// <param name="loggingService"></param>
    public WebhookSubscriptionDBContext(DbContextOptions<WebhookSubscriptionDBContext> context, string connString) : base(context)
    {
      _connString = connString;
    }

    /// <summary>
    /// Constructor with AppSetting
    /// </summary>
    /// <param name="options"></param>
    /// <param name="appSetting"></param>
    /// <param name="loggingService"></param>
    public WebhookSubscriptionDBContext(DbContextOptions<WebhookSubscriptionDBContext> context, IOptions<WebhookSubscriptionAppSettings> appSetting, ILogger<WebhookSubscriptionDBContext> loggingService) : base(context)
    {
      //_connOptions = appSetting.Value;// appSetting.Value.WebhookConnectionStrings;
      _connString = appSetting.Value.WebhookSubscriptionConnectionString;
      _loggingService = loggingService;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }

    /// <summary>
    /// Configure DB options
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(_connString);//Define SQL server as Backend
      base.OnConfiguring(optionsBuilder);
    }
    #endregion

    #region DataSets
    /// <summary>
    /// Dataset for Webhook Subscription 
    /// </summary>
    public virtual DbSet<WebhookSubscription> WebhookSubscriptions
    {
      get;
      set;
    }

    /// <summary>
    /// Datataset for Webhook Event Received by the subscriber
    /// </summary>
    public virtual DbSet<WebhookEvent> WebhookEventQueue
    {
      get;
      set;
    }

    #endregion

    #region Subscription Methods

    /// <summary>
    /// Gets all webhook Subscriptions
    /// </summary>
    /// <param name="token">cancellation Token</param>
    /// <returns>List of Webhook Subscriptions</returns>
    public async Task<List<WebhookSubscription>> GetAllWebhookSubscriptionsAsync(CancellationToken token = default(CancellationToken))
    {
      _loggingService.LogInformation("WebhookDBContext.GetAllWebhookSubscriptionAsync()");
      return await this.WebhookSubscriptions.ToListAsync<WebhookSubscription>(token);
    }
    /// <summary>
    /// Gets all Webhook Subscriptions
    /// </summary>
    /// <returns>List of Webhook Subscription</returns>
    /// <remarks>Synchronous version</remarks>
    public List<WebhookSubscription> GetAllWebhookSubscriptions()
    {
      _loggingService.LogInformation("WebhookDBContext.GetAllWebhookSubscription()");
      return this.WebhookSubscriptions.ToList<WebhookSubscription>();
    }

    /// <summary>
    /// Gets single Webhook Subscription based on Server Name and SubscriptionNAme
    /// </summary>
    /// <param name="webhookServerName">Server Name</param>
    /// <param name="webhookSubscriptionName">Subscription NAme</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    public async Task<WebhookSubscription> GetWebhookSubscriptionAsync(string webhookServerName, string webhookSubscriptionName, CancellationToken token = default(CancellationToken))
    {
      _loggingService.LogInformation("WebhookDBContext.GetWebhookSubscriptionAsync():" + webhookServerName + "," + webhookSubscriptionName);
      WebhookSubscription subscription = await this.WebhookSubscriptions.Where(i => i.ServerName.ToLower() == webhookServerName.ToLower() && i.SubscriptionName.ToLower() == webhookSubscriptionName.ToLower()).FirstOrDefaultAsync();
      return subscription;
    }
    #endregion

    #region EventQueue
    /// <summary>
    /// Get all the events from queue pending for process
    /// Ordered by Subscription.
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>List of WebhookEvent Object</returns>
    public async Task<List<WebhookEvent>> GetWebhookEventQueueAsync(CancellationToken token = default(CancellationToken))
    {
      _loggingService.LogInformation("WebhookDBContext.GetWebhookEventQueue()");
      return await this.WebhookEventQueue.Where<WebhookEvent>(x => x.Status != "Processed").OrderBy(x => x.SubscriptionName).ToListAsync<WebhookEvent>(token);
    }
    /// <summary>
    /// Deletes the Event from the eventQueue
    /// </summary>
    /// <param name="lastEventQueueId">Id of event that was processed at the last</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    public void DequeueWebhookEvents(List<WebhookEvent> webhookEvents, CancellationToken token = default(CancellationToken))
    {
      _loggingService.LogInformation("WebhookDBContext.DequeueWebhookEventsAsync()");
      foreach (WebhookEvent wEvent in webhookEvents)
      {
        wEvent.Status = "Processed";
        wEvent.ProcessedDate = DateTime.UtcNow;
      }
      this.UpdateRange(webhookEvents);
      //this.RemoveRange(webhookEvents);
    }

    /// <summary>
    /// Deletesthe given webhookEvents
    /// </summary>
    /// <param name="webhookEvent">webhook Event oject to be deleted</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Void</returns>
   /* public async Task DeleteWebhookEventAsync(WebhookEvent webhookEvent, CancellationToken token = default(CancellationToken)) {
      _loggingService.LogInformation("WebhookDBContext.DeleteWebhookEventQueue()");
      await Task.Run(() => this.Remove(webhookEvent));
      
    }*/
    #endregion

  }
}

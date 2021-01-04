/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ewApps.Core.CommonService;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using ewApps.Core.SerilogLoggingService;

namespace ewApps.Core.Webhook.Server {
  /// <summary>
  /// Database context for Webhook Operations  for
  /// WebhookServer
  /// Webhook Subscriptions
  /// WebhookEventQueue
  /// WebhookEventDeliveryLog
  /// </summary>
  public class WebhookDBContext : DbContext {

    #region Constructor and Veriable
    private WebhookServerAppSettings _connOptions;
    private string _connString;   


    /// <summary>
    /// Constructor with AppSetting
    /// </summary>
    /// <param name="options"></param>
    /// <param name="appSetting"></param>
    /// <param name="loggingService"></param>
    public WebhookDBContext(DbContextOptions<WebhookDBContext> context, IOptions<WebhookServerAppSettings> appSetting) : base(context) {
      _connOptions = appSetting.Value;// appSetting.Value.WebhookConnectionStrings;
      _connString = _connOptions.WebhookServerConnectionString;     
    }


        ///// <summary>
        ///// Constructor with Context Options
        ///// </summary>
        ///// <param name="context"></param>
        ///// <param name="connString"></param>
        public WebhookDBContext(DbContextOptions<WebhookDBContext> context, string connString) : base(context) {
            _connString = connString;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }
    /// <summary>
    /// Defines all the configuratiion option for the Database
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlServer(_connString);//Use Sql Server as Backend
      base.OnConfiguring(optionsBuilder);
    }
    #endregion

    #region DataSets
    /// <summary>
    /// Webhook Server DataSet
    /// </summary>
    public virtual DbSet<WebhookServer> WebhookServers
    {
      get;
      set;
    }
    /// <summary>
    /// Subscriptions DataSet
    /// </summary>
    public virtual DbSet<WebhookSubscription> WebhookSubscriptions
    {
      get;
      set;
    }
    /// <summary>
    /// Server Event Queue
    /// </summary>
    public virtual DbSet<WebhookEvent> WebhookEventQueue
    {
      get;
      set;
    }
    /// <summary>
    /// Server Delivery Queue
    /// </summary>
    public virtual DbSet<WebhookEventDeliveryLog> WebhookEventDeliveryLog
    {
      get;
      set;
    }

    #endregion

    #region Server Methods
    /// <summary>
    /// Get list of all the Webhook Servers
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>List of Webhook Server defined with this Host</returns>
    public async Task<List<WebhookServer>> GetAllWebhookServersAsync(CancellationToken token = default(CancellationToken)) {
      SerilogLogger.LogInfo("WebhookDBContext.GetAllWebhookServersAsync()");
      return await this.WebhookServers.ToListAsync<WebhookServer>(token);
    }
    /// <summary>
    /// GEt List of All Webhook Servers defined with this Host.
    /// </summary>
    /// <returns>List of Webhook Server object</returns>
    /// <remarks> Synschronous version</remarks>
    public List<WebhookServer> GetAllWebhookServers() {
      SerilogLogger.LogInfo("WebhookDBContext.GetAllWebhookServers()");
      return this.WebhookServers.ToList<WebhookServer>();
    }
    /// <summary>
    /// Gets Webhook Server for the Given Name 
    /// </summary>
    /// <param name="webhookServerName">Wehbook Server name</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Webhook Server Object</returns>
    public async Task<WebhookServer> GetWebhookServerAsync(string webhookServerName, CancellationToken token = default(CancellationToken)) {
      SerilogLogger.LogInfo("WebhookDBContext.GetWebhookServer():" + webhookServerName);
      WebhookServer server = await this.WebhookServers.Where(i => i.ServerName.ToLower() == webhookServerName.ToLower()).FirstOrDefaultAsync();
      return server;
    }
    #endregion

    #region Subscription Methods
    /// <summary>
    /// Get all Subscriptions defined with host Server
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>List of Webhook Subscription object</returns>
    public async Task<List<WebhookSubscription>> GetAllWebhookSubscriptionsAsync(CancellationToken token = default(CancellationToken)) {
      SerilogLogger.LogInfo("WebhookDBContext.GetAllWebhookSubscriptionAsync()");
      return await this.WebhookSubscriptions.ToListAsync<WebhookSubscription>(token);
    }
    /// <summary>
    /// Get all Subscriptions defined with host Server
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>List of Webhook Subscription object</returns>
    /// <remarks>Synschronous Version</remarks>
    public List<WebhookSubscription> GetAllWebhookSubscriptions() {
      SerilogLogger.LogInfo("WebhookDBContext.GetAllWebhookSubscription()");
      return this.WebhookSubscriptions.ToList<WebhookSubscription>();
    }
    /// <summary>
    /// Gets webhook Subscription by the server name
    /// </summary>
    /// <param name="webhookServerName">Server NAae</param>
    /// <param name="webhookSubscriptionName">Subscription Name</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Webhook Subscription object</returns>
    public async Task<WebhookSubscription> GetWebhookSubscriptionAsync(string webhookServerName, string webhookSubscriptionName, CancellationToken token = default(CancellationToken)) {
      SerilogLogger.LogInfo("WebhookDBContext.GetWebhookSubscriptionAsync():" + webhookServerName + "," + webhookSubscriptionName);
      WebhookSubscription subscription = await this.WebhookSubscriptions.Where(i => i.ServerName.ToLower() == webhookServerName.ToLower() && i.SubscriptionName.ToLower() == webhookSubscriptionName.ToLower()).FirstOrDefaultAsync();
      return subscription;
    }
    #endregion

    #region EventQueue
    /// <summary>
    /// Gets all the event from Event queue,which are pending for dispatch
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>List of WebhookEvent Object</returns>
    public async Task<List<WebhookEvent>> GetWebhookEventQueueAsync(CancellationToken token = default(CancellationToken)) {
      SerilogLogger.LogInfo("WebhookDBContext.GetWebhookEventQueue()");
      return await this.WebhookEventQueue.ToListAsync<WebhookEvent>(token);
    }


    /// <summary>
    /// Deletes the given webhookEvents
    /// </summary>
    /// <param name="webhookEvent">webhook Event oject to be deleted</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Void</returns>
    public async Task DeleteWebhookEventAsync(WebhookEvent webhookEvent, CancellationToken token = default(CancellationToken)) {
      SerilogLogger.LogInfo("WebhookDBContext.DeleteWebhookEventQueue()");
      await Task.Run(() => this.Remove(webhookEvent));
    }
    #endregion

    #region Delivery Log
    /// <summary>
    /// Gets event from Delivery log for Dispatch
    /// </summary>
    /// <returns>List of WebhookEventDeliveryLog object</returns>
    public async Task<List<WebhookEventDeliveryLog>> GetWebhookDeliveryLogForDispatchAsync(CancellationToken token) {
      return await this.WebhookEventDeliveryLog.Where(x=>WebhookEventHelper.PendingForDispatchDefault.Invoke(x)).OrderBy(x => x.SubscriptionName).ThenBy(x => x.EventQueueTime).ToListAsync<WebhookEventDeliveryLog>();
    }

    /// <summary>
    /// Gets pending events from Delivery log taht not dispatched by Scheduler
    /// </summary>
    /// <returns>List of webhookEventDeliveryLog Pending for Dispatch</returns>
    public async Task<List<WebhookEventDeliveryLog>> GetPendingWebhookDeliveryLogAsync(string subscriptionName, string serverName, CancellationToken token) {
      return await this.WebhookEventDeliveryLog.Where(x => x.SubscriptionName == subscriptionName && x.ServerName == serverName && WebhookEventHelper.PendingForDispatchForced.Invoke(x)).OrderBy(x => x.SubscriptionName).ToListAsync<WebhookEventDeliveryLog>(token);
    }

    /// <summary>
    /// Add events to Delivery log for Dispatch
    /// </summary>
    /// <param name="deliveryLog">Delivery log object to be added</param>
    /// <returns>void</returns>
    public async Task AddEventToDeliveryLogAsync(WebhookEventDeliveryLog deliveryLog) {
      await this.AddAsync<WebhookEventDeliveryLog>(deliveryLog);
    }

    /// <summary>
    /// Update events to Delivery log for Success/Failure
    /// </summary>
    /// <param name="deliveryLog">List of delivery log to be updated</param>
    /// <param name="isSuccess">Mark the Delivery Log as Sucess or failure</param>
    /// <returns>void</returns>
    public async Task UpdateEventToDeliveryLogAsync(List<WebhookEventDeliveryLog> deliveryLog, bool isSuccess, CancellationToken token) {
      //Loop to update the object properties for failure or success
      for(int i=0;i<deliveryLog.Count;i++){ // each (WebhookEventDeliveryLog webhookEvent in deliveryLog) {
        WebhookEventDeliveryLog log = deliveryLog[i];
        //if success mark delivery log success else mark as failure and increase delivery attempt
        if (isSuccess) {
          
          log = WebhookEventHelper.MarkDeilveryLogForSuccess(log);
         
        }
        else {
          log = WebhookEventHelper.MarkDeilveryLogForFailure(log);
        }
        deliveryLog[i]= log;
      }

      await Task.Run(() => this.UpdateRange(deliveryLog), token);
    }
    #endregion
  }
}

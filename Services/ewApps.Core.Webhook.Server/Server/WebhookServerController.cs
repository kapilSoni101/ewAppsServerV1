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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.Webhook.Server {

    /// <summary>
    /// It handles all request coming for webhook
    /// As this is in a seperate dll the implementor has to Add this dll as part of application
    /// Using following code -
    /// <code> 
    /// Public void ConfigServices(IServiceCollection services)
    ///      services.AddMVC().AddApplicationPart(Assembly.Load(new AssemblyName("Name"));
    /// </code>
    /// </summary>
    [Route("webhook")]
    [ApiController]
    [Authorize]
    public class WebhookServerController :ControllerBase {

        #region Local Member
        private WebhookServerManager _webhookServerManager;
        private ILogger<WebhookServerController> _logger;
        #endregion

        #region  Constructor

        /// <summary>
        /// This is the constructor with webhookManager dependancy injection
        /// </summary>
        /// <param name="WebhookServerManager">WebhookServerManager</param>
        public WebhookServerController(ILogger<WebhookServerController> logger, WebhookServerManager webhookServerManager) {
            _webhookServerManager = webhookServerManager;
            _logger = logger;
            _logger.LogInformation("[{Method}] Service initialized", "WebhookServerController");
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get all the Servers defined with this WebhookHost.
        /// </summary>
        /// <returns>List of Webhook Servers</returns>
        [HttpGet]
        [Route("discovery")]
        [AllowAnonymous]
        public async Task<List<WebhookServer>> GetWebhookServersAsync() {
            _logger.LogDebug("[{Method}] Processing starts ", "GetWebhookServersAsync");
            List<WebhookServer> servers = await _webhookServerManager.GetAllWebhookServersAsync();
            _logger.LogTrace("[{Method}] Processing ends with server list -{@server} ", "GetWebhookServersAsync", servers);
            _logger.LogDebug("[{Method}] Processing ends", "GetWebhookServersAsync");

            return servers;
        }
        #endregion

        /// <summary>
        /// Add webhook subscription
        /// </summary>
        /// <returns>Added Webhook subscription</returns>
        [HttpPost]
        [Route("add/subscription")]
        [AllowAnonymous]
        public async Task<ActionResult> AddWebhookSubscriptionAsync([FromBody]WebhookSubscription webhookSubscription, CancellationToken token = default(CancellationToken)) {
            _logger.LogDebug("[{Method}] Processing starts ", "AddWebhookSubscriptionAsync");
            _logger.LogTrace("[{Method}] Processing Starts with webhook subscription DTO -{@subscription} ", "AddWebhookSubscriptionAsync", webhookSubscription);
            WebhookSubscription subscription = await _webhookServerManager.AddWebhookSubscriptionAsync(webhookSubscription, token);
            _logger.LogTrace("[{Method}] Processing ends with webhook subscription -{@subscription} ", "AddWebhookSubscriptionAsync", subscription);
            _logger.LogDebug("[{Method}] Processing ends ", "AddWebhookSubscriptionAsync");
            return Ok(subscription);
        }

        /// <summary>
        /// Update webhook subscription
        /// </summary>
        /// <returns>Updated Webhook subscription</returns>
        [HttpPost]
        [Route("update/subscription")]
        [AllowAnonymous]
        public async Task<WebhookSubscription> UpdateWebhookSubscriptionAsync([FromBody]WebhookSubscription webhookSubscription) {
            CancellationToken token = default(CancellationToken);
            _logger.LogDebug("[{Method}] Processing starts ", "UpdateWebhookSubscriptionAsync");
            _logger.LogTrace("[{Method}] Processing Starts with webhook subscription DTO -{@subscription} ", "UpdateWebhookSubscriptionAsync", webhookSubscription);
            WebhookSubscription subscription = await _webhookServerManager.UpdateWebhookSubscriptionAsync(webhookSubscription, token);
            _logger.LogTrace("[{Method}] Processing ends with webhook subscription -{@subscription} ", "UpdateWebhookSubscriptionAsync", subscription);
            _logger.LogDebug("[{Method}] Processing ends ", "UpdateWebhookSubscriptionAsync");

            return subscription;
        }

        /// <summary>
        /// Delete webhook subscription
        /// </summary>
        /// <returns>void</returns>
        [HttpPost]
        [Route("remove/subscription")]
        [AllowAnonymous]
        public async Task RemoveWebhookSubscriptionAsync([FromBody]WebhookServerSubscriptionDTO webhookSubscriptionDTO) {
            CancellationToken token = default(CancellationToken);
            _logger.LogTrace("[{Method}] Processing starts with webhook server subscriptionDTO -{@subscription} ", "RemoveWebhookSubscriptionAsync", webhookSubscriptionDTO);
            _logger.LogDebug("[{Method}] Processing starts ", "RemoveWebhookSubscriptionAsync");

            string serverName = webhookSubscriptionDTO.ServerName;
            string subscriptionName = webhookSubscriptionDTO.SubscriptionName;
            await _webhookServerManager.DeleteWebhookSubscriptionAsync(serverName, subscriptionName, token);
            _logger.LogDebug("[{Method}] Processing ends ", "RemoveWebhookSubscriptionAsync");
            return;
        }


        /// <summary>
        /// It synschronizes the subscription for its availability.
        /// All the pending events for delivery are marked available for delivery and picked up by the scheduler in next cycle.
        /// </summary>
        /// <returns>void</returns>
        [HttpPost]
        [Route("sync/subscription")]
        [AllowAnonymous]
        public async Task<ActionResult> SynchronizeSubscription([FromBody] WebhookServerSubscriptionDTO subscription, CancellationToken token = default(CancellationToken)) {
            _logger.LogDebug("[{Method}] Processing starts ", "SynchronizeSubscription");
            _logger.LogTrace("[{Method}] Processing starts with webhook server subscription DTO -{@subscription} ", "SynchronizeSubscription", subscription);
            if (subscription != null) {
                List<WebhookEventDeliveryLog> pendingDeliveryLog = await _webhookServerManager.GetPendingDeliveryLogAsync(subscription.SubscriptionName, subscription.ServerName, token);
                _logger.LogTrace("[{Method}] Pending event delivery log -{@DeliveryLog} ", "SynchronizeSubscription", pendingDeliveryLog);
                foreach (WebhookEventDeliveryLog log in pendingDeliveryLog) {
                    await _webhookServerManager.UpdateDeliveryLogForInitAsync(log, token);
                    _logger.LogTrace("[{Method}] Pending event delivery log updated for Delivery log -{@DeliveryLog} ", "SynchronizeSubscription", log);
                }
            }
            _logger.LogDebug("[{Method}] Processing ends ", "SynchronizeSubscription");
            return Ok();
        }
        /// <summary>
        /// RaiseEvent just for Test
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("raiseevent")]
        [AllowAnonymous]
        public async Task<ActionResult> RaiseEvent(CancellationToken token = default(CancellationToken)) {
            string name = "testEvent";
            _logger.LogDebug("[{Method}] Processing starts ", "RaiseEvent");
            await _webhookServerManager.RaiseWebhookEventAsync("VeriCheckPayment.TestEvent", name, token);
            _logger.LogDebug("[{Method}] Processing ends ", "RaiseEvent");
            return Ok();
        }

        #region Notused
        //Not Used and will be removedin Future
        /// <summary>
        /// Add webhook subscription
        /// </summary>
        /// <returns>Added Webhook subscription</returns>
        [HttpGet]
        [Route("mypending")]
        [AllowAnonymous]
        public async Task<List<PendingWebhookEventDTO>> GetPendingEventsForSubscriptionAsync(string subscriptionName, string serverName, CancellationToken token = default(CancellationToken)) {
            List<WebhookEventDeliveryLog> pendingDeliveryLog = await _webhookServerManager.GetPendingDeliveryLogAsync(subscriptionName, serverName, token);
            List<PendingWebhookEventDTO> list = new List<PendingWebhookEventDTO>();
            foreach (WebhookEventDeliveryLog log in pendingDeliveryLog) {
                PendingWebhookEventDTO webhookEvent = new PendingWebhookEventDTO();
                webhookEvent.EventName = log.EventName;
                webhookEvent.Payload = log.Payload;
                webhookEvent.ServerName = log.ServerName;
                webhookEvent.SubscriptionName = log.SubscriptionName;
                webhookEvent.DeliveryLogId = log.ID;
                list.Add(webhookEvent);
            }
            return list;
        }

        //Not Used - Will be removedin Future
        /// <summary>
        /// Add webhook subscription
        /// </summary>
        /// <returns>Added Webhook subscription</returns>
        [HttpPost]
        [Route("markmypending")]
        [AllowAnonymous]
        public async Task<ActionResult> MarkPendingEventsSucessAsync(List<Guid> eventIds, CancellationToken token = default(CancellationToken)) {
            await _webhookServerManager.MarkPendingDeliveryLog(eventIds, token);
            return Ok();
        }

        #endregion

    }
}

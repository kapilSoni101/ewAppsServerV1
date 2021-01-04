/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Sanjeev Kanna
 * Last Updated On: 10 October 2019
 */

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ewApps.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using ewApps.Payment.DTO;
using ewApps.Payment.DS;
using ewApps.Core.CommonService;
using ewApps.Payment.Common;
using Microsoft.Extensions.Logging;
using ewApps.Core.Webhook.Subscriber;

namespace ewApps.Payment.WebApi {

    /// <summary>
    /// This class manage payment subscripton event registration and event handing
    /// </summary>
    public class PaymentWebhookSubscriptionManager {

        #region Local Members

        IServiceProvider _serviceProvider;
        ILogger<PaymentWebhookSubscriptionManager> _loggerService;

        #endregion Local Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentWebhookSubscriptionManager"/> class.
        /// </summary>
        public PaymentWebhookSubscriptionManager(IServiceProvider serviceProvider, ILogger<PaymentWebhookSubscriptionManager> loggerService) {
            _serviceProvider = serviceProvider;
            _loggerService = loggerService;
        }

        #endregion Constructor

        #region Subscribe Events

        /// <summary>
        /// Initializes the webhook subscription.
        /// </summary>
        /// <param name="app">The application.</param>
        public async void InitWebhookSubscription(IApplicationBuilder app) {
            try {
                Dictionary<string, WebhookEventDelegate.WebhookEventHandlerAsync> eventHandler = new Dictionary<string, WebhookEventDelegate.WebhookEventHandlerAsync>();
                WebhookSubscriptionManager manager = app.ApplicationServices.GetRequiredService<WebhookSubscriptionManager>();
                WebhookSubscription subscription = GetWebhookSubscriptionDefination(manager, app);
                manager.IsInitialized = false;
                eventHandler.Add(subscription.SubscriptionName, subscription.EventDelegation);
                await manager.AddWebhookSubscriptionAsync(subscription);
                await manager.AttachDelegatesAsync(eventHandler);
                manager.IsInitialized = true;
            }
            catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Gets the webhook subscription defination.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        private WebhookSubscription GetWebhookSubscriptionDefination(WebhookSubscriptionManager manager, IApplicationBuilder app) {
            try {
                PaymentAppSettings appSettings = app.ApplicationServices.GetRequiredService<IOptions<PaymentAppSettings>>().Value;
                WebhookSubscription subscription = new WebhookSubscription();
                subscription.ServerName = "VeriCheckPayment";
                List<string> subscriptionEvents = new List<string>();
                subscriptionEvents.Add("VeriCheckPayment.StatusChange");
                subscriptionEvents.Add("VeriCheckPayment.TestEvent");
                subscriptionEvents.Add("Server.Start");
                subscription.SubscribedEventsAsList = subscriptionEvents;
                subscription.ServerHostEndPoint = appSettings.VCServerHostEndPoint;
                subscription.EventDelegation = HandleEvent;
                subscription.CallBackEndPoint = appSettings.VCSubscriberCallBackEndPoint + manager.SubscriptionCallBack;
                subscription.ServerShutDownCallBackEndPoint = appSettings.VCSubscriberCallBackEndPoint + manager.ServerShutDownCallBackEndPoint; // "https://localhost:44358/webhook"
                subscription.SubscriptionName = "TestSubscriber";
                subscription.IsActive = true;
                return subscription;
            }
            catch(Exception ex) {
                return null;
            }
        }

        #endregion Subscribe Events

        #region Event Handler

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="subscription">The subscription.</param>
        /// <param name="webhookEvent">The webhook event.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task HandleEvent(WebhookSubscription subscription, List<WebhookEvent> webhookEvent, CancellationToken token) {
            try {
                using(var serviceScope = _serviceProvider.CreateScope()) {
                    _loggerService.Log(LogLevel.Information, "START: Payment Status change (HandleEvent)");
                    IPaymentAndInvoiceDS iPaymentDataService = serviceScope.ServiceProvider.GetService<IPaymentAndInvoiceDS>();
                    
                    foreach(WebhookEvent item in webhookEvent) {
                        _loggerService.Log(LogLevel.Information, "START: Payment " + item.EventName);
                        // Recieve verichekc status change event
                        if(item.EventName == "VeriCheckPayment.StatusChange") {
                            _loggerService.Log(LogLevel.Information, "START: Payment(HandleEvent) Condition matched ");
                            WHTransactioStatusChangeDTO statusChangeDTO = ParsingHelper.ParseJSONToObject<WHTransactioStatusChangeDTO>(item.Payload);
                            await iPaymentDataService.UpdatePaymentStatusAsync(statusChangeDTO, token);
                        }
                        else {
                            _loggerService.Log(LogLevel.Information, "START: Payment(HandleEvent) Condition not matched " + item.EventName);
                            _loggerService.Log(LogLevel.Information, item.Payload);
                        }
                    }
                    
                }
            }
            catch(Exception ex) {
                _loggerService.Log(LogLevel.Information, "Error occurend in START: Payment Status change (HandleEvent)" + ex.Message);
            }
        }

        #endregion Evnet Handler
    }
}

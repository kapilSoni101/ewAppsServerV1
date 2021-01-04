using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.Webhook.Subscriber;
using ewApps.Core.Webhook.Subscription;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ewApps.Payment.WebApi {
    public class PaymentWebhookEventDelegate:IWebhookEventDelegate {

        #region Constructor

        ILogger<PaymentWebhookSubscriptionManager> _loggerService;
        IServiceProvider _serviceProvider;

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        public PaymentWebhookEventDelegate(ILogger<PaymentWebhookSubscriptionManager> loggerService, IServiceProvider serviceProvider) {
            _loggerService = loggerService;
            _serviceProvider = serviceProvider;
        }

        #endregion Constructor

        #region Event Handler

        /// <inheridoc/>
        public async Task WebhookEventHandlerAsync(WebhookSubscription subscription, List<WebhookEvent> webhookEvent, CancellationToken token) {
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

        #endregion Event Handler

    }
}

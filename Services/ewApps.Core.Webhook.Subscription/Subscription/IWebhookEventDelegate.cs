using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//using ewApps.Core.Webhook.Subscriber;

namespace ewApps.Core.Webhook.Subscriber
{
  public interface IWebhookEventDelegate
  {
    Task WebhookEventHandlerAsync(WebhookSubscription subscription, List<WebhookEvent> webhookEvent, CancellationToken token);
  }
}
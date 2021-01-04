/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using ewApps.Core.CommonService;
using ewApps.Core.Webhook.Server;
using ewApps.Core.Webhook.Subscription;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

//Not used and will be removed

namespace ewApps.Core.Webhook.Subscriber {
  /// <summary>
  /// Not Used - Middleware which will check all incoming request for the 
  /// Webhook Subscriber and handles it.
  /// </summary>
  public class WebhookSubscriptionMiddleware {

    private readonly RequestDelegate _next;
    private string _webhookEndPoint = "";
    private readonly IOptions<WebhookSubscriptionAppSettings> _Webhooksettings;

    public WebhookSubscriptionMiddleware(RequestDelegate next, IOptions<WebhookSubscriptionAppSettings> options) {
      _next = next;
      _Webhooksettings = options;
    }

    public async Task Invoke(HttpContext httpContext) {
      // Session will be set whenever middleware is called in the requestpipeline, if same sessionid is passed the session will be taken from DB
      /*   if(string.IsNullOrEmpty(_webhookEndPoint)) {
           _webhookEndPoint = "webhook/"; //TODO: get it from settings
         }
         //Check for the Endpoint in the Request and add the event in EventQueue
         if(httpContext.Request.Path.Value.Contains(_webhookEndPoint)) {
           WebhookSubscriptionManager manager = (WebhookSubscriptionManager)httpContext.RequestServices.GetService(typeof(WebhookSubscriptionManager));
           string bodyAsText = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
           List<WebhookEvent> events = (List<WebhookEvent>)JsonConvert.DeserializeObject(bodyAsText);
           manager.AddEventsToEventQueue(events);
         }
         else {//If not webhook endpoint continue the request normally to MVC*/

      await _next(httpContext);
      //  }
    }
  }
}
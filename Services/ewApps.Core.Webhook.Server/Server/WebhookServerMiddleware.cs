/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using ewApps.Core.CommonService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

//Not Used - will be removed in Future
namespace ewApps.Core.Webhook.Server {
  /// <summary>
  /// Not used - This Middleware will check all incoming request for the 
  /// Webhook Server and handles it.
  /// Not in Use
  /// </summary>
  public class WebhookServerMiddleware {

    private readonly RequestDelegate _next;
    private string _webhookEndPoint = "";
    private readonly IOptions<WebhookServerAppSettings> _settings;

    public WebhookServerMiddleware(RequestDelegate next, IOptions<WebhookServerAppSettings> options) {
      _next = next;
      _settings = options;
    }

    public async Task Invoke(HttpContext httpContext) {
      // Session will be set whenever middleware is called in the requestpipeline, if same sessionid is passed the session will be taken from DB
     // if(string.IsNullOrEmpty(_webhookEndPoint)) {
      //  _webhookEndPoint = "webhook/"; //TODO: get it from settings
     // }
      //Check for the Endpoint in the Request
      if(httpContext.Request.Path.Value.Contains(_webhookEndPoint)) {
        //It is webhook request and not check it for various endpoints like
        //discovery
        //AddServer
        //Update and so On
      }
      else {//If not webhook endpoint continue the request normally to MVC
        await _next(httpContext);
      }
    }
  }
}
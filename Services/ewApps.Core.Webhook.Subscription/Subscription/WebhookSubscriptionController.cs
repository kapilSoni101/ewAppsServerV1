/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 23 Jan 2019
 */
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.Webhook.Subscriber {

  /// <summary>
  /// It handles all request coming from webhook Server
  /// As this is in a seperate dll the implementor has to Add this dll as part of application
  /// Using following code -
  /// <code> 
  /// Public void ConfigServices(IServiceCollection services)
  ///      services.AddMVC().AddApplicationPart(Assembly.Load(new AssemblyName("Name"));
  /// </code>
  /// </summary>
  [Route("webhook")]
  [ApiController]
  [AllowAnonymous]
  public class WebhookSubscriptionController :ControllerBase {

    #region Local Member
    WebhookSubscriptionManager _webhookSubscriptionManager;

    private ILogger<WebhookSubscriptionController> _logger;
    #endregion

    #region  Constructor

    public WebhookSubscriptionController(ILogger<WebhookSubscriptionController> logger, WebhookSubscriptionManager webhookSubscriptionManager) {
      _webhookSubscriptionManager = webhookSubscriptionManager;
      _logger = logger;
      _logger.LogInformation("[{Method}] Service initialized", "WebhookSubscriptionController");
    }

    #endregion Constructor

    /// <summary>
    /// Receives the Webhook Event send by the Server
    /// </summary>
    /// <param name="events">Webhook Event List</param>
    /// <returns>void</returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("receiveevents")]
    public async Task<ActionResult> ReceiveEventsAsync([FromBody] List<WebhookEvent> events, CancellationToken token = default(CancellationToken)) {
      _logger.LogDebug("[{Method}] Processing starts ", "ReceiveEventsAsync");
      _logger.LogTrace("[{Method}] Processing starts with events object - {@events} ", "ReceiveEventsAsync", events);
     await _webhookSubscriptionManager.AddEventsToEventQueueAsync(events);
      _logger.LogDebug("[{Method}] Processing ends ", "ReceiveEventsAsync");
      return Ok();
    }
    /// <summary>
    /// Receives the Webhook Event send by the Server
    /// </summary>
    /// <param name="dto">ServerShutDown Dto that has Servername and SubscriptionName</param>
    /// <returns>void</returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("servershutdown")]
    public async Task<ActionResult> HandleServerShutDownEventAsync([FromBody] WebhookServerSubscriberDTO dto, CancellationToken token = default(CancellationToken)) {
      _logger.LogDebug("[{Method}] Processing starts ", "HandleServerShutDownEventAsync");
      _logger.LogTrace("[{Method}] Processing starts with webhookserverSubscripton DTO object - {@dto} ", "HandleServerShutDownEventAsync", dto);
      await _webhookSubscriptionManager.ExecuteServerShutDownEventAsync(dto.ServerName, dto.SubscriptionName, token);
      _logger.LogDebug("[{Method}] Processing ends ", "HandleServerShutDownEventAsync");
      return Ok();
    }
  }
}


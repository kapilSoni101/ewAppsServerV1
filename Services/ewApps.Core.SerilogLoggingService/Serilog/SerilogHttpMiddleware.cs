//using ewApps.Core.UserSessionService;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

namespace ewApps.Core.SerilogLoggingService {

  /// <summary>
  /// Middleware to log all incoming requests by serilog
  /// It logs all incoming request with the 
  /// RequestId, UserId, TenantId and SessionId along with Time taken by the request to execute.
  /// </summary>
  public class SerilogHttpMiddleware {
    const string _messageStringTemplate =
        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms from {machine} UserId {userId} and TenantId {tenantId} ";

    static readonly ILogger Log = Serilog.Log.ForContext<SerilogHttpMiddleware>();
    readonly RequestDelegate _next;
    SerilogHttpMiddlewareOptions _serilogHttpMiddlewareOptions = new SerilogHttpMiddlewareOptions();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    public SerilogHttpMiddleware(RequestDelegate next, IOptions<SerilogHttpMiddlewareOptions> options) {
      if(next == null)
        throw new ArgumentNullException(nameof(next));
      _next = next;
      _serilogHttpMiddlewareOptions = options.Value;
    }

    /// <summary>
    /// Invoke whenever this middleware is called in request pipeline
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext httpContext) {
      if(httpContext == null)
        throw new ArgumentNullException(nameof(httpContext));
      //IUserSessionManager userSessionManager = (IUserSessionManager)httpContext.RequestServices.GetService(typeof(IUserSessionManager));
      //Guid userId = userSessionManager == null ? Guid.Empty : userSessionManager.GetSession() == null ? Guid.Empty : userSessionManager.GetSession().AppUserId;
      //Guid tenantId = userSessionManager == null ? Guid.Empty : userSessionManager.GetSession() == null ? Guid.Empty : userSessionManager.GetSession().TenantId;

      //Start the stop watch before request goes next to the pipeline
      var sw = Stopwatch.StartNew();
      try {
        await _next(httpContext);
        sw.Stop();

        if(_serilogHttpMiddlewareOptions.EnableExceptionLogging) {
          var statusCode = httpContext.Response?.StatusCode;
          var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;
          //LogLevel
          var log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : Log;

          Guid userId = new Guid();
          if(httpContext.Response.Headers.ContainsKey("UserId")) {
            userId = Guid.Parse(httpContext.Response.Headers["UserId"]);
          }

          Guid tenantId = new Guid();
          if(httpContext.Response.Headers.ContainsKey("TenantId")) {
            tenantId = Guid.Parse(httpContext.Response.Headers["TenantId"]);
          }

          log.Write(level, _messageStringTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds, httpContext.Connection.RemoteIpAddress, userId, tenantId);
        }
      }
      // Never caught, because `LogException()` returns false.
      catch(Exception ex) when(LogException(httpContext, sw, ex)) { }
    }

    private bool LogException(HttpContext httpContext, Stopwatch sw, Exception ex) {
      sw.Stop();
      if(_serilogHttpMiddlewareOptions.EnableExceptionLogging) {
        //Get the logger context for error and then log Error
        LogForErrorContext(httpContext)
            .Error(ex, _messageStringTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds, httpContext.Connection.RemoteIpAddress);
      }
      return false;
    }
    /// <summary>
    /// Sets error template forserilog middleware
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    //Define Logger context for Error
    static ILogger LogForErrorContext(HttpContext httpContext) {
      var request = httpContext.Request;
      //All ForContext will be added as property in the template of error Log
      var result = Log
          .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
          .ForContext("RequestHost", request.Host)
          .ForContext("RequestProtocol", request.Protocol);

      if(request.HasFormContentType)
        result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

      return result;
    }
  }
}



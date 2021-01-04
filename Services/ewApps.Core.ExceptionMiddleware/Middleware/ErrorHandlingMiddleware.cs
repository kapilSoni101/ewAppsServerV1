/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ewApps.Core.SerilogLoggingService;
//using ewApps.Core.LoggingService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// This class globaly handle all types of exception and retun ewp error object in response.
  /// It also log exception in log file.
  /// Status code of all types of error is 500
  /// If excetpion is system => type is system, subtype is non
  /// If excetpion is custome => type is custome exceptin type, subtype is custome excetpion subtype
  /// </summary>
  public class ErrorHandlingMiddleware {

    #region Local Members

    private readonly RequestDelegate _next;
    //private readonly Logger _logger = null;
    const string _messageStringTemplate =
      "HTTP {RequestMethod} {RequestPath} responded {StatusCode} {EwpError} from Machine: {machine} UserId: {UserId}, UserName: {UserName}, TenantId: {TenantId} and TenantName: {TenantName} {NewLine}";

    const string _errorEmailTemplate = "{EmailBody}";

    private ExceptionAppSettings _appSetting;

    #endregion

    #region Constructor


    /// <summary>
    /// Initializes a new instance of the ErrorHandlingMiddleware class.  
    /// </summary>
    /// <param name="next">Instance of reqeust delegate</param>
    /// <param name="logger">Instance of logger service</param>
    public ErrorHandlingMiddleware(RequestDelegate next, IOptions<ExceptionAppSettings> appSetting) {
      _next = next;
      _appSetting = appSetting.Value;
    }

    #endregion Constructor

    public async Task InvokeAsync(HttpContext httpContext) {
      try {
        await _next(httpContext);
      }
      catch(Exception ex) {
        //_logger.LogError($"Something went wrong: {ex}");
        await HandleExceptionAsync(httpContext, ex);
      }
    }

    /// <summary>
    /// This method is responsible to 
    /// [a] Parse exception to ewp error object
    /// [b] Log exception 
    /// [c] Serialize ewp object to json
    /// [d] Write ewp json to http response 
    /// </summary>
    /// <param name="context">http context </param>
    /// <param name="ex">Exception</param>
    /// <returns></returns>
    private Task HandleExceptionAsync(HttpContext context, Exception ex) {
      System.Diagnostics.TraceEventType severity = ExceptionUtils.GetSeverity(ex);
      List<string> messages = new List<string>();
      ErrorType errorType = ErrorType.System;
      IList<EwpErrorData> errorDataList = new List<EwpErrorData>();

      // Get original exception
      Exception originalEx = ex.GetBaseException();

      if(originalEx is AggregateException) {
        AggregateException aggregateException = originalEx as AggregateException;
        HandleAggregateException(context, aggregateException);
      }

      // Invalid version          
      if(originalEx is EwpInvalidVersionException) {
        messages.Add(originalEx.Message);
        errorType = ErrorType.InvalidVersion;
        EwpErrorData data = new EwpErrorData((int)InvalidVersionErrorSubType.ApplicationVersion, "", "Invalid application version");
        errorDataList.Add(data);
      }

      // System Exception          
      else if(originalEx is EwpSystemException) {
        messages.Add(originalEx.Message);
        errorType = ErrorType.System;
        errorDataList.Add(new EwpErrorData() {
          Data = "ConnectionTimeOut",
          ErrorSubType = (int)SystemErrorSubType.ConnectionTimeOut,
          Message = originalEx.Message
        });
      }

      // Invalid database version
      else if(originalEx is EwpInvalidDBVersionException) {
        EwpInvalidDBVersionException dbVerEx = (EwpInvalidDBVersionException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.InvalidVersion;
        EwpErrorData data = new EwpErrorData((int)InvalidVersionErrorSubType.DatabaseVersion, dbVerEx.LatestDBVersion.ToString(), "Invlid DB version");
        errorDataList.Add(data);
      }
      // Invalid application version
      else if(originalEx is EwpInvalidAppVersionException) {
        EwpInvalidAppVersionException invalidAppVersionEx = (EwpInvalidAppVersionException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.InvalidVersion;
        errorDataList = invalidAppVersionEx.ErrorDataList;
      }
      // Internal Exception
      // System Exception

      // Database Exception   
      else if(originalEx is EwpDatabaseException) {
        EwpDatabaseException databaseEx = (EwpDatabaseException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.Database;
        errorDataList = databaseEx.ErrorDataList;
      }

      // Invalid token
      else if(originalEx is EwpInvalidLoginTokenException) {
        messages.Add(originalEx.Message);
        errorType = ErrorType.Authentication;
        errorDataList.Add(new EwpErrorData() {
          Data = "Invalid Token",
          ErrorSubType = (int)AuthenticationErrorSubType.InvalidLoginToken,
          Message = originalEx.Message
        });
      }
      // Invalid Login Email
      else if(originalEx is EwpInvalidLoginEmailIdException) {
        messages.Add(originalEx.Message);
        errorType = ErrorType.Authentication;
        errorDataList.Add(new EwpErrorData() {
          Data = "Email",
          ErrorSubType = (int)AuthenticationErrorSubType.InvalidEmail,
          Message = originalEx.Message
        });
      }
      // Invalid Password
      else if(originalEx is EwpInvalidPasswordException) {
        messages.Add(originalEx.Message);
        errorType = ErrorType.Authentication;
        errorDataList.Add(new EwpErrorData() {
          Data = "Password",
          ErrorSubType = (int)AuthenticationErrorSubType.InvalidPassword,
          Message = originalEx.Message
        });
      }
      // User Locked Exception
      else if(originalEx is EwpUserLockedException) {
        messages.Add(originalEx.Message);
        errorType = ErrorType.Authentication;
        errorDataList.Add(new EwpErrorData() {
          Data = "UserLocked",
          ErrorSubType = (int)AuthenticationErrorSubType.UserLocked,
          Message = originalEx.Message
        });
      }
      // Security Exception. 
      else if(originalEx is EwpSecurityException) {
        EwpSecurityException securityEx = (EwpSecurityException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.Security;
        errorDataList = securityEx.ErrorDataList;
      }
      // Validation Exception like required fields.
      else if(originalEx is EwpValidationException) {
        EwpValidationException validationEx = (EwpValidationException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.Validation;
        errorDataList = validationEx.ErrorDataList;
      }
      // Duplicate Name Exception.
      else if(originalEx is EwpDuplicateNameException) {
        EwpDuplicateNameException duplciateNameEx = (EwpDuplicateNameException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.Duplicate;
        errorDataList = duplciateNameEx.ErrorDataList;
      }
      // DataConcurrency Excetpion.
      else if(originalEx is EwpDataConcurrencyException) {
        EwpDataConcurrencyException dataConcurrancyEx = (EwpDataConcurrencyException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.Concurrency;
        errorDataList = dataConcurrancyEx.ErrorDataList;
      }
      // Import Data Exception.
      else if(originalEx is EwpImportDataException) {
        EwpImportDataException importDataEx = (EwpImportDataException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.ImportData;
        errorDataList = importDataEx.ErrorDataList;
      }
      // Invalid remote device or device not register on local.
      else if(originalEx is EwpInvalidDeviceIdException) {
        EwpInvalidDeviceIdException invalidDeviceIdEx = (EwpInvalidDeviceIdException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.InvalidDeviceId;
        errorDataList = invalidDeviceIdEx.ErrorDataList;
      }
      // Null Request Argument Exception.
      else if(originalEx is EwpNullRequestArgumentException) {
        messages.Add(originalEx.Message);
        errorType = ErrorType.NullRequestArgument;
        errorDataList = (originalEx as EwpNullRequestArgumentException).ErrorDataList;
      }
      // Invalid remote device or device not register on local.
      else if(originalEx is EwpInvalidTimeZoneException) {
        EwpInvalidTimeZoneException invalidTimeZoneEx = (EwpInvalidTimeZoneException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.InvalidTimeZone;
        errorDataList = invalidTimeZoneEx.ErrorDataList;
      }
      // Invalid field reference exception.
      else if(originalEx is EwpInvalidFieldReferenceException) {
        EwpInvalidFieldReferenceException invalidFieldRefEx = (EwpInvalidFieldReferenceException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.InvalidFieldReference;
        errorDataList = invalidFieldRefEx.ErrorDataList;
      }
      // Invalid operation exception.
      else if(originalEx is EwpInvalidOperationException) {
        EwpInvalidOperationException invalidOperationEx = (EwpInvalidOperationException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.InvalidOperation;
        errorDataList = invalidOperationEx.ErrorDataList;
      }
      // Invalid operation exception.
      else if(originalEx is EwpDowntimeException) {
        EwpDowntimeException downtimeException = (EwpDowntimeException)originalEx;
        messages.Add(originalEx.Message);
        errorType = ErrorType.DowntimeException;
        errorDataList = downtimeException.ErrorDataList;
      }
      // Recoverable Exception.
      else if(originalEx.GetType().BaseType == typeof(EwpRecoverableException)) {
        messages.Add(originalEx.Message);
        errorType = ErrorType.Validation;
      }
      // For email server is down
      else if(originalEx.GetType() == typeof(System.Net.Mail.SmtpException)) {
        messages.Add(ErrorMessages.MailServerUnavailable);
        severity = TraceEventType.Critical;
        errorType = ErrorType.System;
      }
      else if(originalEx.GetType() == typeof(System.Net.Sockets.SocketException)) {
        System.Net.Sockets.SocketException socketEx = (System.Net.Sockets.SocketException)originalEx;
        messages.Add("A connection attempt failed because the connected party did not respond after a period of time, or established connection failed because connected host has failed to respond");
        errorDataList.Add(new EwpErrorData() {
          ErrorSubType = (int)ConcurrencyErrorSubType.None,
          Data = socketEx.ErrorCode,
          Message = "Invalid Request URL"
        });
        severity = TraceEventType.Critical;
        errorType = ErrorType.System;
      }
      else if(originalEx is DbUpdateConcurrencyException) {
        DbUpdateConcurrencyException concurrencyEx = (originalEx as DbUpdateConcurrencyException);
        errorType = ErrorType.Concurrency;

        foreach(var entry in concurrencyEx.Entries) {
          messages.Add(string.Format(ErrorMessages.ConcurrentMessage, entry.Entity.GetType().Name));
          foreach(var property in entry.CurrentValues.Properties) {
            errorDataList.Add(new EwpErrorData() { Message = string.Format(ErrorMessages.FieldConcurrentMessage, property), ErrorSubType = (int)ConcurrencyErrorSubType.None, Data = property });
          }
        }
      }
      else if(originalEx is DbUpdateException) {
        DbUpdateException dbUpdateException = (originalEx as DbUpdateException);
        messages.Add(ErrorMessages.GeneralDBErrorMessage);
        foreach(var entry in dbUpdateException.Entries) {
          errorDataList.Add(new EwpErrorData() { Message = string.Format(ErrorMessages.DatabaseOperationErrorMessage, entry.Entity.GetType().Name), ErrorSubType = (int)ConcurrencyErrorSubType.None, Data = entry.Entity.GetType().Name });
        }
      }

      else if(originalEx is SqlException) {
        if(originalEx.GetBaseException().Message.Contains("Violation of UNIQUE KEY constraint 'UQ_Adderss_ParentEntityId_Label_AddressType'")) {
          messages.Add(ErrorMessages.DuplicateAddressLabelErrorMessage);
          errorDataList.Add(new EwpErrorData() {
            ErrorSubType = (int)DuplicateErrorSubType.None,
            Message = ErrorMessages.DuplicateAddressLabelErrorMessage
          });
          errorType = ErrorType.Duplicate;
        }
        else {
          SqlException sqlEx = (originalEx as SqlException);
          messages.Add(ErrorMessages.GeneralDBErrorMessage);

          StringBuilder errorMessages = new StringBuilder();

          for(int i = 0; i < sqlEx.Errors.Count; i++) {
            errorMessages.AppendFormat("Message: {0}; ", sqlEx.Errors[i].Message);
            errorMessages.AppendLine();
            errorMessages.AppendFormat("LineNumber: {0}; ", sqlEx.Errors[i].LineNumber);
            errorMessages.AppendFormat("Source: {0}; ", sqlEx.Errors[i].Source);
            errorMessages.AppendFormat("Procedure: {0};", sqlEx.Errors[i].Procedure);

            errorDataList.Add(new EwpErrorData() { Message = errorMessages.ToString(), ErrorSubType = (int)ConcurrencyErrorSubType.None, Data = sqlEx.Errors[i].Procedure });
          }
        }
      }
      // For sql connection timeout
      else if(originalEx.GetType() == typeof(System.Data.SqlTypes.SqlTypeException)) {
        if(originalEx.Message.Contains("Timeout expired")) {
          messages.Add(ErrorMessages.SQLConTimeOut);
          errorDataList.Add(new EwpErrorData() {
            ErrorSubType = (int)DatabaseErrorSubType.ConnectionTimeOut,
            Message = originalEx.Message
          });
          severity = TraceEventType.Critical;
          errorType = ErrorType.Database;
        }
        else if(originalEx.Message.Contains("Snapshot isolation transaction aborted")) {
          messages.Add(ErrorMessages.DataConcurrency);
          errorDataList.Add(new EwpErrorData() {
            ErrorSubType = (int)ConcurrencyErrorSubType.None,
            Message = ErrorMessages.DataConcurrency
          });
          severity = TraceEventType.Error;
          errorType = ErrorType.Concurrency;
        }

        else {
          messages.Add(originalEx.Message);
          severity = TraceEventType.Critical;
          errorType = ErrorType.Database;
        }
      }
      else if(originalEx is EwpServiceException) {
        EwpServiceException serviceException = (EwpServiceException)originalEx;
        messages.Add(originalEx.Message);
        errorType = serviceException.GetEwpErrorType();
        errorDataList = serviceException.ErrorDataList;
      }
      // If System generated excetpion or custom  excetpioin.
      else {
        messages.Add(!string.IsNullOrWhiteSpace(originalEx.Message) ? originalEx.Message : string.Format("A fatal error has occurred in completing this operation. Please retry it, and if it happens again, report the problem to application support."));
        severity = TraceEventType.Critical;
        errorType = ErrorType.System;
      }

      // Format and log error message
      if(errorDataList == null || errorDataList.Count == 0) {
        errorDataList = new List<EwpErrorData>();
        EwpErrorData ewpErrorData = new EwpErrorData();
        ewpErrorData.Message = originalEx.Message;
        List<KeyValuePair<string, string>> errorData = new List<KeyValuePair<string, string>>();
        errorData.Add(new KeyValuePair<string, string>("Exception Message", originalEx.Message));
        errorData.Add(new KeyValuePair<string, string>("Source", originalEx.Source));
        errorData.Add(new KeyValuePair<string, string>("StackTrace", originalEx.StackTrace));
        errorData.Add(new KeyValuePair<string, string>("InnerException", Newtonsoft.Json.JsonConvert.SerializeObject(originalEx.InnerException)));
        ewpErrorData.Data = errorData;
        errorDataList.Add(ewpErrorData);
      }

      EwpError error = new EwpError(errorType, messages, errorDataList);

      // Parse object in json
      string jsonError = EwpError.ToJSON(error);

      // Write exception in response
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      //logger.LogError(ex, jsonError);       

      LogException(context, ex, jsonError);

      if(severity == TraceEventType.Critical) {
        SendLogEmail(ex, context, jsonError);
      }


      return context.Response.WriteAsync(jsonError);
    }

    private void HandleAggregateException(HttpContext context, AggregateException aggregateException) {
      foreach(var exception in aggregateException.Flatten().InnerExceptions) {
        HandleExceptionAsync(context, exception);
      }
    }



    /// <summary>
    /// Sets error template forserilog middleware
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    //Define Logger context for Error
    private Serilog.ILogger LogForErrorContext(HttpContext httpContext) {
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

    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="ex">The ex.</param>
    /// <param name="ewpError">The ewp error.</param>
    /// <returns></returns>
    private bool LogException(HttpContext httpContext, Exception ex, string ewpError) {
      //Get the logger context for error and then log Error
      // LogForErrorContext(httpContext)
      string tenantId = "";
      string tenantName = "";
      string userId = "";
      string userName = "";

      if(ex.GetBaseException().Data.Contains("TenantId")) {
        tenantId = ex.GetBaseException().Data["TenantId"].ToString();
      }

      if(ex.GetBaseException().Data.Contains("TenantName")) {
        tenantName = ex.GetBaseException().Data["TenantName"].ToString();
      }

      if(ex.GetBaseException().Data.Contains("UserId")) {
        userId = ex.GetBaseException().Data["UserId"].ToString();
      }

      if(ex.GetBaseException().Data.Contains("UserName")) {
        userName = ex.GetBaseException().Data["UserName"].ToString();
      }

      Log.Error(ex, _messageStringTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, ewpError, httpContext.Connection.RemoteIpAddress, userId, userName, tenantId, tenantName);


      return true;
    }

    /// <summary>
    /// Sends the log email.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="ewpError">The ewp error.</param>
    private bool SendLogEmail(Exception exception, HttpContext httpContext, string ewpError) {
      try {
        if(_appSetting.EnableErrorEmail == true) {

          string tenantId = "";
          string tenantName = "";
          string userId = "";
          string userName = "";

          if(exception.GetBaseException().Data.Contains("TenantId")) {
            tenantId = Convert.ToString(exception.GetBaseException().Data["TenantId"]);
          }

          if(exception.GetBaseException().Data.Contains("TenantName")) {
            tenantName = Convert.ToString(exception.GetBaseException().Data["TenantName"]);
          }

          if(exception.GetBaseException().Data.Contains("UserId")) {
            userId = Convert.ToString(exception.GetBaseException().Data["UserId"]);
          }

          if(exception.GetBaseException().Data.Contains("UserName")) {
            userName = Convert.ToString(exception.GetBaseException().Data["UserName"]);
          }

          StringBuilder sb = new StringBuilder();
          sb.AppendLine();
          sb.AppendLine();
          sb.AppendLine(string.Format("Timestamp: {0}", DateTime.Now.ToString()));
          sb.AppendLine();

          sb.AppendLine(string.Format("Title: {0}", "An error has occurred in Application Service while processing your request."));
          sb.AppendLine();

          sb.AppendLine(string.Format("Request Type: {0}", httpContext.Request.Method));
          sb.AppendLine();

          sb.AppendLine(string.Format("Request Method: {0}", httpContext.Request.Path));
          sb.AppendLine();

          sb.AppendLine(string.Format("Remote IP Address: {0}", httpContext.Connection.RemoteIpAddress));
          sb.AppendLine();

          sb.AppendLine(string.Format("Server Name: {0}", _appSetting.ServerName));
          sb.AppendLine();

          sb.AppendLine(string.Format("Tenant Id: {0}", tenantId));
          sb.AppendLine();

          sb.AppendLine(string.Format("Tenant Name: {0}", tenantName));
          sb.AppendLine();

          sb.AppendLine(string.Format("User Id: {0}", userId));
          sb.AppendLine();

          sb.AppendLine(string.Format("User Name: {0}", userName));
          sb.AppendLine();


          sb.AppendLine(string.Format("Message: {0}", exception.Message));
          sb.AppendLine();

          sb.AppendLine(string.Format("Exception: {0}", exception.StackTrace));
          sb.AppendLine();


          EmailLoggerModel emailLogger = new EmailLoggerModel();
          emailLogger.EmailServer = _appSetting.SMTPServer;
          emailLogger.EmailSubject = "Error occured in " + _appSetting.AppName + " application.";
          emailLogger.SenderEmail = _appSetting.SenderEmail;
          emailLogger.ReceiverEmail = _appSetting.ReceiverEmail;
          emailLogger.UserName = _appSetting.SenderUserId;
          emailLogger.Password = _appSetting.SenderPwd;
          emailLogger.EmailServerPort = _appSetting.SMTPPort;
          emailLogger.EmailServerSSLEnabled = _appSetting.EnableSSL;

          using(Logger logger = SerilogLogger.GetEmailLogger(emailLogger, null)) {
            logger.Error(_errorEmailTemplate, sb.ToString());
          }
          return true;
        }
        return false;
      }
      catch(Exception ex) {
        LogException(httpContext, ex, ewpError);
        return false;
      }
    }
  }
}

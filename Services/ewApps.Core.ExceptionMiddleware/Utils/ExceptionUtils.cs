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
using System.Diagnostics;
using System.Net;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// This class provides utility methods for exception handling.
  /// </summary>
  public class ExceptionUtils {

    /// <summary>
    /// This method provides most last exception.
    /// </summary>
    /// <param name="e">Exception</param>
    /// <returns>Exception</returns>
    public static Exception GetInnermostException(Exception e) {
      if (e == null) {
        return null;
      }
      while (e.InnerException != null) {
        e = e.InnerException;
      }
      return e;
    }

    /// <summary>
    /// This method provides original exception.
    /// <remarks>
    /// If orginal exception is not found it returns null value.
    /// </remarks>
    /// </summary>
    /// <param name="ex">Source exception.</param>
    /// <returns>Original exception.</returns>
    public static Exception GetOriginalException(Exception ex) {
      Exception innerEx = GetInnermostException(ex);
      if (innerEx != null && innerEx.Data.Contains(Constants.IsOriginaleKey) && Convert.ToBoolean(innerEx.Data.Contains(Constants.IsOriginaleKey)))
        return innerEx;
      return null;
    }

    /// <summary>
    /// This method is used to check for original exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <returns>If true, this exception is wrapped else fale.</returns>
    public static bool IsWrapper(Exception ex) {
      return Convert.ToBoolean(GetData(ex, Constants.IsWrapperKey));
    }

    /// <summary>
    /// This method is used to set wrapper flag in an exception.
    /// </summary>
    /// <param name="ex">Exception.</param>   
    public static void MarkAsWrapper(Exception ex) {
      SetData(ex, Constants.IsWrapperKey, true);
    }

    /// <summary>
    /// This method is used to check for original exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <returns>If true, this exception is wrapped else fale.</returns>
    public static bool IsOriginal(Exception ex) {
      return Convert.ToBoolean(GetData(ex, Constants.IsOriginaleKey));
    }

    /// <summary>
    /// This method is used to set original flag in an exception.
    /// </summary>
    /// <param name="ex">Exception.</param>   
    public static void MarkAsOriginal(Exception ex) {
      SetData(ex, Constants.IsOriginaleKey, true);
    }

    /// <summary>
    /// This method is used to check for pass through an exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <returns>If true, this exception is pass through else false.</returns>
    public static bool IsPassThrough(Exception ex) {
      return Convert.ToBoolean(GetData(ex, Constants.IsPassThroughKey));
    }

    /// <summary>
    /// This method is used to set pass through flag in an exception.
    /// </summary>
    /// <param name="ex">Exception.</param>   
    public static void MarkAsPassThrough(Exception ex) {
      SetData(ex, Constants.IsPassThroughKey, true);
    }

    /// <summary>
    /// This method is used to check for rethrow type exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <returns>If true, this exception is rethrow else false.</returns>
    public static bool IsRethrow(Exception ex) {
      return Convert.ToBoolean(GetData(ex, Constants.IsRethrowKey));
    }

    /// <summary>
    /// This method is used to set pass through flag in an exception.
    /// </summary>
    /// <param name="ex">Exception.</param>   
    public static void MarkAsRethrow(Exception ex) {
      SetData(ex, Constants.IsPassThroughKey, true);
    }

    /// <summary>
    /// This method is used to check for rethrow type exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <returns>If true, this exception is rethrow else false.</returns>
    public static bool IsNew(Exception ex) {
      return Convert.ToBoolean(GetData(ex, Constants.IsNewKey));
    }

    /// <summary>
    /// This method is used to set pass through flag in an exception.
    /// </summary>
    /// <param name="ex">Exception.</param>   
    public static void MarkAsNew(Exception ex) {
      SetData(ex, Constants.IsNewKey, true);
    }

    /// <summary>
    /// This method is used to check for logged.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <returns>If true, this exception is logged else false.</returns>
    public static bool IsLogged(Exception ex) {
      return Convert.ToBoolean(GetData(ex, Constants.IsLoggedKey));
    }

    /// <summary>
    /// This method is used to set pass through flag in an exception.
    /// </summary>
    /// <param name="ex">Exception.</param>   
    public static void MarkAsLogged(Exception ex) {
      SetData(ex, Constants.IsLoggedKey, true);
    }

    /// <summary>
    /// This method provides to set severity of exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <param name="severity">Severity.</param>
    public static void SetSeverity(Exception ex, System.Diagnostics.TraceEventType severity) {
      if (!ex.Data.Contains(Constants.SeverityKey))
        ex.Data.Add(Constants.SeverityKey, severity.ToString());
    }

    /// <summary>
    /// This method provides to get Severity of given exception.
    /// </summary>
    /// <param name="ex">Exception</param>
    /// <returns>Trace event type</returns>
    public static TraceEventType GetSeverity(Exception ex) {
      string strSeverity = Convert.ToString(GetData(ex, Constants.SeverityKey));
      TraceEventType severity = EnumUtils.ToEnum<TraceEventType>(strSeverity, TraceEventType.Error);
      return severity;
    }

    /// <summary>
    /// This method provides to add additional messages in a given exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <param name="msg">Message.</param>
    public static void SetAdditionalMsg(Exception ex, string msg) {
      if (!ex.Data.Contains(Constants.AdditionalMsgKey))
        ex.Data.Add(Constants.AdditionalMsgKey, msg);
    }

    /// <summary>
    /// This method provides to get additional message from a given exception.
    /// </summary>
    /// <param name="ex">exception</param>
    /// <returns>Additional string message.</returns>
    public static string GetAdditionalMsg(Exception ex) {
      return Convert.ToString(GetData(ex, Constants.AdditionalMsgKey));
    }

    /// <summary>
    /// Adds extra information in an exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <param name="key">Extra information key.</param>
    /// <param name="data">Extra information of.</param>    
    public static void SetData(Exception ex, string key, object data) {
      // This test is done to protect from the error of re-addition of a key.     
      if (ex.Data.Contains(key))
        ex.Data[key] = data;
      else
        ex.Data.Add(key, data);
    }

    /// <summary>
    /// Gets extra information from exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <param name="key">Extra information key.</param>
    /// <returns></returns>
    public static object GetData(Exception ex, string key) {
      object data = null;
      if (ex != null && ex.Data.Contains(key))
        data = ex.Data[key];
      return data;
    }

    /// <summary>
    ///  Sets common exception properties.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <param name="message">Message.</param>
    /// <param name="severity">Severity.</param>
    public static void SetCommonProperties(ref Exception ex, string message, TraceEventType severity) {

      // Add additional message.
      ExceptionUtils.SetAdditionalMsg(ex, message);

      // Set Severity.
      ExceptionUtils.SetSeverity(ex, severity);

      // Mark as original.
      if (!ex.Data.Contains(Constants.IsOriginaleKey) && ExceptionUtils.GetOriginalException(ex) == null)
        ExceptionUtils.MarkAsOriginal(ex);
    }

    /// <summary>
    /// This method provide to set severity of exception.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <param name="statusCode">Http Status Code.</param>
    public static void SetHttpSatusCode(Exception ex, HttpStatusCode statusCode) {
      if (!ex.Data.Contains(Constants.HttpStatusCodeKey))
        ex.Data.Add(Constants.HttpStatusCodeKey, statusCode.ToString());
    }

    /// <summary>
    /// This method provides to get Severity of given exception.
    /// </summary>
    /// <param name="ex">Exception</param>
    /// <returns>Http status code.</returns>
    public static HttpStatusCode GetHttpSatusCode(Exception ex) {
      string strHttpStatusCode = Convert.ToString(GetData(ex, Constants.HttpStatusCodeKey));
      HttpStatusCode statusCode = EnumUtils.ToEnum<HttpStatusCode>(strHttpStatusCode, HttpStatusCode.InternalServerError);
      return statusCode;
    }


    /// <summary>
    /// Validate entity based on broken rulles.
    /// </summary>
    /// <param name="entityName">Entity Name.</param>    
    /// <param name="errorDataList">Error Data List.</param>
    /// <returns>True if entity passes all validation rules.</returns>
    public  static bool RaiseValidationException(string entityName, IList<EwpErrorData> errorDataList) {
      if (errorDataList.Count > 0) {
        string message = string.Format(ErrorMessages.InvalidEntity, entityName);
        throw new EwpValidationException(message, errorDataList);
      }
      return true;
    }
  }
}

//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 23 April 2015
//===============================================================================
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ewApps.Core.ServiceProcessor {


  /// <summary>
  /// It provides abstract methods for Http Header.
  /// </summary>
  public class HttpHeaderHelper : IHttpHeaderHelper {

    IHttpContextAccessor _httpContextAccessor;

    #region Constructor 

    public HttpHeaderHelper(IHttpContextAccessor httpContextAccessor) {
      _httpContextAccessor = httpContextAccessor;
    }

    #endregion Constructor

    /// <summary>
    /// Get value from http header
    /// </summary>
    /// <param name="key">Key</param>
    /// <returns>Value</returns>  
    //public static string GetHeaderValue(string key) {
    //  string s = string.Empty;
    //  if (HttpContext.Current.Request.Headers.AllKeys.Contains(key))
    //    s = HttpContext.Current.Request.Headers[key];
    //  return s;
    //}

    /// <summary>
    /// Set value in http response header
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="value">Value</param>
    public  void SetHeaderValue(string key, object value) {
      //HttpContext.Current.Response.AddHeader(key, Convert.ToString(value));
      _httpContextAccessor.HttpContext.Response.Headers.Append(key, Convert.ToString(value));
    }

    ///// <summary>
    ///// Gets login user access token
    ///// </summary>
    ///// <returns>Returns requester access token</returns>
    //public static string GetRequesterAccessToken() {
    //  return GetHeaderValue(Constants.EwpAccessTokenKey);
    //}

  }
}
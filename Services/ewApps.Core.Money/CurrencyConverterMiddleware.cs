/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna<skhanna@eworkplaceapps.com>
 * Date: 14 January 2019
 * 
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;

namespace ewApps.Core.Money{

  // Hari: Discuss

  /// <summary>
  /// Middleware extention to mange CurrencyConversion, 
  /// This is used to initialize Tenant Specific Class
  /// Its just an example and implementor need to have there own middleware so set the 
  /// FixedCurrencyConversion Data 
  /// </summary>
  public class CurrencyConverterMiddleware {

    private readonly RequestDelegate _next;
  
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyConverterMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next.</param>
    public CurrencyConverterMiddleware(RequestDelegate next) {
      _next = next;
    }

    /// <summary>
    /// Invokes the specified HTTP context.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns></returns>
    public async Task Invoke(HttpContext httpContext,IUserSessionManager userSessionManager,ICurrencyConversion currencyConversion) {
      List<CurrencyConversionRate> list = new List<CurrencyConversionRate>();
      //get the list data based on USerSession
      currencyConversion.SetFixedRateList(list);
      await _next(httpContext);
    }
  }
}

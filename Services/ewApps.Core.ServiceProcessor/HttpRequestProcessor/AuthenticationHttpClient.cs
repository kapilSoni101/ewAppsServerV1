///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
// * Date: 24 September 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 10 October 2018
// */
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;

//namespace ewApps.Core.CommonService
//{
//  public class AuthenticationHttpClient : HttpRequestProcessor
//  {
//    #region Constructor
//    public AuthenticationHttpClient(HttpClient httpClient, IOptions<AppSettings> appSetting) : base(httpClient) {
//      HttpClientInstance.BaseAddress = new Uri(appSetting.Value.ConnectionStrings.ToString()); // Read from configuration file.
//      AddDefaultHeaders();
//    } 
//    #endregion

//  }
//}

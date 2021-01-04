///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Atul Badgujar <abadgujar@batchmaster.com>
// * Date: 20 November 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 20 November 2018
// */
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ewApps.Report.DS;
//using ewApps.Report.DTO;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ewApps.Report.WebApi.Controllers {
//    /// <summary>
//    ///dashboard Controller expose all Dashboard related APIs, It allow get operation on dashboard entity.
//    /// </summary>
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DashboardController : ControllerBase
//    {

//    /// <summary>
//    /// App controller expose all Application related APIs, It allow add/update/delete operation on Application entity.
//    /// </summary>
    
//    #region Local Member

//    IDashboardDataService _dashboardDataService;

//    #endregion

//    #region  Constructor

//    /// <summary>
//    /// This is the constructor injecting dashboard dataservice
//    /// </summary>
//    /// <param name="dashboardDataService"></param>
//    public DashboardController(IDashboardDataService dashboardDataService) {
//      _dashboardDataService = dashboardDataService;
//    }

//    #endregion Constructor


//    #region Get
//    /// <summary>
//    /// Get Dashboard for App business and subscription 
//    /// </summary>
//    /// <returns></returns>
//    [Route("getallpublisherdashboarddataForbusinessaapandsubscription/{appId}")]
//    [HttpGet]
//    public async Task<PubDashboardAppBusinessAndSubcriptionCount> GetAllPublisherDashboardDataForBusinessAppAndSubscription([FromRoute] Guid appId) {

//      return await _dashboardDataService.GetAllPublisherDashboardDataForBusinessAppAndSubscription(appId);

//    } 
//    #endregion
//  }
//}
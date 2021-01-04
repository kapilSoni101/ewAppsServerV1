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
//  /// <summary>
//  ///dashboard Controller expose all Dashboard related APIs, It allow get operation on dashboard entity.
//  /// </summary>
//  [Route("api/[controller]")]
//  [ApiController]
//  public class BusinessDashboardController :ControllerBase {
//    #region Local Member

//    IBusinessDashboardDataService _dashboardDataService;

//    #endregion

//    #region  Constructor

//    /// <summary>
//    /// This is the constructor injecting dashboard dataservice
//    /// </summary>
//    /// <param name="dashboardDataService"></param>
//    public BusinessDashboardController(IBusinessDashboardDataService dashboardDataService) {
//      _dashboardDataService = dashboardDataService;
//    }

//    #endregion Constructor

//    #region public methods 

//    #region Get

    

//    /// <summary>
//    /// Get Invoice Status Count.
//    /// </summary>
//    /// <returns></returns>
//    [HttpGet]
//    [Route("invoicesstatuscountfortenant")]
//    public async Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByTenant() {
//      return await _dashboardDataService.GetInvoicesStatusCountForDashBoardByTenant();
//    }

//    /// <summary>
//    /// Get Invoice Status Count.
//    /// </summary>
//    /// <returns></returns>
//    [HttpGet]
//    [Route("getmonthnameandsumofinvoicefortenant")]
//    public async Task<List<InoviceAndMonthNameDTO>> GetMonthNameAndSumOfInvoiceByTenant() {
//      return await _dashboardDataService.GetMonthNameAndSumOfInvoiceByTenant();
//    }

//    #endregion

//    #endregion

//  }
//}
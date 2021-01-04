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
//    [ApiController]
//    public class BusinessPartnerDashboardController : ControllerBase
//    {
//    #region Local Member

//    IBusinessPartnerDashboardDataService _dashboardDataService;

//    #endregion

//    #region  Constructor

//    /// <summary>
//    /// This is the constructor injecting dashboard dataservice
//    /// </summary>
//    /// <param name="dashboardDataService"></param>
//    public BusinessPartnerDashboardController(IBusinessPartnerDashboardDataService dashboardDataService) {
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
//    [Route("invoicesstatuscountforcustomer/{customerId}")]
//    public async Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByCustomer(Guid customerId) {
//      return await _dashboardDataService.GetInvoicesStatusCountForDashBoardByCustomer(customerId);
//    }

//    /// <summary>
//    /// Get Invoice Status Count.
//    /// </summary>
//    /// <returns></returns>
//    [HttpGet]
//    [Route("getbusinessnameandsumofinvoiceforcustomer/{customerId}")]
//    public async Task<List<InoviceAndMonthNameDTO>> GetBusinessNameAndSumOfInvoiceByCustomer(Guid customerId) {
//      return await _dashboardDataService.GetBusinessNameAndSumOfInvoiceByCustomer(customerId);
//    }

   

//    #endregion

//    #endregion

//  }
//}
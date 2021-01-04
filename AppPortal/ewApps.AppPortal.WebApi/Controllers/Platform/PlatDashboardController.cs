/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 20 November 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 November 2018
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Report.DTO;
using ewApps.Report.QDS;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    ///dashboard Controller expose all Dashboard related APIs, It allow get operation on dashboard entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatDashboardController:ControllerBase {

        /// <summary>
        /// App controller expose all Application related APIs, It allow add/update/delete operation on Application entity.
        /// </summary>

        #region Local Member

        IQPlatDashboardDS _platDashboardDS;

        #endregion

        #region  Constructor

        /// <summary>
        /// This is the constructor injecting dashboard dataservice
        /// </summary>
        /// <param name="platDashboardDS"></param>
        public PlatDashboardController(IQPlatDashboardDS platDashboardDS) {
            _platDashboardDS = platDashboardDS;
        }

        #endregion Constructor

        #region Get
        /// <summary>
        /// Get Dashboard for App business and subscription 
        /// </summary>
        /// <returns></returns>
        [Route("getallplatformdashboarddataforapplication")]
        [HttpGet]
        public async Task<PlatDashboardAppBusinessAndPublisherCount> GetAllPlatformDashboardDataForApplicationListAsync(CancellationToken token = default(CancellationToken)) {

            return await _platDashboardDS.GetAllPlatformDashboardDataForApplicationListAsync(token);

        }
        #endregion
    }
}
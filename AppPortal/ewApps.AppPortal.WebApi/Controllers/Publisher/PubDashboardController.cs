/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 04 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 04 September 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Report.DTO;
using ewApps.Report.QDS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi
{

    /// <summary>
    ///publisher dashboard Controller expose all Dashboard related APIs, It allow get operation on dashboard entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PubDashboardController : ControllerBase
    {
        /// <summary>
        /// App controller expose all Application related APIs, It allow add/update/delete operation on Application entity.
        /// </summary>

        #region Local Member

        IQPubDashboardDS _pubDashboardDS;

        #endregion

        #region  Constructor

        /// <summary>
        /// This is the constructor injecting dashboard dataservice
        /// </summary>
        /// <param name="pubDashboardDS"></param>
        public PubDashboardController(IQPubDashboardDS pubDashboardDS) {
            _pubDashboardDS = pubDashboardDS;
        }

        #endregion Constructor

        #region Get
        /// <summary>
        /// Get Dashboard for App business and subscription 
        /// </summary>
        /// <returns></returns>
        [Route("getallpublisherdashboarddataForbusinessaapandsubscription")]
        [HttpGet]
        public async Task<PubDashboardAppBusinessAndSubcriptionCount> GetAllPublisherDashboardDataForBusinessAppAndSubscriptionListAsync(CancellationToken token = default(CancellationToken)) {

            return await _pubDashboardDS.GetAllPublisherDashboardDataForBusinessAppAndSubscriptionListAsync(token);

        }
        #endregion

    }
}
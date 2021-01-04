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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    ///Platform Version Controller expose all Version related APIs, It allow get operation on Version.
    /// </summary>
    public class PlatVersionController : ControllerBase
    {
        #region Local Veriable
        IPlatVersionDS _platVersionDS;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize Object
        /// </summary>
        /// <param name="platVersionDS"></param>
        public PlatVersionController(IPlatVersionDS platVersionDS) {
            _platVersionDS = platVersionDS;
        }
        #endregion

        /// <summary>
        /// Get Dashboard for App business and subscription 
        /// </summary>
        /// <returns></returns>
        [Route("platgetapplicationversion")]
        [HttpGet]
        public async Task<ServerVersionDTO> ApplicationVersionAsync() {

            return await _platVersionDS.ApplicationVersionAsync();


        }
    }
}
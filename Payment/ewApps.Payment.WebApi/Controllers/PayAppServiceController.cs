/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Payment.WebApi.Controllers
{

    /// <summary>
    /// Payment is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PayAppServiceController : ControllerBase
    {
        #region Local Variable 
        IPayAppServiceDS _payAppServiceDS;
        #endregion

        public PayAppServiceController(IPayAppServiceDS payAppServiceDS) {
            _payAppServiceDS = payAppServiceDS;
        }

        ///<summary>
        /// Get Branding Setting Detail
        /// <paramref name="appid">App Id </paramref>
        /// <paramref name="tenantid">tenant Id</paramref>
        ///</summary>
        [HttpGet]
        [Route("getbuspayappservice/{tenantId}/{appId}")]
        public async Task<List<PayAppServiceDetailDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync([FromRoute] Guid appId, [FromRoute] Guid tenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _payAppServiceDS.GetBusinessAppServiceListByAppIdsAndTenantIdAsync(appId, tenantId, cancellationToken);
        }

    }
}
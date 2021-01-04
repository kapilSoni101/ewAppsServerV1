/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 31 August 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Business class contains all add/update/delete/get methods for Business.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PubBusinessController:ControllerBase {

        #region Local variables

        IBusinessDS _businessDS;
        IBusinessSignUpDS _businessUserDS;
        IQBusinessAndUserDS _qBusinessDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize constructor for Business Tenant.
        /// </summary>
        /// <param name="businessDS">Business data service.</param>
        public PubBusinessController(IBusinessDS businessDS, IQBusinessAndUserDS qBusinessDS, IBusinessSignUpDS businessUserDS) {
            _businessDS = businessDS;
            _qBusinessDS = qBusinessDS;
            _businessUserDS = businessUserDS;
        }

        /// <summary>
        /// Gets the business name list by publisher id.
        /// </summary>
        /// <param name="publisherId">The publisher identifier.</param>
        /// <returns>Returns list of business names that matches provided publisher id.</returns>
        [HttpGet]
        [Route("businessname/{publisherId}")]
        public List<string> GetBusinessNameListByPublisherId(Guid publisherId) {
            return _qBusinessDS.GetBusinessNameListByPublisherId(publisherId, Core.BaseService.BooleanFilterEnum.All);
        }

        /// <summary>
        /// Gets the active business name list by publisher id.
        /// </summary>
        /// <param name="publisherId">The publisher identifier.</param>
        /// <returns>Returns list of active business names that matches provided publisher id.</returns>
        [HttpGet]
        [Route("businessname/active/{publisherId}")]
        public List<string> GetActiveBusinessNameListByPublisherId(Guid publisherId) {
            return _qBusinessDS.GetBusinessNameListByPublisherId(publisherId, Core.BaseService.BooleanFilterEnum.True);
        }

        #endregion Constructor


    }
}

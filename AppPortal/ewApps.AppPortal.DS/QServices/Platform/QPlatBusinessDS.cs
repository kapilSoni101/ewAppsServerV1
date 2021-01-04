/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 14 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic related to business entity by creating an object to this class.
    /// </summary>
    public class QPlatBusinessDS:IQPlatBusinessDS {

        #region Local Member      
        IQPlatBusinessRepository _platBusinessRepository;

        /// <summary>
        /// Its default entery for business tenant application.
        /// </summary>
        public const string BusinessApplicationId = "F4952EF3-F1BD-4621-A5F9-290FD09BC81B";
        #endregion


        #region Constructor 

        /// <summary>
        /// Dependency injection for all the required data service and services.
        /// </summary>
        public QPlatBusinessDS(IQPlatBusinessRepository platBusinessRepository) {
            _platBusinessRepository = platBusinessRepository;
        }

        #endregion Constructor   
       

        #region Get
        ///<inheritdoc/>
        public async Task<List<PlatBusinessDTO>> GetTenantListOnPlatformAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {            
            Guid homeAppId = new Guid(BusinessApplicationId);
            return await _platBusinessRepository.GetTenantListOnPlatformAsync(filter, homeAppId, token);            
        } 
        #endregion
    }
}

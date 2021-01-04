/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 06 September 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 06 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using System.Linq;
using System.Threading;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class Contain Business Login of PubBusinessSubsPlan 
    /// </summary>   
    public class PubBusinessSubsPlanDS:BaseDS<PubBusinessSubsPlan>, IPubBusinessSubsPlanDS {

        #region Local Member 


        IPubBusinessSubsPlanRepository _pubBusinessSubsPlanRepository;


        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables of Contructor
        /// </summary>
        /// <param name="addressRepo"></param>
        public PubBusinessSubsPlanDS(IPubBusinessSubsPlanRepository pubBusinessSubsPlanRepository) : base(pubBusinessSubsPlanRepository) {
            _pubBusinessSubsPlanRepository = pubBusinessSubsPlanRepository;
        }

        #endregion

        #region Get Methods

        /// <inheritdoc/>   
        public async Task<PubBusinessSubsPlan> GetByPublisherTenantAndAppAndPlanIdAsync(Guid publisherTenantId, Guid appId, Guid subscriptionPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _pubBusinessSubsPlanRepository.GetByPublisherTenantAndAppAndPlanIdAsync(publisherTenantId, appId, subscriptionPlanId, cancellationToken);
        }


        #endregion

        #region Add/Update/Delete Methods

        #endregion
    }
}

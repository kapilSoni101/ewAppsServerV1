/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra.
 * Date: 30 January 2019
 * 
 * Contributor/s: Amit Mundra
 * Last Updated On: 31 January 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Class provide add/update delete tenant appservice methods, it contains the relationship between tenant and app.
    /// </summary>
    public class PubBusinessSubsPlanAppServiceDS:BaseDS<Entity.PubBusinessSubsPlanAppService>, IPubBusinessSubsPlanAppServiceDS {

        IPubBusinessSubsPlanAppServiceRepository _pubAppServiceRepository;

        #region Constructor

        public PubBusinessSubsPlanAppServiceDS(IPubBusinessSubsPlanAppServiceRepository pubAppServiceRepository) : base(pubAppServiceRepository) {
            _pubAppServiceRepository = pubAppServiceRepository;
        }

        #endregion

        #region Get

        public async Task<List<PubBusinessSubsPlanAppService>> GetListPubBusinessSubsPlanAppServiceAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _pubAppServiceRepository.GetListPubBusinessSubsPlanAppService(appId, tenantId, token);
        }

        public async Task<List<PubBusinessSubsPlanAppService>> GetListByPubBusinessSubsPlanIdAsync(Guid pubBusinessSubsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return (await _pubAppServiceRepository.FindAllAsync(i => i.PubBusinessSubsPlanId == pubBusinessSubsPlanId, cancellationToken)).ToList();
        }

        #endregion Get

    }
}

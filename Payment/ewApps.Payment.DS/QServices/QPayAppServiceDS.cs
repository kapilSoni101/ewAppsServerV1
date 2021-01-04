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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DTO;
using ewApps.Payment.QData;

namespace ewApps.Payment.DS {
    public class QPayAppServiceDS :IQPayAppServiceDS {

        #region Local Variable 
        IQPayAppServiceRepository _qPayAppServiceRepository;
        #endregion

        #region Constructor
        public QPayAppServiceDS(IQPayAppServiceRepository qPayAppServiceRepository) {
            _qPayAppServiceRepository = qPayAppServiceRepository;
        } 
        #endregion

        public async Task<List<PayAppServiceDetailDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPayAppServiceRepository.GetBusinessAppServiceListByAppIdsAndTenantIdAsync(appId, tenantId, cancellationToken);
        }

        public async Task<List<PayAppServiceAttributeDetailDTO>> GetBusinessAppServiceAttributeAndAccountListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, Guid serviceId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPayAppServiceRepository.GetBusinessAppServiceAttributeAndAccountListByAppIdsAndTenantIdAsync(appId, tenantId, serviceId, cancellationToken);
        }

        public async Task<List<AppServiceAcctDetailDTO>> GetBusinessAppServiceAccountListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, Guid serviceAttributeId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPayAppServiceRepository.GetBusinessAppServiceAccountListByAppIdsAndTenantIdAsync(appId, tenantId, serviceAttributeId, cancellationToken);
        }
    }
}

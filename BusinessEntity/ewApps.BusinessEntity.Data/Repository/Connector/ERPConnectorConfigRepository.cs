/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// Contains application connector setting.
    /// </summary>
    public class ERPConnectorConfigRepository:BaseRepository<ERPConnectorConfig, BusinessEntityDbContext>, IERPConnectorConfigRepository {

        #region Constructor
        public ERPConnectorConfigRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion Constructor

        #region public methods 

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="token"></param>
        /// <returns>return list of application.</returns>
        public async Task<List<ERPConnectorConfigDQ>> GetBusinessAppConnectorConfigByBusinessIdAsync(Guid businessId, CancellationToken token = default(CancellationToken)) {
            string sql = string.Format(" SELECT ID, SettingJson, Status, Message, ConnectorKey FROM BE.ERPConnectorConfig  Where TenantId= '{0}' And Deleted = 0 ", businessId.ToString());
            return await GetQueryEntityListAsync<ERPConnectorConfigDQ>(sql, null,token);
        }

        /// <summary>
        /// Get business detail by tenantId And AppId.
        /// </summary>
        /// <param name="tenantId">Id of Business Tenant</param>
        /// <param name="appId">Id of Business Application</param>
        /// <param name="token"></param>
        public async Task<ERPConnectorConfig> GetConnectorConfigByTenantIdAndAppIdAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            ERPConnectorConfig appConnectorConfig = await _context.ERPConnectorConfig.Where(a => a.TenantId == tenantId ).FirstOrDefaultAsync(token);
            return appConnectorConfig;
        }

        #endregion public methods 

    }
}

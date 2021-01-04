using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.QData {

    public class QTenantRepository: QBaseRepository<QAppMgmtDbContext> {

        #region Constructor

        /// <summary>
        /// Initlize local variables
        /// </summary>
        /// <param name="qAppMgmtDbContext"></param>
        public QTenantRepository(QAppMgmtDbContext qAppMgmtDbContext): base(qAppMgmtDbContext) {
            
        }

        #endregion Constructor        

        #region Get

        /// <inheritdoc>
        public async Task<List<TenantAppServiceDQ>> GetAppServiceByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {            
            string sql = "SELECT tasl.ID,appS.Name, appS.ID as AppServiceId, appS.AppId, asAttr.Name AS  AttributeName,  tasl.ServiceAttributeId AS AttributeId, tasl.TenantId, actDTl.AccountJson " +
                         "FROM TenantAppServiceLinking tasl INNER JOIN AppService appS ON tasl.ServiceId = appS.ID And tasl.TenantId = '{0}' " +
                         "LEFT JOIN AppServiceAttribute asAttr ON asAttr.AppServiceID = appS.ID AND asAttr.ID = tasl.ServiceAttributeId " +
                         "LEFT JOIN AppServiceAccountDetail actDTl ON actDTl.ServiceID = appS.ID AND actDTl.Deleted = 0 AND actDTl.ServiceAttributeId = tasl.ServiceAttributeId And actDTl.EntityId = '{0}' ";
            sql = string.Format(sql, tenantId.ToString());
            return await GetQueryEntityListAsync<TenantAppServiceDQ>(sql, null, token);
        }

        #endregion Get      

    }
}

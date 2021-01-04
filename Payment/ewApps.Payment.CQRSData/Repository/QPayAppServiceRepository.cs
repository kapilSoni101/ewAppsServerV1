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
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public class QPayAppServiceRepository  :QBaseRepository<QPaymentDBContext>, IQPayAppServiceRepository {        

        /// <summary>
        /// Initlize local variables
        /// </summary>
        /// <param name="qPaymentDBContext"></param>
        public QPayAppServiceRepository(QPaymentDBContext qPaymentDBContext) : base(qPaymentDBContext) {

        }

        ///<inheritdoc/>
        public async Task<List<PayAppServiceDetailDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT Distinct aps.ID,aps.Name,aps.ServiceKey,aps.Active FROM am.TenantSubscription ts
                            INNER JOIN ap.PubBusinessSubsPlan pbsp ON pbsp.ID = ts.SubscriptionPlanId
                            INNER JOIN ap.PubBusinessSubsPlanAppService pbspas ON pbspas.PubBusinessSubsPlanId = pbsp.ID
                            INNER JOIN am.SubscriptionPlan sp ON sp.ID = pbsp.SubscriptionPlanId
                            INNER JOIN am.SubscriptionPlanService sps ON sps.ID = pbspas.SubsPlanAppServiceId
                            INNER JOIN am.AppService aps ON aps.ID = sps.AppServiceId
                            WHERE ts.TenantId = @TenantId AND ts.AppId  = @AppId";         
           
            SqlParameter paramtenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramappId = new SqlParameter("@AppId", appId);

            return await GetQueryEntityListAsync<PayAppServiceDetailDTO>(query, new SqlParameter[] { paramtenantId, paramappId }, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<List<PayAppServiceAttributeDetailDTO>> GetBusinessAppServiceAttributeAndAccountListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId,Guid serviceId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT Distinct apsa.ID,apsa.Name,apsa.AttributeKey,apsa.Active FROM am.TenantSubscription ts
                            INNER JOIN ap.PubBusinessSubsPlan pbsp ON pbsp.ID = ts.SubscriptionPlanId
                            INNER JOIN ap.PubBusinessSubsPlanAppService pbspas ON pbspas.PubBusinessSubsPlanId = pbsp.ID
                            INNER JOIN am.SubscriptionPlan sp ON sp.ID = pbsp.SubscriptionPlanId
                            INNER JOIN am.SubscriptionPlanService sps ON sps.ID = pbspas.SubsPlanAppServiceId
                            INNER JOIN am.SubscriptionPlanServiceAttribute spsa ON spsa.ID = pbspas.SubsPlanAppServiceAttributeId
                            INNER JOIN am.AppService aps ON aps.ID = sps.AppServiceId
                            INNER JOIN am.AppServiceAttribute apsa ON apsa.ID = spsa.AppServiceAttributeId
                            WHERE ts.TenantId = @TenantId AND ts.AppId  = @AppId and aps.ID = @ServiceId";

            SqlParameter paramtenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramappId = new SqlParameter("@AppId", appId);
            SqlParameter paramserviceId = new SqlParameter("@ServiceId", serviceId);

            return await GetQueryEntityListAsync<PayAppServiceAttributeDetailDTO>(query, new SqlParameter[] { paramtenantId, paramappId, paramserviceId }, cancellationToken);
        }

        public async Task<List<AppServiceAcctDetailDTO>> GetBusinessAppServiceAccountListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId,Guid serviceAttributeId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT Distinct apsad.ID,apsad.AccountJson FROM am.TenantSubscription ts
                            INNER JOIN ap.PubBusinessSubsPlan pbsp ON pbsp.ID = ts.SubscriptionPlanId
                            INNER JOIN ap.PubBusinessSubsPlanAppService pbspas ON pbspas.PubBusinessSubsPlanId = pbsp.ID
                            INNER JOIN am.SubscriptionPlan sp ON sp.ID = pbsp.SubscriptionPlanId
                            INNER JOIN am.SubscriptionPlanService sps ON sps.ID = pbspas.SubsPlanAppServiceId
                            INNER JOIN am.SubscriptionPlanServiceAttribute spsa ON spsa.ID = pbspas.SubsPlanAppServiceAttributeId
                            INNER JOIN am.AppService aps ON aps.ID = sps.AppServiceId
                            INNER JOIN am.AppServiceAttribute apsa ON apsa.ID = spsa.AppServiceAttributeId
                            INNER JOIN am.AppServiceAccountDetail apsad ON apsad.TenantId = ts.TenantId AND apsad.AppId = ts.AppId
                            WHERE apsad.TenantId = @TenantId AND apsad.AppId  = @AppId AND apsad.ServiceAttributeId = @ServiceAttributeId";

            SqlParameter paramtenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramappId = new SqlParameter("@AppId", appId);
            SqlParameter paramserviceAttributeId = new SqlParameter("@ServiceAttributeId", serviceAttributeId);

            return await GetQueryEntityListAsync<AppServiceAcctDetailDTO>(query, new SqlParameter[] { paramtenantId, paramappId, paramserviceAttributeId }, cancellationToken);
        }
    }
}

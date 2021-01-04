/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 29 August 2019
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {

    /// <summary>
    /// This class implements standard database logic and operations for Publisher dashboard entity.
    /// </summary>
    public class QPubDashboardRepository:BaseRepository<BaseDTO, QReportDbContext>, IQPubDashboardRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        /// <param name="connectionManager"></param>
        public QPubDashboardRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor 

        #region Publisher Dashboard Repository
        ///<inheritdoc/>
        public async Task<AppAndBusinessCountDTO> GetAllApplicationAndBusinessCountAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(1) AS TotalApplications,
                                 COUNT(CASE WHEN pas.Active = '1' THEN 0 ELSE NULL END) AS ActiveApplications ,
                                 COUNT(CASE WHEN pas.Active = '0' THEN 0 ELSE NULL END) AS InActiveApplications,
                                 (SELECT Count(distinct t.Id) FROM ap.Publisher p										   
						         INNER JOIN am.TenantLinking tl ON tl.PublisherTenantId = p.TenantId
								 INNER JOIN am.Tenant t ON tl.BusinessTenantId = t.ID
								 Where t.Deleted = 0 and t.TenantType = 3 AND p.TenantId = @TenantId) AS TotalBusiness,
                                 (SELECT Count(distinct t.id) FROM am.Tenant t 
								 INNER JOIN am.TenantLinking tl ON t.Id = tl.BusinessTenantId 
								 AND tl.PublisherTenantId=@TenantId AND t.Deleted = 0
								 WHERE t.id  IN(
								 SELECT ts.TenantId FROM am.TenantSubscription ts 
								 INNER JOIN am.app a ON a.id = ts.AppId and a.AppSubscriptionMode = 2
							     AND ts.Deleted=0)) AS BusinessWithApplication,
                                 (SELECT Count(distinct t.id) FROM am.Tenant t 
								 INNER JOIN am.TenantLinking tl ON t.Id = tl.BusinessTenantId 
								 AND tl.PublisherTenantId=@TenantId AND t.Deleted = 0
								 WHERE t.id NOT IN(
								 SELECT ts.TenantId FROM am.TenantSubscription ts 
								 INNER JOIN am.app a ON a.id = ts.AppId and a.AppSubscriptionMode = 2
							     AND ts.Deleted=0)) as BusinessWithOutApplication
                                 FROM am.App a INNER JOIN ap.PublisherAppSetting pas ON pas.AppId = a.Id 
								 WHERE a.Deleted = '0' AND a.AppSubscriptionMode = 2 and pas.TenantId = @TenantId AND pas.Deleted = 0";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityAsync<AppAndBusinessCountDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<BusinessAndSubscriptionCountDTO> GetAllBusinessAndSubscriptionCountAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@"exec prcQPubGetAllBusinessAndSubscriptionCount @AppId, @TenantId";
            SqlParameter comappId = new SqlParameter("@AppId", appId);
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityAsync<BusinessAndSubscriptionCountDTO>(sql.ToString(), parameters: new object[] { comappId, comtenantId });
        }

        ///<inheritdoc/>
        public async Task<ShipmentBusinessAndSubscriptionCountDTO> GetAllShipBusinessAndSubscriptionCountAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@"exec prcQPubGetAllBusinessAndSubscriptionCount @AppId, @TenantId";
            SqlParameter comappId = new SqlParameter("@AppId", appId);
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityAsync<ShipmentBusinessAndSubscriptionCountDTO>(sql.ToString(), parameters: new object[] { comappId, comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<ApplicationUserCountDTO>> GetAllApplicationUserCountListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT a.Name AS Application, COUNT(distinct au.id) AS NoofUser from am.App a 
								 INNER JOIN ap.PublisherAppSetting pas ON pas.AppId = a.Id 
                                 INNER JOIN am.TenantSubscription ts ON a.Id = ts.AppId
								 INNER JOIN am.TenantLinking tl on tl.PublisherTenantId = @TenantId
								 INNER JOIN am.UserTenantLinking UL on UL.TenantId =tl.BusinessTenantId	
								 INNER JOIN am.TenantUser au ON au.ID = UL.TenantUserId
								 INNER JOIN am.TenantUserAppLinking ual on ual.TenantUserId =  au.Id 
                                 WHERE a.Deleted = 0 And  a.AppSubscriptionMode = 2 AND tl.PublisherTenantId = @TenantId 
                                 AND pas.TenantId = @TenantId AND ual.AppId = a.Id
								 AND (ual.Usertype = 3 or ual.Usertype = 4) AND ual.Deleted = 0 AND ts.Deleted = 0 
                                 GROUP BY a.Name,a.CreatedOn order by a.CreatedOn";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<ApplicationUserCountDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<BusinessCountDTO>> GetAllBusinessCountListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@"SELECT COUNT(distinct t.Id) AS NoofBusiness, a.Name AS Application FROM am.App a 
                                   INNER JOIN ap.PublisherAppSetting pas ON pas.AppId = a.Id  
                                   INNER JOIN am.TenantSubscription ts ON a.Id = ts.AppId 
					               INNER JOIN am.Tenant t on t.Id = ts.TenantId and ts.Deleted = 0
								   INNER JOIN am.TenantLinking tl on t.Id = tl.BusinessTenantId 
					               AND t.Deleted = 0 and t.TenantType = 3
                                   WHERE a.AppSubscriptionMode = 2 AND a.Deleted = 0 AND tl.PublisherTenantId = @TenantId 
                                   And ts.Deleted = 0 AND pas.TenantId = @TenantId
					               GROUP BY a.name,a.CreatedOn order by a.CreatedOn";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<BusinessCountDTO>(sql.ToString(), parameters: new object[] { comtenantId });


        }

        ///<inheritdoc/>
        public async Task<List<BusinessNameAndSumCount>> GetAllBusinessNameWithHeightestAmountListAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@"SELECT TOP 5 t.Name AS BusinessName,SUM(Amount) as TotalBusinessSum FROM pay.Payment p
                                INNER JOIN am.Tenant t ON t.ID = p.TenantId and t.TenantType = 3
								INNER JOIN am.TenantLinking tl ON tl.BusinessTenantId = t.Id
                                INNER JOIN am.TenantSubscription ts ON ts.TenantId = p.TenantId and ts.AppId = @AppId 
                                WHERE tl.PublisherTenantId = @TenantId and p.CreatedOn 
                                BETWEEN DATEADD(DAY, -7, getdate()) and (getDate() + 1)
                                GROUP BY p.TenantId,t.Name  ORDER BY TotalBusinessSum DESC";
            SqlParameter comappId = new SqlParameter("@AppId", appId);
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<BusinessNameAndSumCount>(sql.ToString(), parameters: new object[] { comappId, comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<ShipmentBusinessNameAndSumCount>> GetAllShipBusinessNameWithMaximumShippingOrdersListAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@";WIth SalesOrderTenantId AS (SELECT TOP 5 t.ID FROM be.BASalesOrder so
								INNER JOIN am.Tenant t ON t.ID = so.TenantId and t.TenantType = 3								
                                INNER JOIN am.TenantSubscription ts ON ts.TenantId = so.TenantId and ts.AppId = @AppId 	
								INNER JOIN am.TenantLinking tl ON tl.BusinessTenantId = t.Id AND tl.BusinessPartnerTenantId is null						
								WHERE tl.PublisherTenantId = @TenantId
							    GROUP BY t.ID ORDER BY COUNT(DISTINCT(so.ID)) desc)
								SELECT t.Name AS BusinessName,COUNT(DISTINCT(so.ID)) AS SalesOrders,so.ShippingTypeText AS ServiceName,t.ID FROM be.BASalesOrder so
								INNER JOIN SalesOrderTenantId s ON s.ID = so.TenantId 
								INNER JOIN am.Tenant t ON t.ID = s.ID
                                WHERE so.CreatedOn BETWEEN DateAdd(Day,-7, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0)) 
                                AND DATEADD(dd, DATEDIFF(dd, -1, SysUTCDATETIME()), 0) 
							    GROUP BY t.Name,so.ShippingTypeText,t.ID ORDER BY t.Name";
            SqlParameter comappId = new SqlParameter("@AppId", appId);
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<ShipmentBusinessNameAndSumCount>(sql.ToString(), parameters: new object[] { comappId, comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<BusinessAddedCountAndMonthDTO>> GetAllBusinessCountPerMonthListAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC prc_QPubGetAllBusinessCountPerMonthForPublisher @AppId, @TenantId";

            SqlParameter comappId = new SqlParameter("@AppId", appId);
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<BusinessAddedCountAndMonthDTO>(sql, parameters: new object[] { comappId, comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<ShipmentBusinessAddedCountAndMonthDTO>> GetAllShipBusinessCountPerMonthListAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC prc_QPubGetAllBusinessCountPerMonthForPublisher @AppId, @TenantId";

            SqlParameter comappId = new SqlParameter("@AppId", appId);
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<ShipmentBusinessAddedCountAndMonthDTO>(sql, parameters: new object[] { comappId, comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<ShipmentServiceNameAndCountDTO>> GetAllServiceNameForShipmentsCountListForPublisherAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(DISTINCT(s.ID)) AS NoOfShipment,aps.Name as ServiceName FROM ship.Shipment s
							          INNER JOIN am.AppService aps On aps.ID = s.CarrierId
									  INNER JOIN am.TenantLinking tl ON tl.PublisherTenantId = @TenantId   
							          WHERE s.Deleted = 0 AND tl.BusinessTenantId = s.TenantId   
							          GROUP BY aps.Id,aps.Name";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<ShipmentServiceNameAndCountDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<AppDTO>> GetAllPubAppListAsync(CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ID,AppKey FROM am.App WHERE AppSubscriptionMode = 2";
            return await GetQueryEntityListAsync<AppDTO>(sql.ToString(), null);
        }

        ///<inheritdoc/>
        public async Task<List<ShipmentServiceNameAndCountDTO>> GetAllServiceNameForShipmentsCountListForPublisherAsync(CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(DISTINCT(s.ID)) AS NoOfShipment,aps.Name as ServiceName FROM ship.Shipment s
							          INNER JOIN am.AppService aps On aps.ID = s.CarrierId
							          WHERE s.Deleted = 0 GROUP BY aps.Id,aps.Name";

            //SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<ShipmentServiceNameAndCountDTO>(sql.ToString(), null);
        }

        #endregion
    }
}

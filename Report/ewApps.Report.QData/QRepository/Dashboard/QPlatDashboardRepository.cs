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
    /// This class implements standard database logic and operations for Platfrom dashboard entity.
    /// </summary>
    public class QPlatDashboardRepository:BaseRepository<BaseDTO, QReportDbContext>, IQPlatDashboardRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        /// <param name="connectionManager"></param>
        public QPlatDashboardRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor 

        #region Platform Dashboard Repository
        ///<inheritdoc/>
        public async Task<List<ApplicationPublisherCountDTO>> GetAllApplicationPublisherCountListAsync(CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(distinct p.id) AS NoOfPublisher,a.Name AS Application FROM am.App a 
								                 INNER JOIN ap.PublisherAppSetting pas ON pas.AppId = a.ID 
								                 INNER JOIN ap.Publisher p ON p.TenantId = pas.TenantId 
								                 WHERE a.AppSubscriptionMode = 2 AND a.Deleted = 0 AND p.Deleted = 0
								                 GROUP BY a.name,a.CreatedOn ORDER BY a.CreatedOn";
            return await GetQueryEntityListAsync<ApplicationPublisherCountDTO>(sql.ToString(), null);
        }

        public async Task<List<PublisherTenantCountDTO>> GetAllPublisherTenantCountListAsync(CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT TOP 10 COUNT(distinct t.Id) AS NoOfBusiness, p.Name AS Publisher FROM ap.Publisher p
                                           LEFT JOIN am.TenantLinking tl ON p.TenantId = tl.PublisherTenantId
					                       LEFT JOIN am.Tenant t ON t.Id = tl.BusinessTenantId
					                       WHERE t.Deleted = 0 AND p.Deleted = 0 
					                       GROUP BY tl.PublisherTenantId,p.name ORDER BY COUNT(DISTINCT t.id) DESC";
            return await GetQueryEntityListAsync<PublisherTenantCountDTO>(sql.ToString(), null);
        }

        ///<inheritdoc/>
        public async Task<PlatAppAndBusinessCountDTO> GetAllPlatApplicationAndBusinessCountAsync(CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(1) AS TotalApplications,
                                 COUNT(CASE WHEN Active = '1' THEN 0 ELSE NULL END) AS ActiveApplications ,
                                 COUNT(CASE WHEN Active = '0' THEN 0 ELSE NULL END) AS InActiveApplications,
                                 (SELECT Count(distinct t.Id) FROM ap.Publisher p										   
										             INNER JOIN am.TenantLinking tl ON tl.PublisherTenantId = p.TenantId
										             INNER JOIN am.Tenant t ON tl.BusinessTenantId = t.ID
										             Where t.Deleted = 0 and t.TenantType = 3) AS TotalBusiness,
                                 (SELECT Count(distinct t.id) FROM am.Tenant t								 
								 INNER JOIN am.TenantLinking tl ON t.Id = tl.BusinessTenantId 
								 INNER JOIN ap.Publisher p on p.TenantId = tl.PublisherTenantId 
								 AND t.Deleted = 0 AND p.Deleted = 0
								 WHERE t.id IN(
								 SELECT ts.TenantId FROM am.TenantSubscription ts 
								 INNER JOIN am.app a ON a.id = ts.AppId and a.AppSubscriptionMode = 2
							     AND ts.Deleted=0)) AS BusinessWithApplication,
                                 ( SELECT Count(distinct t.id) FROM am.Tenant t								 
								 INNER JOIN am.TenantLinking tl ON t.Id = tl.BusinessTenantId 
								 INNER JOIN ap.Publisher p on p.TenantId = tl.PublisherTenantId 
								 AND t.Deleted = 0 AND p.Deleted = 0
								 WHERE t.id Not IN(
								 SELECT ts.TenantId FROM am.TenantSubscription ts 
								 INNER JOIN am.app a ON a.id = ts.AppId and a.AppSubscriptionMode = 2
							     AND ts.Deleted=0)) as BusinessWithOutApplication,
                                 (SELECT COUNT(1) FROM ap.Publisher) AS TotalPublisher
                                 FROM am.App WHERE Deleted = '0' AND AppSubscriptionMode = 2";
            return await GetQueryEntityAsync<PlatAppAndBusinessCountDTO>(sql.ToString(), null);
        }

        ///<inheritdoc/>
        public async Task<AapNameAndBusinessCountDTO> GetAllPlatAppNameAndBusinessCountAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@"SELECT (COUNT(DISTINCT t.Id)) as TotalBusiness,
                                       (SELECT COUNT(DISTINCT au.Id) FROM am.TenantUser au 
									   INNER JOIN am.UserTenantLinking UL ON UL.TenantUserId = au.Id
                                       INNER JOIN am.TenantSubscription ts ON ts.TenantId = UL.TenantId AND ts.AppId = @AppId 
									   INNER JOIN am.TenantLinking tl ON tl.BusinessTenantId = UL.TenantId AND tl.PublisherTenantId IS NOT NULL
									   INNER JOIN am.TenantUserAppLinking ual ON ual.AppId = @AppId AND ual.TenantUserId = au.ID
                                       WHERE au.Deleted = 0 AND ts.Deleted = 0 AND ual.UserType = 3) AS TotalBusinessUser,
                                       (SELECT Name FROM am.App Where Id = @AppId) AS AppName,
                                       COUNT(CASE WHEN t.Active = 1 THEN 0 ELSE NULL END) as ActiveBusiness,
                                       COUNT(CASE WHEN t.Active = 0 THEN 0 ELSE NULL END) as InActiveBusiness,
                                       (SELECT COUNT(DISTINCT TenantId) FROM am.TenantSubscription ts 
                                       INNER JOIN am.Tenant t ON ts.TenantId = t.Id
                                       WHERE t.createdon BETWEEN DATEADD(dd, -7, GETDATE()) AND (GETDATE()+1) 
                                       AND AppId = @AppId AND ts.Deleted = 0  AND t.TenantType = 3 AND t.Deleted = 0) as BusinessAddedLaskWeek
                                       from am.Tenant t INNER JOIN am.TenantSubscription ts on t.id = ts.TenantId 
                                       INNER JOIN am.App a on a.Id = ts.AppId AND ts.AppId = @AppId AND t.Deleted = 0 
                                       AND ts.Deleted = 0 AND a.Deleted = 0 AND t.TenantType = 3";
            SqlParameter comappId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityAsync<AapNameAndBusinessCountDTO>(sql.ToString(), parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<ShipmentAapNameAndBusinessCountDTO> GetAllPlatShipAppNameAndBusinessCountAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@"SELECT (COUNT(DISTINCT t.Id)) as TotalBusiness,
                                       (SELECT COUNT(DISTINCT au.Id) FROM am.TenantUser au 
									   INNER JOIN am.UserTenantLinking UL ON UL.TenantUserId = au.Id
                                       INNER JOIN am.TenantSubscription ts ON ts.TenantId = UL.TenantId AND ts.AppId = @AppId 
									   INNER JOIN am.TenantLinking tl ON tl.BusinessTenantId = UL.TenantId AND tl.PublisherTenantId IS NOT NULL
									   INNER JOIN am.TenantUserAppLinking ual ON ual.AppId = @AppId AND ual.TenantUserId = au.ID
                                       WHERE au.Deleted = 0 AND ts.Deleted = 0 AND ual.UserType = 3) AS TotalBusinessUser,
                                       (SELECT Name FROM am.App Where Id = @AppId) AS AppName,
                                       COUNT(CASE WHEN t.Active = 1 THEN 0 ELSE NULL END) as ActiveBusiness,
                                       COUNT(CASE WHEN t.Active = 0 THEN 0 ELSE NULL END) as InActiveBusiness,
                                       (SELECT COUNT(DISTINCT TenantId) FROM am.TenantSubscription ts 
                                       INNER JOIN am.Tenant t ON ts.TenantId = t.Id
                                       WHERE t.createdon BETWEEN DATEADD(dd, -7, GETDATE()) AND (GETDATE()+1) 
                                       AND AppId = @AppId AND ts.Deleted = 0  AND t.TenantType = 3 AND t.Deleted = 0) as BusinessAddedLaskWeek
                                       from am.Tenant t INNER JOIN am.TenantSubscription ts on t.id = ts.TenantId 
                                       INNER JOIN am.App a on a.Id = ts.AppId AND ts.AppId = @AppId AND t.Deleted = 0 
                                       AND ts.Deleted = 0 AND a.Deleted = 0 AND t.TenantType = 3";
            SqlParameter comappId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityAsync<ShipmentAapNameAndBusinessCountDTO>(sql.ToString(), parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<ApplicationUserCountDTO>> GetAllPlatApplicationUserCountListAsync(CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT a.Name AS Application, COUNT(DISTINCT au.id) AS NoofUser from am.App a 
                                 INNER JOIN am.TenantSubscription ts ON a.Id = ts.AppId
								 INNER JOIN am.TenantLinking tl ON tl.PublisherTenantId IS NOT NULL AND tl.BusinessTenantId = ts.TenantId
                                 INNER JOIN am.UserTenantLinking UL on UL.TenantId =tl.BusinessTenantId	
								 INNER JOIN am.TenantUser au ON au.ID = UL.TenantUserId
								 INNER JOIN am.TenantUserAppLinking ual on ual.TenantUserId = au.Id and ual.AppId = a.ID 
								 WHERE a.Deleted = 0 ANd a.AppSubscriptionMode = 2 AND (ual.Usertype = 3 or ual.Usertype = 4) AND ual.Deleted = 0 AND ts.Deleted = 0 
                                 GROUP BY a.Name,a.CreatedOn order by a.CreatedOn";
            return await GetQueryEntityListAsync<ApplicationUserCountDTO>(sql.ToString(), null);
        }

        ///<inheritdoc/>
        public async Task<List<BusinessCountDTO>> GetAllPlatBusinessCountListAsync(CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@"SELECT COUNT(DISTINCT t.Id) AS NoofBusiness, a.Name AS Application FROM am.App a 
                                   INNER JOIN am.TenantSubscription ts ON a.Id = ts.AppId 
					               INNER join am.Tenant t on t.Id = ts.TenantId and ts.Deleted = 0 											             
					               AND t.Deleted = 0 and t.TenantType = 3
                                   WHERE a.AppSubscriptionMode = 2 AND a.Deleted = 0 
					               GROUP BY a.name,a.CreatedOn order by a.CreatedOn";
            return await GetQueryEntityListAsync<BusinessCountDTO>(sql.ToString(), null);
        }

        ///<inheritdoc/>
        public async Task<List<BusinessNameAndSumCount>> GetAllPlatBusinessNameWithHeightestAmountListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@"SELECT TOP 5 t.Name AS BusinessName,SUM(Amount) as TotalBusinessSum FROM pay.Payment p
                                INNER JOIN am.Tenant t ON t.ID = p.TenantId and t.TenantType = 3
                                INNER JOIN am.TenantSubscription ts ON ts.TenantId = p.TenantId and ts.AppId = @AppId 
                                and p.CreatedOn BETWEEN DATEADD(DAY, -7, getdate()) and (getDate() + 1)
                                GROUP BY p.TenantId,t.Name  ORDER BY TotalBusinessSum DESC";
            SqlParameter comappId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityListAsync<BusinessNameAndSumCount>(sql.ToString(), parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<ShipmentBusinessNameAndSumCount>> GetAllPlatShipBusinessNameWithMaximumShippedOrderListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            FormattableString sql = $@";WIth SalesOrderTenantId AS (SELECT TOP 5 t.ID FROM be.BASalesOrder so
								INNER JOIN am.Tenant t ON t.ID = so.TenantId and t.TenantType = 3								
                                INNER JOIN am.TenantSubscription ts ON ts.TenantId = so.TenantId and ts.AppId = @AppId 	
							    GROUP BY t.ID ORDER BY COUNT(DISTINCT(so.ID)) desc)
								SELECT t.Name AS BusinessName,COUNT(DISTINCT(so.ID)) AS SalesOrders,so.ShippingTypeText AS ServiceName,t.ID FROM be.BASalesOrder so
								INNER JOIN SalesOrderTenantId s ON s.ID = so.TenantId 
								INNER JOIN am.Tenant t ON t.ID = s.ID
								WHERE so.CreatedOn BETWEEN DateAdd(Day,-7, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0)) 
                                AND DATEADD(dd, DATEDIFF(dd, -1, SysUTCDATETIME()), 0) 
							    GROUP BY t.Name,so.ShippingTypeText,t.ID ORDER BY t.Name";
            
            SqlParameter comappId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityListAsync<ShipmentBusinessNameAndSumCount>(sql.ToString(), parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<BusinessAddedCountAndMonthDTO>> GetAllPlatBusinessCountPerMonthListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC prc_QPlatGetAllBusinessCountPerMonth @AppId";

            SqlParameter comappId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityListAsync<BusinessAddedCountAndMonthDTO>(sql, parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<ShipmentBusinessAddedCountAndMonthDTO>> GetAllPlatShipBusinessCountPerMonthListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC prc_QPlatGetAllBusinessCountPerMonth @AppId";

            SqlParameter comappId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityListAsync<ShipmentBusinessAddedCountAndMonthDTO>(sql, parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<ShipmentServiceNameAndCountDTO>> GetAllServiceNameForShipmentsCountListForPlatformAsync(CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(DISTINCT(s.ID)) AS NoOfShipment,aps.Name as ServiceName FROM ship.Shipment s
							          INNER JOIN am.AppService aps On aps.ID = s.CarrierId
							          WHERE s.Deleted = 0 GROUP BY aps.Id,aps.Name";

            //SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<ShipmentServiceNameAndCountDTO>(sql.ToString(), null);
        }

        ///<inheritdoc/>
        public async Task<List<AppDTO>> GetAllPlatAppListAsync(CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ID,AppKey FROM am.App WHERE AppSubscriptionMode = 2";
            return await GetQueryEntityListAsync<AppDTO>(sql.ToString(), null);
        }
        #endregion

    }
}

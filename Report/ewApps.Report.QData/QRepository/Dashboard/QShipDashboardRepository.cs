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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {

    /// <summary>
    /// This class implements standard database logic and operations for Shipment dashboard entity.
    /// </summary>
    public class QShipDashboardRepository:BaseRepository<BaseDTO, QReportDbContext>, IQShipDashboardRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        
        public QShipDashboardRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Shipment Dashboard Repository

        ///<inheritdoc/>
        public async Task<SalesOrderStatusDTO> GetSalesOrderStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            

            FormattableString sql = $@"SELECT COUNT(DISTINCT(CASE WHEN so.Status = 1 THEN so.ID END)) AS TotalOpenOrders,
                                    COUNT(DISTINCT(CASE WHEN so.Status = 3 THEN so.ID END)) AS CompletedOrders,
                                    COUNT(DISTINCT(CASE WHEN so.Status = 2 THEN so.ID END)) AS PartialDeliveredOrders,
									ISNULL(SUM(so.SubTotal),0) AS TotalSales 
                                    FROM SalesOrder so                                   
                                    WHERE  so.TenantId = @TenantId AND so.Deleted = 0";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return GetQueryEntityList<SalesOrderStatusDTO>(sql.ToString(), parameters: new object[] { comtenantId }).FirstOrDefault<SalesOrderStatusDTO>();
        }

        ///<inheritdoc/>
        public async Task<List<SaleOrderCreatedMonthWiseDTO>> GetMonthNameAndSumOfSalesOrderByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC stpr_GetMonthNameAndNoOfSalesOrderByTenant @TenantId";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<SaleOrderCreatedMonthWiseDTO>(sql, parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<ShippingStatusDTO> GetShippingStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(CASE WHEN Status = 2 THEN ID END) AS PendingShipping,
									COUNT(CASE WHEN Status = 3 THEN ID END) AS TotalShipped
									FROM Shipment WHERE TenantId = @TenantId AND Deleted = 0";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return GetQueryEntityList<ShippingStatusDTO>(sql.ToString(), parameters: new object[] { comtenantId }).FirstOrDefault<ShippingStatusDTO>();
        }

        ///<inheritdoc/>
        public async Task<List<TotalDeliveriesMonthWiseDTO>> GetMonthNameAndSumOfTotalDeliveriesByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC stpr_GetMonthNameAndNoOfShipmentByTenant @TenantId";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<TotalDeliveriesMonthWiseDTO>(sql, parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<UpComingShipmentsDTO>> GetAllUpcomingShipmentsByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT s.ID,s.OrderRefId,c.CustomerRefId,c.Name AS CustomerName FROM SalesOrder s
							  INNER JOIN Customer c ON c.Id = s.CustomerId
							  WHERE s.Deleted = 0 AND s.TenantId = @TenantId 
                              AND s.CarrierExpectedDeliveryDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0) 
                              AND DateAdd(Month,3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
							  AND s.DeliveryDate = '0001-01-01 00:00:00.0000000' order By s.CreatedOn desc";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<UpComingShipmentsDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }


        ///<inheritdoc/>
        public async Task<List<RecentShipmentsDTO>> GetAllRecentShipmentsByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT s.ID,s.ShipmentRefId ,c.Name AS CustomerName,s.PostingOn AS PostedOn,s.ShipmentRefId AS ShippingNo FROM Shipment s							 
							  INNER JOIN Customer c ON c.Id = s.CustomerId
							  WHERE s.Deleted = 0 AND s.TenantId = @TenantId AND s.Status = 3
                              AND s.DeliveryDate BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                              AND SysUTCDATETIME() order by s.CreatedOn Desc";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<RecentShipmentsDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<TotalSalesMonthWiseDTO>> GetMonthNameAndSumOfTotalSalesByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"EXEC stpr_GetMonthNameAndSalesOrderAmountByTenant @TenantId";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<TotalSalesMonthWiseDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<ValuableCustomerDTO>> GetTotalTopValuaubleCustomerByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT Top 5 SUM(SubTotal) AS Value,COUNT(so.ID) AS Quantity,c.Name FROM SalesOrder so
                                    INNER JOIN Customer c ON c.ID = so.CustomerId
                                    WHERE so.TenantId = @TenantId AND so.Deleted = 0 AND so.Cancel = 0
                                    GROUP BY c.ID,c.Name ORDER BY SUM(SubTotal) DESC";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<ValuableCustomerDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<ShipmentServiceNameAndCountDTO>> GetAllServiceNameForShipmentsCountListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(DISTINCT(s.ID)) AS NoOfShipment,aps.Name as ServiceName FROM Shipment s
							          INNER JOIN AppService aps On aps.ID = s.CarrierId
							          WHERE s.Deleted = 0 AND s.TenantId = @TenantId 
							          GROUP BY aps.Id,aps.Name";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<ShipmentServiceNameAndCountDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }



        #endregion
    }
}

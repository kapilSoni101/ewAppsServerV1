/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 15 Oct 2019
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
    /// This class implements standard database logic and operations for Business portal cust app dashboard entity.
    /// </summary>
    public class QBizCustDashboardRepository:BaseRepository<BaseDTO, QReportDbContext>, IQBizCustDashboardRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>        
        public QBizCustDashboardRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor 

        ///<inheritdoc/>
        public async Task<SalesQuotationsAndOrdersStatusCountDTO> GetSalesQuotationsAndOrdersStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ISNULL(COUNT(DISTINCT ID),0) AS SalesOrders,ISNULL(SUM(TotalPaymentDue),0) AS SalesOrderAmount,
								     ISNULL((SELECT COUNT(DISTINCT ID) FROM be.BASalesQuotation WHERE TenantId = @TenantId AND Deleted = 0 AND Createdon 
                                     BETWEEN DATEADD(day, -7, getdate()) and getDate() + 1),0) AS SalesQuotaions FROM be.BASalesOrder WHERE TenantId = @TenantId AND Deleted = 0 AND Createdon 
                                     BETWEEN DATEADD(day, -7, getdate()) and getDate() + 1";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityAsync<SalesQuotationsAndOrdersStatusCountDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }      

        ///<inheritdoc/>
        public async Task<List<RecentSalesQuotationsDTO>> GetAllRecentSalesQuotationsByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ID,ERPDocNum,ERPSalesQuotationKey,ERPCustomerKey,CustomerId,CustomerName,PostingDate,ValidUntil,TotalPaymentDue,Status,StatusText
                                 FROM BE.BASalesQuotation         
                                 WHERE TenantId = @TenantId AND Deleted = 0 AND Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() ORDER BY CreatedOn DESC";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<RecentSalesQuotationsDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<RecentSalesOrdersDTO>> GetAllRecentSalesOrdersByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ID,ERPDocNum,ERPSalesOrderKey,ERPCustomerKey,CustomerId,CustomerName,PostingDate,DocumentDate,TotalPaymentDue,Status,StatusText
                                 FROM BE.BASalesOrder         
                                 WHERE TenantId = @TenantId AND Deleted = 0 AND Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() ORDER BY CreatedOn DESC";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<RecentSalesOrdersDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<RecentDeliveriesDTO>> GetAllRecentDeliveriesByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ID,ERPDocNum,ERPDeliveryKey,ERPCustomerKey,CustomerId,CustomerName,PostingDate,DeliveryDate,TotalPaymentDue,Status,StatusText,ShippingType,ShippingTypeText
                                 FROM BE.BADelivery         
                                 WHERE TenantId = @TenantId AND Deleted = 0 AND Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() ORDER BY CreatedOn DESC";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<RecentDeliveriesDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<UpcomingDeliveriesDTO>> GetAllUpcomingDeliveriesByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ID,ERPDocNum,ERPDeliveryKey,ERPCustomerKey,CustomerId,CustomerName,DeliveryDate,TotalPaymentDue,Status,StatusText,ShippingType,ShippingTypeText
                              FROM BE.BADelivery                                                            
                              WHERE TenantId = @TenantId AND Deleted = 0
                              AND DeliveryDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0) 
                              AND DateAdd(Month,3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<UpcomingDeliveriesDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<DeliveriesStatusCountDTO> GetDeliveriesStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ISNULL(COUNT(DISTINCT ID),0) AS OutgoingDeliveries,ISNULL(SUM(TotalPaymentDue),0) AS OutgoingDeliveriesAmount
								     FROM be.BADelivery WHERE TenantId = @TenantId AND Deleted = 0 AND Createdon 
                                     BETWEEN DATEADD(day, -7, getdate()) and getDate() + 1";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityAsync<DeliveriesStatusCountDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }
    }
}

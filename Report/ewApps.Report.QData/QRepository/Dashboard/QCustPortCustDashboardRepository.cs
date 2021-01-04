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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {  

        /// <summary>
        /// This class implements standard database logic and operations for Customer portal cust app dashboard entity.
        /// </summary>
        public class QCustPortCustDashboardRepository:BaseRepository<BaseDTO, QReportDbContext>, IQCustPortCustDashboardRepository {

            #region Constructor 

            /// <summary>
            ///  Constructor initializing the base variables
            /// </summary>
            /// <param name="context"></param>
            /// <param name="sessionManager"></param>        
            public QCustPortCustDashboardRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            }

        #endregion Constructor 

       

        ///<inheritdoc/>
        public async Task<List<RecentPurchaseQuotationsFCDTO>> GetAllRecentPurchaseQuotationsByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT sq.ID,sq.ERPDocNum,sq.ERPSalesQuotationKey,sq.PostingDate,sq.ValidUntil,sq.TotalPaymentDue AS TotalPaymentDueFC,sq.Status,sq.StatusText,
								 (SELECT COUNT(DISTINCT ID) FROM BE.BASalesQuotationItem WHERE SalesQuotationId = ID AND Deleted = 0) AS TotalLineItems
                                 FROM BE.BASalesQuotation sq
								 INNER JOIN be.BACustomer c ON c.ID = sq.CustomerId        
                                 WHERE BusinessPartnerTenantId = @CustomerId AND sq.Deleted = 0 AND sq.Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() ORDER BY sq.CreatedOn DESC";

            SqlParameter comcustomerId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<RecentPurchaseQuotationsFCDTO>(sql.ToString(), parameters: new object[] { comcustomerId });
        }

        ///<inheritdoc/>
        public async Task<List<RecentSalesOrdersFCDTO>> GetAllRecentSalesOrdersByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT so.ID,so.ERPDocNum,so.ERPSalesOrderKey,so.PostingDate,so.DocumentDate,so.TotalPaymentDue AS TotalPaymentDueFC,so.Status,so.StatusText,
                                 (SELECT COUNT(DISTINCT ID) FROM BE.BASalesOrderItem WHERE SalesOrderId = ID AND Deleted = 0) AS TotalLineItems
                                 FROM BE.BASalesOrder so
								 INNER JOIN be.BACustomer c ON c.ID = so.CustomerId        
                                 WHERE BusinessPartnerTenantId = @CustomerId AND so.Deleted = 0 AND so.Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() ORDER BY so.CreatedOn DESC";

            SqlParameter comcustomerId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<RecentSalesOrdersFCDTO>(sql.ToString(), parameters: new object[] { comcustomerId });
        }

        ///<inheritdoc/>
        public async Task<List<RecentDeliveriesFCDTO>> GetAllRecentDeliveriesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT d.ID,d.ERPDocNum,d.ERPDeliveryKey,d.PostingDate,d.DeliveryDate,d.TotalPaymentDue AS TotalPaymentDueFC,d.ShippingType,d.ShippingTypeText,d.Status,d.StatusText,
                                 (SELECT COUNT(DISTINCT ID) FROM BE.BADeliveryItem WHERE DeliveryID = ID AND Deleted = 0) AS TotalLineItems
                                 FROM BE.BADelivery d
								 INNER JOIN be.BACustomer c ON c.ID = d.CustomerId        
                                 WHERE BusinessPartnerTenantId = @CustomerId AND d.Deleted = 0 AND d.Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() ORDER BY d.CreatedOn DESC";

            SqlParameter comcustomerId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<RecentDeliveriesFCDTO>(sql.ToString(), parameters: new object[] { comcustomerId });
        }

        ///<inheritdoc/>
        public async Task<List<UpcomingDeliveriesFCDTO>> GetAllUpcomingDeliveriesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT d.ID,d.ERPDocNum,d.ERPDeliveryKey,d.DeliveryDate,d.PostingDate,d.TotalPaymentDue AS TotalPaymentDueFC,d.Status,d.StatusText,d.ShippingType,d.ShippingTypeText,
							  (SELECT COUNT(DISTINCT ID) FROM BE.BADeliveryItem WHERE DeliveryID = ID AND Deleted = 0) AS TotalLineItems
                              FROM BE.BADelivery d 
							  INNER JOIN be.BACustomer c ON c.ID = d.CustomerId        
                              WHERE BusinessPartnerTenantId = @CustomerId AND d.Deleted = 0
                              AND d.DeliveryDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0) 
                              AND DateAdd(Month,3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))";

            SqlParameter comcustomerId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<UpcomingDeliveriesFCDTO>(sql.ToString(), parameters: new object[] { comcustomerId });
        }

        ///<inheritdoc/>
        public async Task<DeliveriesStatusCountFCDTO> GetDeliveriesStatusCountForDashBoardByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT ISNULL(COUNT(DISTINCT d.ID),0) AS OutgoingDeliveries,ISNULL(SUM(d.TotalPaymentDue),0) AS OutgoingDeliveriesAmount
								     FROM be.BADelivery d
									 INNER JOIN be.BACustomer c ON c.Id = d.CustomerID
									 WHERE BusinessPartnerTenantId = @CustomerId AND d.Deleted = 0 AND d.Createdon 
                                     BETWEEN DATEADD(day, -7, getdate()) and getDate() + 1";
            SqlParameter comcustomerId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityAsync<DeliveriesStatusCountFCDTO>(sql.ToString(), parameters: new object[] { comcustomerId });
        }

        ///<inheritdoc/>
        public async Task<List<PurchaseAndMonthNameFCDTO>> GetMonthNameAndSumOfPurchaseByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC prcQGetMonthNameAndSumOfPurchaseByCustomer @CustomerId";

            SqlParameter comappId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<PurchaseAndMonthNameFCDTO>(sql, parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<SalesOrdersItemsCountFCDTO>> GetItemNameAndSumOfItemsQuantityByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string sql = $@"SELECT Top 10 COUNT(soi.SalesOrderId) AS NoOfItems,soi.ItemName AS SalesOrderName FROM be.BASalesOrderItem soi
                            INNER JOIN be.BASalesOrder so ON so.Id = soi.SalesOrderId
                            INNER JOIN be.BACustomer c ON c.Id = so.CustomerID
							Where c.BusinessPartnerTenantId = @CustomerId
                            GROUP BY soi.ItemName,soi.SalesOrderId ORDER BY COUNT(soi.SalesOrderId) DESC";

            SqlParameter comappId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<SalesOrdersItemsCountFCDTO>(sql, parameters: new object[] { comappId });
        }
    }
    }

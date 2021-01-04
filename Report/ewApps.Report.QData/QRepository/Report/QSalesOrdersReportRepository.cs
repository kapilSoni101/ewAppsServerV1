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
        /// This class implements standard database logic and operations for Sales Orders Report entity.
        /// </summary>
        public class QSalesOrdersReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQSalesOrdersReportRepository {

            #region Constructor 

            /// <summary>
            ///  Constructor initializing the base variables
            /// </summary>
            /// <param name="context"></param>
            /// <param name="sessionManager"></param>            
            public QSalesOrdersReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            }

        #endregion Constructor


        #region Business
        ///<inheritdoc/>
        public async Task<List<BizCustSalesOrdersReportDTO>> GetBizCustSalesOrdersListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT ID,ERPDocNum,ERPSalesOrderKey,ERPCustomerKey,CustomerId,CustomerName,PostingDate,DeliveryDate,DocumentDate,TotalPaymentDue,Status,StatusText,Remarks,ShippingType,ShippingTypeText
								FROM be.BASalesOrder
                                WHERE TenantId = @TenantId AND (DocumentDate BETWEEN @FromDate AND @ToDate)
                                ORDER BY CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            List<BizCustSalesOrdersReportDTO> bizCustSalesOrdersReportDTO = await GetQueryEntityListAsync<BizCustSalesOrdersReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return bizCustSalesOrdersReportDTO;

        }

       
        #endregion

        #region Customer
        ///<inheritdoc/>
        public async Task<List<PartCustOrdersReportDTO>> GetPartCustSalesOrdersListByCustomerIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT so.ID,so.ERPDocNum,so.ERPSalesOrderKey,so.PostingDate,so.DeliveryDate,so.DocumentDate,so.TotalPaymentDueFC,so.Status,so.StatusText,so.Remarks,so.ShippingType,so.ShippingTypeText
								         FROM be.BASalesOrder so
								         INNER JOIN be.BACustomer c ON c.ID = so.CustomerId        
                                         WHERE BusinessPartnerTenantId = @CustomerId AND (so.DocumentDate BETWEEN @FromDate AND @ToDate)
                                         ORDER BY so.CreatedOn DESC	";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter customerParam = new SqlParameter("@CustomerId", filter.CustomerId);
            List<PartCustOrdersReportDTO> partCustOrdersReportDTO = await GetQueryEntityListAsync<PartCustOrdersReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, customerParam });
            return partCustOrdersReportDTO;

        }
        #endregion
    }
}

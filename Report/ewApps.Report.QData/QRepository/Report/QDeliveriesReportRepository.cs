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
        /// This class implements standard database logic and operations for Deliveries Report entity.
        /// </summary>
        public class QDeliveriesReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQDeliveriesReportRepository {

            #region Constructor 

            /// <summary>
            ///  Constructor initializing the base variables
            /// </summary>
            /// <param name="context"></param>
            /// <param name="sessionManager"></param>            
            public QDeliveriesReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<BizCustSalesDeliveriesReportDTO>> GetBizCustSalesDeliveriesListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT ID,ERPDeliveryKey,ERPDocNum,ERPCustomerKey,CustomerId,CustomerName,PostingDate,DocumentDate,DeliveryDate,TotalPaymentDue,Status,StatusText,ShippingType,ShippingTypeText,Remarks
								         FROM be.BADelivery
                                         WHERE TenantId = @TenantId AND (DocumentDate BETWEEN @FromDate AND @ToDate)
                                         ORDER BY CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            List<BizCustSalesDeliveriesReportDTO> bizCustSalesDeliveriesReportDTO = await GetQueryEntityListAsync<BizCustSalesDeliveriesReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return bizCustSalesDeliveriesReportDTO;

        }
        #endregion

        #region Customer
        ///<inheritdoc/>
        public async Task<List<PartCustDeliveriesReportDTO>> GetPartCustSalesDeliveriesListByCustomerIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT d.ID,d.ERPDeliveryKey,d.ERPDocNum,d.PostingDate,d.DocumentDate,d.DeliveryDate,d.TotalPaymentDueFC,d.Status,d.StatusText,d.ShippingType,d.ShippingTypeText,d.Remarks
								         FROM be.BADelivery d
								         INNER JOIN be.BACustomer c ON c.ID = d.CustomerId        
                                         WHERE BusinessPartnerTenantId = @CustomerId AND (d.DocumentDate BETWEEN @FromDate AND @ToDate)
                                         ORDER BY d.CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter customerParam = new SqlParameter("@CustomerId", filter.CustomerId);
            List<PartCustDeliveriesReportDTO> partCustDeliveriesReportDTO = await GetQueryEntityListAsync<PartCustDeliveriesReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, customerParam });
            return partCustDeliveriesReportDTO;

        }
        #endregion
    }
}

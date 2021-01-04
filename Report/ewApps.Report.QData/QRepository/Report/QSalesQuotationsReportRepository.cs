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
        /// This class implements standard database logic and operations for Sales Quotations Report entity.
        /// </summary>
        public class QSalesQuotationsReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQSalesQuotationsReportRepository {

            #region Constructor 

            /// <summary>
            ///  Constructor initializing the base variables
            /// </summary>
            /// <param name="context"></param>
            /// <param name="sessionManager"></param>            
            public QSalesQuotationsReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<BizCustSalesQuotationsReportDTO>> GetBizCustSalesQuotationsListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT ID,ERPDocNum,ERPSalesQuotationKey,ERPCustomerKey,CustomerId,CustomerName,PostingDate,ValidUntil,DocumentDate,TotalPaymentDue,Status,StatusText,Remarks
								FROM be.BASalesQuotation
                                WHERE TenantId = @TenantId AND (DocumentDate BETWEEN @FromDate AND @ToDate)
                                ORDER BY CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            List<BizCustSalesQuotationsReportDTO> bizCustSalesQuotationsReportDTO = await GetQueryEntityListAsync<BizCustSalesQuotationsReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return bizCustSalesQuotationsReportDTO;

        }
        #endregion

        #region Customer
        ///<inheritdoc/>
        public async Task<List<PartCustQuotationsReportDTO>> GetPartCustSalesQuotationsListByCustomerIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT sq.ID,sq.ERPDocNum,sq.ERPSalesQuotationKey,sq.PostingDate,sq.ValidUntil,sq.DocumentDate,sq.TotalPaymentDueFC,sq.Status,sq.StatusText,sq.Remarks
								         FROM BE.BASalesQuotation sq
								         INNER JOIN be.BACustomer c ON c.ID = sq.CustomerId        
                                         WHERE BusinessPartnerTenantId = @CustomerId AND (sq.DocumentDate BETWEEN @FromDate AND @ToDate)
                                         ORDER BY sq.CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter customerParam = new SqlParameter("@CustomerId", filter.CustomerId);
            List<PartCustQuotationsReportDTO> partCustQuotationsReportDTO = await GetQueryEntityListAsync<PartCustQuotationsReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, customerParam });
            return partCustQuotationsReportDTO;

        }
        #endregion
    }
}

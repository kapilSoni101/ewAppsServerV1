/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {

    /// <summary>
    /// This class implements standard database logic and operations for Invoice Report entity.
    /// </summary>
    public class QInvoiceReportRepository : BaseRepository<BaseDTO, QReportDbContext>, IQInvoiceReportRepository {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public QInvoiceReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<BizInvoiceReportDTO>> GetBizPayInvoiceListByTenantIdAsync(ReportFilterDTO filter,Guid tenantId, CancellationToken token = default(CancellationToken)) {
      FormattableString query = $@"SELECT DISTINCT i.Id,i.ERPDocNum,i.ERPARInvoiceKey AS InvoiceNo,i.DueDate,i.CustomerName,i.StatusText,i.ERPCustomerKey,
                                    i.Deleted,i.PostingDate,i.DocumentDate,i.ShippingType,i.TotalPaymentDue,i.Remarks,i.BalanceDue,i.LocalCurrency,
									(SELECT COUNT(DISTINCT ID) FROM be.BAARInvoiceItem WHERE ARInvoiceID = i.ID AND TenantId = @TenantId) AS TotalLineItems,      
                                    i.Status FROM be.BAARInvoice i																		
 									WHERE i.TenantId= @TenantId  
                                    AND (i.CreatedOn BETWEEN @FromDate AND @ToDate)";

      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
      SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);

      List<BizInvoiceReportDTO> invoiceDTOs = await GetQueryEntityListAsync<BizInvoiceReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
      return invoiceDTOs;
    }

        ///<inheritdoc/>
        public async Task<List<VendAPInvoicesReportDTO>> GetBizVendInvoiceListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString query = $@"SELECT DISTINCT i.ID,i.ERPDocNum,i.ERPApInvoiceKey,i.DueDate,i.VendorName,i.StatusText, i.ERPVendorKey,
                                    i.Deleted,i.PostingDate,i.DocumentDate,i.TotalPaymentDue,i.Remarks,     
                                    i.Status FROM be.BAAPInvoice i																		
 									WHERE i.TenantId= @TenantId  
                                    AND (i.CreatedOn BETWEEN @FromDate AND @ToDate)";

            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);

            List<VendAPInvoicesReportDTO> vendAPInvoicesReportDTOs = await GetQueryEntityListAsync<VendAPInvoicesReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return vendAPInvoicesReportDTOs;
        }

        public async Task<List<PartInvoiceReportDTO>> GetPartPayInvoiceListByCustomerAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      FormattableString query = $@"SELECT i.Id,i.ERPDocNum,i.ERPARInvoiceKey AS InvoiceNo,i.DueDate,i.CustomerName,i.StatusText, 
                                    i.Deleted,i.PostingDate,i.DocumentDate,i.ShippingType,i.TotalPaymentDue,i.Remarks,i.BalanceDue,
									(SELECT COUNT(DISTINCT ID) FROM be.BAARInvoiceItem WHERE ARInvoiceID = i.ID AND CustomerId = @CustomerId) AS TotalLineItems,      
                                    i.Status FROM be.BAARInvoice i
									INNER JOIN be.BACustomer c ON c.ID = i.CustomerId									
								    WHERE c.BusinessPartnerTenantId = @CustomerId  
                                    AND (i.CreatedOn Between @FromDate AND @ToDate) ORDER BY i.CreatedOn DESC";

      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
      SqlParameter customerParam = new SqlParameter("@CustomerId", filter.CustomerId);

      List<PartInvoiceReportDTO> invoiceDTOs = await GetQueryEntityListAsync<PartInvoiceReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, customerParam });
      return invoiceDTOs;
    }

    #endregion
  }
}

   

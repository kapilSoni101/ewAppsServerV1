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
    /// This class implements standard database logic and operations for Payment Report entity.
    /// </summary>
    public class QPaymentReportRepository :BaseRepository<BaseDTO, QReportDbContext>, IQPaymentReportRepository {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public QPaymentReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<BizPaymentReceivedReportDTO>> GetBizPayRecPayListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

      FormattableString query = $@"SELECT p.Id,p.IdentityNumber,p.Status,apsa.Name AS Type,p.CreatedOn as PaidOn,'' as StatusText,c.CustomerName,c.ERPCustomerKey,
                                   pl.Amount as CreditAmount,p.Deleted,i.TotalPaymentDue as OriginalAmount,
                                   i.ERPARInvoiceKey as InvoiceName,i.ERPARInvoiceKey as InvoiceId,i.DueDate,
                                   ap.Name as Service  
                                   FROM pay.Payment p
								   INNER JOIN pay.PaymentInvoiceLinking pl on pl.PaymentId = p.ID
                                   INNER JOIN be.BAARInvoice i on i.Id = pl.InvoiceId 
                                   INNER JOIN be.BACustomer c on c.Id = i.CustomerId 
                                   INNER JOIN am.TenantSubscription ta on ta.TenantId = p.TenantId
                                   INNER JOIN am.AppService ap on ap.AppId = ta.AppId and ap.Id = p.AppServiceId
                                   INNER JOIN am.AppServiceAttribute apsa ON ap.ID = apsa.AppServiceId AND apsa.ID = p.AppServiceAttributeId                                   		
                                   WHERE i.TenantId = @TenantId AND (p.CreatedOn BETWEEN @FromDate AND @ToDate)
								   ORDER BY p.CreatedOn DESC";
      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
      SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
      List<BizPaymentReceivedReportDTO> paymentreceivedDtos = await GetQueryEntityListAsync<BizPaymentReceivedReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
      return paymentreceivedDtos;

    }

    ///<inheritdoc/>
    public async Task<List<BizCustomerWisePaymentReportDTO>> GetBizPayCustPaymentListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

      FormattableString query = $@"SELECT DISTINCT c.Id,c.CustomerRefId as CustomerId,c.Name as CustomerName,
                                   (SELECT COUNT(1) FROM invoice WHERE CustomerId = c.Id and (Amount = AmountPaid) AND Deleted = 0) AS PaidInvoices,
							       (SELECT COUNT(1) FROM invoice WHERE CustomerId = c.Id and (AmountPaid = 0) AND Deleted = 0) AS OpenInvoices,
							       (SELECT COUNT(1) FROM invoice WHERE CustomerId = c.Id and (Amount - AmountPaid) > 0 and (AmountPaid > 0) AND Deleted = 0) AS														PartialPaidInvoices,
							       (SELECT SUM(pl.Amount) FROM PaymentInvoiceLinking pl
								   INNER JOIN Invoice i ON i.Id = pl.InvoiceId 
								   WHERE i.CustomerId = c.Id and i.TenantId = @TenantId ) AS CreditAmount,
								   bus.CurrencyCode  AS DocumentCurrencyCode, 1.0 AS FinalConversionRate                                        				   
							        FROM Customer c                                 
                                   INNER JOIN invoice i on i.CustomerId = c.ID AND i.Deleted = 0 AND c.TenantId = @TenantId 
                                   INNER JOIN PaymentInvoiceLinking pl on pl.InvoiceId = i.ID
                                   INNER JOIN Payment p on p.ID = pl.PaymentId
								   INNER JOIN Business bus ON bus.TenantId = p.TenantId AND bus.TenantId = @TenantId
								   WHERE c.TenantId = @TenantId AND (c.CreatedOn BETWEEN @FromDate AND @ToDate)";

      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
        SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
        SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
        List<BizCustomerWisePaymentReportDTO> customerpaymentDtos = await GetQueryEntityListAsync<BizCustomerWisePaymentReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
        return customerpaymentDtos;
     
    }

    ///<inheritdoc/>
    public async Task<List<PartPaymentReportDTO>> GetPartPaymentListAsyncByCustomerAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {

      FormattableString query = $@"SELECT p.Id,p.IdentityNumber,p.Status,apsa.Name AS Type,p.CreatedOn as PaidOn,'' as StatusText,
                                   pl.Amount as CreditAmount,p.Deleted,i.TotalPaymentDue as OriginalAmount,
                                   i.ERPARInvoiceKey as InvoiceName,i.ERPARInvoiceKey as InvoiceId,i.DueDate,
                                   ap.Name as Service  
                                   FROM pay.Payment p
								   INNER JOIN pay.PaymentInvoiceLinking pl on pl.PaymentId = p.ID
                                   INNER JOIN be.BAARInvoice i on i.Id = pl.InvoiceId 
                                   INNER JOIN be.BACustomer c on c.Id = i.CustomerId AND c.BusinessPartnerTenantId = @CustomerId 
                                   INNER JOIN am.TenantSubscription ta on ta.TenantId = p.TenantId
                                   INNER JOIN am.AppService ap on ap.AppId = ta.AppId and ap.Id = p.AppServiceId
                                   INNER JOIN am.AppServiceAttribute apsa ON ap.ID = apsa.AppServiceId AND apsa.ID = p.AppServiceAttributeId                                   		
                                   WHERE c.BusinessPartnerTenantId = @CustomerId AND (p.CreatedOn BETWEEN @FromDate AND @ToDate)
								   ORDER BY p.CreatedOn DESC";
      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
      SqlParameter customerParam = new SqlParameter("@CustomerId", filter.CustomerId);
      List<PartPaymentReportDTO> customerpaymentDtos = await GetQueryEntityListAsync<PartPaymentReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, customerParam });
      return customerpaymentDtos;

    }

    #endregion

  }
}

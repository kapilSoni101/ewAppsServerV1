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
    /// This class implements standard database logic and operations for Customer Report entity.
    /// </summary>
    public class QCustomerReportRepository : BaseRepository<BaseDTO, QReportDbContext>, IQCustomerReportRepository {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public QCustomerReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<CustomerReportDTO>> GetCustomerListByTenantAsync(ReportFilterDTO filter, Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
   
        FormattableString query = $@"SELECT DISTINCT c.Id,c.CustomerName AS Customer,t.joinedOn as JoinedOn,c.ERPCustomerKey,
	                              t.InvitedOn,au.FullName AS InvitedBy,c.Deleted,'' AS Status,
                                  (SELECT COUNT(1) FROM be.BAARInvoice WHERE CustomerId = c.Id) as TotalInvoices,
                                  (SELECT COUNT(1) FROM be.BAARInvoice WHERE Status = 0 and CustomerId = c.Id) AS OpenInvoices,
                                  (SELECT Top 1 TotalPaymentDue from be.BAARInvoice WHERE CustomerId = c.Id ORDER BY TotalPaymentDue DESC) AS UpcomingDueDate,
                                  (SELECT Top 1 pl.Createdon From pay.PaymentInvoiceLinking pl 
                                  LEFT JOIN be.BAARInvoice i ON pl.InvoiceId = i.Id and i.CustomerId = c.Id
	                              ORDER BY pl.CreatedOn DESC)  as LastPaymentOn FROM be.BACustomer c 
                                  LEFT JOIN am.Tenant t ON t.ID = c.BusinessPartnerTenantId
								  LEFT JOIN am.TenantUser au ON t.InvitedBy = au.Id 
                                  where c.TenantId = @TenantId AND (c.CreatedOn BETWEEN @FromDate AND @ToDate)";
        SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
        SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
        SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
          //SqlParameter appParam = new SqlParameter("@AppId", appId);
      List<CustomerReportDTO> customerDtos = await GetQueryEntityListAsync<CustomerReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
        return customerDtos;     
     
    }

    #endregion
  }
}

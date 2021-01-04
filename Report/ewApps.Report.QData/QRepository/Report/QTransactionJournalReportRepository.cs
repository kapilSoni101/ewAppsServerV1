/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 2 May 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 2 May 2019
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {

    /// <summary>
    /// This is the repository responsible for filtering data realted to Transaction Journal Report and services related to it
    /// </summary>
    public class QTransactionJournalReportRepository :BaseRepository<BaseDTO, QReportDbContext>, IQTransactionJournalReportRepository
  {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public QTransactionJournalReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion Constructor

    ///<inheritdoc/>
    public async Task<List<TransactionJournalReportDTO>> GetTransactionJournalListAsync(ListFilterDTO filter) {
      FormattableString query = $@"SELECT distinct ul.ID,pub.IdentityNumber AS PublisherId,pub.Name AS PublisherName,
                                b.IdentityNumber AS BusinessId,t.Name AS BusinessName,c.ERPCustomerKey AS CustomerId,
                                c.CustomerName, (Case when ul.Usertype = 3 then 'Business' else 'Customer' End) AS PayeeUserType,
                                tu.IdentityNumber AS PayeeId,tu.FullName AS PayeeUserName,p.IdentityNumber AS TransactionId,
                                p.Status AS TransactionStatus,p.CreatedOn AS TransactionDateTime,p.Amount AS TransactionAmount,
                                Concat(aps.Name,'/', apsa.Name ) AS ServiceAttributeName
								,'110.24.24' AS ClientIP,'Chrome' AS ClientBrowser,'Window' AS ClientOS
								--,pl.ClientIP,pl.ClientBrowser,pl.ClientOS 
								FROM pay.Payment p
                                INNER JOIN am.Tenant t ON t.Id = p.BusinessId
                                INNER JOIN ap.Business b ON b.TenantId = t.Id 
                                INNER JOIN be.BACustomer c ON c.Id = p.PartnerId
                                INNER JOIN am.TenantUser tu ON tu.Id = p.CreatedBy
                                INNER JOIN am.UserTenantLinking ul ON ul.TenantUserId = p.CreatedBy AND ul.TenantId = p.TenantId
                                INNER JOIN am.AppService aps ON aps.Id = p.AppServiceId
                                LEFT JOIN am.AppServiceAttribute apsa ON apsa.Id = p.AppServiceAttributeId
                                INNER JOIN am.TenantLinking tl ON tl.BusinessTenantId = p.BusinessId AND tl.BusinessPartnerTenantId is null  
                                INNER JOIN ap.Publisher pub ON pub.TenantId = tl.PublisherTenantId
								--INNER JOIN pay.PaymentLog pl ON p.ID = pl.PaymentId
                                WHERE (p.CreatedOn Between @FromDate AND @ToDate)
                                ORDER BY p.CreatedOn DESC";
      
      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

      List<TransactionJournalReportDTO> appDTOs = await base.GetQueryEntityListAsync<TransactionJournalReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate });
      return appDTOs;
    }
  }
}

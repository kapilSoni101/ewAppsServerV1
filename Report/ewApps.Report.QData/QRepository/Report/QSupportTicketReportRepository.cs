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
    /// This class implements standard database logic and operations for Support Ticket Report entity.
    /// </summary>
    public class QSupportTicketReportRepository :BaseRepository<BaseDTO, QReportDbContext>, IQSupportTicketReportRepository {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public QSupportTicketReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<PlatSupportTicketReportDTO>> GetPFSupportTicketListAsync(ReportFilterDTO filter, short generationLevel, CancellationToken token = default(CancellationToken)) {


      FormattableString query = $@"SELECT st.Id,st.IdentityNumber,st.Title,b.IdentityNumber as TenantIdentityNumber ,t.Name AS TenantName,st.CreatedOn,st.UpdatedOn AS ModifiedOn,au.FullName AS ModifiedBy,st.Status ,'' as StatusText,st.Currentlevel,st.Deleted,c.ERPCustomerKey,
                                    case st.Currentlevel when 2 then t.Name when 1 then c.CustomerName else 'SuperAdmin' end as AssignTo
                                    FROM ap.SupportTicket st 
                                    INNER JOIN am.Tenant t ON t.Id = st.TenantId 
                                    LEFT JOIN be.BACustomer c ON c.BusinessPartnerTenantId = st.CustomerId
                                    LEFT JOIN ap.Business AS b ON st.businessTenantId = b.TenantId
                       
                                    INNER Join am.TenantUser au on au.Id = st.UpdatedBy 
									WHERE Exists (Select SupportId FROM ap.LevelTransitionHistory AS lth 
                                    WHERE st.ID=lth.SupportId AND TargetLevel=@GenerationLevel)
                                    AND st.CustomerId IS NOT NULL And  
                                    (st.CreatedOn BETWEEN @FromDate AND @ToDate)
									ORDER BY st.UpdatedOn DESC";
      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter generationLevelParam = new SqlParameter("@GenerationLevel", generationLevel);
            List<PlatSupportTicketReportDTO> supportTicket = await GetQueryEntityListAsync<PlatSupportTicketReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, generationLevelParam });
      return supportTicket;


    }

    ///<inheritdoc/>
    public async Task<List<PubSupportTicketReportDTO>> GetPubSupportTicketListAsync(ReportFilterDTO filter, short generationLevel, Guid tenantId ,CancellationToken token = default(CancellationToken)) {
    

        FormattableString query = $@"SELECT DISTINCT  st.Id,st.IdentityNumber,st.Title,t.IdentityNumber as TenantIdentityNumber ,c.ERPCustomerKey as CustomerId,t.Name AS TenantName,c.CustomerName,st.CreatedOn,st.UpdatedOn AS ModifiedOn,au.FullName AS ModifiedBy,st.Status ,'' as StatusText,st.Currentlevel,st.Deleted,
                                    case st.Currentlevel when 2 then t.Name when 1 then c.CustomerName else 'SuperAdmin' end as AssignTo
                                    FROM ap.SupportTicket st 
                                    INNER JOIN am.Tenant t ON t.Id = st.TenantId 
                                    LEFT JOIN be.BACustomer c ON c.Id = st.CustomerId
									INNER JOIN am.TenantLinking as tl ON (st.customerid=tl.BusinessPartnerTenantId) OR (st.TenantId=tl.BusinessTenantId AND st.CustomerId='00000000-0000-0000-0000-000000000000') 
                                    INNER JOIN am.TenantUser au on au.Id = st.UpdatedBy 
									Where Exists (Select SupportId FROM ap.LevelTransitionHistory AS lth 
                                    Where st.ID=lth.SupportId AND TargetLevel=@GenerationLevel)
                                    AND st.CustomerId IS NOT NULL And  
                                    (st.CreatedOn BETWEEN @FromDate AND @ToDate) 
								    AND tl.PublisherTenantId=@PublisherTenantId
									ORDER BY st.UpdatedOn DESC";
        SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
        SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter generationLevelParam = new SqlParameter("@GenerationLevel", generationLevel);
            SqlParameter publisherTenantId = new SqlParameter("@PublisherTenantId", tenantId);
            List<PubSupportTicketReportDTO> supportTicket = await GetQueryEntityListAsync<PubSupportTicketReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, generationLevelParam, publisherTenantId});

            return supportTicket;
      

    }

    ///<inheritdoc/>
    public async Task<List<BizSupportTicketReportDTO>> GetBizPaySupportTicketListByTenantAsync(PartReportSupportTicketParamDTO filter, short generationLevel,CancellationToken token = default(CancellationToken)) {
     
        FormattableString query = $@"SELECT st.Id,st.IdentityNumber,st.Title,st.CreatedOn,st.UpdatedOn AS ModifiedOn,
                                  au.FullName AS ModifiedBy,st.status,st.Currentlevel,
                                  st.Deleted,'' as StatusText,c.ERPCustomerKey as CustomerId,c.CustomerName,t.name as BusinessName,
                                  case  st.Currentlevel when 2 then t.Name
				                  when  1 then c.CustomerName else 'SuperAdmin' end as AssignTo FROM ap.SupportTicket st                     
                                 INNER JOIN am.TenantUser as au ON st.CreatorId = au.ID
                                 INNER JOIN am.TenantUser as auUO ON st.UpdatedBy = auUO.ID
                                 INNER JOIN am.Tenant as t ON st.TenantId = t.ID
                                 INNER JOIN be.BACustomer AS c ON st.CustomerId = c.BusinessPartnerTenantId
                                 Where st.AppKey = @AppKey 
                                 AND st.GenerationLevel = @GenerationLevel 
					           AND st.TenantId = @TenantId AND (st.CreatedOn BETWEEN @FromDate AND @ToDate) ORDER BY st.UpdatedOn DESC  ";
        SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
        SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
        SqlParameter tenantParam = new SqlParameter("@TenantId", filter.TenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", filter.AppKey);
        //SqlParameter creatorIdParam = new SqlParameter("CreatorId", filter.CreatorId);
        SqlParameter generationLevelParam = new SqlParameter("GenerationLevel", generationLevel);
      List<BizSupportTicketReportDTO> supportTicket = await GetQueryEntityListAsync<BizSupportTicketReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam, appKeyParam, generationLevelParam });
        return supportTicket;
      
    }

    ///<inheritdoc/>
    public async Task<List<PartSupportTicketReportDTO>> GetPartPaySupportTicketListByCustomerIdAsync(PartReportSupportTicketParamDTO filter,  CancellationToken token = default(CancellationToken)) {
     
        FormattableString query = $@"SELECT st.Id,st.IdentityNumber,st.Title,st.CreatedOn,st.UpdatedOn AS ModifiedOn,
                                     au.Fullname AS ModifiedBy,st.status,'' as StatusText,t.Name as BusinessName,c.CustomerName,
                                     st.Currentlevel, case  st.Currentlevel when 2 then t.Name
				                     when  1 then c.CustomerName else 'SuperAdmin' end as AssignTo FROM ap.SupportTicket as st
                                     INNER JOIN am.TenantUser as au ON st.CreatorId = au.ID
                                     INNER JOIN am.TenantUser as auUO ON st.UpdatedBy = auUO.ID
                                     INNER JOIN am.Tenant as t ON st.TenantId = t.ID
					                 INNER JOIN be.BACustomer AS c ON st.CustomerId=c.BusinessPartnerTenantId
					                 Where st.TenantId = @TenantId AND st.AppKey = @AppKey 
                                     AND st.CreatorId = @CreatorId AND st.GenerationLevel = @GenerationLevel 
								     AND st.CustomerId=@CustomerId 
								     AND (st.CreatedOn BETWEEN @FromDate AND @ToDate) ORDER BY st.UpdatedOn DESC ";
        SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
        SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
        SqlParameter tenantIdParam = new SqlParameter("TenantId", filter.TenantId);
        SqlParameter appKeyParam = new SqlParameter("AppKey", filter.AppKey);
        SqlParameter creatorIdParam = new SqlParameter("CreatorId", filter.CreatorId);
        SqlParameter generationLevelParam = new SqlParameter("GenerationLevel", filter.GenerationLevel);     
        SqlParameter customerParam = new SqlParameter("@CustomerId", filter.CustomerId);
        List<PartSupportTicketReportDTO> supportTicket = await GetQueryEntityListAsync<PartSupportTicketReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, customerParam, tenantIdParam, appKeyParam , creatorIdParam, generationLevelParam });
        return supportTicket;     

    }

    #endregion
  }
}

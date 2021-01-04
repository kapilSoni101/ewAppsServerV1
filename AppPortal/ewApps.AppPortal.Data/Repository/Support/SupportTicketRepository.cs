/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data
{
  /// <summary>
  /// This repository class implements <seealso cref="ISupportTicketRepository"/> interface to get <see cref="SupportTicket"/> entity related data.
  /// </summary>
  /// <seealso cref="SupportTicket"/>
  /// <seealso cref="CoreDbContext"/>
  /// <seealso cref="ISupportTicketRepository"/>
  public class SupportTicketRepository : BaseRepository<SupportTicket, AppPortalDbContext>, ISupportTicketRepository
  {

    /// <summary>
    /// Initializes the member variables.
    /// </summary>
    /// <param name="context">An instance of <see cref="CoreDbContext"/> to communicate with data storage.</param>
    /// <param name="sessionManager">An instance of <see cref="IUserSessionManager"/> to get requester user session information.</param>
    public SupportTicketRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager)
    {
    }

    /// <inheritdoc/>
    public async Task<List<SupportMyTicketDTO>> GetUserSupportTicketByCreatorAndCustomerAndTenantId(string appKey, Guid tenantId, int generationLevel, Guid creatorId, Guid? customerId, bool onlyDeleted, CancellationToken token = default(CancellationToken))
    {
      string sql = @"Select st.ID AS SupportTicketId, st.Title, st.IdentityNumber AS 'SupportIdentityNumber', st.CurrentLevel, st.CustomerId, 
                     st.Priority, st.Status, st.GenerationLevel, st.CreatedOn AS CreatedOn, st.UpdatedOn AS UpdatedOn, auUO.FullName as 'UpdatedByName',
                     st.TenantId, t.Name AS 'TenantName', t.IdentityNumber AS 'TenantIdentityNumber', au.FullName AS 'CreaterFullName', {0}
                     FROM ap.SupportTicket as st
                     INNER JOIN am.TenantUser as au ON st.CreatorId = au.ID
                     INNER JOIN am.TenantUser as auUO ON st.UpdatedBy = auUO.ID
                     INNER JOIN am.Tenant as t ON st.TenantId = t.ID ";

      string sqlWhereClause = @" Where st.TenantId = @TenantId AND st.AppKey = @AppKey 
                                 AND st.CreatorId = @CreatorId AND st.GenerationLevel = @GenerationLevel AND st.Deleted=@Deleted";

      object[] sqlParamArray = null;

      SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
      SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);
      SqlParameter creatorIdParam = new SqlParameter("CreatorId", creatorId);
      SqlParameter generationLevelParam = new SqlParameter("GenerationLevel", generationLevel);
      SqlParameter deletedParam = new SqlParameter("Deleted", onlyDeleted);

      // Add SQL if customer id filter is required.
      if (customerId.HasValue)
      {
        sql = string.Format(sql, " c.ERPCustomerKey AS 'CustomerIdentityNumber', c.CustomerName AS 'CustomerName'");
        sqlWhereClause += @" AND st.CustomerId=@CustomerId ";
        sql += @" INNER JOIN be.BACustomer AS c ON st.CustomerId=c.BusinessPartnerTenantId ";
        SqlParameter customerIdParam = new SqlParameter("CustomerId", customerId.Value);
        sqlParamArray = new object[] { tenantIdParam, appKeyParam, creatorIdParam, generationLevelParam, deletedParam, customerIdParam };
      }
      else
      {
        // sql += @" LEFT JOIN Customer AS c ON st.CustomerId=c.BusinessPartnerTenantId ";
        sql = string.Format(sql, " '' AS 'CustomerIdentityNumber', '' AS 'CustomerName'");
        sqlParamArray = new object[] { tenantIdParam, appKeyParam, creatorIdParam, generationLevelParam, deletedParam };
      }



      sql = string.Format("{0} {1} {2}", sql, sqlWhereClause, " ORDER BY st.UpdatedOn DESC ");

      return await _context.SupportMyTicketDTOQuery.FromSql(sql, sqlParamArray).ToListAsync();
    }


    /// <inheritdoc/>
    public List<SupportTicketDTO> GetSupportTicketByAppAndTenantIdAndLevel(string appKey, Guid tenantId, short generationLevel, bool includeDeleted)
    {
      string sql = @"Select st.ID AS SupportTicketId, st.Title, st.IdentityNumber AS 'SupportIdentityNumber', st.CurrentLevel, st.CustomerId, 
                     st.Priority, st.Status, st.GenerationLevel, st.CreatedOn AS CreatedOn, st.UpdatedOn AS UpdatedOn, auUO.FullName as 'UpdatedByName', st.TenantId,'' AS 'PublisherIdentityNumber',
                      '' AS 'PublisherName', t.Name AS 'TenantName', t.IdentityNumber AS 'TenantIdentityNumber', c.Name AS 'CustomerName', c.CustomerRefId, au.FullName AS 'CreaterFullName',
null as publisherId, null as businessId,'' as AppName, '' as OriginatedBy, '' as SubdomainName
                     FROM ap.SupportTicket as st
                     INNER JOIN am.TenantUser as au ON st.CreatorId = au.ID
                     INNER JOIN am.TenantUser as auUO ON st.UpdatedBy = auUO.ID
                     INNER JOIN am.Tenant as t ON st.TenantId = t.ID
                     INNER JOIN ap.Customer AS c ON st.CustomerId = c.BusinessPartnerTenantId
                     Where st.AppKey = @AppKey 
                     AND st.GenerationLevel = @GenerationLevel AND st.Deleted=@Deleted ";

      ArrayList sqlParamArrayList = new ArrayList();
      SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);
      sqlParamArrayList.Add(appKeyParam);

      SqlParameter generationLevelParam = new SqlParameter("GenerationLevel", generationLevel);
      sqlParamArrayList.Add(generationLevelParam);

      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);
      sqlParamArrayList.Add(deletedParam);


      if (tenantId.Equals(Guid.Empty) == false)
      {
        sql += " AND st.TenantId = @TenantId ";
        SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
        sqlParamArrayList.Add(tenantIdParam);
      }

      sql = string.Format("{0} {1}", sql, " ORDER BY st.UpdatedOn DESC ");

      return _context.SupportTicketDTOQuery.FromSql(sql, sqlParamArrayList.ToArray()).ToList();
    }

    /// <inheritdoc/>
    public List<SupportTicketDTO> GetSupportTicketByEscalationLevel(short escalationLevel, bool includeDeleted)
    {
      string sql = @"SELECT st.ID AS 'SupportTicketId', st.IdentityNumber AS 'SupportIdentityNumber', st.Title, st.Priority, st.CurrentLevel, 
                    st.GenerationLevel, st.Status, st.CustomerId, null as 'CustomerRefId' , t.IdentityNumber AS 'TenantIdentityNumber','' AS 'PublisherIdentityNumber',
                      '' AS 'PublisherName', '' AS 'CustomerName', st.TenantId, t.Name AS 'TenantName', st.CreatedOn, st.UpdatedOn, au.FullName AS 'UpdatedByName'
                    , au.FullName AS 'CreaterFullName',null as publisherId, null as businessId,'' as AppName, '' as OriginatedBy, '' as SubdomainName
                    FROM ap.SupportTicket AS st
                    INNER JOIN am.Tenant AS t ON t.TenantId = st.TenantId
                    INNER JOIN am.TenantUser AS au ON st.UpdatedBy = au.ID
                    LEFT JOIN ap.Customer AS c ON st.CustomerId = c.ID
                    Where Exists 
                    (Select SupportId FROM ap.LevelTransitionHistory AS lth Where st.ID=lth.SupportId AND TargetLevel=@EscalationLevel)
                    AND st.CustomerId IS NOT NULL AND st.Deleted=@Deleted ORDER BY st.UpdatedOn DESC ";

      SqlParameter escalationLevelParam = new SqlParameter("EscalationLevel", escalationLevel);
      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);

      object[] sqlParamArray = new object[] { escalationLevelParam, deletedParam };

      return _context.SupportTicketDTOQuery.FromSql(sql, sqlParamArray).ToList();
    }

    /// <inheritdoc/>
    public SupportTicketDetailDTO GetSupportTicketDetailById(Guid supportId, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {

      string sql = @"SELECT DISTINCT st.ID, st.IdentityNumber AS 'SupportIdentityNumber', st.Title, st.Description, st.Priority, st.Status, st.CurrentLevel,st.AppKey, 
                    st.GenerationLevel, st.CreatedOn, auCreatedBy.FullName AS 'CreatedByName',
                    st.UpdatedOn, auUpdatedBy.FullName AS 'UpdatedByName', st.TenantId, b.Name AS 'TenantName',
                    st.CustomerId, c.CustomerName AS 'CustomerName', auCreatedBy.Phone AS 'ContactPhone', auCreatedBy.Email AS 'ContactEmail',
                    b.IdentityNumber AS 'TenantIdentityNumber', c.ERPCustomerKey AS 'CustomerIdentityNumber',tPublisher.IdentityNumber AS 'PublisherIdentityNumber', tPublisher.Name AS 'PublisherName'
                    FROM ap.SupportTicket AS st                   
					         
					          INNER JOIN ap.Publisher AS tPublisher ON tPublisher.TenantId = st.PublisherTenantId                   
                    INNER JOIN am.TenantUser AS auCreatedBy ON st.CreatedBy = auCreatedBy.ID
                    INNER JOIN am.TenantUser AS auUpdatedBy ON ST.UpdatedBy = auUpdatedBy.ID
                    LEFT JOIN ap.Business AS b ON b.TenantId = st.BusinessTenantId
                    LEFT JOIN be.BACustomer AS c ON st.CustomerId = c.BusinessPartnerTenantId
                    Where st.ID=@SupportId ";

      if (!includeDeleted)
        sql = sql + " AND st.Deleted=@Deleted  ";

      SqlParameter supportIdParam = new SqlParameter("SupportId", supportId);
      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);

      return _context.SupportTicketDetailDTOQuery.FromSql(sql, new object[] { supportIdParam, deletedParam }).SingleOrDefault();
    }

    /// <inheritdoc/>
    public int SupportTicketAssignedToLevel3(Guid supportTicketId, int level)
    {
      //string sql = @"Select COUNT(lth.ID) 
      //              FROM LevelTransitionHistory AS lth 
      //              Where lth.SupportId=@SupportId AND TargetLevel=@TargetLevel ";

      //SqlParameter supportIdParam = new SqlParameter("SupportId", supportTicketId);
      //SqlParameter deletedParam = new SqlParameter("TargetLevel", level);

      List<LevelTransitionHistory> list = _context.LevelTransitionHistory.Where(lth => lth.SupportId == supportTicketId && lth.TargetLevel == level).ToList();
      return list.Count;
    }

    // New
    public List<SupportTicketDTO> GetSupportTicketAssignedToLevel2List(string appKey, Guid level2TenantId, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {
      string sql = @"SELECT st.ID AS 'SupportTicketId', st.IdentityNumber AS 'SupportIdentityNumber', st.Title, st.Priority, st.CurrentLevel, 
                        st.GenerationLevel, st.Status, st.CustomerId, c.ERPCustomerKey as CustomerRefId, t.IdentityNumber AS 'TenantIdentityNumber','' AS 'PublisherIdentityNumber',
                        '' AS 'PublisherName',c.CustomerName AS 'CustomerName', st.TenantId, t.Name AS 'TenantName', st.CreatedOn, st.UpdatedOn, au.FullName AS 'UpdatedByName', 
                        auc.FullName AS 'CreaterFullName',null as publisherId, null as businessId, '' as AppName, '' as OriginatedBy, '' as SubdomainName
                        FROM ap.SupportTicket AS st
                        INNER JOIN am.TenantLinking as tl ON st.customerid=tl.BusinessPartnerTenantId AND tl.BusinessTenantId=@Level2TenantId
                        INNER JOIN am.Tenant AS t ON t.ID = st.TenantId
                        INNER JOIN am.TenantUser AS au ON st.UpdatedBy = au.ID
                        INNER JOIN am.TenantUser AS auc ON st.CreatedBy = auc.ID
                        LEFT JOIN be.BACustomer AS c ON st.CustomerId = c.BusinessPartnerTenantId
                        Where Exists 
                        (Select SupportId FROM ap.LevelTransitionHistory AS lth Where st.ID=lth.SupportId AND TargetLevel=@Level2SupportLevel)
                        AND st.CustomerId IS NOT NULL AND st.Deleted=@Deleted AND st.AppKey=@AppKey
                        ORDER BY st.UpdatedOn DESC ";

      SqlParameter level2Param = new SqlParameter("Level2SupportLevel", (short)SupportLevelEnum.Level2);
      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);
      SqlParameter level2TenantIdParam = new SqlParameter("Level2TenantId", level2TenantId);
      SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);


      object[] sqlParamArray = new object[] { level2Param, deletedParam, level2TenantIdParam, appKeyParam };

      return _context.SupportTicketDTOQuery.FromSql(sql, sqlParamArray).ToList();

    }

    // New
    public List<SupportTicketDTO> GetSupportTicketAssignedToLevel3List(Guid publisherTenantId, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {

      string sql = @"SELECT DISTINCT st.ID AS 'SupportTicketId', st.IdentityNumber AS 'SupportIdentityNumber', st.Title, st.Priority, st.CurrentLevel, 
                        st.GenerationLevel, st.Status, st.CustomerId,c.ERPCustomerKey as CustomerRefId, bus.IdentityNumber AS 'TenantIdentityNumber','' AS 'PublisherIdentityNumber',
                      '' AS 'PublisherName', c.CustomerName AS 'CustomerName', st.TenantId, t.Name AS 'TenantName', st.CreatedOn, st.UpdatedOn, au.FullName AS 'UpdatedByName', 
                        auc.FullName AS 'CreaterFullName',null as publisherId, null as businessId, a.Name as AppName, '' as OriginatedBy, '' as SubdomainName
                        FROM ap.SupportTicket AS st
                        INNER JOIN am.TenantLinking as tl ON (st.customerid=tl.BusinessPartnerTenantId) OR (st.TenantId=tl.BusinessTenantId AND st.CustomerId='00000000-0000-0000-0000-000000000000') 
                        INNER JOIN ap.LevelTransitionHistory as lth ON lth.SupportId=st.ID and lth.TargetLevel=@Level3SupportLevel
                        INNER JOIN am.Tenant AS t ON t.ID = st.TenantId
                        INNER JOIN ap.Business AS bus ON bus.TenantId = st.TenantId
                        INNER JOIN am.TenantUser AS au ON st.UpdatedBy = au.ID                       
                        INNER JOIN am.TenantUser AS auc ON st.CreatedBy = auc.ID
                        INNER JOIN am.App AS a ON a.Id = st.AppId
                        LEFT JOIN be.BACustomer AS c ON st.CustomerId = c.BusinessPartnerTenantId
                        Where tl.PublisherTenantId=@PublisherTenantId AND st.Deleted=@Deleted
                        ORDER BY st.UpdatedOn DESC";

      SqlParameter level3Param = new SqlParameter("Level3SupportLevel", (short)SupportLevelEnum.Level3);
      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);
      SqlParameter publisherTenantIdParam = new SqlParameter("PublisherTenantId", publisherTenantId);

      object[] sqlParamArray = new object[] { level3Param, deletedParam, publisherTenantIdParam };

      return _context.SupportTicketDTOQuery.FromSql(sql, sqlParamArray).ToList();
    }


    public List<SupportTicketDTO> GetSupportTicketAssignedToLevel4List(Guid platformTenantId, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {

      string sql = @"SELECT DISTINCT st.ID AS 'SupportTicketId', st.IdentityNumber AS 'SupportIdentityNumber', st.Title, st.Priority, st.CurrentLevel, 
                        st.GenerationLevel, st.Status, st.CustomerId, c.ERPCustomerKey as CustomerRefId,b.IdentityNumber AS 'TenantIdentityNumber',p.IdentityNumber AS 'PublisherIdentityNumber',
                       p.Name AS 'PublisherName',c.CustomerName AS 'CustomerName', st.TenantId, b.Name AS 'TenantName', st.CreatedOn, st.UpdatedOn, au.FullName AS 'UpdatedByName', 
                        auc.FullName AS 'CreaterFullName',p.Id as PublisherId, b.TenantId as BusinessId, a.Name as AppName, port.Name as OriginatedBy, tpub.SubDomainName as SubdomainName
                        FROM ap.SupportTicket AS st
                        INNER JOIN am.TenantLinking as tl ON (st.customerid=tl.BusinessPartnerTenantId) 
                        OR (st.TenantId=tl.BusinessTenantId AND st.CustomerId='00000000-0000-0000-0000-000000000000') 
                        OR (st.TenantID=tl.PublisherTenantId  AND st.CustomerId='00000000-0000-0000-0000-000000000000') 
                        INNER JOIN ap.LevelTransitionHistory as lth ON lth.SupportId=st.ID and lth.TargetLevel=@Level4SupportLevel
                        INNER JOIN am.Tenant AS t ON t.ID = st.TenantId
                          INNER JOIN am.Tenant AS tpub ON tpub.ID = tl.PublisherTenantId
                        INNER JOIN am.TenantUser AS au ON st.UpdatedBy = au.ID
                        INNER JOIN am.TenantUser AS auc ON st.CreatedBy = auc.ID
                        INNER JOIN ap.Publisher AS p ON p.TenantId = tl.PublisherTenantId
                        INNER JOIN am.App AS a ON a.Id = st.AppId
                        INNER JOIN ap.Portal AS port ON port.Id = st.PortalId
                       
                        LEFT JOIN ap.Business AS b ON st.businessTenantId = b.TenantId
                        LEFT JOIN be.BACustomer AS c ON st.CustomerId = c.BusinessPartnerTenantId
                        Where st.Deleted=@Deleted
                        ORDER BY st.UpdatedOn DESC";


      // remove this inner join from sql
      //INNER JOIN ap.Business AS bus ON bus.TenantId = st.TenantId
      SqlParameter level3Param = new SqlParameter("Level4SupportLevel", (short)SupportLevelEnum.Level4);
      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);
      SqlParameter publisherTenantIdParam = new SqlParameter("PlatformTenantId", platformTenantId);

      object[] sqlParamArray = new object[] { level3Param, deletedParam, publisherTenantIdParam };

      return _context.SupportTicketDTOQuery.FromSql(sql, sqlParamArray).ToList();
    }
    // INNER JOIN ap.Business AS bus ON bus.TenantId = tl.BusinessTenantId and tl.BusinessPartnerTenantId is Null


    // New
    public List<SupportTicketDTO> GetSupportTicketAssignedToLevel2BusinessList(Guid level2TenantId, bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken))
    {
      string sql = @"SELECT st.ID AS 'SupportTicketId', st.IdentityNumber AS 'SupportIdentityNumber', st.Title, st.Priority, st.CurrentLevel, 
                        st.GenerationLevel, st.Status, st.CustomerId,null as CustomerRefId, t.IdentityNumber AS 'TenantIdentityNumber','' AS 'PublisherIdentityNumber',
                        '' AS 'PublisherName',''  AS 'CustomerName', st.TenantId, t.Name AS 'TenantName', st.CreatedOn, st.UpdatedOn, au.FullName AS 'UpdatedByName', 
                        auc.FullName AS 'CreaterFullName',null as publisherId, null as businessId, '' as AppName, '' as OriginatedBy, '' as SubdomainName
                        FROM ap.SupportTicket AS st                        
                        INNER JOIN am.Tenant AS t ON t.ID = st.TenantId
                        INNER JOIN am.TenantUser AS au ON st.UpdatedBy = au.ID
                        INNER JOIN am.TenantUser AS auc ON st.CreatedBy = auc.ID                      
                        Where st.Deleted=@Deleted  AND st.TenantId= @level2TenantId AND st.GenerationLevel= @GenerationLevel
                        ORDER BY st.UpdatedOn DESC ";

      SqlParameter GenerationLevelParam = new SqlParameter("GenerationLevel", generationLevel);
      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);
      SqlParameter level2TenantIdParam = new SqlParameter("Level2TenantId", level2TenantId);
      //SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);


      object[] sqlParamArray = new object[] { GenerationLevelParam, deletedParam, level2TenantIdParam };

      return _context.SupportTicketDTOQuery.FromSql(sql, sqlParamArray).ToList();

    }

    // New
    public List<SupportTicketDTO> GetSupportTicketAssignedToLevel2CustomerList(Guid level2TenantId, bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken))
    {
      string sql = @"SELECT st.ID AS 'SupportTicketId', st.IdentityNumber AS 'SupportIdentityNumber', st.Title, st.Priority, st.CurrentLevel, 
                        st.GenerationLevel, st.Status, st.CustomerId,c.ErpCustomerKey as CustomerRefId, t.IdentityNumber AS 'TenantIdentityNumber','' AS 'PublisherIdentityNumber',
                        '' AS 'PublisherName',c.CustomerName  AS 'CustomerName', st.TenantId, t.Name AS 'TenantName', st.CreatedOn, st.UpdatedOn, au.FullName AS 'UpdatedByName', 
                        auc.FullName AS 'CreaterFullName', null as publisherId, null as businessId, a.Name as AppName, port.Name as OriginatedBy, '' as SubdomainName
                        FROM ap.SupportTicket AS st                        
                        INNER JOIN am.Tenant AS t ON t.ID = st.TenantId
                        INNER JOIN am.TenantUser AS au ON st.UpdatedBy = au.ID
                        INNER JOIN am.TenantUser AS auc ON st.CreatedBy = auc.ID  
                        INNER JOIN am.App AS a ON a.Id = st.AppId 
                        INNER JOIN ap.Portal AS port ON port.Id = st.PortalId
                        INNER JOIN be.BACustomer AS c ON st.CustomerId = c.BusinessPartnerTenantId                   
                        Where st.Deleted=@Deleted  AND st.TenantId= @level2TenantId AND st.GenerationLevel= @GenerationLevel
                        ORDER BY st.UpdatedOn DESC ";

      SqlParameter GenerationLevelParam = new SqlParameter("GenerationLevel", generationLevel);
      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);
      SqlParameter level2TenantIdParam = new SqlParameter("Level2TenantId", level2TenantId);
      //SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);


      object[] sqlParamArray = new object[] { GenerationLevelParam, deletedParam, level2TenantIdParam };

      return _context.SupportTicketDTOQuery.FromSql(sql, sqlParamArray).ToList();

    }

    /// <inheritdoc/>
    public BusinessSupportNotificationDTO GetBusinessSupportNotificationData(Guid supportId, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {

      string sql = @"SELECT DISTINCT st.Id as TicketId, st.Title, st.Description, st.Priority , st.Status, st.CreatedBy,st.IdentityNumber,auCreatedBy.IdentityNumber as UserIdentityNumber, c.ERPCustomerKey as CustIdentityNumber,
                    st.CreatedOn, auCreatedBy.FullName AS 'UserName',app.Name as AppName,app.id as AppId,
                    st.UpdatedOn as ModifiedOn, auUpdatedBy.FullName AS 'ModifiedBy', b.Name AS 'BusinessName',st.BusinessTenantId,
                    c.CustomerName AS 'CustomerName',  auCreatedBy.Email AS 'ContactEmail', t.SubDomainName As subdomain, tPublisher.Copyright as Copyright,                   
                    tPublisher.Name AS 'PublisherName',b.TimeZone, b.DateTimeFormat
                    FROM ap.SupportTicket AS st   
                    INNER JOIN am.Tenant As t on t.Id = st.BusinessTenantId               
					          INNER JOIN am.App As app on app.Id = st.AppId
					          INNER JOIN ap.Publisher AS tPublisher ON tPublisher.TenantId = st.PublisherTenantId                   
                    INNER JOIN am.TenantUser AS auCreatedBy ON st.CreatedBy = auCreatedBy.ID
                    INNER JOIN am.TenantUser AS auUpdatedBy ON ST.UpdatedBy = auUpdatedBy.ID
                    LEFT JOIN ap.Business AS b ON b.TenantId = st.BusinessTenantId
                    LEFT JOIN be.BACustomer AS c ON st.CustomerId = c.BusinessPartnerTenantId
                    Where st.ID=@SupportId ";

      if (!includeDeleted)
        sql = sql + " AND st.Deleted=@Deleted  ";

      SqlParameter supportIdParam = new SqlParameter("SupportId", supportId);
      SqlParameter deletedParam = new SqlParameter("Deleted", includeDeleted);

      return _context.BusinessSupportNotificationDTOQuery.FromSql(sql, new object[] { supportIdParam, deletedParam }).SingleOrDefault();
    }

   
  }
}

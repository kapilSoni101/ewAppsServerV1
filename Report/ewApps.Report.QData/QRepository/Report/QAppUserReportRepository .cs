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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Report.QData {

    /// <summary>
    /// This class implements standard database logic and operations for AppUser Report entity.
    /// </summary>
    public class QAppUserReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQAppUserReportRepository {

    #region Constructor 

   
    public QAppUserReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<PlatAppUserReportDTO>> GetAllPFAppUserListByUserTypeAsync(ReportFilterDTO filter, int userType, Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {

      string query = @"SELECT DISTINCT a.ID, a.FullName AS UserName,a.Email,au.FullName AS InvitedBy,UAL.joineddate as JoinedOn,
					   Case WHEN r.RoleKey='Admin' THEN CONVERT(bit,1) ELSE CONVERT(bit,0) END AS Role 
                      ,a.CreatedOn AS InvitedOn, Permissions=1,UAL.Active , r.PermissionBitMask, a.Deleted, '' AS Status  
                      FROM am.TenantUser a 
                      INNER JOIN am.TenantUser au ON a.CreatedBy = au.ID 
                      INNER JOIN am.TenantUser upd ON upd.ID = a.UpdatedBy
                      INNER JOIN am.UserTenantLinking TL ON TL.TenantUserId= a.ID and TL.TenantId= @tenantId                     
                      INNER JOIN ap.RoleLinking rl ON rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId 
                      INNER JOIN ap.Role r ON r.ID = rl.RoleId 
                      INNER JOIN am.TenantUserAppLinking UAL ON a.ID =UAL.TenantUserid and UAL.AppId= @AppId 
                      WHERE UAL.UserType = @UserType AND UAL.AppId=@AppId  AND TL.tenantId= @tenantId AND 
                      (a.CreatedOn BETWEEN @FromDate AND @ToDate) order by a.FullName";

      SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
      //SqlParameter parentrefIdParam = new SqlParameter("@parentrefid", parenretfid);
      SqlParameter appIdparam = new SqlParameter("@AppId", appId);
      SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

      List<PlatAppUserReportDTO> appUserDTO = await GetQueryEntityListAsync<PlatAppUserReportDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, fromDate, toDate });// _context.Query<AppUserDetailsDTO>().FromSql(query, userTypeParam, parentrefIdParam, appIdparam, tenantIdParam).ToListAsync<AppUserDetailsDTO>();
      return appUserDTO;

    }

    ///<inheritdoc/>
    public async Task<List<PubAppUserReportDTO>> GetAllPubAppUserListByUserTypeAsync(ReportFilterDTO filter, int userType, Guid tenantId, Guid parenretfid, Guid appId, CancellationToken token = default(CancellationToken)) 

{
      
        string query = @"SELECT DISTINCT a.ID, a.FullName as UserName,a.Email,au.FullName as InvitedBy,UAL.joineddate as JoinedOn,
                        a.CreatedOn as InvitedOn,  UAL.Active ,a.Deleted, r.PermissionBitMask,Permissions=1,
                       Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Role,'' as Status FROM am.TenantUser a                        
                      INNER JOIN am.TenantUser au on a.CreatedBy = au.ID 
                      INNER JOIN am.TenantUser upd on upd.ID = a.UpdatedBy
                      INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId                      
                      INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId 
                      INNER JOIN ap.Role r on r.ID = rl.RoleId 
                      INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId 
                      WHERE UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId
                      AND (a.CreatedOn BETWEEN @FromDate AND @ToDate) order by a.FullName";

        SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
        //SqlParameter parentrefIdParam = new SqlParameter("@parentrefid", parenretfid);
        SqlParameter appIdparam = new SqlParameter("@AppId", appId);
        SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
        SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
        SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

        List<PubAppUserReportDTO> appUserDTO = await GetQueryEntityListAsync<PubAppUserReportDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, fromDate, toDate });// _context.Query<AppUserDetailsDTO>().FromSql(query, userTypeParam, parentrefIdParam, appIdparam, tenantIdParam).ToListAsync<AppUserDetailsDTO>();
        return appUserDTO;
  
    }

    ///<inheritdoc/>
    public async Task<List<BizAppUserReportDTO>> GetBizPayAppUserListByUserTypeAsync(ReportFilterDTO filter, int userType, Guid tenantId, Guid parenretfid, Guid appId, CancellationToken token = default(CancellationToken)) {
      
        string query = @"SELECT DISTINCT a.ID, a.FullName as UserName,au.FullName as InvitedBy,UAL.joineddate as JoinedOn,a.CreatedOn as InvitedOn,  UAL.Active ,UAL.Deleted, r.PermissionBitMask,Permissions=1,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Role,'' as Status 
						FROM am.TenantUser a 
						INNER JOIN am.TenantUser au on a.CreatedBy = au.ID 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId 
                        INNER JOIN pay.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId AND rl.Deleted = 0  
                        INNER JOIN pay.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  
                        INNER JOIN am.TenantUser upd on upd.ID = UAL.UpdatedBy 
                        INNER JOIN am.TenantUser inv on inv.ID = UAL.InvitedBy 
                        WHERE UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId 
                        AND (a.CreatedOn BETWEEN @FromDate AND @ToDate) order by a.FullName";


        SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
        SqlParameter parentrefIdParam = new SqlParameter("@parentrefid", parenretfid);
        SqlParameter appIdparam = new SqlParameter("@AppId", appId);
        SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
        SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
        SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

        List<BizAppUserReportDTO> appUserDTO = await GetQueryEntityListAsync<BizAppUserReportDTO>(query, new object[] { userTypeParam, parentrefIdParam, appIdparam, tenantIdParam,fromDate,toDate });// _context.Query<AppUserDetailsDTO>().FromSql(query, userTypeParam, parentrefIdParam, appIdparam, tenantIdParam).ToListAsync<AppUserDetailsDTO>();
        return appUserDTO;
     
    }

        ///<inheritdoc/>
        public async Task<List<BizAppUserReportDTO>> GetBizCustAppUserListByUserTypeAsync(ReportFilterDTO filter, int userType, Guid tenantId, Guid parenretfid, Guid appId, CancellationToken token = default(CancellationToken)) {

            string query = @"SELECT DISTINCT a.ID, a.FullName as UserName,au.FullName as InvitedBy,UAL.joineddate as JoinedOn,a.CreatedOn as InvitedOn,  UAL.Active ,UAL.Deleted, r.PermissionBitMask,Permissions=1,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Role,'' as Status 
						FROM am.TenantUser a 
						INNER JOIN am.TenantUser au on a.CreatedBy = au.ID 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId 
                        INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId AND rl.Deleted = 0  
                        INNER JOIN ap.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  
                        INNER JOIN am.TenantUser upd on upd.ID = UAL.UpdatedBy 
                        INNER JOIN am.TenantUser inv on inv.ID = UAL.InvitedBy 
                        WHERE UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId 
                        AND (a.CreatedOn BETWEEN @FromDate AND @ToDate) order by a.FullName";


            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter parentrefIdParam = new SqlParameter("@parentrefid", parenretfid);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            List<BizAppUserReportDTO> appUserDTO = await GetQueryEntityListAsync<BizAppUserReportDTO>(query, new object[] { userTypeParam, parentrefIdParam, appIdparam, tenantIdParam, fromDate, toDate });// _context.Query<AppUserDetailsDTO>().FromSql(query, userTypeParam, parentrefIdParam, appIdparam, tenantIdParam).ToListAsync<AppUserDetailsDTO>();
            return appUserDTO;

        }

        ///<inheritdoc/>
        public async Task<List<PartAppUserReportDTO>> GetPartPayAppUserListAsync(ReportFilterDTO filter, int userType, Guid tenantId, Guid parenretfid, Guid appId, CancellationToken token = default(CancellationToken)) {
      string query = @"SELECT DISTINCT a.ID, a.FullName as UserName,au.FullName as InvitedBy,UAL.joineddate as JoinedOn,a.CreatedOn as InvitedOn,  UAL.Active ,UAL.Deleted, r.PermissionBitMask,Permissions=1,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Role,'' as Status, UAL.BusinessPartnerTenantId FROM am.TenantUser a 
						INNER JOIN am.TenantUser au on a.CreatedBy = au.ID 
                      INNER JOIN am.TenantUser upd on upd.ID = a.UpdatedBy
                      INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId                      
                      INNER JOIN pay.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId AND rl.Deleted = 0
                      INNER JOIN pay.Role r on r.ID = rl.RoleId 
                      INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserid and UAL.AppId= @AppId AND UAL.TenantId =  @tenantId 
                      WHERE UAL.UserType = @UserType AND (a.CreatedOn BETWEEN @FromDate AND @ToDate) AND UAL.AppId=@AppId  and TL.tenantId= @tenantId  order by a.FullName";


      SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
      //SqlParameter parentrefIdParam = new SqlParameter("@parentrefid", parenretfid);
      SqlParameter appIdparam = new SqlParameter("@AppId", appId);
      SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

      List<PartAppUserReportDTO> appUserDTO = await GetQueryEntityListAsync<PartAppUserReportDTO>(query, new object[] { userTypeParam,  appIdparam, tenantIdParam, fromDate, toDate });// _context.Query<AppUserDetailsDTO>().FromSql(query, userTypeParam, parentrefIdParam, appIdparam, tenantIdParam).ToListAsync<AppUserDetailsDTO>();
      return appUserDTO;
    }

        ///<inheritdoc/>
        public async Task<List<PartAppUserReportDTO>> GetPartCustAppUserListAsync(ReportFilterDTO filter, int userType, Guid tenantId, Guid parenretfid, Guid appId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT DISTINCT a.ID, a.FullName as UserName,au.FullName as InvitedBy,UAL.joineddate as JoinedOn,a.CreatedOn as InvitedOn,  UAL.Active ,UAL.Deleted, r.PermissionBitMask,Permissions=1,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Role,'' as Status, UAL.BusinessPartnerTenantId FROM am.TenantUser a 
						INNER JOIN am.TenantUser au on a.CreatedBy = au.ID 
                      INNER JOIN am.TenantUser upd on upd.ID = a.UpdatedBy
                      INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId                      
                      INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId AND rl.Deleted = 0
                      INNER JOIN ap.Role r on r.ID = rl.RoleId 
                      INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserid and UAL.AppId= @AppId AND UAL.TenantId =  @tenantId 
                      WHERE UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId AND (a.CreatedOn BETWEEN @FromDate AND @ToDate)  order by a.FullName";


            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            //SqlParameter parentrefIdParam = new SqlParameter("@parentrefid", parenretfid);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            List<PartAppUserReportDTO> appUserDTO = await GetQueryEntityListAsync<PartAppUserReportDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, fromDate, toDate });// _context.Query<AppUserDetailsDTO>().FromSql(query, userTypeParam, parentrefIdParam, appIdparam, tenantIdParam).ToListAsync<AppUserDetailsDTO>();
            return appUserDTO;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetUserNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
      FormattableString query = $@"SELECT tu.FullName AS Name FROM am.TenantLinking tl 
								INNER JOIN am.UserTenantLinking tul ON tl.BusinessTenantId = tul.TenantId And tl.BusinessPartnerTenantId IS NULL
                                INNER JOIN am.TenantUserAppLinking u ON tul.TenantUserId = u.TenantUserId AND u.TenantId = tul.TenantId 
                                INNER JOIN am.TenantUser tu ON tu.Id = u.TenantUserId
                                INNER JOIN am.App a ON a.ID = u.AppId and a.AppSubscriptionMode = 2
                                WHERE u.Deleted = 0 And u.UserType = 3 And  u.TenantUserId <> '00000000-0000-0000-0000-000000000000' AND u.TenantId = @TenantId AND tu.Deleted = 0 GROUP BY tu.Id,tu.FullName";
      SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
      IQueryable<NameDTO> querable = _context.Query<NameDTO>().FromSql(query.ToString(), new object[] { tenantIdParam });
      List<NameDTO> UserNameList = await querable.ToListAsync();
      return UserNameList;
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPFUserNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
      FormattableString query = $@"SELECT FullName AS Name FROM am.TenantUser 
                                   WHERE UserType = 2 AND TenantId = @TenantId AND Deleted = 0";
      SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
      IQueryable<NameDTO> querable = _context.Query<NameDTO>().FromSql(query.ToString(), new object[] { tenantIdParam });
      List<NameDTO> UserNameList = await querable.ToListAsync();
      return UserNameList;
    }

    #endregion
  }
}

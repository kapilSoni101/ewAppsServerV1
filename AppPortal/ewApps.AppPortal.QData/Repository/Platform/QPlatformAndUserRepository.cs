/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 24 September 2018

 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {

    public class QPlatformAndUserRepository:QBaseRepository<QAppPortalDbContext>, IQPlatformAndUserRepository {


        public QPlatformAndUserRepository(QAppPortalDbContext context) : base(context) {
        }

        /// <summary>
        /// Get Platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PlatformBrandingDQ> GetPlatformBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            //string query = @"SELECT ID,TenantId,Name,PoweredBy,Copyright,ThemeId,ThemeKey FROM ap.vQPlatformBranding
            //WHERE TenantId = @TenantId AND AppId = @AppId";

            string query = @"SELECT ap.Platform.ID,ap.Platform.TenantId,ap.Platform.Name,ap.Platform.PoweredBy,ap.Platform.Copyright,am.TenantSubscription.ThemeId,am.Theme.ThemeKey 
                                    FROM ap.Platform
                                    INNER JOIN am.TenantSubscription ON am.TenantSubscription.TenantId = ap.Platform.TenantId and am.TenantSubscription.AppId = @AppId
                                    INNER JOIN am.Theme ON am.Theme.ID = am.TenantSubscription.ThemeId
                            WHERE ap.Platform.TenantId = @TenantId AND am.TenantSubscription.AppId = @AppId";
            
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityAsync<PlatformBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }

        ///<inheritdoc/>
        public async Task<List<AppDetailDTO>> GetAppDetailsAsync() {           

            string query = @"SELECT a.ID,a.CreatedBy,a.ID as AppID,a.CreatedOn,a.UpdatedBy, a.UpdatedOn,a.Deleted,Name,a.Active,a.ThemeId, 
                           a.InactiveComment, (select count(1) from am.AppService where AppId= a.id) servicecount ,(select count(1) from ap.PublisherAppSetting pas
                           INNER JOIN ap.publisher pub on pub.TenantId = pas.TenantId  where pas.deleted = 0 and pas.AppId = a.ID ) publishercount, (SELECT COUNT(1)
                           FROM am.TenantSubscription ts 
                           INNER JOIN am.Tenant t on t.ID=ts.TenantId where AppId= a.id and ts.Deleted=0 and t.Deleted = 0 and t.TenantType = 3) 
                           PurchaseCount ,'' as ThemeKey,u.FullName as CreaterName,null as ThumbnailId,'' as FileName ,  ThumbnailUrl=null,a.IdentityNumber , a.appkey FROM am.App a 
                           INNER JOIN am.TenantUser u on a.CreatedBy=u.ID where a.deleted=0 and a.AppSubscriptionMode = 2";
            List<AppDetailDTO> appDetailDTOs = await GetQueryEntityListAsync<AppDetailDTO>(query, null);
            return appDetailDTOs;
        }

        ///<inheritdoc/>
        public async Task <AppDetailDTO> GetAppDetailsByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {           

            string query = @"select a.ID,a.CreatedBy,a.ID as AppID,a.CreatedOn,a.UpdatedBy, a.UpdatedOn,a.Deleted,Name,a.Active,a.ThemeId, 
                           a.InactiveComment, (select count(1) from am.AppService where AppId= a.id) servicecount ,(select count(1) from ap.PublisherAppSetting pas
                           INNER JOIN ap.publisher pub on pub.TenantId = pas.TenantId  where pas.deleted = 0 and pas.AppId = a.ID ) publishercount, (select  count(1)
                           from am.TenantSubscription ts 
                           INNER JOIN am.Tenant t on t.ID=ts.TenantId where AppId= a.id and ts.Deleted=0 and t.Deleted = 0 and t.TenantType = 3) 
                           PurchaseCount ,th.themekey as ThemeKey,u.FullName as CreaterName,t.ID as ThumbnailId,t.filename as FileName ,  ThumbnailUrl=null,a.IdentityNumber , a.appkey from am.App a 
                           INNER JOIN am.TenantUser u on a.CreatedBy=u.ID
                           LEFT JOIN core.EntityThumbnail t on a.ID=t.OwnerEntityId left join am.Theme th on th.ID =a.ThemeId 
                           where a.deleted=0 and a.AppSubscriptionMode = 2 and a.Id = @AppId";

            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityAsync<AppDetailDTO>(query, new SqlParameter[] {paramAppId }, token);
            
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetPlatformTenantUsers(int userType, Guid tenantId, Guid appId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT Distinct a.ID, a.FullName,FeatureCount=1,au.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                        , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber 
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , TL.UpdatedOn as UpdatedOn , TL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId and tl.Deleted=@deleted
                        INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  
                        INNER JOIN ap.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  
                        INNER JOIN am.TenantUser au on UAL.InvitedBy = au.ID 
                        INNER JOIN am.TenantUser upd on upd.ID = TL.UpdatedBy
                        WHERE  UAL.UserType = @UserType  and UAL.AppId=@AppId  order by a.FullName ";
            }
            else {
                query = @"SELECT distinct a.ID, a.FullName,FeatureCount=1,inv.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                        , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber 
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , UAL.UpdatedOn as UpdatedOn , UAL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId AND TL.Deleted=@deleted
                        INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  AND rl.Deleted=@deleted
                        INNER JOIN ap.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  AND UAL.Deleted=@deleted
                        INNER JOIN am.TenantUser upd on upd.ID = UAL.UpdatedBy 
                        INNER JOIN am.TenantUser inv on inv.ID = UAL.InvitedBy 
                        WHERE UAL.Deleted = @deleted  and UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId order by a.FullName ";
            }
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserDetailsDTO> appUserDTO = await GetQueryEntityListAsync<TenantUserDetailsDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, deletedParam });
            return appUserDTO;
        }


        public async Task<TenantUserAndPermissionViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey, bool deleted) {

            string query = string.Empty;
            if(deleted) {
                query = @"select top 1 Tu.Id as 'TenantUserId', TU.FirstName, TU.LastName, TU.Email, TU.IdentityNumber,
                                    TU.CreatedOn , CTU.FullName as 'CreatedBy',TU.Phone , TL.JoinedDate, RR.PermissionBitMask, TL.Active, TL.[Status] as 'InvitationStatus'
                                    from am.TenantUser TU
                                    INNER JOIN am.App AP ON ap.AppKey = @appkey
                                    INNER JOIN am.TenantUserAppLinking TL ON TL.TenantUserId = @tenantUserId AND TL.AppId = ap.ID ANd TL.TenantId = @tenantId ANd TL.Deleted = @deleted
                                    INNER JOIN am.TenantUser CTU ON CTU.ID = TU.CreatedBy
                                    INNER JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.AppId = ap.ID ANd RL.TenantId = @tenantId
                                    INNER JOIN ap.[Role] RR ON RR.ID = RL.RoleId
                                    WHERE TU.ID = @tenantUserId";
            }
            else {
                query = @" select Tu.Id as 'TenantUserId', TU.FirstName, TU.LastName, TU.Email, TU.IdentityNumber,
                                TU.CreatedOn , CTU.FullName as 'CreatedBy',TU.Phone , TL.JoinedDate, RR.PermissionBitMask, TL.Active, TL.[Status] as 'InvitationStatus'
                                from am.TenantUser TU
                                INNER JOIN am.App AP ON ap.AppKey = @appkey
                                INNER JOIN am.TenantUserAppLinking TL ON TL.TenantUserId = @tenantUserId AND TL.AppId = ap.ID ANd TL.TenantId = @tenantId ANd TL.Deleted=@deleted
                                INNER JOIN am.TenantUser CTU ON CTU.ID = TU.CreatedBy 
                                INNER JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.AppId = ap.ID ANd RL.TenantId = @tenantId ANd RL.Deleted=@deleted
                                INNER JOIN ap.[Role] RR ON RR.ID = RL.RoleId
                                WHERE TU.ID = @tenantUserId AND TU.Deleted =@deleted ";
            }

            // Sql parameters.
            SqlParameter tenantUserIdParam = new SqlParameter("@tenantUserId", tenantUserId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter appKeyParam = new SqlParameter("@appkey", appKey);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            TenantUserAndPermissionViewDTO tenantUserInfo = await GetQueryEntityAsync<TenantUserAndPermissionViewDTO>(query, new object[] { tenantUserIdParam, tenantIdParam, appKeyParam, deletedParam });
            return tenantUserInfo;
        }

        ///<inheritdoc/>
        public async Task<List<RoleKeyCountDTO>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId, Guid? businessPartnerTenantId) {
            string query = string.Empty;
            if(businessPartnerTenantId == null) {
                query = @" SELECT RoleKey , RL.TenantUserId 
                            from am.UserTenantLinking UT 
                            INNER JOIN ap.RoleLinking RL ON RL.TenantUserId = UT.TenantUserId  AND RL.AppId= @AppId
                            INNER JOIN ap.Role r ON r.ID= RL.RoleId
                            WHERE UT.TenantId= @TenantId AND UT.BusinessPartnerTenantId is null AND RoleKey= @RoleKey AND UT.UserType= @UserType  AND RL.Deleted = 0  ";

                SqlParameter tenantIdparam = new SqlParameter("@TenantId", tenantId);
                SqlParameter appIdparam = new SqlParameter("@AppId", appId);
                SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
                SqlParameter roleKeyParam = new SqlParameter("@RoleKey", ewApps.AppPortal.Common.AppPortalConstants.AdminRoleKey);
                object[] paramlist = new object[] { tenantIdparam, userTypeParam, roleKeyParam, appIdparam };

                List<RoleKeyCountDTO> count = await GetQueryEntityListAsync<RoleKeyCountDTO>(query, paramlist);
                return count;

            }
            else {
                query = @" SELECT RoleKey, RL.TenantUserId  
                            from am.UserTenantLinking UT 
                            INNER JOIN ap.RoleLinking RL ON RL.TenantUserId = UT.TenantUserId  AND RL.AppId= @AppId
                            INNER JOIN ap.Role r ON r.ID= RL.RoleId
                            WHERE UT.TenantId= @TenantId and UT.BusinessPartnerTenantId = @BusinessPartnerTenantId and RoleKey= @RoleKey and UT.UserType= @UserType AND RL.Deleted = 0 ";

                SqlParameter tenantIdparam = new SqlParameter("@TenantId", tenantId);
                SqlParameter appIdparam = new SqlParameter("@AppId", appId);
                SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
                SqlParameter roleKeyParam = new SqlParameter("@RoleKey", Common.AppPortalConstants.AdminRoleKey);
                SqlParameter busPartnerTenantIdParam = new SqlParameter("@BusinessPartnerTenantId", businessPartnerTenantId);
                object[] paramlist = new object[] { tenantIdparam, userTypeParam, roleKeyParam, busPartnerTenantIdParam, appIdparam };

                List<RoleKeyCountDTO> count = await GetQueryEntityListAsync<RoleKeyCountDTO>(query, paramlist);
                return count;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {
    public class QCustomerAppRepository:QBaseRepository<QAppPortalDbContext>, IQCustomerAppRepository {

        public QCustomerAppRepository(QAppPortalDbContext context) : base(context) {
        }

        ///<inheritdoc/>
        public async Task<List<AppInfoDTO>> GetAllCustomerApplicationsAsync(Guid businessPartnerTenantId, string defaultFreeAppKey) {
            string query = @"select AP.ID , ap.IdentityNumber, PAS.Name, PAS.ThemeId, PAS.Active, ap.AppKey,CAST(0 as  bigint ) as 'PermissionBitMask' 
                            from am.TenantUserAppLinking TAL
                            INNER JOIN am.TenantLinking TL on TL.BusinessPartnerTenantId = @businessPartnerTenantId 
                            INNER JOIN ap.PublisherAppSetting PAS on PAS.TenantId = TL.PublisherTenantId and PAS.Active = 1 and TAL.AppId = PAS.AppId
                            INNER JOIN am.App AP on AP.ID = TAL.AppId 
                            WHERE TAL.BusinessPartnerTenantId = @businessPartnerTenantId And TAL.Deleted = 0
                            union 
                            select AP.ID , ap.IdentityNumber, AP.Name, AP.ThemeId, AP.Active, ap.AppKey ,CAST(0 as  bigint ) as 'PermissionBitMask'
                            from am.App AP
                            where AppKey =@appkey";

            SqlParameter tenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
            SqlParameter appKeyParam = new SqlParameter("@appkey", defaultFreeAppKey);

            return await GetQueryEntityListAsync<AppInfoDTO>(query, new object[] { tenantIdParam, appKeyParam });
        }

        public async Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey, bool deleted) {

            string query = string.Empty;
            if(deleted) {
                query = @"select AP.ID as 'AppId', AP.Name, AppKey  , isnull (R.PermissionBitMask, 0)  as 'PermissionBitMask' , ISNULL( TUL.Active , CAST(0 as bit)) as 'Active' , isnull(TUL.Status , 0 ) as 'InvitationStatus' , case when TUL.ID is null then CAST(0 as bit) else CAST(1 as bit) end as 'Assigned' , TUL.InvitedOn as 'InvitedOn' , TUL.JoinedDate as 'JoinedDate' , InvTU.FullName as 'InvitedByName'
                                from am.App AP
                                LEFT JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.TenantId = @tenantId AND RL.AppId = AP.ID 
                                LEFT JOIN ap.Role R ON R.ID = RL.RoleId
                                LEFT JOIN am.TenantUserAppLinking TUL ON TUL.TenantUserId = @tenantUserId AND TUL.AppId = AP.ID ANd TUL.TenantId =@tenantId  
                                LEFT JOIN am.TenantUser InvTU ON InvTU.ID = TUL.InvitedBy 
                                where AppKey =@appkey";

                SqlParameter tenantUserIdParam = new SqlParameter("@tenantUserId", tenantUserId);
                SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
                SqlParameter appKeyParam = new SqlParameter("@appkey", appKey);
                List<TenantUserAppPermissionDTO> tenantUserAppInfos = await GetQueryEntityListAsync<TenantUserAppPermissionDTO>(query, new object[] { tenantUserIdParam, tenantIdParam, appKeyParam });
                return tenantUserAppInfos;

            }
            else {
                query = @"  select AP.ID as 'AppId' , PAS.Name , AP.AppKey ,COALESCE (PayR.PermissionBitMask , R.PermissionBitMask , 0) as 'PermissionBitMask', ISNULL( TUL.Active , CAST(1 as bit)) as 'Active' , isnull(TUL.Status , 0 ) as 'InvitationStatus' , case when TUL.ID is null then CAST(0 as bit) else CAST(1 as bit) end as 'Assigned' , TUL.InvitedOn as 'InvitedOn' , TUL.JoinedDate as 'JoinedDate' , InvTU.FullName as 'InvitedByName'
                            from am.TenantUserAppLinking TAL
                            INNER JOIN am.TenantLinking TL on TL.BusinessTenantId = @tenantId and TL.BusinessPartnerTenantId = @businessPartnerTenantId
                            INNER JOIN ap.PublisherAppSetting PAS on PAS.TenantId = TL.PublisherTenantId and PAS.Active = 1 and TAL.AppId = PAS.AppId
                            INNER JOIN am.App AP on AP.ID = TAL.AppId 
                            LEFT JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.TenantId = @tenantId AND RL.AppId = AP.ID AND RL.Deleted = 0
                            LEFT JOIN ap.[Role] R ON R.ID = RL.RoleId
                            LEFT JOIN pay.RoleLinking PayRL ON PayRL.TenantUserId = @tenantUserId AND PayRL.TenantId = @tenantId AND PayRL.AppId = AP.ID AND PayRL.Deleted = 0
                            LEFT JOIN pay.[Role] PayR ON PayR.ID = PayRL.RoleId
                            LEFT JOIN am.TenantUserAppLinking TUL ON TUL.TenantUserId = @tenantUserId AND TUL.AppId = AP.ID ANd TUL.TenantId =@tenantId AND TUL.BusinessPartnerTenantId = @businessPartnerTenantId AND TUL.Deleted = 0
                            LEFT JOIN am.TenantUser InvTU ON InvTU.ID = TUL.InvitedBy AND TUL.Deleted = 0
                            WHERE TAL.TenantId = @tenantId And TAL.BusinessPartnerTenantId = @businessPartnerTenantId
                            union 
                            select AP.ID as 'AppId', AP.Name, AppKey  , isnull (R.PermissionBitMask, 0)  as 'PermissionBitMask' , ISNULL( TUL.Active , CAST(0 as bit)) as 'Active' , isnull(TUL.Status , 0 ) as 'InvitationStatus' , case when TUL.ID is null then CAST(0 as bit) else CAST(1 as bit) end as 'Assigned' , TUL.InvitedOn as 'InvitedOn' , TUL.JoinedDate as 'JoinedDate' , InvTU.FullName as 'InvitedByName'
                            from am.App AP
                            LEFT JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.TenantId = @tenantId AND RL.AppId = AP.ID AND RL.Deleted = 0
                            LEFT JOIN ap.Role R ON R.ID = RL.RoleId
                            LEFT JOIN am.TenantUserAppLinking TUL ON TUL.TenantUserId = @tenantUserId AND TUL.AppId = AP.ID ANd TUL.TenantId =@tenantId AND TUL.Deleted = 0
                            LEFT JOIN am.TenantUser InvTU ON InvTU.ID = TUL.InvitedBy AND TUL.Deleted = 0
                            where AppKey =@appkey";

                SqlParameter tenantUserIdParam = new SqlParameter("@tenantUserId", tenantUserId);
                SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
                SqlParameter appKeyParam = new SqlParameter("@appkey", appKey);
                SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
                List<TenantUserAppPermissionDTO> tenantUserAppInfos = await GetQueryEntityListAsync<TenantUserAppPermissionDTO>(query, new object[] { tenantUserIdParam, tenantIdParam, appKeyParam, businessPartnerTenantIdParam });
                return tenantUserAppInfos;
            }
        }

    
  }
}

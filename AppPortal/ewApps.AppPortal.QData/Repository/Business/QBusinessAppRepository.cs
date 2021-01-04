using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {
    public class QBusinessAppRepository:QBaseRepository<QAppPortalDbContext>, IQBusinessAppRepository {

        #region Local         
        #endregion Local

        #region Constructor         

        /// <summary>
        /// Initializes a new instance of the <see cref="QBusinessAppRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QBusinessAppRepository(QAppPortalDbContext context) : base(context) {
        }

        #endregion Constructor

        public async Task<List<AppInfoDTO>> GetAllApplicationForTenantAsync(Guid tenantId, string defaultFreeAppKey) {
            string query = @"select AP.ID , ap.IdentityNumber, PAS.Name, PAS.ThemeId, PAS.Active, ap.AppKey ,CAST(0 as  bigint ) as 'PermissionBitMask'
                            from ap.TenantAppLinking TAL
                            INNER JOIN am.TenantLinking TL on TL.BusinessTenantId = @tenantId and TL.BusinessPartnerTenantId is null
                            INNER JOIN ap.PublisherAppSetting PAS on PAS.TenantId = TL.PublisherTenantId and PAS.Active = 1 and TAL.AppId = PAS.AppId
                            INNER JOIN am.App AP on AP.ID = TAL.AppId 
                            WHERE TAL.TenantId = @tenantId
                            union 
                            select AP.ID , ap.IdentityNumber, AP.Name, AP.ThemeId, AP.Active, ap.AppKey ,CAST(0 as  bigint ) as 'PermissionBitMask' 
                            from am.App AP
                            where AppKey =@appkey";

            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter appKeyParam = new SqlParameter("@appkey", defaultFreeAppKey);

            return await GetQueryEntityListAsync<AppInfoDTO>(query, new object[] { tenantIdParam, appKeyParam });
        }

        public async Task<List<TenantUserAppPermissionDTO>> GetTenantUserAppAndPermissionDetailsAsync(Guid tenantUserId, Guid tenantId, string appKey , bool deleted ) {

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
                query = @"select AP.ID as 'AppId' , PAS.Name , AP.AppKey ,COALESCE (PayR.PermissionBitMask , ShipR.PermissionBitMask , R.PermissionBitMask , 0) as 'PermissionBitMask', ISNULL( TUL.Active , CAST(1 as bit)) as 'Active' , isnull(TUL.Status , 0 ) as 'InvitationStatus' , case when TUL.ID is null then CAST(0 as bit) else CAST(1 as bit) end as 'Assigned' , TUL.InvitedOn as 'InvitedOn' , TUL.JoinedDate as 'JoinedDate' , InvTU.FullName as 'InvitedByName'
                                from ap.TenantAppLinking TAL
                                INNER JOIN am.TenantLinking TL on TL.BusinessTenantId = @tenantId and TL.BusinessPartnerTenantId is null
                                INNER JOIN ap.PublisherAppSetting PAS on PAS.TenantId = TL.PublisherTenantId and PAS.Active = 1 and TAL.AppId = PAS.AppId
                                INNER JOIN am.App AP on AP.ID = TAL.AppId 
                                LEFT JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.TenantId = @tenantId AND RL.AppId = AP.ID AND RL.Deleted = 0
                                LEFT JOIN ap.[Role] R ON R.ID = RL.RoleId
                                LEFT JOIN pay.RoleLinking PayRL ON PayRL.TenantUserId = @tenantUserId AND PayRL.TenantId = @tenantId AND PayRL.AppId = AP.ID AND PayRL.Deleted = 0
                                LEFT JOIN pay.[Role] PayR ON PayR.ID = PayRL.RoleId
                                LEFT JOIN ship.RoleLinking ShipRL ON ShipRL.TenantUserId = @tenantUserId AND ShipRL.TenantId = @tenantId AND ShipRL.AppId = AP.ID AND ShipRL.Deleted = 0
                                LEFT JOIN ship.[Role] ShipR ON ShipR.ID = ShipRL.RoleId
                                LEFT JOIN am.TenantUserAppLinking TUL ON TUL.TenantUserId = @tenantUserId AND TUL.AppId = AP.ID ANd TUL.TenantId =@tenantId AND TUL.Deleted = 0
                                LEFT JOIN am.TenantUser InvTU ON InvTU.ID = TUL.InvitedBy AND TUL.Deleted = 0
                                WHERE TAL.TenantId = @tenantId
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
                List<TenantUserAppPermissionDTO> tenantUserAppInfos = await GetQueryEntityListAsync<TenantUserAppPermissionDTO>(query, new object[] { tenantUserIdParam, tenantIdParam, appKeyParam });
                return tenantUserAppInfos;
            }
        }

        public async Task<List<BusCustomerApplicationDTO>> GetAppForCustomerAsync(Guid tenantId, Guid businessPartnerTenantId) {
            string query = @" Select distinct AP.ID , AP.AppKey, PAS.Name AS 'AppName', SP.PlanName AS 'SubscriptionPlanName', TS.SubscriptionStartEnd AS 'SubscriptionExpiry' , TU.FullName AS 'SubscribedBy',
                            TS.CreatedOn AS 'SubscribedOn', TS.AutoRenewal AS 'AutoRenewal',
                            CASE WHEN TUL.ID is null then CAST(0 as bit)
                            else   CAST(1 as bit) end As 'Assigned'
                            from ap.PortalAppLinking PAL
                            INNER JOIN ap.Portal PR ON PR.ID = PAL.PortalId
                            INNER JOIN ap.TenantAppLinking TAL ON PAL.AppId = TAL.AppId AND TAL.TenantId = @tenantId
                            INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = @tenantId AND TL.BusinessPartnerTenantId is null 
                            INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = TL.PublisherTenantId AND PAS.AppId = TAL.AppId
                            INNER JOIN am.App AP ON AP.ID = TAL.AppId AND AP.AppSubscriptionMode =2
                            INNER JOIN am.TenantSubscription TS ON TS.TenantId = TAL.TenantId AND TS.AppId = AP.ID 
                            INNER JOIN ap.PubBusinessSubsPlan SP ON SP.ID = TS.SubscriptionPlanId
                            INNER JOIN am.TenantUser TU ON TU.ID = TS.CreatedBy
                            LEFT JOIN am.TenantUserAppLinking TUL ON TUL.BusinessPartnerTenantId = @businessPartnerTenantId AND TUL.AppId = PAS.AppId AND TUL.Deleted=0
                            WHERE PR.UserType=4";

            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);

            return await GetQueryEntityListAsync<BusCustomerApplicationDTO>(query, new object[] { tenantIdParam, businessPartnerTenantIdParam });
        }
    public async Task<List<BusVendorApplicationDTO>> GetAppForVendorAsync(Guid tenantId, Guid businessPartnerTenantId)
    {
      string query = @" Select distinct AP.ID , AP.AppKey, PAS.Name AS 'AppName', SP.PlanName AS 'SubscriptionPlanName', TS.SubscriptionStartEnd AS 'SubscriptionExpiry' , TU.FullName AS 'SubscribedBy',
                            TS.CreatedOn AS 'SubscribedOn', TS.AutoRenewal AS 'AutoRenewal',
                            CASE WHEN TUL.ID is null then CAST(0 as bit)
                            else   CAST(1 as bit) end As 'Assigned'
                            from ap.PortalAppLinking PAL
                            INNER JOIN ap.Portal PR ON PR.ID = PAL.PortalId
                            INNER JOIN ap.TenantAppLinking TAL ON PAL.AppId = TAL.AppId AND TAL.TenantId = @tenantId
                            INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = @tenantId AND TL.BusinessPartnerTenantId is null 
                            INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = TL.PublisherTenantId AND PAS.AppId = TAL.AppId
                            INNER JOIN am.App AP ON AP.ID = TAL.AppId AND AP.AppSubscriptionMode =2 And AP.Appkey = @vendApp
                            INNER JOIN am.TenantSubscription TS ON TS.TenantId = TAL.TenantId AND TS.AppId = AP.ID 
                            INNER JOIN ap.PubBusinessSubsPlan SP ON SP.ID = TS.SubscriptionPlanId
                            INNER JOIN am.TenantUser TU ON TU.ID = TS.CreatedBy
                            LEFT JOIN am.TenantUserAppLinking TUL ON TUL.BusinessPartnerTenantId = @businessPartnerTenantId AND TUL.AppId = PAS.AppId AND TUL.Deleted=0
                            WHERE PR.UserType=5";

      SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
      SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
      SqlParameter appKeyParam = new SqlParameter("@vendApp", AppKeyEnum.vend.ToString());

      return await GetQueryEntityListAsync<BusVendorApplicationDTO>(query, new object[] { tenantIdParam, businessPartnerTenantIdParam, appKeyParam });
    }

    public async Task<BusCustomerApplicationCountDTO> GetAppCountForCustomerAsync(Guid businessPartnerTenantId) {
            string query = @" select count (1) as ApplicationCount from (select distinct AppId from am.TenantUserAppLinking  as TUL
                            INNER JOIN am.App AP ON  AP.AppSubscriptionMode =2
                            where BusinessPartnerTenantId= @businessPartnerTenantId AND TUL.AppId = AP.ID AND TUL.Deleted=0
                            ) a";
            
            SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
            return await GetQueryEntityAsync<BusCustomerApplicationCountDTO>(query, new object[] { businessPartnerTenantIdParam });
        }
    public async Task<BusVendorApplicationCountDTO> GetAppCountForVendorAsync(Guid businessPartnerTenantId)
    {
      string query = @" select count (1) as ApplicationCount from (select distinct AppId from am.TenantUserAppLinking  as TUL
                            INNER JOIN am.App AP ON  AP.AppSubscriptionMode =2
                            where BusinessPartnerTenantId= @businessPartnerTenantId AND TUL.AppId = AP.ID AND TUL.Deleted=0
                            ) a";

      SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
      return await GetQueryEntityAsync<BusVendorApplicationCountDTO>(query, new object[] { businessPartnerTenantIdParam });
    }
    public async Task<List<AppInfoDTO>> GetCustomerAsignedAppInfoAsync(Guid businessPartnerTenantId) {

            // In this squery we have to ignore customer setup app.
            string query = @"SELECT ap.ID,ap.IdentityNumber, PAS.Name, AP.ThemeId, ap.Active, AppKey , CAST(0 as  bigint ) as 'PermissionBitMask'
                            FROM am.TenantUserAppLinking TUL 
                            INNER JOIN am.App Ap ON TUL.AppId = ap.ID AND AP.AppSubscriptionMode = 2
                            INNER JOIN am.TenantLinking TL ON TL.BusinessPartnerTenantId = @businessPartnerTenantId 
                            INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = TL.PublisherTenantId AND TUL.AppId = PAS.AppId
                            WHERE TUL.BusinessPartnerTenantId = @businessPartnerTenantId  and TUL.Deleted = 0";

            SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
            List<AppInfoDTO> appInfo = await GetQueryEntityListAsync<AppInfoDTO>(query, new object[] { businessPartnerTenantIdParam });
            return appInfo;
        }

    public async Task<List<AppInfoDTO>> GetVendorAsignedAppInfoAsync(Guid businessPartnerTenantId)
    {

      // In this squery we have to ignore customer setup app.
      string query = @"SELECT ap.ID,ap.IdentityNumber, PAS.Name, AP.ThemeId, ap.Active, AppKey, CAST(0 as  bigint ) as 'PermissionBitMask'
                            FROM am.TenantUserAppLinking TUL 
                            INNER JOIN am.App Ap ON TUL.AppId = ap.ID AND AP.AppSubscriptionMode = 2
                            INNER JOIN am.TenantLinking TL ON TL.BusinessPartnerTenantId = @businessPartnerTenantId 
                            INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = TL.PublisherTenantId AND TUL.AppId = PAS.AppId
                            WHERE TUL.BusinessPartnerTenantId = @businessPartnerTenantId  and TUL.Deleted = 0";

      SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
      List<AppInfoDTO> appInfo = await GetQueryEntityListAsync<AppInfoDTO>(query, new object[] { businessPartnerTenantIdParam });
      return appInfo;
    }

    public async Task<AppShortInfoDTO> GetAppShortInfoByAppId(Guid appId, Guid tenantId) {
           
            string query = @"SELECT PAS.AppId, PAS.Name as AppName,a.AppKey 
                            FROM am.Tenant t 
                            INNER JOIN am.App a ON a.ID = @AppId 
                            INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = @TenantId
                            INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = TL.PublisherTenantId AND PAS.AppId = @AppId
                            WHERE t.ID = @TenantId AND tl.BusinessPartnerTenantId is null ";

            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityAsync<AppShortInfoDTO>(query, new object[] { appIdParam, tenantIdParam });
            
        }

    }
}

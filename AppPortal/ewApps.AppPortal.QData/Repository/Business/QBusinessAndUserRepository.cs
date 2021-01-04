using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QBusinessAndUserRepository:QBaseRepository<QAppPortalDbContext>, IQBusinessAndUserRepository {

        #region Local         

        #endregion Local

        #region Constructor         

        /// <summary>
        /// Initializes a new instance of the <see cref="QBusinessAndUserRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QBusinessAndUserRepository(QAppPortalDbContext context) : base(context) {
        }

        #endregion Constructor

        #region Public Methods

        #region Branding

        /// <summary>
        /// Get Platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BusinessBrandingDQ> GetBusinessBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {

            string query = @"SELECT ap.Business.ID, ap.Business.TenantId, ap.Business.Name, am.TenantSubscription.ThemeId,am.TenantSubscription.AppId,am.Theme.ThemeKey
                             FROM ap.Business
                             INNER JOIN am.TenantSubscription ON am.TenantSubscription.TenantId = ap.Business.TenantId AND am.TenantSubscription.AppId = @AppId
                             INNER JOIN am.Theme ON am.Theme.ID = am.TenantSubscription.ThemeId
                             WHERE ap.Business.TenantId = @TenantId AND am.TenantSubscription.AppId = @AppId";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);

            return await GetQueryEntityAsync<BusinessBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }

        #endregion Branding

        #region User 

        #region Business

        ///<inheritdoc/>
        public async Task<List<TenantUserSetupListDTO>> GetAllUsersByUserTypeAsync(int userType, Guid tenantId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT distinct TU.ID as 'TenantUserId', TU.FullName as 'FullName' , TU.IdentityNumber as 'IdentityNumber',
                        TU.Email as 'Email' , TL.Deleted as 'Deleted' , (select COUNT(1) from am.TenantUserAppLinking TUL INNER JOIN am.App App on App.ID = TUL.AppId And App.AppSubscriptionMode =2 
                        where TUL.TenantUserId = TU.ID AND TUL.TenantId = @tenantId and TUL.Deleted <> @deleted) as 'ApplicationCount' , TL.UpdatedOn as 'DeletedOn' , DELTU.FullName as 'DeletedByName'
                        FROM am.TenantUser TU
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= TU.ID and TL.TenantId= @tenantId AND TL.Deleted=@deleted
                        INNER JOIN am.TenantUser DELTU on DELTU.ID = TL.UpdatedBy 
                        WHERE TL.UserType = @UserType  order by TU.FullName ";
            }
            else {
                query = @"SELECT distinct TU.ID as 'TenantUserId', TU.FullName as 'FullName' , TU.IdentityNumber as 'IdentityNumber',
                            TU.Email as 'Email' , TL.Deleted as 'Deleted' , (select COUNT(1) from am.TenantUserAppLinking TUL INNER JOIN am.App App on App.ID = TUL.AppId And App.AppSubscriptionMode =2 
                            where TUL.TenantUserId = TU.ID AND TUL.TenantId = @tenantId and TUL.Deleted =@deleted) as 'ApplicationCount' , TL.UpdatedOn as 'DeletedOn' , '' as 'DeletedByName'
                            FROM am.TenantUser TU
                            INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= TU.ID and TL.TenantId= @tenantId AND TL.Deleted=@deleted
                            WHERE TU.Deleted = @deleted and TL.UserType = @UserType  order by TU.FullName ";
            }
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserSetupListDTO> tenantUserSetupListDTO = await GetQueryEntityListAsync<TenantUserSetupListDTO>(query, new object[] { userTypeParam, tenantIdParam, deletedParam });
            return tenantUserSetupListDTO;
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetAllPaymentUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT Distinct a.ID, a.FullName,FeatureCount=1,au.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                        , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber 
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , TL.UpdatedOn as UpdatedOn , TL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId
                        INNER JOIN pay.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  
                        INNER JOIN pay.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  and UAL.Deleted=@deleted
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
                        INNER JOIN pay.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  AND rl.Deleted=@deleted
                        INNER JOIN pay.Role r on r.ID = rl.RoleId 
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

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetAllShipmentUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT Distinct a.ID, a.FullName,FeatureCount=1,au.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                        , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber 
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , TL.UpdatedOn as UpdatedOn , TL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId 
                        INNER JOIN ship.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  
                        INNER JOIN ship.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  and UAL.Deleted=@deleted
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
                        INNER JOIN ship.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  AND rl.Deleted=@deleted
                        INNER JOIN ship.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  AND UAL.Deleted=@deleted
                        INNER JOIN am.TenantUser upd on upd.ID = UAL.UpdatedBy 
                        INNER JOIN am.TenantUser inv on inv.ID = UAL.InvitedBy 
                        WHERE UAL.Deleted = @deleted  and UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId order by a.FullName  ";
            }
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserDetailsDTO> appUserDTO = await GetQueryEntityListAsync<TenantUserDetailsDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, deletedParam });
            return appUserDTO;
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetAllCustomerAppUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT Distinct a.ID, a.FullName,FeatureCount=1,au.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                            , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber 
                            ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , TL.UpdatedOn as UpdatedOn , TL.Deleted 
                            FROM am.TenantUser a 
                            INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId 
                            INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  
                            INNER JOIN ap.Role r on r.ID = rl.RoleId 
                            INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId and UAL.Deleted=@deleted 
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
                            WHERE UAL.Deleted = @deleted  and UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId order by a.FullName  ";
            }
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserDetailsDTO> appUserDTO = await GetQueryEntityListAsync<TenantUserDetailsDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, deletedParam });
            return appUserDTO;
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetAllVendorAppUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT Distinct a.ID, a.FullName,FeatureCount=1,au.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                            , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber 
                            ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , TL.UpdatedOn as UpdatedOn , TL.Deleted 
                            FROM am.TenantUser a 
                            INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId 
                            INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  
                            INNER JOIN ap.Role r on r.ID = rl.RoleId 
                            INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  and UAL.Deleted=@deleted
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
                            WHERE UAL.Deleted = @deleted  and UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId order by a.FullName  ";
            }
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserDetailsDTO> appUserDTO = await GetQueryEntityListAsync<TenantUserDetailsDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, deletedParam });
            return appUserDTO;
        }

        #endregion Business


        ///<inheritdoc/>
        public async Task<List<BusCustomerUserDTO>> GetAllCustomerUserByBusinessPartnerId(Guid businessPartnerTenantId) {

            // In this squery we have to ignore customer setup app.
            string query = @"SELECT TU.ID as 'TenantUserId', tu.FirstName, TU.LastName, TU.Email ,UTL.IsPrimary,TUAL.JoinedDate,
                            (SELECT COUNT(1) FROM  am.TenantUserAppLinking TUL INNER JOIN am.App AP ON TUL.AppId = AP.ID and ap.AppSubscriptionMode = 2 WHERE TUL.TenantUserId = UTL.TenantUserId AND TUL.TenantId = UTL.TenantId AND TUL.BusinessPartnerTenantId = UTL.BusinessPartnerTenantId AND TUL.Deleted =0) AS 'ApplicationAccess'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking TUAL on TUAL.TenantUserId = TU.ID
                            INNER JOIN am.App  apnew on apnew.ID = tual.AppId and  TUaL.BusinessPartnerTenantId = @businessPartnerTenantId and apnew.AppKey= @appkey
                            INNER JOIN am.UserTenantLinking UTL ON UTL.TenantUserId = TU.ID AND UTL.BusinessPartnerTenantId= @businessPartnerTenantId AND UTL.Deleted=0
                            where Tu.Deleted =0";

              SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
            SqlParameter appKeyParam = new SqlParameter("@appkey", AppKeyEnum.custsetup.ToString());
            List<BusCustomerUserDTO> busCustomerUserDTOs = await GetQueryEntityListAsync<BusCustomerUserDTO>(query, new object[] { businessPartnerTenantIdParam, appKeyParam });
            return busCustomerUserDTOs;
        }
    ///<inheritdoc/>
    public async Task<List<BusVendorUserDTO>> GetAllVendorUserByBusinessPartnerId(Guid businessPartnerTenantId)
    {

      // In this squery we have to ignore customer setup app.
      string query = @"SELECT TU.ID as 'TenantUserId', tu.FirstName, TU.LastName, TU.Email ,UTL.IsPrimary,TUAL.JoinedDate,
                            (SELECT COUNT(1) FROM  am.TenantUserAppLinking TUL INNER JOIN am.App AP ON TUL.AppId = AP.ID and ap.AppSubscriptionMode = 2 WHERE TUL.TenantUserId = UTL.TenantUserId AND TUL.TenantId = UTL.TenantId AND TUL.BusinessPartnerTenantId = UTL.BusinessPartnerTenantId AND TUL.Deleted =0) AS 'ApplicationAccess'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking TUAL on TUAL.TenantUserId = TU.ID
                            INNER JOIN am.App  apnew on apnew.ID = tual.AppId and  TUaL.BusinessPartnerTenantId = @businessPartnerTenantId and apnew.AppKey= @appkey
                            INNER JOIN am.UserTenantLinking UTL ON UTL.TenantUserId = TU.ID AND UTL.BusinessPartnerTenantId= @businessPartnerTenantId AND UTL.Deleted=0
                            where Tu.Deleted =0";

      SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
      SqlParameter appKeyParam = new SqlParameter("@appkey", AppKeyEnum.vendsetup.ToString());
      List<BusVendorUserDTO> busCustomerUserDTOs = await GetQueryEntityListAsync<BusVendorUserDTO>(query, new object[] { businessPartnerTenantIdParam, appKeyParam });
      return busCustomerUserDTOs;
    }

    ///<inheritdoc/>
    public async Task<List<AppInfoDTO>> GetCustomerUserAsignedAppInfo(Guid tenantId,Guid businessPartnerTenantId, Guid tenantUserId) {

            // In this squery we have to ignore customer setup app.
            string query = @"SELECT ap.ID,ap.IdentityNumber, PAS.Name, AP.ThemeId, ap.Active, AppKey ,isnull (R.PermissionBitMask, 0)  as 'PermissionBitMask'
                            FROM am.TenantUserAppLinking TUL 
                            INNER JOIN am.App Ap ON TUL.AppId = ap.ID AND AP.AppSubscriptionMode = 2
                            LEFT JOIN ap.RoleLinking RL ON RL.TenantUserId = @tenantUserId AND RL.TenantId = @tenantId AND RL.AppId = Ap.ID 
                            LEFT JOIN ap.Role R ON R.ID = RL.RoleId
                            INNER JOIN am.TenantLinking TL ON TL.BusinessPartnerTenantId = @businessPartnerTenantId 
                            INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = TL.PublisherTenantId AND TUL.AppId = PAS.AppId
                            WHERE TUL.BusinessPartnerTenantId = @businessPartnerTenantId and TUL.tenantUserId = @tenantUserId and TUL.Deleted = 0";

            SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
      SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
      SqlParameter tenantUserIdParam = new SqlParameter("@tenantUserId", tenantUserId);
            List<AppInfoDTO> appInfo = await GetQueryEntityListAsync<AppInfoDTO>(query, new object[] { businessPartnerTenantIdParam, tenantIdParam, tenantUserIdParam });
            return appInfo;
        }

        ///<inheritdoc/>
        public async Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey) {

            string query = @" select Tu.Id as 'TenantUserId', TU.FirstName, TU.LastName, TU.Email, TU.IdentityNumber,
                             TU.CreatedOn , CTU.FullName as 'CreatedBy',TU.Phone , TL.JoinedDate   
                             from am.TenantUser TU
                             INNER JOIN am.App AP ON ap.AppKey = @appkey
                             INNER JOIN am.TenantUserAppLinking TL ON TL.TenantUserId = @tenantUserId AND TL.AppId = ap.ID ANd TL.TenantId = @tenantId
                             INNER JOIN am.TenantUser CTU ON CTU.ID = TU.CreatedBy
                             WHERE TU.ID = @tenantUserId";

            SqlParameter tenantUserIdParam = new SqlParameter("@tenantUserId", tenantUserId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter appKeyParam = new SqlParameter("@appkey", appKey);

            TenantUserAndAppViewDTO tenantUserInfo = await GetQueryEntityAsync<TenantUserAndAppViewDTO>(query, new object[] { tenantUserIdParam, tenantIdParam, appKeyParam });
            return tenantUserInfo;
        }

        ///<inheritdoc/>
        public async Task<List<AppShortInfoDTO>> GetApplicationForUserAsync(Guid tenantUserId, Guid tenantId) {

            string query = @" select App.AppKey as 'Appkey' , PAS.Name as 'AppName', App.ID as 'AppId'
                                From am.TenantUser TU
                                INNER JOIN am.TenantUserAppLinking TUL ON TUL.TenantUserId = TU.ID and TUL.TenantId = @tenantId and TUl.Deleted =0
                                INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = @tenantId and TL.BusinessPartnerTenantId is null
                                INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = TL.PublisherTenantId And PAS.AppId = TUL.AppId
                                INNER JOIN am.App App ON App.ID = PAS.AppId
                                where TU.ID = @tenantUserId order by PAS.Name";

            SqlParameter tenantUserIdParam = new SqlParameter("@tenantUserId", tenantUserId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);

            List<AppShortInfoDTO> appList = await GetQueryEntityListAsync<AppShortInfoDTO>(query, new object[] { tenantUserIdParam, tenantIdParam });
            return appList;
        }

        #endregion  User

        #region Other 

        /// <summary>
        /// Get filtered business list.
        /// </summary>
        /// <param name="filter">Filter the business list by filter object.</param>
        /// <param name="homeAppId">Home application id.</param>
        /// <param name="tenantId">tenantid</param>
        /// <param name="token"></param>
        /// <returns>return filtered business list.</returns>
        public async Task<List<BusinessViewModelDQ>> GetFilterBusinessListByTenantIdAsync(ListDateFilterDTO filter, Guid tenantId, Guid homeAppId, CancellationToken token = default(CancellationToken)) {
            SqlParameter start = new SqlParameter("@startDate", filter.FromDate);
            SqlParameter end = new SqlParameter("@endDate", filter.ToDate);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter homeAppIdParam = new SqlParameter("@homeAppId", homeAppId);
            SqlParameter deletedParam = new SqlParameter("@deleted", filter.Deleted);

            return await GetQueryEntityListAsync<BusinessViewModelDQ>("prcGetFilterBusinessList @startDate, @endDate, @tenantId, @homeAppId,@deleted", new SqlParameter[] { start, end, tenantIdParam, homeAppIdParam, deletedParam }, token);
        }


        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <returns>return list of application.</returns>
        public async Task<List<PubBusinessAppSubscriptionInfoDTO>> GetBusinessAppSubscriptionInfoDTOByBusinessIdAsync(Guid businessId, Guid homeAppId, Guid publisherTenantId, bool isdeleted, CancellationToken token = default(CancellationToken)) {
            // INNER JOIN UserTenantLinking ut on ut.TenantUserId = u.ID and ut.TenantId = @tenantId And ut.IsPrimary = 1 And ut.UserType = @uType     
            string sql;
            if(isdeleted) {
                sql = "SELECT DISTINCT a.Name, ts.Status, ual.JoinedDate as PrimaryUserActivateDate FROM AM.TenantSubscription ts  ";
                sql += " INNER JOIN AP.PublisherAppSetting a on a.AppID = ts.AppId AND a.TenantId = '{2}' And ts.TenantId= '{0}' AND ts.AppId <> '{1}' AND ts.Deleted=1 ";
                sql += " INNER JOIN AM.TenantUserAppLinking ual on ual.AppId = a.AppID And ual.TenantId= '{0}' AND ual.UserType = 3 ";
                sql += " INNER JOIN AM.UserTenantLinking utl on utl.IsPrimary = 1 AND utl.TenantId= ual.TenantId AND utl.TenantUserId = ual.TenantUserId ";
                sql += " INNER JOIN AM.TenantUser user1 on user1.ID = ual.TenantUserId  ";
            }
            else {
                sql = "SELECT DISTINCT a.Name, ts.Status, ual.JoinedDate as PrimaryUserActivateDate FROM AM.TenantSubscription ts  ";
                sql += " INNER JOIN AP.PublisherAppSetting a on a.AppID = ts.AppId AND a.TenantId = '{2}' And ts.TenantId= '{0}' And ts.Deleted = 0 AND ts.AppId <> '{1}' ";
                sql += " INNER JOIN AM.TenantUserAppLinking ual on ual.Deleted = 0  And ual.AppId = a.AppID And ual.TenantId= '{0}' AND ual.UserType = 3 ";
                sql += " INNER JOIN AM.UserTenantLinking utl on utl.IsPrimary = 1 AND utl.Deleted = 0 AND utl.TenantId= ual.TenantId AND utl.TenantUserId = ual.TenantUserId ";
                sql += " INNER JOIN AM.TenantUser user1 on user1.Deleted = 0  And user1.ID = ual.TenantUserId  ";
            }

            sql = string.Format(sql, businessId.ToString(), homeAppId.ToString(), publisherTenantId.ToString());
            return await GetQueryEntityListAsync<PubBusinessAppSubscriptionInfoDTO>(sql, null, token);
        }

        public async Task<BusConfigurationDTO> GetBusinessConfigurationDetailAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {

            string query = @"SELECT Tenant.ID AS TenantId, Tenant.Name, Tenant.SubDomainName, TenantUAL.InvitedOn,	
                             AppU.FullName AS InvitedBy, TenantUAL.JoinedDate, Tenant.Active,
	                         AU.ID AS AdminUserId, AU.FirstName AS AdminUserFirstName,AU.LastName AS AdminUserLastName,
                             AU.Email AS AdminUserEmail,
                             bus.ID AS BusinessId, bus.IdentityNumber, bus.ContactPersonName, bus.ContactPersonDesignation, bus.ContactPersonEmail, 
							 bus.ContactPersonPhone, bus.CurrencyCode, bus.GroupValue, bus.GroupSeperator, bus.DecimalSeperator,
							 bus.DecimalPrecision, bus.CanUpdateCurrency,bus.Website,bus.Language,bus.DateTimeFormat,bus.TimeZone,
                             bus.FederalTexId,bus.TelePhone1,bus.TelePhone2,bus.MobilePhone,bus.Email,bus.Status,bus.InitDB
			                 FROM am.Tenant
					         INNEr JOIN am.UserTenantLinking AS UserTLK ON UserTLK.TenantId = @TenantId AND UserTLK.IsPrimary= '1' 
                             INNER JOIN am.TenantUser AS AU ON AU.ID =  UserTLK.TenantUserId
					         INNER JOIN am.TenantUserAppLinking AS TenantUAL ON TenantUAL.TenantId =@TenantId AND TenantUAL.AppId = @AppId AND TenantUAL.BusinessPartnerTenantId is Null AND					TenantUAL.TenantUserId= AU.ID
                             INNER JOIN am.TenantUser AS AppU ON Tenant.CreatedBy = AppU.ID
					         INNER JOIN ap.Business bus ON bus.TenantId = Tenant.ID
					         WHERE Tenant.ID = @TenantId";

            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);
            return await GetQueryEntityAsync<BusConfigurationDTO>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
        }

        public List<string> GetBusinessNameListByPublisherId(Guid publisherId, BooleanFilterEnum businessActiveState) {
            string sql = @"SELECT T.Name AS 'BusinessName' FROM am.TenantLinking tl 
						    INNER JOIN am.Tenant T ON T.ID= TL.BusinessTenantId
						    INNER JOIN ap.Publisher pub on pub.TenantId = TL.PublisherTenantId 
                            WHERE pub.Id = @PublisherId AND tl.BusinessTenantId IS NOT NULL 
						    AND T.Deleted = 0 and t.TenantType = @TenantType ";

            if(businessActiveState != BooleanFilterEnum.All) {
                sql += " AND T.Active = @ActiveState";
            }

            sql += " Group By T.Id,T.Name ";
            SqlParameter publisherIdParam = new SqlParameter("PublisherId", publisherId);
            SqlParameter tenantTypeParam = new SqlParameter("TenantType", TenantType.Buisness);
            SqlParameter activeStateParam = new SqlParameter("ActiveState", (int)businessActiveState);
            return _context.PlatBusinessDTOQuery.FromSql(sql, new object[] { publisherIdParam, tenantTypeParam, activeStateParam }).Select(i => i.BusinessName).ToList();
        }

        #endregion Other 

        #region Notification PreData

        public async Task<BusinessOnBoardNotificationDTO> GetBusinessOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid userId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT tu.FullName AS 'UserName', pas.Name AS 'AppName',  a.AppKey, tual.JoinedDate, 
                            b.TimeZone, p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId', 
                            b.Name AS 'BusinessName',p.Copyright, tu.Id AS 'OnBoardedUserId', a.Id AS 'AppId',  te.SubDomainName AS SubDomain,
                            b.DateTimeFormat
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId
                            INNER JOIN am.Tenant as te ON b.TenantId = te.ID
                            Where tl.BusinessTenantId=@BusinessTenantId AND tl.BusinessPartnerTenantId IS NULL 
                            AND a.AppKey=@AppKey AND tual.TenantUserId=@UserId AND tual.Deleted=0";
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);
            SqlParameter userIdParam = new SqlParameter("UserId", userId);

            return await _context.BusinessOnBoardNotificationDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, appKeyParam, userIdParam }).FirstAsync(cancellationToken);
        }



        public async Task<BusinessOnBoardNotificationDTO> GetBusinessSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid userId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT DISTINCT tl.BusinessPartnerTenantId, tu.FullName AS 'UserName', a.Name AS 'AppName',  a.AppKey, tual.JoinedDate, 
                            b.TimeZone, p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId', 
                            b.Name AS 'BusinessName',p.Copyright, tu.Id AS 'OnBoardedUserId', a.Id AS 'AppId',  te.SubDomainName AS SubDomain,
                            b.DateTimeFormat    
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId                           
                            INNER JOIN am.App as a ON a.AppKey='biz'
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId
                            INNER JOIN am.Tenant as te ON b.TenantId = te.ID
                            Where tl.BusinessTenantId=@BusinessTenantId AND tl.BusinessPartnerTenantId IS NULL 
                            AND a.AppKey=@AppKey AND tual.TenantUserId=@UserId";
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);
            SqlParameter userIdParam = new SqlParameter("UserId", userId);

            return await _context.BusinessOnBoardNotificationDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, appKeyParam, userIdParam }).FirstAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<BusinessUserNotificationGeneralDTO> GetBusinessUserNotificationGeneralDataAsync(Guid tenantUserId, Guid tenantId, string appKey) {

            //string sql = @"SELECT PUB.Name as 'PublisherName', PAS.Name as 'ApplicationName', TN.SubDomainName as 'SubDomain',
            //                TN.Name as 'BusinessCompanyName',TU.FullName as 'InvitedUserFullName' , PUB.Copyright as 'CopyRightText' ,
            //                TN.ID as 'TenantId', TU.ID as 'InvitedUserTenantId' , Ap.ID as 'InvitedUserAppId', ap.AppKey as 'InvitedUserAppKey'
            //                FROM am.TenantUser TU
            //                INNER JOIN am.Tenant TN ON TN.ID = @TenantId
            //                INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = TN.ID AND TL.BusinessPartnerTenantId is null
            //                INNER JOIN ap.Publisher PUB ON PUB.TenantId = TL.PublisherTenantId
            //                INNER JOIN am.App AP ON AP.AppKey = @AppKey
            //                INNER JOIN ap.PublisherAppSetting PAS ON PAS.AppId = AP.ID AND PAS.TenantId = TL.PublisherTenantId
            //                WHERE TU.ID = @TenantUserId";

            string sql = @" SELECT DISTINCT PUB.Name as 'PublisherName', TN.SubDomainName as 'SubDomain',b.DateTimeFormat,b.TimeZone,
                            TN.Name as 'BusinessCompanyName',TU.FullName as 'InvitedUserFullName' , PUB.Copyright as 'CopyRightText' ,
                            TN.ID as 'TenantId', TU.ID as 'InvitedUserId' 
                            {0}
                            FROM am.TenantUser TU
                            INNER JOIN am.Tenant TN ON TN.ID = @TenantId
                            INNER JOIN ap.Business as b on b.TenantId=TN.ID
                            INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = TN.ID AND TL.BusinessPartnerTenantId is null
                            INNER JOIN ap.Publisher PUB ON PUB.TenantId = TL.PublisherTenantId
                            INNER JOIN ap.PublisherAppSetting PAS ON PAS.TenantId = TL.PublisherTenantId and pas.Deleted=0
                            {1}
                            WHERE TU.ID = @TenantUserId ";

            string appSelectColumns = @" , PAS.Name as 'ApplicationName',  Ap.ID as 'InvitedUserAppId', ap.AppKey as 'InvitedUserAppKey' ";
            string emptyAppSelectColumns = string.Format(" , '' as 'ApplicationName',  NULL as 'InvitedUserAppId', '' as 'InvitedUserAppKey' ", Guid.Empty.ToString());
            string appInnerJoin = @" INNER JOIN am.App AP ON AP.ID = pas.AppId And ap.AppKey = @AppKey ";

            if(string.IsNullOrEmpty(appKey)) {
                sql = string.Format(sql, emptyAppSelectColumns, "");
            }
            else {
                sql = string.Format(sql, appSelectColumns, appInnerJoin);
            }
            SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
            SqlParameter tenantUserIdParam = new SqlParameter("TenantUserId", tenantUserId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _context.BusinessUserNotificationGeneralDTOQuery.FromSql(sql, new object[] { tenantIdParam, tenantUserIdParam, appKeyParam }).FirstOrDefaultAsync();
        }


        ///<inheritdoc/>
        public async Task<BusinessUserNotificationAppAccessUpdateDTO> GetBusinessUserNotificationGenDataAsync(Guid tenantUserId, Guid tenantId, Guid userId) {

            string sql = @"SELECT PUB.Name as 'PublisherName', 'pay' as 'ApplicationName', TN.SubDomainName as 'SubDomain',b.DateTimeFormat,b.TimeZone,
                            TN.Name as 'BusinessCompanyName',TU.FullName as 'InvitedUserFullName' , PUB.Copyright as 'CopyRightText' ,
                            TN.ID as 'TenantId', TU.ID as 'InvitedUserId' , TU.ID as 'InvitedUserAppId', 'pay' as 'InvitedUserAppKey',tus.FullName as UpdatedBy
                            FROM am.TenantUser TU
                            INNER JOIN am.Tenant TN ON TN.ID = @TenantId
							INNER JOIN am.TenantUser tus ON tus.ID = @UserId
                            INNER JOIN ap.Business as b on b.TenantId=TN.ID
                            INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = TN.ID AND TL.BusinessPartnerTenantId is null
                            INNER JOIN ap.Publisher PUB ON PUB.TenantId = TL.PublisherTenantId							
                            WHERE TU.ID = @TenantUserId ";


        
            SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
            SqlParameter tenantUserIdParam = new SqlParameter("TenantUserId", tenantUserId);
            SqlParameter userIdParam = new SqlParameter("UserId", userId);

            return await _context.BusinessUserNotificationAppAccessUpdateDTOQuery.FromSql(sql, new object[] { tenantIdParam, tenantUserIdParam, userIdParam }).FirstOrDefaultAsync();
        }

        ///<inheritdoc/>
        public async Task<BusinessUserPermissionNotificationGeneralDTO> GetBusinessUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId,Guid invitedTenantUserId, Guid tenantId, string appKey) {

            string sql = @"SELECT Distinct PUB.Name as 'PublisherName', {0}, TN.SubDomainName as 'SubDomain',b.DateTimeFormat,b.TimeZone,
                            TN.Name as 'BusinessCompanyName',TU.FullName as 'InvitedUserFullName' , PUB.Copyright as 'CopyRightText' ,
                            TN.ID as 'TenantId', TU.ID as 'InvitedUserId' , Ap.ID as 'InvitedUserAppId', ap.AppKey as 'InvitedUserAppKey',CTU.FullName AS 'UserFullName'
                            FROM am.TenantUser TU
							INNER JOIN am.TenantUser CTU ON CTU.ID = @TenantUserId
                            INNER JOIN am.Tenant TN ON TN.ID = @TenantId
                            INNER JOIN ap.Business as b on b.TenantId=TN.ID
                            INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = TN.ID AND TL.BusinessPartnerTenantId is null
                            INNER JOIN ap.Publisher PUB ON PUB.TenantId = TL.PublisherTenantId
                            INNER JOIN am.App AP ON AP.AppKey = @AppKey
                            {1}
                            WHERE TU.ID = @InvitedTenantUserId";
            string pubappName = @"PAS.Name as 'ApplicationName'";
            string bizappName = @"AP.Name as 'ApplicationName'";
            string pubappInnerJoin = @"INNER JOIN ap.PublisherAppSetting PAS ON PAS.AppId = AP.ID AND PAS.TenantId = TL.PublisherTenantId";
            string bizappJoin = @"";
            if(AppKeyEnum.biz.ToString() == appKey) {
                sql = string.Format(sql, bizappName, bizappJoin);
            }
            else {
                sql = string.Format(sql, pubappName, pubappInnerJoin);
            }
            SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
            SqlParameter invitedtenantUserIdParam = new SqlParameter("InvitedTenantUserId", invitedTenantUserId);
            SqlParameter tenantUserIdParam = new SqlParameter("TenantUserId", tenantUserId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _context.BusinessUserPermissionNotificationGeneralDTOQuery.FromSql(sql, new object[] { tenantIdParam, tenantUserIdParam, appKeyParam, invitedtenantUserIdParam }).FirstOrDefaultAsync();
        }

    #endregion Notification PreData

    ///<inheritdoc/>
    public async Task<List<RoleKeyCountDTO>> CheckUserIsLastBusinessAdminUserAsync(Guid tenantId, int userType, Guid appId) {

      string query = @" SELECT RoleKey , RL.TenantUserId 
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

    #endregion Public Methods

  }
}

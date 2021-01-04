using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QCustomerAndUserRepository:QBaseRepository<QAppPortalDbContext>, IQCustomerAndUserRepository {

        public QCustomerAndUserRepository(QAppPortalDbContext context) : base(context) {
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserSetupListDTO>> GetAllCustomerSetupUsersAsync(int userType, Guid tenantId, Guid businessPartnerTenantId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT distinct TU.ID as 'TenantUserId', TU.FullName as 'FullName' , TU.IdentityNumber as 'IdentityNumber',
                        TU.Email as 'Email' , TL.Deleted as 'Deleted' , (select COUNT(1) from am.TenantUserAppLinking TUL INNER JOIN am.App App on App.ID = TUL.AppId And App.AppSubscriptionMode =2 
                        where TUL.TenantUserId = TU.ID AND TUL.TenantId = @tenantId And TUL.BusinessPartnerTenantId = @businessPartnerTenantId and TUL.Deleted <> @deleted) as 'ApplicationCount'  , TL.UpdatedOn as 'DeletedOn' , DELTU.FullName as 'DeletedByName'
                        FROM am.TenantUser TU
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= TU.ID and TL.TenantId= @tenantId AND TL.Deleted=@deleted And TL.BusinessPartnerTenantId = @businessPartnerTenantId
                        INNER JOIN am.TenantUser DELTU on DELTU.ID = TL.UpdatedBy 
                        WHERE TL.UserType = @UserType  order by TU.FullName ";
            }
            else {
                query = @"SELECT distinct TU.ID as 'TenantUserId', TU.FullName as 'FullName' , TU.IdentityNumber as 'IdentityNumber',
                        TU.Email as 'Email' , TL.Deleted as 'Deleted' , (select COUNT(1) from am.TenantUserAppLinking TUL INNER JOIN am.App App on App.ID = TUL.AppId And App.AppSubscriptionMode =2 
                        where TUL.TenantUserId = TU.ID AND TUL.TenantId = @tenantId And TUL.BusinessPartnerTenantId = @businessPartnerTenantId and TUL.Deleted =@deleted) as 'ApplicationCount', TL.UpdatedOn as 'DeletedOn' , '' as 'DeletedByName'
                        FROM am.TenantUser TU
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= TU.ID and TL.TenantId= @tenantId AND TL.Deleted=@deleted And TL.BusinessPartnerTenantId = @businessPartnerTenantId
                        WHERE TU.Deleted = @deleted and TL.UserType = @UserType  order by TU.FullName ";
            }
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter businessPartnertenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserSetupListDTO> tenantUserSetupListDTO = await GetQueryEntityListAsync<TenantUserSetupListDTO>(query, new object[] { userTypeParam, tenantIdParam, deletedParam, businessPartnertenantIdParam });
            return tenantUserSetupListDTO;
        }

        ///<inheritdoc/>
        public async Task<List<AppShortInfoDTO>> GetApplicationForCustomerUserAsync(Guid tenantUserId, Guid tenantId) {

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

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetPaymentAppCustomerUser(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT Distinct a.ID, a.FullName,FeatureCount=1,au.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                        , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber 
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary as Admin, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , TL.UpdatedOn as UpdatedOn , TL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId  ANd TL.BusinessPartnerTenantId =@businessPartnerTenantId
                        INNER JOIN pay.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  
                        INNER JOIN pay.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  and UAL.Deleted = @deleted
                        INNER JOIN am.TenantUser au on UAL.InvitedBy = au.ID 
                        INNER JOIN am.TenantUser upd on upd.ID = TL.UpdatedBy
                        WHERE  UAL.UserType = @UserType  and UAL.AppId=@AppId  order by a.FullName ";
            }
            else {
                query = @"SELECT distinct a.ID, a.FullName,FeatureCount=1,inv.FullName as InvitedBy,UAL.joineddate as JoinedDate,Case when r.RoleKey='Admin' then CONVERT(bit,1) else CONVERT(bit,0) end as Admin 
                        , a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary as Admin, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , UAL.UpdatedOn as UpdatedOn , UAL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId AND TL.Deleted=@deleted ANd TL.BusinessPartnerTenantId =@businessPartnerTenantId
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
            SqlParameter businessPartnertenantIdParam = new SqlParameter("@businessPartnertenantId", businessPartnerTenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserDetailsDTO> appUserDTO = await GetQueryEntityListAsync<TenantUserDetailsDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, deletedParam, businessPartnertenantIdParam });
            return appUserDTO;
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetCustomerAppCustomerUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted) {
            string query = string.Empty;
            if(deleted) {
                query = @"SELECT Distinct a.ID, a.FullName,FeatureCount=1,au.FullName as InvitedBy,UAL.joineddate as JoinedDate, a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber 
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary as Admin, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , TL.UpdatedOn as UpdatedOn , TL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId  ANd TL.BusinessPartnerTenantId =@businessPartnerTenantId
                        INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  
                        INNER JOIN ap.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  and UAL.Deleted = @deleted
                        INNER JOIN am.TenantUser au on UAL.InvitedBy = au.ID 
                        INNER JOIN am.TenantUser upd on upd.ID = TL.UpdatedBy
                        WHERE  UAL.UserType = @UserType  and UAL.AppId=@AppId  order by a.FullName ";
            }
            else {
                query = @"SELECT distinct a.ID, a.FullName,FeatureCount=1,inv.FullName as InvitedBy,UAL.joineddate as JoinedDate, a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary as Admin, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , UAL.UpdatedOn as UpdatedOn , UAL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and TL.TenantId= @tenantId AND TL.Deleted=@deleted ANd TL.BusinessPartnerTenantId =@businessPartnerTenantId
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
            SqlParameter businessPartnertenantIdParam = new SqlParameter("@businessPartnertenantId", businessPartnerTenantId);
            SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

            List<TenantUserDetailsDTO> appUserDTO = await GetQueryEntityListAsync<TenantUserDetailsDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, deletedParam, businessPartnertenantIdParam });
            return appUserDTO;
        }

        ///<inheritdoc/>
        public async Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId,Guid businessPartnerTenantId, string appKey) {

            string query = @" select Tu.Id as 'TenantUserId', TU.FirstName, TU.LastName, TU.Email, TU.IdentityNumber,
                             TU.CreatedOn , CTU.FullName as 'CreatedBy',TU.Phone , TL.JoinedDate   
                             from am.TenantUser TU
                             INNER JOIN am.App AP ON ap.AppKey = @appkey
                             INNER JOIN am.TenantUserAppLinking TL ON TL.TenantUserId = @tenantUserId AND TL.AppId = ap.ID ANd TL.TenantId = @tenantId AND TL.businessPartnerTenantId = @businessPartnerTenantId
                             INNER JOIN am.TenantUser CTU ON CTU.ID = TU.CreatedBy
                             WHERE TU.ID = @tenantUserId";

            SqlParameter tenantUserIdParam = new SqlParameter("@tenantUserId", tenantUserId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter businessPartnerTenantIdParam = new SqlParameter("@businessPartnerTenantId", businessPartnerTenantId);
            SqlParameter appKeyParam = new SqlParameter("@appkey", appKey);

            TenantUserAndAppViewDTO tenantUserInfo = await GetQueryEntityAsync<TenantUserAndAppViewDTO>(query, new object[] { tenantUserIdParam, tenantIdParam, businessPartnerTenantIdParam, appKeyParam });
            return tenantUserInfo;
        }
    ///<inheritdoc/>
    public async Task<TenantUserDetailsDTO> GetAdminCustomerUsersDetailsAsync(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted)
    {
     // string query = string.Empty;

      string query = @"SELECT distinct a.ID, a.FullName,FeatureCount=1,inv.FullName as InvitedBy,UAL.joineddate as JoinedDate,
                         a.FirstName,a.LastName,UAL.InvitedOn as InvitedOn, a.email ,a.IdentityNumber
                        ,a.Phone, ual.Active , r.PermissionBitMask, r.ID as RoleId , tl.IsPrimary as Admin, UAL.BusinessPartnerTenantId , upd.FullName as UpdatedByName , UAL.UpdatedOn as UpdatedOn , UAL.Deleted 
                        FROM am.TenantUser a 
                        INNER JOIN am.UserTenantLinking TL on TL.TenantUserId= a.ID and tl.IsPrimary = 1 and TL.TenantId= @tenantId AND TL.Deleted=@deleted ANd TL.BusinessPartnerTenantId =@businessPartnerTenantId
                        INNER JOIN ap.RoleLinking rl on rl.TenantUserId = a.ID AND rl.AppId = @AppId AND rl.tenantId= @tenantId  AND rl.Deleted=@deleted
                        INNER JOIN ap.Role r on r.ID = rl.RoleId 
                        INNER JOIN am.TenantUserAppLinking UAL on a.ID =UAL.TenantUserId and UAL.AppId= @AppId and UAL.tenantId= @tenantId  AND UAL.Deleted=@deleted
                        INNER JOIN am.TenantUser upd on upd.ID = UAL.UpdatedBy 
                        INNER JOIN am.TenantUser inv on inv.ID = UAL.InvitedBy 
                        WHERE UAL.Deleted = @deleted  and tl.IsPrimary = 1 and UAL.UserType = @UserType  and UAL.AppId=@AppId  and TL.tenantId= @tenantId ";
      
      SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
      SqlParameter appIdparam = new SqlParameter("@AppId", appId);
      SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
      SqlParameter businessPartnertenantIdParam = new SqlParameter("@businessPartnertenantId", businessPartnerTenantId);
      SqlParameter deletedParam = new SqlParameter("@deleted", deleted);

      TenantUserDetailsDTO appUserDTO = await GetQueryEntityAsync<TenantUserDetailsDTO>(query, new object[] { userTypeParam, appIdparam, tenantIdParam, deletedParam, businessPartnertenantIdParam });
      return appUserDTO;
    }

    #region Notification PreData

    ///<inheritdoc/>
    public async Task<CustomerOnBoardNotificationDTO> GetCustomerOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId) {

            string sql = @"SELECT DISTINCT tu.FullName AS 'UserName', pas.Name AS 'AppName',  a.AppKey , a.Id AS 'AppId', tual.JoinedDate, 
                            apcust.TimeZone, p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId', tl.BusinessPartnerTenantId as 'BusinessPartnerTenantId', becust.CustomerName as 'CustomerName',
                            b.Name AS 'BusinessName',p.Copyright,TN.SubDomainName as 'SubDomain' , ' ' as 'PortalURL', tu.Id AS 'OnBoardedUserId',
                            apcust.DateTimeFormat
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId
                            INNER JOIN am.App as a ON pas.AppId=a.ID
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId
                            INNER JOIN ap.Customer as apcust ON apcust.BusinessPartnerTenantId = @BusinessPartnerTenantId
                            INNER JOIN be.BACustomer as becust ON becust.BusinessPartnerTenantId = @BusinessPartnerTenantId
                            INNER JOIN am.Tenant TN ON TN.ID = @BusinessTenantId
                            Where tl.BusinessTenantId=@BusinessTenantId AND tl.BusinessPartnerTenantId =@BusinessPartnerTenantId
                            AND a.AppKey=@AppKey AND tual.TenantUserId=@UserId And tual.Deleted =0";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter businessPartnerTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);
            SqlParameter userIdParam = new SqlParameter("UserId", userId);

            return await _context.CustomerOnBoardNotificationDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, businessPartnerTenantIdParam, appKeyParam, userIdParam }).FirstOrDefaultAsync();
        }


       
        ///<inheritdoc/>   
        public async Task<CustomerOnBoardNotificationDTO> GetCustomerSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId) {

            string sql = @"SELECT DISTINCT tu.FullName AS 'UserName', a.Name AS 'AppName',  a.AppKey , a.Id AS 'AppId', tual.JoinedDate, 
                           apcust.TimeZone, p.Name AS 'PublisherName', b.TenantId AS 'BusinessTenantId', tl.BusinessPartnerTenantId as 'BusinessPartnerTenantId', becust.CustomerName as 'CustomerName',
                           b.Name AS 'BusinessName',p.Copyright,TN.SubDomainName as 'SubDomain' , ' ' as 'PortalURL', tu.Id AS 'OnBoardedUserId',
                           apcust.DateTimeFormat
                           FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
                            INNER JOIN am.App as a ON a.AppKey='custsetup'
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId
                            INNER JOIN ap.Customer as apcust ON apcust.BusinessPartnerTenantId = @BusinessPartnerTenantId
                            INNER JOIN be.BACustomer as becust ON becust.BusinessPartnerTenantId = @BusinessPartnerTenantId
                            INNER JOIN am.Tenant TN ON TN.ID = @BusinessTenantId
                            Where tl.BusinessTenantId=@BusinessTenantId AND tl.BusinessPartnerTenantId =@BusinessPartnerTenantId
                            AND a.AppKey=@AppKey AND tual.TenantUserId=@UserId";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter businessPartnerTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);
            SqlParameter userIdParam = new SqlParameter("UserId", userId);

            return await _context.CustomerOnBoardNotificationDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, businessPartnerTenantIdParam, appKeyParam, userIdParam }).FirstOrDefaultAsync();
        }

        

        ///<inheritdoc/>
        public async Task<CustomerUserNotificationGeneralDTO> GetCustomerUserNotificationGeneralDataAsync(Guid tenantUserId,Guid businessPartnerTenantId, Guid tenantId, string appKey) {

            string sql = @"SELECT DISTINCT p.Name as 'PublisherName',PAS.Name as 'ApplicationName', TN.SubDomainName as 'SubDomain',
                            TN.Name as 'BusinessCompanyName',TU.FullName as 'InvitedUserFullName' , p.Copyright as 'CopyRightText' ,
                            TN.ID as 'TenantId', TU.ID as 'InvitedUserId' , a.ID as 'InvitedUserAppId', a.AppKey as 'InvitedUserAppKey'
                            FROM ap.Publisher AS p
                            INNER JOIN am.TenantLinking AS tl ON p.TenantId=tl.PublisherTenantId
                            INNER JOIN ap.Business as b ON b.TenantId=tl.BusinessTenantId
                            INNER JOIN ap.PublisherAppSetting AS pas ON p.TenantId=pas.TenantId AND pas.Deleted = 0
                            INNER JOIN am.App as a ON a.AppKey=@AppKey
                            INNER JOIN am.TenantUserAppLinking as tual ON tual.AppId=a.ID
                            INNER JOIN am.TenantUser as tu ON tu.ID=tual.TenantUserId
                            INNER JOIN ap.Customer as apcust ON apcust.BusinessPartnerTenantId = @BusinessPartnerTenantId
                            INNER JOIN be.BACustomer as becust ON becust.BusinessPartnerTenantId = @BusinessPartnerTenantId
                            INNER JOIN am.Tenant TN ON TN.ID = @BusinessTenantId
                            Where tl.BusinessTenantId=@BusinessTenantId AND tl.BusinessPartnerTenantId =@BusinessPartnerTenantId
                            AND tual.TenantUserId=@UserId";
            

            SqlParameter tenantIdParam = new SqlParameter("BusinessTenantId", tenantId);
            SqlParameter businessPartnertenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter tenantUserIdParam = new SqlParameter("UserId", tenantUserId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _context.CustomerUserNotificationGeneralDTOQuery.FromSql(sql, new object[] { tenantIdParam, businessPartnertenantIdParam, tenantUserIdParam, appKeyParam }).FirstOrDefaultAsync();
        }

        ///<inheritdoc/>
        public async Task<CustomerUserPermissionChangeNotificationGeneralDTO> GetCustomerUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId, string appKey) {

            string sql = @"SELECT Distinct PUB.Name as 'PublisherName',{0} , TN.SubDomainName as 'SubDomain',
                            TN.Name as 'BusinessCompanyName',TU.FullName as 'InvitedUserFullName' , PUB.Copyright as 'CopyRightText' ,
                            TN.ID as 'TenantId', TU.ID as 'InvitedUserId' , Ap.ID as 'InvitedUserAppId', ap.AppKey as 'InvitedUserAppKey',CTU.FullName AS 'UserFullName'
                            FROM am.TenantUser TU
							INNER JOIN am.TenantUser CTU ON CTU.ID = @TenantUserId
                            INNER JOIN am.Tenant TN ON TN.ID = @TenantId
                            INNER JOIN am.TenantLinking TL ON TL.BusinessTenantId = TN.ID AND TL.BusinessPartnerTenantId is null
                            INNER JOIN ap.Publisher PUB ON PUB.TenantId = TL.PublisherTenantId
                            INNER JOIN am.App AP ON AP.AppKey = @AppKey
                            {1}
                            WHERE TU.ID = @InvitedTenantUserId";

            string selectApp = @"PAS.Name as 'ApplicationName'";
            string custSetupApp = @"AP.Name as 'ApplicationName'";
            string appInnerJoin = @"INNER JOIN ap.PublisherAppSetting PAS ON PAS.AppId = AP.ID AND PAS.TenantId = TL.PublisherTenantId";
            string custSetupInnerJoin = @"";

            if(AppKeyEnum.custsetup.ToString() == appKey) {
                sql = string.Format(sql, custSetupApp, custSetupInnerJoin);
            }
            else {
                sql = string.Format(sql, selectApp, appInnerJoin);
            }


            SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
            SqlParameter invitedtenantUserIdParam = new SqlParameter("InvitedTenantUserId", invitedTenantUserId);
            SqlParameter tenantUserIdParam = new SqlParameter("TenantUserId", tenantUserId);
            SqlParameter appKeyParam = new SqlParameter("AppKey", appKey);

            return await _context.CustomerUserPermissionChangeNotificationGeneralDTOQuery.FromSql(sql, new object[] { tenantIdParam, tenantUserIdParam, appKeyParam, invitedtenantUserIdParam }).FirstOrDefaultAsync();
        }

    #endregion Notification PreData

    ///<inheritdoc/>
    public async Task<List<RoleKeyCountDTO>> CheckUserIsLastCustomerAdminUserAsync(Guid tenantId, int userType, Guid appId) {

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

  }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QBizNotificationRecipientData:IQBizNotificationRecipientData {

        #region Local Variable

        QAppPortalDbContext _qAppPortalDbContext;

        #endregion Local Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QBizNotificationRecipientData"/> class.
        /// </summary>
        /// <param name="qAppPortalDbContext">The q application portal database context.</param>
        public QBizNotificationRecipientData(QAppPortalDbContext qAppPortalDbContext) {
            _qAppPortalDbContext = qAppPortalDbContext;
        }

        #endregion Constructor

        // TODO: Prefrencess.
        public List<NotificationRecipient> GetBizUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId, long preferenceValue) {

            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'ASPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType,  TU.Phone AS 'SMSRecipient', te.SubDomainName AS SubDomain  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId
                            INNER JOIN am.Tenant as te ON b.TenantId = te.ID
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                            AND tual.UserType=@UserType AND tu.Id<>@OnBoardedUser AND tual.Status=@ActiveUserStatus
                            AND (
                            (ap.EmailPreference & @PreferenceMask)=@PreferenceMask OR
                            (ap.SMSPreference & @PreferenceMask)=@PreferenceMask OR
                            (ap.ASPreference & @PreferenceMask)=@PreferenceMask 
                            )";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Business);
            SqlParameter onBoardedUserParam = new SqlParameter("OnBoardedUser", onboardedUserId);
            SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter preferenceValueParam = new SqlParameter("PreferenceMask", preferenceValue);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, onBoardedUserParam, userStatusParam, preferenceValueParam }).ToList();
        }

        public List<NotificationRecipient> GetBizPaymentUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId) {

            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST(ap.EmailPreference AS bit) As 'EmailPreference', CAST('True' AS bit) As 'ASPreference', CAST('True' AS bit) As 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType,  TU.Phone AS 'SMSRecipient',  te.SubDomainName AS SubDomain  
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId
                            INNER JOIN am.Tenant as te ON b.TenantId = te.ID
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                            AND tual.UserType=@UserType AND tu.Id<>@OnBoardedUser  AND tual.Status=@ActiveUserStatus";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Business);
            SqlParameter onBoardedUserParam = new SqlParameter("OnBoardedUser", onboardedUserId);
            SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, onBoardedUserParam, userStatusParam }).ToList();
        }

        /// <summary>
        /// Get Business portal customer app useronBoard 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="businessTenantId"></param>
        /// <param name="onboardedUserId"></param>
        /// <returns></returns>
        public List<NotificationRecipient> GetBizCustUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId, long preferenceValue) {

            //CAST(ap.EmailPreference AS bit) As 'EmailPreference',
            //                CAST('True' AS bit) As 'ASPreference', 
            //                CAST('True' AS bit) As 'SMSPreference',

            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, 
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'ASPreference',                        

                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType,  TU.Phone AS 'SMSRecipient',  te.SubDomainName AS SubDomain
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId
                            INNER JOIN am.Tenant as te ON b.TenantId = te.ID
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                            AND tual.UserType=@UserType AND tu.Id<>@OnBoardedUser  AND tual.Status=@ActiveUserStatus
                            AND (
                            (ap.EmailPreference & @PreferenceMask)=@PreferenceMask OR
                            (ap.SMSPreference & @PreferenceMask)=@PreferenceMask OR
                            (ap.ASPreference & @PreferenceMask)=@PreferenceMask 
                            )";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Business);
            SqlParameter onBoardedUserParam = new SqlParameter("OnBoardedUser", onboardedUserId);
            SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter preferenceValueParam = new SqlParameter("PreferenceMask", preferenceValue);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, onBoardedUserParam, userStatusParam, preferenceValueParam }).ToList();
        }

        public List<NotificationRecipient> GetBizSetupUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId) {

            string sql = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST(ap.EmailPreference AS bit) As 'EmailPreference', CAST('True' AS bit) As 'ASPreference', CAST('True' AS bit) As 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId , Tual.UserType,  TU.Phone AS 'SMSRecipient',  te.SubDomainName AS SubDomain
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId
                            INNER JOIN am.Tenant as te ON b.TenantId = te.ID
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId 
                            AND tual.UserType=@UserType AND tu.Id<>@OnBoardedUser  AND tual.Status=@ActiveUserStatus";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter userTypeParam = new SqlParameter("UserType", (int)UserTypeEnum.Business);
            SqlParameter onBoardedUserParam = new SqlParameter("OnBoardedUser", onboardedUserId);
            SqlParameter userStatusParam = new SqlParameter("ActiveUserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(sql, new object[] { appIdParam, businessTenantIdParam, userTypeParam, onBoardedUserParam, userStatusParam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAppUserOnBusiness(Guid tenantId, Guid tenantUserId, Guid appId) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                        BIZ.Language As 'RegionLanguage', UL.AppId AS 'ApplicationId', T.TenantId, T.UserType
                        , Cast('False' AS Bit) AS 'SMSPreference' , Cast('False' AS Bit) AS 'ASPreference', tu.Phone AS 'SMSRecipient'
                        FROM am.TenantUser TU
                        INNER JOIN am.UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId
                        INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId AND UL.Deleted= 1 
                        INNER JOIN ap.Business BIZ ON BIZ.TenantId = T.TenantId 
                        WHERE  TU.Id = @TenantUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAppUserAccessUpdateOnBusiness(Guid tenantId, Guid tenantUserId) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                        BIZ.Language As 'RegionLanguage', TU.Id AS 'ApplicationId', T.TenantId, T.UserType
                        , Cast('False' AS Bit) AS 'SMSPreference' , Cast('False' AS Bit) AS 'ASPreference', tu.Phone AS 'SMSRecipient'
                        FROM am.TenantUser TU
                        INNER JOIN am.UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId                        
                        INNER JOIN ap.Business BIZ ON BIZ.TenantId = T.TenantId 
                        WHERE  TU.Id = @TenantUserId AND t.TenantId = @TenantId";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@TenantUserId", tenantUserId);           
            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantParam, appuserParam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAppUserPermissionOnBusiness(Guid tenantId, Guid tenantUserId, Guid appId, long bizPreferrenceValue, int userType, int userStatus) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            {1}
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            {0}                          
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@TenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@TenantId
                            AND TU.ID = @TenantUserId";            
            string custappInnerJoin = @"INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId ";
            string payappInnerJoin = @"INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId ";
            //string setupColumn = @"CAST('True' AS bit) As 'EmailPreference', 
            //            Cast('False' AS Bit) AS 'SMSPreference' , Cast('True' AS Bit) AS 'ASPreference',";
            string appColumn = @"CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',";

            if((int)BusinessUserPaymentAppPreferenceEnum.MyPermissionUpdated == bizPreferrenceValue) {
                query = string.Format(query, payappInnerJoin, appColumn);
            }
            else if((int)BusinessUserCustomerAppPreferenceEnum.MyPermissionUpdated == bizPreferrenceValue) {
                query = string.Format(query, custappInnerJoin, appColumn);
            }
            else if((int)BusinessUserBusinessSetupAppPreferenceEnum.MyPermissionUpdated == bizPreferrenceValue) {
                query = string.Format(query, custappInnerJoin, appColumn);
            }

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter preferrenceParam = new SqlParameter("@PreferenceMask", bizPreferrenceValue);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);
            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantParam, appuserParam, appParam, preferrenceParam, userTypeparam, userStatusparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN pay.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN pay.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId AND TU.ID <> @UserId
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@UserId", tenantUserId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", preferenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferenceMaskparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId AND TU.ID <> @UserId 
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@UserId", tenantUserId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", preferenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferenceMaskparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerUsersCustAppForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long permissionMask, long preferrenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
							LEFT JOIN BE.BACustomer c ON c.BusinessPartnerTenantId = tual.BusinessPartnerTenantId
							INNER JOIN BE.BAARInvoice i ON i.CustomerId = c.ID
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.TenantId=@BusinessTenantId AND tual.AppId=@AppId 
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId  AND i.ID = @EntityId
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@EntityId", entityId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferrenceMaskparam = new SqlParameter("@PreferenceMask", preferrenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferrenceMaskparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerUsersCustAppSalesQuotationForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
							LEFT JOIN BE.BACustomer c ON c.BusinessPartnerTenantId = tual.BusinessPartnerTenantId
							INNER JOIN BE.BASalesQuotation sq ON sq.CustomerId = c.ID
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.TenantId=@BusinessTenantId AND tual.AppId=@AppId 
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId  AND sq.ID = @EntityId
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@EntityId", entityId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", preferenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferenceMaskparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerUsersCustAppSalesOrderForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
							LEFT JOIN BE.BACustomer c ON c.BusinessPartnerTenantId = tual.BusinessPartnerTenantId
							INNER JOIN BE.BASalesOrder so ON so.CustomerId = c.ID
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.TenantId=@BusinessTenantId AND tual.AppId=@AppId 
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId  AND so.ID = @EntityId
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@EntityId", entityId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", preferenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferenceMaskparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerUsersCustAppDeliveryForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
							LEFT JOIN BE.BACustomer c ON c.BusinessPartnerTenantId = tual.BusinessPartnerTenantId
							INNER JOIN BE.BADelivery d ON d.CustomerId = c.ID
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.TenantId=@BusinessTenantId AND tual.AppId=@AppId 
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId  AND d.ID = @EntityId
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@EntityId", entityId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", preferenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferenceMaskparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerUsersCustAppDraftDeliveryForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
							LEFT JOIN BE.BACustomer c ON c.BusinessPartnerTenantId = tual.BusinessPartnerTenantId
							INNER JOIN BE.BAASN a ON a.CustomerId = c.ID
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.TenantId=@BusinessTenantId AND tual.AppId=@AppId 
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId  AND a.ID = @EntityId
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@EntityId", entityId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", preferenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferenceMaskparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerUsersCustAppContactForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
							LEFT JOIN BE.BACustomer c ON c.BusinessPartnerTenantId = tual.BusinessPartnerTenantId
							INNER JOIN BE.BAContract bc ON bc.CustomerId = c.ID
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN ap.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN ap.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.TenantId=@BusinessTenantId AND tual.AppId=@AppId 
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId  AND bc.ID = @EntityId
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@EntityId", entityId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", preferenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferenceMaskparam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerPayAppUsersForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
							LEFT JOIN BE.BACustomer c ON c.BusinessPartnerTenantId = tual.BusinessPartnerTenantId
							INNER JOIN BE.BAARInvoice i ON i.CustomerId = c.ID
                            INNER JOIN ap.Business as b ON tual.TenantId=b.TenantId
                            INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                            INNER JOIN pay.RoleLinking as rl ON tu.ID=rl.TenantUserId
                            INNER JOIN pay.Role as r ON r.ID=rl.RoleId
                            Where tual.Deleted=0 AND tual.TenantId=@BusinessTenantId AND tual.AppId=@AppId 
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                            AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId  AND i.ID = @EntityId
                            AND (r.PermissionBitMask & @PermissionBitMask)=@PermissionBitMask";

            SqlParameter tenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userIdParam = new SqlParameter("@EntityId", entityId);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            SqlParameter appIdparam = new SqlParameter("@AppId", appId);
            SqlParameter permissionparam = new SqlParameter("@PermissionBitMask", permissionMask);
            SqlParameter preferenceMaskparam = new SqlParameter("@PreferenceMask", preferenceMask);

            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantIdParam, userIdParam, userTypeparam, userStatusparam, appIdparam, permissionparam, preferenceMaskparam }).ToList();
        }


        ///<inheritdoc/>
        public List<NotificationRecipient> GetCustomerUser(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long paymentPermissionMask, long preferenceMask) {

            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                      CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                      CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                      CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'ASPreference',
                      b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                      FROM am.TenantUser TU
                      INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                      Inner JOIn am.UserTenantLinking as ul on ul.TenantUserId = tual.TenantUserId and ul.TenantId = tual.TenantId
                      Inner JOIN ap.customer as b ON ul.BusinessPartnerTenantId = b.BusinessPartnerTenantId
                      INNER JOIN pay.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId
                      INNER JOIN pay.RoleLinking as rl ON tu.ID=rl.TenantUserId
                      INNER JOIN pay.Role as r ON r.ID=rl.RoleId
                      WHERE tual.Deleted=0 AND tual.AppId=@AppId AND tual.TenantId=@BusinessTenantId
                      AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@BusinessTenantId
                      AND rl.TenantId=@BusinessTenantId AND rl.AppId=@AppId
                      AND (r.PermissionBitMask & @SOPermissionBitMask)=@SOPermissionBitMask AND tu.ID =@TenantUserId 
                      AND (
                            (ap.EmailPreference & @PreferenceMask)=@PreferenceMask OR
                            (ap.SMSPreference & @PreferenceMask)=@PreferenceMask OR
                            (ap.ASPreference & @PreferenceMask)=@PreferenceMask 
                          )";

            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            SqlParameter BusinessTenantIdParam = new SqlParameter("@BusinessTenantId", tenantId);
            SqlParameter userStatusParam = new SqlParameter("@UserStatus", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter invoicePermissionMaskParam = new SqlParameter("@SOPermissionBitMask", paymentPermissionMask);
            SqlParameter userTypeParam = new SqlParameter("@UserType", userType);
            SqlParameter permissionmaskparam = new SqlParameter("@PreferenceMask", preferenceMask);
            SqlParameter tenantUserIdParam = new SqlParameter("@TenantUserId", tenantUserId);



            return _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { appIdParam, BusinessTenantIdParam, userStatusParam, invoicePermissionMaskParam, userTypeParam, permissionmaskparam, tenantUserIdParam }).ToList();
        }

        ///<inheritdoc/>
        public List<AppInfoDTO> GetAppListByBusinessTenantIdAsync(Guid businessTenantId) {
            string sql = @"Select a.ID, a.AppKey, a.Active, a.IdentityNumber, a.Name, a.ThemeId FROM am.App AS a
                            INNER JOIN ap.TenantAppLinking AS tal ON a.ID=tal.AppId
                            Where tal.TenantId=@BusinessTenantId";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            return _qAppPortalDbContext.AppInfoDTOQuery.FromSql(sql, new object[] { businessTenantIdParam }).ToList();
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId) {

            string query = @" SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                            BIZ.Language As 'RegionLanguage', UL.AppId AS 'ApplicationId', T.TenantId, T.UserType
                            , Cast('False' AS Bit) AS 'SMSPreference', tu.Phone AS 'SMSRecipient' , CAST('False' AS bit) as ASPreference
                            FROM am.TenantUser TU
                            INNER JOIN am.UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId
                            INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId AND UL.Deleted= 0 
                            INNER JOIN ap.Business BIZ ON BIZ.TenantId = T.TenantId 
                            WHERE  TU.Id = @TenantUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            List<NotificationRecipient> notificationRecipients = _qAppPortalDbContext.NotificationRecipientQuery.FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllPublisherUsersWithPreference(Guid tenantId) {
            string query = @"SELECT top 1 TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                                '' As 'RegionLanguage', UL.AppId as 'ApplicationId', utl.TenantId , utl.UserType  , CAST('False' AS bit) as SMSPreference ,'' as SMSRecipient , 
								CAST('False' AS bit) as ASPreference
                                FROM am.TenantUser TU
                                INNER JOIN am.UserTenantLinking uTl ON TU.ID= uTl.TenantUserId 
                                INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = uTl.TenantId								 
								INNER JOIN am.TenantLinking tl ON tl.PublisherTenantId = uTl.TenantId  
						        INNER JOIN am.Tenant t ON t.ID = tl.BusinessTenantId AND tl.BusinessPartnerTenantId is null
                                WHERE utl.UserType = @UserType AND tl.BusinessTenantId = @tenantId";

            // Input parameters
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Publisher);
            List<NotificationRecipient> notificationRecipients = _qAppPortalDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { userTypeParam, tenantIdParam }).ToList();
           
            return notificationRecipients;
        }
    }
}

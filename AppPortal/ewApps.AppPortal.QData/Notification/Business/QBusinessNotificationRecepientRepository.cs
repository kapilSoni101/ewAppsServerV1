/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {

    /// <summary>
    /// Repository for implementing all the receipent get methods for the business setup notification events.
    /// </summary>
    public class QBusinessNotificationRecepientRepository: IQBusinessNotificationRecepientRepository {

        #region Local Variable

        QAppPortalDbContext _businessDbContext;

        #endregion Local Variable

        #region Constructor

        /// <summary>
        /// Initalizing local variable and DI.
        /// </summary>
        /// <param name="businessDbContext">object of business db context</param>
        /// <param name="sessionManager">Session manger object for passing to base class.</param>
        /// <param name="connectionManager">connection manager for transaction mangment.</param>
        public QBusinessNotificationRecepientRepository(QAppPortalDbContext businessDbContext) {
            _businessDbContext = businessDbContext;
        }

        #endregion Constructor

        #region Public Methods

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedUserRecipientList(Guid appId, Guid tenantId, Guid appUserId) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                    TAS.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  
                    FROM TenantUser TU
                    INNER JOIN UserTenantLinking T ON TU.ID= t.TenantUserId AND t.TenantId = @TenantId 
                    INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId
                    INNER JOIN TenantAppSetting TAS ON TAS.TenantId =  T.TenantId AND TAS.AppId= @AppId
                    WHERE  TU.Id = @AppUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@AppUserId", appUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);

            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetForgotPasswordBusinessUser(Guid tenantId, Guid tenantUserId) {

            string query = @"SELECT top 1 TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                            Bus.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  , CAST('False' AS bit) as SMSPreference ,'' as SMSRecipient , CAST('False' AS bit) as ASPreference
                            FROM am.TenantUser TU
                            INNER JOIN am.UserTenantLinking T ON TU.ID= t.TenantUserId AND t.TenantId = @TenantId 
                            INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId 
                            INNER JOIN ap.[Business] Bus ON Bus.TenantId = @tenantId 
                            WHERE  TU.Id = @tenantUserId AND t.TenantId = @TenantId ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@tenantUserId", tenantUserId);
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam }).ToList();
            return notificationRecipients;
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
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedDeletedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId) {

            string query = @" SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                        BIZ.Language As 'RegionLanguage', UL.AppId AS 'ApplicationId', T.TenantId, T.UserType
                        , Cast('False' AS Bit) AS 'SMSPreference', tu.Phone AS 'SMSRecipient'
                        FROM TenantUser TU
                        INNER JOIN UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId
                        INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId AND UL.Deleted= 1 
                        INNER JOIN Business BIZ ON BIZ.TenantId = T.TenantId 
                        WHERE  TU.Id = @TenantUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedBusinessPartnerUser(Guid tenantId, Guid tenantUserId, Guid appId) {

            string query = @" SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                            BIZ.Language As 'RegionLanguage', UL.AppId AS 'ApplicationId', T.TenantId, T.UserType
                            , Cast('False' AS Bit) AS 'SMSPreference', tu.Phone AS 'SMSRecipient', CAST('False' AS bit) as ASPreference
                            FROM am.TenantUser TU
                            INNER JOIN am.UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId
                            INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId AND UL.Deleted= 0 
                            INNER JOIN ap.Business BIZ ON BIZ.TenantId = T.TenantId 
                            WHERE  TU.Id = @TenantUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId  ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedBusinessPartnerDeletedUser(Guid tenantId, Guid tenantUserId, Guid appId) {

            string query = @" SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                        CUST.Language As 'RegionLanguage', UL.AppId AS 'ApplicationId', T.TenantId, T.UserType
                        , Cast('False' AS Bit) AS 'SMSPreference', tu.Phone AS 'SMSRecipient'
                        FROM TenantUser TU
                        INNER JOIN UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId
                        INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId AND UL.Deleted= 1 
                        INNER JOIN Customer CUST ON CUST.BusinessPartnerTenantId = T.BusinessPartnerTenantId 
                        WHERE  TU.Id = @TenantUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, long firstPermission, long secondPermission, long eventPrefrence) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'EmailPreference',
                            PUB.Language As 'RegionLanguage',UL.AppId as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                            from UserTenantLinking TUL 
                            INNER JOIN TenantUser TU ON  TUL.TenantUserId = TU.ID
                            INNER JOIN RoleLinking RL ON RL.TenantUserId = TU.ID AND RL.TenantId =@TenantId AND RL.AppId= @AppId
                            INNER JOIN Role R on R.ID= RL.RoleId
                            INNER JOIN Publisher PUB ON PUB.TenantId = TUL.TenantId
                            INNER JOIN TenantUserAppPreference ASP on  ASP.TenantId = TUL.TenantId AND ASP.TenantUserId= TU.ID
                            INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId
                            where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UL.Status = @Status  
                            AND ( (PermissionBitMask & @firstPermission)=@firstPermission OR (PermissionBitMask & @secondPermission)=@secondPermission )  ";

            // Input parameters
            SqlParameter viewParam = new SqlParameter("@firstPermission", firstPermission);
            SqlParameter manageParam = new SqlParameter("@secondPermission", secondPermission);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Publisher);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter eamilPerefernceParam = new SqlParameter("@EmailPreferencEvent", eventPrefrence);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);

            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { statusTypeParam, viewParam, tenantParam, manageParam, userTypeParam, eamilPerefernceParam, appParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessUser(Guid tenantId, Guid appId) {

            string query = @" SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                        BIZ.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId, T.UserType
                        , Cast('False' AS Bit) AS 'SMSPreference', tu.Phone AS 'SMSRecipient'
                        FROM TenantUser TU
                        INNER JOIN UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId  
                        INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId AND UL.Deleted= 0 
                        INNER JOIN Business BIZ ON BIZ.TenantId = T.TenantId 
                        WHERE  T.UserType=@UserType AND t.TenantId = @TenantId  AND UL.AppId = @AppId And UL.status=2  ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Business);
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appParam, userTypeParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessUserWithPrefrence(Guid tenantId, Guid appId, long emailPreference) {

            string query = @" SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email,CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS 'EmailPreference', 
                        BIZ.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId, T.UserType
                        , Cast('False' AS Bit) AS 'SMSPreference', tu.Phone AS 'SMSRecipient'
                        FROM TenantUser TU
                        INNER JOIN UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId  
                        INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId AND UL.Deleted= 0 
                        INNER JOIN Business BIZ ON BIZ.TenantId = T.TenantId 
                        WHERE  T.UserType=@UserType AND t.TenantId = @TenantId  AND UL.AppId = @AppId And UL.status=2  ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Business);
            SqlParameter emailPrefPram = new SqlParameter("@EmailPreferencEvent", emailPreference);
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appParam, userTypeParam, emailPrefPram }).ToList();
            return notificationRecipients;
        }


        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessPartnerUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, Guid businessPatnerTenantId, long emailPreference, long smsPreference, long firstpermission, long secondPermission) {

            string query = @" SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS 'EmailPreference', 
                        CUST.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId, T.UserType
                        , CASE WHEN (ASP.SMSPreference & @SMSPreferencEvent)=@SMSPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'SMSPreference', tu.Phone AS 'SMSRecipient'
                        FROM TenantUser TU
                        INNER JOIN UserTenantLinking T ON TU.ID = t.TenantUserId AND t.TenantId = @TenantId AND T.BusinessPartnerTenantId= @BusinessPartnerTenantId and t.Deleted=0
                        INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId and UL.Deleted=0
                        INNER JOIN Customer CUST ON CUST.BusinessPartnerTenantId = T.BusinessPartnerTenantId 
                        INNER JOIN RoleLinking RL on RL.TenantUserId =TU.ID AND RL.AppId= @AppId And RL.TenantId= @TenantId  and RL.Deleted=0
                        INNER JOIN Role R on R.ID=RL.RoleId
                        INNER JOIN TenantUserAppPreference ASP on  ASP.TenantId = T.TenantId AND ASP.TenantUserId= TU.ID AND ASP.AppId = @AppId AND ASP.Deleted=0
                        WHERE T.UserType=@UserType AND UL.DELETED=0 AND UL.Status = 2 And T.TenantId= @TenantId ANd UL.AppId= @AppId ANd UL.Deleted= 0
                        and ( (PermissionBitMask & @FirstPermission)=@FirstPermission or (PermissionBitMask & @SecondPermission)=@SecondPermission )   ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter businessPartnerTenantIdParam = new SqlParameter("@BusinessPartnerTenantId", businessPatnerTenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Customer);
            SqlParameter emialPreferenceParam = new SqlParameter("@EmailPreferencEvent", emailPreference);
            SqlParameter smsPreferenceParam = new SqlParameter("@SMSPreferencEvent", smsPreference);
            SqlParameter firstPermissionParam = new SqlParameter("@FirstPermission", firstpermission);
            SqlParameter secondPermissionParam = new SqlParameter("@SecondPermission", secondPermission);
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appParam, businessPartnerTenantIdParam, userTypeParam, emialPreferenceParam, smsPreferenceParam, firstPermissionParam, secondPermissionParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, long emailPreference, long smsPreference, long firstpermission, long secondPermission) {

            string query = @" SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit) END AS  'EmailPreference', 
                        BIZ.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId, T.UserType
                        , CASE WHEN (ASP.SMSPreference & @SMSPreferencEvent)=@SMSPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'SMSPreference', tu.Phone AS 'SMSRecipient'
                        FROM TenantUser TU
                        INNER JOIN UserTenantLinking T ON TU.ID=t.TenantUserId AND t.TenantId=@TenantId and t.Deleted=0
                        INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId and UL.Deleted=0
                        INNER JOIN Business BIZ ON BIZ.TenantId = T.TenantId 
                        INNER JOIN RoleLinking RL on RL.TenantUserId =TU.ID AND RL.AppId= @AppId And RL.TenantId= @TenantId  and RL.Deleted=0
                        INNER JOIN Role R on R.ID=RL.RoleId
                        INNER JOIN TenantUserAppPreference ASP on  ASP.TenantId = T.TenantId AND ASP.TenantUserId= TU.ID AND ASP.AppId = @AppId AND ASP.Deleted=0
                        WHERE T.UserType=@UserType AND UL.DELETED=0 AND UL.Status = 2 And T.TenantId= @TenantId ANd UL.AppId= @AppId ANd UL.Deleted= 0
                        and ( (PermissionBitMask & @FirstPermission)=@FirstPermission or (PermissionBitMask & @SecondPermission)=@SecondPermission )    ";

            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Business);
            SqlParameter emialPreferenceParam = new SqlParameter("@EmailPreferencEvent", emailPreference);
            SqlParameter smsPreferenceParam = new SqlParameter("@SMSPreferencEvent", smsPreference);
            SqlParameter firstPermissionParam = new SqlParameter("@FirstPermission", firstpermission);
            SqlParameter secondPermissionParam = new SqlParameter("@SecondPermission", secondPermission);
            List<NotificationRecipient> notificationRecipients = _businessDbContext.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appParam, userTypeParam, emialPreferenceParam, smsPreferenceParam, firstPermissionParam, secondPermissionParam }).ToList();
            return notificationRecipients;
        }

        #endregion Public Methods
    }
}

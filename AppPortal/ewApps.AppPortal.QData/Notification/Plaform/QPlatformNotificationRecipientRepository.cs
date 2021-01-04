/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ewApps.AppPortal.Common;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {

    public class QPlatformNotificationRecipientRepository:IQPlatformNotificationRecipientRepository {

        QAppPortalDbContext _context;

        #region Constructor

        public QPlatformNotificationRecipientRepository(QAppPortalDbContext context) {
            _context = context;
        }

        #endregion

        #region public methods 

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedPlatformUser(Guid appId, Guid tenantId, Guid appUserId) {
            // ToDo: Review: 1) Why language is hard-coded in query? 2) Query is not formatted properly. 3) AppUser.TenantId is proposed to remove.
            string query = @"SELECT distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                                Pl.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  , CAST('False' AS bit) as SMSPreference ,'' as SMSRecipient , CAST('False' AS bit) as ASPreference
                                FROM am.TenantUser TU
                                INNER JOIN am.UserTenantLinking T ON TU.ID= t.TenantUserId AND t.TenantId = @TenantId 
                                INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId
                                INNER JOIN ap.[Platform] Pl ON Pl.TenantId = @tenantId 
                                WHERE  TU.Id = @AppUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@AppUserId", appUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetForgotPasswordPlatformUser( Guid tenantId, Guid appUserId) {
            
            string query = @"SELECT top 1 TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                            Pl.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  , CAST('False' AS bit) as SMSPreference ,'' as SMSRecipient , CAST('False' AS bit) as ASPreference
                            FROM am.TenantUser TU
                            INNER JOIN am.UserTenantLinking T ON TU.ID= t.TenantUserId AND t.TenantId = @TenantId 
                            INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId 
                            INNER JOIN ap.[Platform] Pl ON Pl.TenantId = @tenantId 
                            WHERE  TU.Id = @AppUserId AND t.TenantId = @TenantId   ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@AppUserId", appUserId);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedPublisherUser(Guid appId, Guid tenantId, Guid appUserId) {

            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                            Pl.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  , CAST('False' AS bit) as SMSPreference ,'' as SMSRecipient, CAST('False' AS bit) as ASPreference
                            FROM am.TenantUser TU
                            INNER JOIN am.UserTenantLinking T ON TU.ID= t.TenantUserId AND t.TenantId = @TenantId 
                            INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId
                            INNER JOIN ap.Publisher Pl ON Pl.TenantId = @tenantId 
                            WHERE  TU.Id = @AppUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@AppUserId", appUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            return notificationRecipients;
        }


        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedUserWithPrefrence(Guid appId, Guid tenantId, Guid appUserId, long emailPreference) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'EmailPreference',
                    TAS.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  
                    FROM TenantUser TU
                    INNER JOIN UserTenantLinking T ON TU.ID= t.TenantUserId AND t.TenantId = @TenantId 
                    INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId
                    INNER JOIN TenantAppSetting TAS ON TAS.TenantId =  T.TenantId AND TAS.AppId= @AppId
                    INNER JOIN TenantUserAppPreference ASP on  ASP.TenantId = TAS.TenantId AND ASP.TenantUserId= TU.ID AND ASP.AppId = @AppId
                    WHERE  TU.Id = @AppUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@AppUserId", appUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter prefrenceParam = new SqlParameter("@EmailPreferencEvent", emailPreference);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, parameters: new[] { tenantParam, appuserParam, appParam, prefrenceParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllPublisherUsers(Guid tenantId) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                        TAS.Language As 'RegionLanguage',UL.AppId as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                        from UserTenantLinking TUL 
                        INNER JOIN TenantUser TU  ON  TUL.TenantUserId = TU.ID
                        INNER JOIN TenantAppSetting TAS ON TAS.TenantId = TUL.TenantId
                        INNER JOIN TenantUserAppLinking UL ON UL.TenantUserId= TU.ID AND UL.TenantId= @TenantId
                        where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UL.Status = @Status  ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Publisher);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, parameters: new[] { tenantParam, userTypeParam, statusTypeParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllPlatformUsersWithPreference(Guid tenantId, long emailPrefrence) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'EmailPreference',
                      TAS.Language As 'RegionLanguage',TU.ID as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                      from UserTenantLinking TUL 
                      INNER JOIN TenantUser TU ON  TUL.TenantUserId = TU.ID
                      INNER JOIN TenantAppSetting TAS ON TAS.TenantId = TUL.TenantId
                      INNER JOIN TenantUserAppPreference ASP on  ASP.TenantId = TAS.TenantId AND ASP.TenantUserId= TU.ID
                      INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId 
                      where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UL.Status = @Status  ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Platform);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter prefrenceParam = new SqlParameter("@EmailPreferencEvent", emailPrefrence);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, parameters: new[] { tenantParam, userTypeParam, statusTypeParam, prefrenceParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPlatformUsersWithApplicationPermissionAndPreference(Guid tenantId, long emailPrefrence) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'EmailPreference',
                      TAS.Language As 'RegionLanguage',TU.ID as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                      from UserTenantLinking TUL 
                      INNER JOIN TenantUser TU ON  TUL.TenantUserId = TU.ID
                      INNER JOIN RoleLinking RL ON RL.TenantUserId = TU.ID AND RL.TenantId =@TenantId 
                      INNER JOIN Role R on R.ID= RL.RoleId
                      INNER JOIN TenantAppSetting TAS ON TAS.TenantId = TUL.TenantId
                      INNER JOIN TenantUserAppPreference ASP on  ASP.TenantId = TAS.TenantId AND ASP.TenantUserId= TU.ID
                      INNER JOIN TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId
                      where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UL.Status = @Status  
                      AND ( (PermissionBitMask & @ViewApplicationPermission)=@ViewApplicationPermission OR (PermissionBitMask & @ManageApplicationPermission)=@ManageApplicationPermission ) ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Platform);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter prefrenceParam = new SqlParameter("@EmailPreferencEvent", emailPrefrence);
            SqlParameter viewAppParam = new SqlParameter("@ViewApplicationPermission", PlatformUserPlatformAppPermissionEnum.ViewApplications);
            SqlParameter manageAppParam = new SqlParameter("@ManageApplicationPermission", PlatformUserPlatformAppPermissionEnum.ManageApplications);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, parameters: new[] { tenantParam, userTypeParam, statusTypeParam, prefrenceParam, viewAppParam, manageAppParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithPreference(long emailPreferencEvent) {
            string query = @" SELECT AU.Id as 'AppUserId' ,FullName,Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'EmailPreference'
                      ,'en' As 'RegionLanguage', RL.AppId  as 'ApplicationId' ,AU.TenantId, AU.UserType  FROM AppUser AU
                      INNER JOIN RoleLinking RL ON RL.AppUserId =AU.ID
                      INNER JOIN Role AS R on R.ID=RL.RoleId
					            INNER JOIN AppUserPreference ASP on ASP.AppUserId= AU.ID 
                      WHERE AU.UserType=@UserType AND ASP.Deleted=0  AND AU.Status = @Status 
                      AND ( (PermissionBitMask & @ViewSupportPermission)=@ViewSupportPermission OR (PermissionBitMask & @ManageSupportPermission)=@ManageSupportPermission ) ";

            // Input parameters
            SqlParameter viewParam = new SqlParameter("@ViewSupportPermission", PlatformUserPlatformAppPermissionEnum.ViewTickets);
            SqlParameter manageParam = new SqlParameter("@ManageSupportPermission", PlatformUserPlatformAppPermissionEnum.ManageTickets);
            SqlParameter prefrenceParam = new SqlParameter("@EmailPreferencEvent", emailPreferencEvent);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Publisher);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, parameters: new[] { statusTypeParam, viewParam, manageParam, prefrenceParam, userTypeParam }).ToList();
            return notificationRecipients;
        }

        #endregion
    }
}

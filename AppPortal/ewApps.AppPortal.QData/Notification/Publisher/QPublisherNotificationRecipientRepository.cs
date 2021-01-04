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

    public class QPublisherNotificationRecipientRepository:IQPublisherNotificationRecipientRepository {

        QAppPortalDbContext _context;

        #region Constructor

        public QPublisherNotificationRecipientRepository(QAppPortalDbContext context) {
            _context = context;
        }

        #endregion

        #region public methods 

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedUser(Guid appId, Guid tenantId, Guid appUserId) {
            // ToDo: Review: 1) Why language is hard-coded in query? 2) Query is not formatted properly. 3) AppUser.TenantId is proposed to remove.
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                            'en' 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  , CAST('False' AS bit) as SMSPreference ,'' as SMSRecipient , CAST('False' AS bit) as ASPreference
                            FROM am.TenantUser TU
                            INNER JOIN am.UserTenantLinking T ON TU.ID= t.TenantUserId AND t.TenantId = @TenantId 
                            INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId = @AppId
                            WHERE  TU.Id = @AppUserId AND t.TenantId = @TenantId  AND UL.AppId = @AppId  ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@AppUserId", appUserId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, appParam }).ToList();
            // GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { tenantParam, appuserParam, appParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetForgotPasswordPublisherUser(Guid tenantId, Guid tenantUserId) {

            string query = @"SELECT top 1 TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                                Pub.Language As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  , CAST('False' AS bit) as SMSPreference ,'' as SMSRecipient , CAST('False' AS bit) as ASPreference
                                FROM am.TenantUser TU
                                INNER JOIN am.UserTenantLinking T ON TU.ID= t.TenantUserId AND t.TenantId = @tenantId 
                                INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @tenantId  
                                INNER JOIN ap.[Publisher] Pub ON Pub.TenantId = @tenantId 
                                WHERE  TU.Id = @tenantUserId AND t.TenantId = @tenantId  ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@tenantId ", tenantId);
            SqlParameter appuserParam = new SqlParameter("@tenantUserId", tenantUserId);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam }).ToList();
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetInvitedUserWithPrefrence(Guid tenantId, Guid tenantUserId, long emailPreference, int userType, int userStatus) {
            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email,
                            CASE WHEN (ap.EmailPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'EmailPreference',
                            CASE WHEN (ap.ASPreference & @PreferenceMask)=@PreferenceMask THEN cast(1 as bit) ELSE cast(0 as bit) END AS 'ASPreference',
                            CASE WHEN (ap.SMSPreference & @PreferenceMask)=@PreferenceMask THEN  cast(1 as bit) ELSE cast(0 as bit)  END AS 'SMSPreference',
                            b.Language As 'RegionLanguage', tual.AppId as 'ApplicationId', tual.TenantId, Tual.UserType, Cast(tu.ID As varchar(100)) AS 'SMSRecipient'
                            FROM am.TenantUser TU
                            INNER JOIN am.TenantUserAppLinking as tual ON tu.id=tual.TenantUserId
                            INNER JOIN ap.Publisher as b ON tual.TenantId=b.TenantId
                            INNER JOIN ap.TenantUserAppPreference as ap ON tu.ID=ap.TenantUserId AND ap.AppId=tual.AppId                           
                            Where tual.Deleted=0 AND tual.TenantId=@TenantId
                            AND tual.UserType=@UserType AND tual.Status=@UserStatus AND ap.TenantId=@TenantId
                            AND TU.ID = @TenantUserId";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appuserParam = new SqlParameter("@TenantUserId", tenantUserId);
            SqlParameter emailPreferenceParam = new SqlParameter("@PreferenceMask", emailPreference);
            SqlParameter userTypeparam = new SqlParameter("@UserType", userType);
            SqlParameter userStatusparam = new SqlParameter("@UserStatus", userStatus);

            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appuserParam, emailPreferenceParam, userTypeparam, userStatusparam }).ToList();
            //GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { tenantParam, appuserParam, appParam, prefrenceParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllPlatformUsersWithPreference() {
            string query = @"SELECT top 1 TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                                '' As 'RegionLanguage', UL.AppId as 'ApplicationId', T.TenantId , T.UserType  , CAST('False' AS bit) as SMSPreference ,'' as SMSRecipient , 
								CAST('False' AS bit) as ASPreference
                                FROM am.TenantUser TU
                                INNER JOIN am.UserTenantLinking T ON TU.ID= t.TenantUserId 
                                INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = T.TenantId
                                WHERE T.UserType = @UserType";

            // Input parameters
           
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Platform);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] {  userTypeParam}).ToList();
            // GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { tenantParam, userTypeParam, statusTypeParam, prefrenceParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessUserWithApplicationAccess(Guid tenantId, Guid appId) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                      '' As 'RegionLanguage',UAL.AppId as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                      from am.UserTenantLinking TUL 
                      INNER JOIN am.TenantUser TU ON  TUL.TenantUserId = TU.ID
                      INNER JOIN am.TenantUserAppLinking UAL ON UAL.TenantUserId =TU.ID AND UAL.TenantId=@TenantId AND UAL.AppId= @AppId                     
                      where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UAL.Status = @Status AND UAL.AppId= @AppId  ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", UserTypeEnum.Business);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);

            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appParam, userTypeParam, statusTypeParam }).ToList();
            // GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { tenantParam, appParam, userTypeParam, statusTypeParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessUserWithApplicationAccessWithoutStatus(Guid tenantId, Guid appId) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                      '' As 'RegionLanguage',UAL.AppId as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                      from am.UserTenantLinking TUL 
                      INNER JOIN am.TenantUser TU ON  TUL.TenantUserId = TU.ID
                      INNER JOIN am.TenantUserAppLinking UAL ON UAL.TenantUserId =TU.ID AND UAL.TenantId=@TenantId AND UAL.AppId= @AppId                      
                      where TUL.UserType=@UserType and TUL.TenantId= @TenantId  AND UAL.AppId= @AppId  ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", UserTypeEnum.Business);

            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appParam, userTypeParam }).ToList();
            // GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { tenantParam, appParam, userTypeParam, statusTypeParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetBusinessPartnerUserWithApplicationAccess(Guid businessTenantId, Guid businessPartnerTenantId, Guid appId) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                            cust.Language As 'RegionLanguage',UAL.AppId as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                            from am.UserTenantLinking TUL 
                            INNER JOIN am.TenantUser TU ON  TUL.TenantUserId = TU.ID
                            INNER JOIN am.TenantUserAppLinking UAL ON UAL.TenantUserId =TU.ID AND UAL.TenantId=@TenantId AND UAL.AppId= @AppId
                            INNER JOIN ap.Customer cust ON cust.BusinessPartnerTenantId =@BusinessPartnerTenantId
                            where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UAL.Status = @Status AND UAL.AppId= @AppId AND UAL.BusinessPartnerTenantId= @BusinessPartnerTenantId ";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", businessTenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", UserTypeEnum.Customer);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter partnerTenantIdParam = new SqlParameter("@BusinessPartnerTenantId", businessPartnerTenantId);

            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appParam, userTypeParam, statusTypeParam, partnerTenantIdParam }).ToList();
            //GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { tenantParam, appParam, userTypeParam, statusTypeParam, partnerTenantIdParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessUsers(Guid tenantId) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
                      '' As 'RegionLanguage',UL.AppId as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                      from am.UserTenantLinking TUL 
                      INNER JOIN am.TenantUser TU ON  TUL.TenantUserId = TU.ID
                      INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId AND UL.AppId=@AppId                      
                      where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UL.Status = @Status   ";

            // Input parameters
            // ToDo: hardcoded application id.
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appParam = new SqlParameter("@AppId", "F4952EF3-F1BD-4621-A5F9-290FD09BC81B");
            SqlParameter userTypeParam = new SqlParameter("@UserType", UserTypeEnum.Business);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);

            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, appParam, userTypeParam, statusTypeParam }).ToList();
            //GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { tenantParam, appParam, userTypeParam, statusTypeParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetAllBusinessPartnerUsers(Guid tenantId, Guid businessPartnerTenantId) {
            string query = @"SELECT Distinct TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CAST('True' AS bit) As 'EmailPreference', 
            'en' As 'RegionLanguage','00000000-0000-0000-0000-000000000000' as 'ApplicationId', TUL.TenantId ,TUL.UserType 
            from am.UserTenantLinking TUL 
            INNER JOIN am.TenantUser TU ON  TUL.TenantUserId = TU.ID
            INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId
            where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UL.Status = @Status AND TUL.BusinessPartnerTenantId= @SubTenantId";

            // Input parameters
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter userTypeParam = new SqlParameter("@UserType", UserTypeEnum.Customer);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter partnerTenantIdParam = new SqlParameter("@BusinessPartnerTenantId", businessPartnerTenantId);

            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { tenantParam, userTypeParam, statusTypeParam, partnerTenantIdParam }).ToList();
            //GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { tenantParam, userTypeParam, statusTypeParam, partnerTenantIdParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, long viewPermission, long managePermission, long eventPrefrence) {
            string query = @"SELECT TU.Id AS 'TenantUserId', TU.FullName, TU.Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'EmailPreference',
                      '' As 'RegionLanguage',UL.AppId as 'ApplicationId', TUL.TenantId ,TUL.UserType 
                      from am.UserTenantLinking TUL 
                      INNER JOIN am.TenantUser TU ON  TUL.TenantUserId = TU.ID
                      INNER JOIN ap.RoleLinking RL ON RL.TenantUserId = TU.ID AND RL.TenantId =@TenantId AND RL.AppId= @AppId
                      INNER JOIN ap.Role R on R.ID= RL.RoleId                      
                      INNER JOIN ap.TenantUserAppPreference ASP on  ASP.TenantId = TAS.TenantId AND ASP.TenantUserId= TU.ID
                      INNER JOIN am.TenantUserAppLinking  UL ON TU.ID = UL.TenantUserId AND UL.TenantId = @TenantId
                      where TUL.UserType=@UserType and TUL.TenantId= @TenantId AND UL.Status = @Status  
                      AND ( (PermissionBitMask & @ViewPermission)=@ViewPermission OR (PermissionBitMask & @ManagePermission)=@ManagePermission )   ";

            // Input parameters
            SqlParameter viewParam = new SqlParameter("@ViewPermission", viewPermission);
            SqlParameter manageParam = new SqlParameter("@ManagePermission", managePermission);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Publisher);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            SqlParameter eamilPerefernceParam = new SqlParameter("@EmailPreferencEvent", eventPrefrence);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter appParam = new SqlParameter("@AppId", appId);

            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { statusTypeParam, viewParam, tenantParam, manageParam, userTypeParam, eamilPerefernceParam, appParam }).ToList();
            // GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { statusTypeParam, viewParam, tenantParam, manageParam, userTypeParam, eamilPerefernceParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsers() {
            string query = @"SELECT AU.Id as 'AppUserId' ,FullName,Email,CAST('True' AS bit) As 'EmailPreference'
                      ,'en' As 'RegionLanguage', RL.AppId  as 'ApplicationId' ,AU.TenantId , UAL.UserType  FROM AppUser AU
                      INNER JOIN RoleLinking RL on RL.AppUserId =AU.ID
                      INNER JOIN Role R ON R.ID=RL.RoleId
					            Inner join UserAppLinking UAL on UAL.AppUserId=AU.ID
                      WHERE UAL.UserType=@UserType AND AU.Deleted=0 AND AU.Status = @Status 
                      AND ( (PermissionBitMask & @ViewSupportPermission)=@ViewSupportPermission OR (PermissionBitMask & @ManageSupportPermission)=@ManageSupportPermission ) ";
            SqlParameter viewParam = new SqlParameter("@ViewSupportPermission", PublisherUserPublisherAppPermissionEnum.ViewTickets);
            SqlParameter manageParam = new SqlParameter("@ManageSupportPermission", PublisherUserPublisherAppPermissionEnum.ManageTickets);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Publisher);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { statusTypeParam, viewParam, manageParam, userTypeParam }).ToList();
            //GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { statusTypeParam, viewParam, manageParam, userTypeParam });
            return notificationRecipients;
        }

        ///<inheritdoc/>
        public List<NotificationRecipient> GetPublisherSupportUsersWithPreference(long emailPreferencEvent) {
            string query = @" SELECT AU.Id as 'AppUserId' ,FullName,Email, CASE WHEN (ASP.EmailPreference & @EmailPreferencEvent)=@EmailPreferencEvent THEN CAST('True' AS bit) ELSE CAST('false' AS bit)END AS  'EmailPreference'
                      ,'en' As 'RegionLanguage', RL.AppId  as 'ApplicationId' ,AU.TenantId, UAL.UserType  FROM AppUser AU
                      INNER JOIN RoleLinking RL ON RL.AppUserId =AU.ID
                      INNER JOIN Role AS R on R.ID=RL.RoleId
					            Inner join UserAppLinking UAL on UAL.AppUserId=AU.ID 
					            INNER JOIN AppUserPreference ASP on ASP.AppUserId= AU.ID 
                      WHERE UAL.UserType=@UserType AND ASP.Deleted=0  AND AU.Status = @Status 
                      AND ( (PermissionBitMask & @ViewSupportPermission)=@ViewSupportPermission OR (PermissionBitMask & @ManageSupportPermission)=@ManageSupportPermission ) ";
            SqlParameter viewParam = new SqlParameter("@ViewSupportPermission", PublisherUserPublisherAppPermissionEnum.ViewTickets);
            SqlParameter manageParam = new SqlParameter("@ManageSupportPermission", PublisherUserPublisherAppPermissionEnum.ManageTickets);
            SqlParameter prefrenceParam = new SqlParameter("@EmailPreferencEvent", emailPreferencEvent);
            SqlParameter userTypeParam = new SqlParameter("@UserType", (int)UserTypeEnum.Publisher);
            SqlParameter statusTypeParam = new SqlParameter("@Status", (int)TenantUserInvitaionStatusEnum.Accepted);
            List<NotificationRecipient> notificationRecipients = _context.Query<NotificationRecipient>().FromSql(query, new object[] { statusTypeParam, viewParam, manageParam, prefrenceParam, userTypeParam }).ToList();
            //GetQueryEntityList<NotificationRecipient>(query, parameters: new[] { statusTypeParam, viewParam, manageParam, prefrenceParam, userTypeParam });
            return notificationRecipients;
        }

        #endregion

    }
}

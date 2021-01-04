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
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.QData {
    public interface IQPublisherNotificationRecipientRepository {//: IBaseRepository<ewApps.Core.Entity.NotificationQueue> {

        List<NotificationRecipient> GetInvitedUser(Guid appId, Guid tenantId, Guid appUserId);

        List<NotificationRecipient> GetPublisherSupportUsers();

        List<NotificationRecipient> GetPublisherSupportUsersWithPreference(long emailPreferencEvent);

        List<NotificationRecipient> GetBusinessUserWithApplicationAccess(Guid tenantId, Guid appId);

        List<NotificationRecipient> GetPublisherUserWithPermissionAndPrefrence(Guid tenantId, Guid appId, long viewPermission, long managePermission, long eventPrefrence);
        List<NotificationRecipient> GetAllBusinessUsers(Guid tenantId);
        List<NotificationRecipient> GetAllBusinessPartnerUsers(Guid tenantId, Guid businessPartnerTenantId);
        List<NotificationRecipient> GetAllPlatformUsersWithPreference();
        List<NotificationRecipient> GetInvitedUserWithPrefrence(Guid tenantId, Guid tenantUserId, long emailPreference, int userType, int userStatus);
        List<NotificationRecipient> GetBusinessPartnerUserWithApplicationAccess(Guid businessTenantId, Guid businessPartnerTenantId, Guid appId);

        List<NotificationRecipient> GetBusinessUserWithApplicationAccessWithoutStatus(Guid tenantId, Guid appId);
        List<NotificationRecipient> GetForgotPasswordPublisherUser(Guid tenantId, Guid tenantUserId);
    }
}

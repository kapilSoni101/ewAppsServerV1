using System;
using System.Collections.Generic;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.QData {

    public interface IQPlatformNotificationRecipientRepository {

        List<NotificationRecipient> GetInvitedPlatformUser(Guid appId, Guid tenantId, Guid appUserId);

        List<NotificationRecipient> GetInvitedPublisherUser(Guid appId, Guid tenantId, Guid appUserId);

        List<NotificationRecipient> GetInvitedUserWithPrefrence(Guid appId, Guid tenantId, Guid appUserId, long emailPreference);

        List<NotificationRecipient> GetPublisherSupportUsersWithPreference(long emailPreferencEvent);

        List<NotificationRecipient> GetAllPublisherUsers(Guid tenantId);

        List<NotificationRecipient> GetAllPlatformUsersWithPreference(Guid tenantId, long emailPrefrence);

        List<NotificationRecipient> GetPlatformUsersWithApplicationPermissionAndPreference(Guid tenantId, long emailPrefrence);

        List<NotificationRecipient> GetForgotPasswordPlatformUser(Guid tenantId, Guid appUserId);
    }
}

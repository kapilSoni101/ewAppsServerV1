using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {
    public interface ICustNotificationRecipientsDS {

        // List<NotificationRecipient> GetCustPaymentUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId);

         List<NotificationRecipient> GetCustCustomerUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId);

        List<NotificationRecipient> GetCustUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId);

        List<NotificationRecipient> GetCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId,  int entityType, int userType, int userStatus, long permissionMask);

        List<NotificationRecipient> GetCustomerUsersForNotesNotification(Guid tenantId, Guid tenantUserId, Guid appId,  int userType, int userStatus);

        List<NotificationRecipient> GetBusinessUsersForNotesNotification(Guid tenantId, Guid tenantUserId, Guid appId,  int userType, int userStatus);

        List<NotificationRecipient> GetCustomerUserOnAppDeletedRecipients(Guid businessTenantId, Guid tenantUserId, Guid appId);

    List<NotificationRecipient> GetBusinessUsersForSupportTicketNotification(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus);

        List<NotificationRecipient> GetCustomerUserOnAppPermissionRecipients(Guid businessTenantId, Guid tenantUserId, Guid appId, int userType, int userStatus);

        List<NotificationRecipient> GetAllPublisherUsersWithPreference(Guid tenantId);
    }
}

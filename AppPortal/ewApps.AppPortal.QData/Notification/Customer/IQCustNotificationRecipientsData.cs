using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.DTO;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.QData {
    public interface IQCustNotificationRecipientsData {
        List<NotificationRecipient> GetCustPaymentUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId);

        List<NotificationRecipient> GetCustCustomerUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId);

        List<NotificationRecipient> GetCustUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, Guid onboardedUserId);

        List<AppInfoDTO> GetAppListByBusinessTenantIdAsync(Guid businessTenantId);        

        List<NotificationRecipient> GetCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask);

        List<NotificationRecipient> GetCustomerPayUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask);

        List<NotificationRecipient> GetBusinessCustomerUsersForNotes(Guid tenantId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask);

        List<NotificationRecipient> GetBusinessUsersForNotes(Guid tenantId,  Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask);

        List<NotificationRecipient> GetCustomerUserOnAppDeletedRecipients(Guid tenantId, Guid tenantUserId, Guid appId);

        List<NotificationRecipient> GetCustomerUserOnAppPermissionRecipients(Guid tenantId, Guid tenantUserId, Guid appId, long custPreferrenceEnumValue, int userType, int userStatus);

        List<NotificationRecipient> GetAllPublisherUsersWithPreference(Guid tenantId);

        }
}

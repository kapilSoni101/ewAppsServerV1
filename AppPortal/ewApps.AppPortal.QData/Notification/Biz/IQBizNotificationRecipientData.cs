using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.QData {
    public interface IQBizNotificationRecipientData {
        List<NotificationRecipient> GetBizUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId, long preferenceValue);
        List<NotificationRecipient> GetBizPaymentUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId);
        List<NotificationRecipient> GetBizCustUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId, long preferenceValue);
        List<NotificationRecipient> GetBizSetupUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId);
        List<NotificationRecipient> GetAppUserOnBusiness(Guid tenantId, Guid tenantUserId, Guid appId);

        List<NotificationRecipient> GetBusinessUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long permissionMask, long preferenceMask);

        List<NotificationRecipient> GetBusinessCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);
        List<NotificationRecipient> GetCustomerUser(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);

        List<NotificationRecipient> GetCustomerUsersCustAppForNotes(Guid tenantId, Guid entity, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);

        List<NotificationRecipient> GetCustomerPayAppUsersForNotes(Guid tenantId, Guid entity, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);

        List<AppInfoDTO> GetAppListByBusinessTenantIdAsync(Guid businessTenantId);

        List<NotificationRecipient> GetCustomerUsersCustAppSalesQuotationForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);

        List<NotificationRecipient> GetCustomerUsersCustAppSalesOrderForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);

        List<NotificationRecipient> GetCustomerUsersCustAppDeliveryForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);

        List<NotificationRecipient> GetCustomerUsersCustAppDraftDeliveryForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);

        List<NotificationRecipient> GetCustomerUsersCustAppContactForNotes(Guid tenantId, Guid entityId, Guid appId, int userType, int userStatus, long paymentPermissionMask, long permissionMask);

        /// <summary>
        /// Get the invited business users where is is of businesss type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns></returns>
        List<NotificationRecipient> GetInvitedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId);

        List<NotificationRecipient> GetAppUserPermissionOnBusiness(Guid tenantId, Guid tenantUserId, Guid appId, long bizPreferrenceValue, int userType, int userStatus);

        List<NotificationRecipient> GetAllPublisherUsersWithPreference(Guid tenantId);

        List<NotificationRecipient> GetAppUserAccessUpdateOnBusiness(Guid tenantId, Guid tenantUserId);
    }
}

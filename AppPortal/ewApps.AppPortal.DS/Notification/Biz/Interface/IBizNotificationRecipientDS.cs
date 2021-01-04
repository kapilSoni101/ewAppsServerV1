using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {
    public interface IBizNotificationRecipientDS {

        List<NotificationRecipient> GetBizUserAppOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId, long preferenceValue);
        List<NotificationRecipient> GetBizPaymentUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId);
        List<NotificationRecipient> GetBizCustUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId, long preferenceValue);

        List<NotificationRecipient> GetBizSetupUserOnBoardRecipients(Guid appId, Guid businessTenantId, Guid onboardedUserId);
        

        List<NotificationRecipient> GetAppUserOnBusiness(Guid tenantId, Guid tenantUserId, Guid appId);
        List<NotificationRecipient> GetCustomerUser(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus, long paymentPermissionMask);

        List<NotificationRecipient> GetBusinessUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, Guid entityId, int userType, int userStatus);

        List<NotificationRecipient> GetCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, Guid entityId, int userType, int userStatus);

        List<NotificationRecipient> GetBusinessCustomerUsersForNotes(Guid tenantId, Guid tenantUserId, Guid appId, Guid entityId, int entityType, int userType, int userStatus, long paymentPermissionMask);

        /// <summary>
        /// Get the invited business users where is is of businesss type.
        /// </summary>
        /// <param name="tenantId">tenant identifier for the user</param>
        /// <param name="tenantUserId"> tenant user identitfier for the tenant user</param>
        /// <param name="appId">application id for the user to which user belongs</param>
        /// <returns>Returns invited user as notification recipient.</returns>
        List<NotificationRecipient> GetInvitedBusinessUser(Guid tenantId, Guid tenantUserId, Guid appId);

        /// <summary>
        /// Get User With Email and As Permission 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<NotificationRecipient> GetAppUserPermissionOnBusiness(Guid tenantId, Guid tenantUserId, Guid appId, int userType, int userStatus);


        List<NotificationRecipient> GetAllPublisherUsersWithPreference(Guid tenantId);

        List<NotificationRecipient> GetAppUserAccessUpdateOnBusiness(Guid tenantId, Guid tenantUserId);

    }
}

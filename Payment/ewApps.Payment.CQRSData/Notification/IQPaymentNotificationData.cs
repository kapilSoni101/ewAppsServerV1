using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.NotificationService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public interface IQPaymentNotificationData {

        List<NotificationRecipient> GetBusinessUserPaymentAppUsers(Guid appId, Guid businessTenantId, int paymentPermissionMask, int userPreferenceMask);

        PaymentRelatedDataDTO GetPaymentNotificationInfo(Guid paymentId, Guid tenantUserId, string AppKey);

        //List<NotificationRecipient> GetCustomerUserPaymentAppUsers(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId);
        List<NotificationRecipient> GetCustomerUserPaymentAppUsers(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int paymentPermissionMask, int userPreferenceMask);

        //List<NotificationRecipient> GetPaymentAppCustomerUserOnRefundForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int paymentPermissionMask);
        List<NotificationRecipient> GetPaymentAppCustomerUserOnRefundForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int paymentPermissionMask, int userPreferenceMask);

        /// <summary>
        /// Get customer notification preference for payment.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant id.</param>
        /// <param name="businessPartnerTenantId">Business partner tenant id.</param>
        /// <param name="paymentPermissionMask">Assigned permission.</param>
        /// <param name="userPreferenceMask">Notification preference is on.</param>
        /// <returns></returns>
        //    List<NotificationRecipient> GetPaymentAppCustomerUserOnPaymentForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int paymentPermissionMask, int userPreferenceMask);
        List<NotificationRecipient> GetPaymentAppCustomerUserOnPaymentForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, int paymentPermissionMask, int userPreferenceMask);

        /// <summary>
        /// Get business user and their payment preference.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant id.</param>
        /// <param name="paymentPermissionMask">Assigned permission.</param>
        /// <param name="userPreferenceMask">Notification preference is on.</param>
        /// <returns></returns>
        List<NotificationRecipient> GetPaymentAppBusinessUserOnPaymentForBusiness(Guid appId, Guid businessTenantId, int paymentPermissionMask, int userPreferenceMask);

        /// <summary>
        /// Get preauth payment detail.
        /// </summary>
        /// <param name="preAuthPaymentId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="AppKey"></param>
        /// <returns>return authorized payment information.</returns>
        PreAuthPaymentRelatedDataDTO GetPreAuthPaymentNotificationInfo(Guid preAuthPaymentId, Guid tenantUserId, string AppKey);
    }
}

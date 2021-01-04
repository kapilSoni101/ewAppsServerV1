using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.DS {
    public interface IPaymentNotificationRecipientDS {

        List<NotificationRecipient> GetBusinessUserPaymentAppUsers(Guid appId, Guid businessTenantId);

        PaymentRelatedDataDTO GetPaymentNotificationInfo(Guid paymentId, Guid tenantUserId, string AppKey);

        List<NotificationRecipient> GetCustomerUserPaymentAppUsers(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId);

        List<NotificationRecipient> GetPaymentAppCustomerUserOnRefundForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId);

        /// <summary>
        /// Get customer payment notification preference.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant Id</param>
        /// <param name="businessPartnerTenantId">Business partner Tenant id(customer tenant id)</param>
        /// <returns></returns>
        List<NotificationRecipient> GetPaymentAppCustomerUserOnPaymentForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId);

        /// <summary>
        /// Get business user to notify on payment.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant Id</param>        
        /// <returns></returns>
        List<NotificationRecipient> GetPaymentAppBusinessUserOnPaymentForBusiness(Guid appId, Guid businessTenantId);

        /// <summary>
        /// Get preauth payment detail.
        /// </summary>
        /// <param name="preAuthPaymentId">Pre auth payment id.</param>
        /// <param name="tenantUserId">Tenmant userid.</param>
        /// <param name="AppKey">Application key</param>
        /// <returns>return authorized payment information.</returns>
        PreAuthPaymentRelatedDataDTO GetPreAuthPaymentNotificationInfo(Guid preAuthPaymentId, Guid tenantUserId, string AppKey);

        /// <summary>
        /// Get customer payment notification preference.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant Id</param>
        /// <param name="businessPartnerTenantId">Business partner Tenant id(customer tenant id)</param>
        /// <returns></returns>
        List<NotificationRecipient> GetAdvancePaymentAppCustomerUserOnPaymentForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, CustomerUserPaymentAppPreferenceEnum prefEnum);

        /// <summary>
        /// Get business user to notify on payment.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant Id</param>        
        /// <returns></returns>
        List<NotificationRecipient> GetAdvanceAndPreAuthPaymentAppBusinessUserOnPaymentForBusiness(Guid appId, Guid businessTenantId, BusinessUserPaymentAppPreferenceEnum prefEnum);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;
using ewApps.Payment.DTO;
using ewApps.Payment.QData;

namespace ewApps.Payment.DS {
    public class PaymentNotificationRecipientDS:IPaymentNotificationRecipientDS {

        IQPaymentNotificationData _qPaymentNotificationRecipientData;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentNotificationRecipientDS"/> class.
        /// </summary>
        /// <param name="qPaymentNotificationRecipientData">The q biz payment notification recipient data.</param>
        public PaymentNotificationRecipientDS(IQPaymentNotificationData qPaymentNotificationRecipientData) {
            _qPaymentNotificationRecipientData = qPaymentNotificationRecipientData;
        }

        public List<NotificationRecipient> GetBusinessUserPaymentAppUsers(Guid appId, Guid businessTenantId) {
            return _qPaymentNotificationRecipientData.GetBusinessUserPaymentAppUsers(appId, businessTenantId,
            (int)CustomerUserPaymentAppPermissionEnum.AllowPaymentActivities |
            (int)CustomerUserPaymentAppPermissionEnum.AccessTransactionHistory,
            (int)BusinessUserPaymentAppPreferenceEnum.NewPaymentIsInitiated);
    }

        public PaymentRelatedDataDTO GetPaymentNotificationInfo(Guid paymentId, Guid tenantUserId, string AppKey) {
            return _qPaymentNotificationRecipientData.GetPaymentNotificationInfo(paymentId, tenantUserId, AppKey);
        }

        /// <summary>
        /// Get preauth payment detail.
        /// </summary>
        /// <param name="preAuthPaymentId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="AppKey"></param>
        /// <returns>return authorized payment information.</returns>
        public PreAuthPaymentRelatedDataDTO GetPreAuthPaymentNotificationInfo(Guid preAuthPaymentId, Guid tenantUserId, string AppKey) {
            return _qPaymentNotificationRecipientData.GetPreAuthPaymentNotificationInfo(preAuthPaymentId, tenantUserId, AppKey);
        }

        public List<NotificationRecipient> GetCustomerUserPaymentAppUsers(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId) {
          return _qPaymentNotificationRecipientData.GetCustomerUserPaymentAppUsers(appId, businessTenantId, businessPartnerTenantId,
            (int)CustomerUserPaymentAppPermissionEnum.AllowPaymentActivities |
            (int)CustomerUserPaymentAppPermissionEnum.AccessTransactionHistory, (int)BusinessUserPaymentAppPreferenceEnum.NewPaymentIsInitiated);
        }

        public List<NotificationRecipient> GetPaymentAppCustomerUserOnRefundForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId) {
          return _qPaymentNotificationRecipientData.GetPaymentAppCustomerUserOnRefundForCust(appId, businessTenantId, businessPartnerTenantId ,
          (int)CustomerUserPaymentAppPermissionEnum.AllowPaymentActivities |
          (int)CustomerUserPaymentAppPermissionEnum.AccessTransactionHistory, (int)CustomerUserPaymentAppPreferenceEnum.RefundIsInitiated);
        }

        /// <summary>
        /// Get customer payment notification preference.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant Id</param>
        /// <param name="businessPartnerTenantId">Business partner Tenant id(customer tenant id)</param>
        /// <returns></returns>
        public List<NotificationRecipient> GetPaymentAppCustomerUserOnPaymentForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId) {
            return _qPaymentNotificationRecipientData.GetPaymentAppCustomerUserOnPaymentForCust(appId, businessTenantId, businessPartnerTenantId,
            (int)CustomerUserPaymentAppPermissionEnum.AllowPaymentActivities |
            (int)CustomerUserPaymentAppPermissionEnum.AccessTransactionHistory, (int)BusinessUserPaymentAppPreferenceEnum.NewPaymentIsInitiated);
        }

        /// <summary>
        /// Get customer payment notification preference.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant Id</param>
        /// <param name="businessPartnerTenantId">Business partner Tenant id(customer tenant id)</param>
        /// <returns></returns>
        public List<NotificationRecipient> GetAdvancePaymentAppCustomerUserOnPaymentForCust(Guid appId, Guid businessTenantId, Guid businessPartnerTenantId, CustomerUserPaymentAppPreferenceEnum prefEnum) {
            return _qPaymentNotificationRecipientData.GetPaymentAppCustomerUserOnPaymentForCust(appId, businessTenantId, businessPartnerTenantId,
            (int)CustomerUserPaymentAppPermissionEnum.ManagePaymentInfo | (int)CustomerUserPaymentAppPermissionEnum.AllowPaymentActivities |
            (int)CustomerUserPaymentAppPermissionEnum.AccessTransactionHistory, (int)prefEnum);
        }

        /// <summary>
        /// Get business user to notify on payment.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant Id</param>        
        /// <returns></returns>
        public List<NotificationRecipient> GetPaymentAppBusinessUserOnPaymentForBusiness(Guid appId, Guid businessTenantId) {
            return _qPaymentNotificationRecipientData.GetPaymentAppBusinessUserOnPaymentForBusiness(appId, businessTenantId, 
            (int)BusinessUserPaymentAppPermissionEnum.ManageBusinessPaymentInfo |
            (int)BusinessUserPaymentAppPermissionEnum.AccessTransactionHistory, (int)BusinessUserPaymentAppPreferenceEnum.NewPaymentIsInitiated);
        }

        /// <summary>
        /// Get business user to notify on payment.
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="businessTenantId">Business tenant Id</param>        
        /// <returns></returns>
        public List<NotificationRecipient> GetAdvanceAndPreAuthPaymentAppBusinessUserOnPaymentForBusiness(Guid appId, Guid businessTenantId, BusinessUserPaymentAppPreferenceEnum prefEnum) {
            return _qPaymentNotificationRecipientData.GetPaymentAppBusinessUserOnPaymentForBusiness(appId, businessTenantId,
            (int)BusinessUserPaymentAppPermissionEnum.ManageBusinessPaymentInfo | (int)BusinessUserPaymentAppPermissionEnum.ManageBusinessPaymentInfo |
            (int)BusinessUserPaymentAppPermissionEnum.AccessTransactionHistory, (int)prefEnum);
        }

    }
}

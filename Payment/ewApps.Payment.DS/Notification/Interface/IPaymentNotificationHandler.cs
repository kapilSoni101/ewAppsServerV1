using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.NotificationService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.DS {

    public interface IPaymentNotificationHandler {

        Task GenerateTransactionVoidRequestedNotification(PaymentNotificationDTO paymentNotificationDTO);

        Task GenerateTransactionRefundRequestedNotification(PaymentNotificationDTO paymentNotificationDTO);

        Task GenerateTransactionStatusChangedNotification(PaymentNotificationDTO paymentNotificationDTO);

        Task GenerateTransactionVoidRequestedNotificationForCust(PaymentNotificationDTO paymentNotificationDTO);

        Task GenerateTransactionStatusChangedNotificationForCust(PaymentNotificationDTO paymentNotificationDTO);

        Task GenerateTransactionRefundRequestedNotificationForCust(PaymentNotificationDTO paymentNotificationDTO);

        Task GenerateTransactionInitiateRequestedNotificationForCust(PaymentNotificationDTO paymentNotificationDTO);

        #region Payment

    /// <summary>
    /// Send payment notification email to business user.
    /// </summary>
    /// <param name="paymentNotificationDTO">Payment notification dto</param>
    /// <param name="moduleType">Current logged in module</param>
    /// <returns></returns>
    Task GeneratePaymentNotificationForBusinessAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Send payment notification email to customer.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification dto</param>
        /// <param name="moduleType">Current logged in module</param>
        /// <returns></returns>
    //    Task GeneratePaymentNotificationForCustomerAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Send payment notification email to business user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification dto</param>
        /// <param name="moduleType">Current logged in module</param>
        /// <returns></returns>
        Task GenerateBizAdvancedPaymentNotificationForBusinessAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Generate the notification object and send the payment notification email to subscribed customer user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Task GenerateBizAdvancedPaymentNotificationForCustomerAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Generate the notification object and send the notification email to all subscribed business user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task GeneratePreAuthPaymentNotificationForBusinessAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Generate the notification object and send the payment notification email to subscribed customer user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task GeneratePreAuthPaymentNotificationForCustomerAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken));

        #endregion Payment

        #region Advance Payment


        /// <summary>
        /// Generate the notification object and send the notification email to all subscribed business user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Task GenerateCustAdvancedPaymentNotificationForBusinessAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Generate the notification object and send the payment notification email to subscribed customer user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Task GenerateCustAdvancedPaymentNotificationForCustomerAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken));

        #endregion Advance Payment
    }
}

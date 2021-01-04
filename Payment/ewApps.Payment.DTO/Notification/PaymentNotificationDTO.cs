using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.UserSessionService;

namespace ewApps.Payment.DTO {
    public class PaymentNotificationDTO {

        public Guid LoginUserTenantUserId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        public string PublisherCompanyName {
            get; set;
        }

        public string BusinessCompanyName {
            get; set;
        }

        public string CustomerCompanyName {
            get; set;
        }

        public string CustomerCompanyId {
            get; set;
        }

        public string SubDomain {
            get; set;
        }

        public string CopyRightText {
            get; set;
        }

        public string PortalURL {
            get; set;
        }

        public string UserFullName {
            get; set;
        }

        public string UserId {
            get; set;
        }

        public string TransactionId {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public string ActionDate {
            get; set;
        }

        public string TransactionAmountWithCurrency {
            get; set;
        }

        public string TransactionStatus {
            get; set;
        }

        public string TransactionDate {
            get; set;
        }

        public UserSession UserSessionInfo {
            get; set;
        }

        public Guid PaymentId {
            get; set;
        }

        public string NewTransactionStatus {
            get; set;
        }

        public string OldTransactionStatus {
            get; set;
        }

        public string BusinessPartnerTenantId {
            get; set;
        }

        public string PaymentApplicationName {
            get; set;
        }

        public string TransactionService {
            get; set;
        }

        public string TransactionMode {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        public string CustomerId {
            get; set;
        }

        public string PaidByUserIdentityNumber {
            get; set;
        }

        public string PaidByUserName {
            get; set;
        }

        public string AccountNumber {
            get; set;
        }

        public List<InvoicePaymentDTO> InvoicePaymentList {
            get; set;
        }

        public string UserTypeText {
            get; set;
        }


        public string TimeZone {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }


    }

}

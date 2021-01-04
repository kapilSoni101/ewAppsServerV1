using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class PaymentRelatedDataDTO {

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
        public string UserFullName {
            get; set;
        }
        public string UserId {
            get; set;
        }
        public string IdentityNumber {
            get; set;
        }

        public string TransactionId {
            get; set;
        }
        public decimal TransactionAmount {
            get; set;
        }
        public DateTime? TransactionDate {
            get; set;
        }
        public string Currency {
            get; set;
        }
        public string CustomerCurrency {
            get; set;
        }
        public Guid? BusinessPartnerTenantId {
            get; set;
        }

        public string PaymentApplicationName {
            get; set;
        }

        public string TransactionStatus {
            get; set;
        }

        public string AccountNumber {
            get; set;
        }

        public string TransactionService {
            get; set;
        }

        public string TransactionMode {
            get; set;
        }

        public string CustomerRefId {
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

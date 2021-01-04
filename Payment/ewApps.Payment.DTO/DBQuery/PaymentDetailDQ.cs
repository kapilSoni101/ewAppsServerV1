/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra
 * Date: 10 Nov 2018
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// Contains payment detail.
    /// Contains all payment history with respact to invoice.
    /// </summary>
    public class PaymentDetailDQ {

        public Guid Id {
            get; set;
        }

        public Guid PaymentId {
            get; set;
        }

        public decimal AmountPaid {
            get; set;
        }

        public decimal AmountPaidFC {
            get; set;
        }

        public string Status {
            get; set;
        }

        public DateTime? PaymentDate {
            get; set;
        }

        public string Note {
            get; set;
        }

        /// <summary>
        /// Unique payment identity number.
        /// </summary>
        public string PaymentIdentityNo {
            get; set;
        }

        /// <summary>
        /// Number of invoice paid for a payment.
        /// </summary>
        public int TotalInvoice {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        public string CustomerId {
            get; set;
        }
        public int DocumentCurrencyCode {
            get; set;
        }
        public decimal FinalConversionRate {
            get; set;
        }

        /// <summary>
        /// Business currency.
        /// </summary>
        public string LocalCurrency {
            get; set;
        }

        public string PayServiceName {
            get; set;
        }

        public string PayServiceAttributeName {
            get;set;
        }

        /// <summary>
        /// customer currency
        /// </summary>
        public string CustomerCurrency {
            get; set;
        }

        public string PayeeName {
            get;set;
        }

        public string CustomerAccountNumber {
            get;set;
        }

        /// <summary>
        /// Payment currency.
        /// </summary>
        public string PaymentTransectionCurrency {
            get; set;
        }

        /// <summary>
        /// Payment can be Advance, Invoice
        /// </summary>
        public int PaymentType {
            get; set;
        }

        [NotMapped]
        public decimal CalculateAmount {
            get; set;
        }

        [NotMapped]
        public bool IntegratedMode {
            get;set;
        }
    }
}

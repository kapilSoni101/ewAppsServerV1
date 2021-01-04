/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 29 October 2019
 * 
 * Contributor/s: 
 * Last Updated On: 29 October 2019
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Contains payment detail.
    /// Contains all authorized payment history.
    /// </summary>
    public class PreAuthPaymentDetailDTO {

        public Guid Id {
            get; set;
        }

        public decimal AmountPaid {
            get; set;
        }

        public decimal AmountPaidFC {
            get; set;
        }

        public decimal RemainingAmount {
            get; set;
        }

        public decimal RemainingAmountFC {
            get; set;
        }

        public string Status {
            get; set;
        }

        public string Captured {
            get; set;
        }

        public int CurrentPaymentSequenceNumber {
            get; set;
        }

        public int MaxTotalPaymentCount {
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

        public Guid CustomerId {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        public string NameOnCard {
            get;set;
        }

        public string ERPCustomerKey {
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
            get; set;
        }

        /// <summary>
        /// customer currency
        /// </summary>
        public string CustomerCurrency {
            get; set;
        }

        public string PayeeName {
            get; set;
        }

        public string CustomerAccountNumber {
            get; set;
        }

        /// <summary>
        /// Payment currency.
        /// </summary>
        public string PaymentTransectionCurrency {
            get; set;
        }

    }
}

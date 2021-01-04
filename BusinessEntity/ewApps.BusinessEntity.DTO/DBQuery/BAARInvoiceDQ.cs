/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// Invoice list details Data trasnfer object.
    /// </summary>
    public class BAARInvoiceDQ: BaseDQ {

        /// <summary>
        /// Unique id of invoice.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Auto increamental number of customer .its must be unique.
        /// CustomerRefId => CustomerRefNo
        /// </summary>
        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// Unique id of customer
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// invoice name
        /// </summary>
        public string InvoiceName {
            get; set;
        }

        /// <summary>
        /// invoice customer name
        /// </summary>
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// Invoice number InvoiceNo => TrackingNo 
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        /// <summary>
        /// Invoice payment due date
        /// </summary>
        public DateTime? DueDate {
            get; set;
        }

        /// <summary>
        /// Total  amount of invoice
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Total amount(Foreighn currency) of invoice
        /// </summary>
        public decimal TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Invoice partial paid amount
        /// </summary>
        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Invoice status
        /// </summary>
        public bool Status {
            get; set;
        }

        /// <summary>
        /// Invoice created date
        /// </summary>
        public new DateTime? CreatedOn {
            get; set;
        }

        /// <summary>
        /// There will be 3 type of payment status
        /// 1 means: Due
        /// 2 means: Partial
        /// 3 means: Paid
        /// </summary>
        public int PaymentStatus {
            get; set;
        }

        /// <summary>
        /// Amount remains to pay
        /// </summary>
        public decimal OutstandingAmount {
            get; set;
        }

        /// <summary>
        /// Updated date of invoice
        /// </summary>
        public new DateTime? UpdatedOn {
            get; set;
        }

        /// <summary>
        /// Update Full Name
        /// </summary>
        public string UpdatedByFullName {
            get; set;

        }

        /// <summary>
        /// LocalCurrency
        /// </summary>
        public string LocalCurrency {
            get; set;

        }

        /// <summary>
        /// business currency code.
        /// </summary>
        public int DocumentCurrencyCode {
            get; set;
        }

        /// <summary>
        /// customer currency code.
        /// </summary>
        public int AgentCurrencyCode {
            get; set;
        }

        /// <summary>
        /// Currency conversion rate FromCurrency to ToCurrency.
        /// </summary>
        public decimal FinalConversionRate {
            get; set;
        }

    }

}

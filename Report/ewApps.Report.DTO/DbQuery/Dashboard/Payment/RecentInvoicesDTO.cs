﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 29 August 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="RecentInvoicesDTO"/> .
    /// </summary>
    public class RecentInvoicesDTO:BaseDTO {

        /// <summary>
        /// Unique id of invoice.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Auto increamental number of customer .its must be unique.
        /// </summary>
        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// Unique customer referenc ekey for ERP.
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// Unique id of customer
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// invoice key
        /// </summary>
        public string ERPARInvoiceKey {
            get; set;
        }

        /// <summary>
        /// invoice customer name
        /// </summary>
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// TrackingNo number
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        /// <summary>
        /// Total  amount of invoice
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Invoice total payment due
        /// </summary>
        public decimal TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Balance due to pay. It is changable as per amount pay.
        /// </summary>
        public decimal BalanceDue {
            get; set;
        }

        /// <summary>
        /// Balance in customer currency to pay. It is changable as per amount pay.
        /// </summary>
        public decimal BalanceDueFC {
            get; set;
        }

        /// <summary>
        /// Amount paid.
        /// </summary>
        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Amount paid in customer currency.
        /// </summary>
        public decimal AmountPaidFC {
            get; set;
        }

        /// <summary>
        /// Business currency.
        /// </summary>
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// customer currency
        /// </summary>
        public string CustomerCurrency {
            get; set;
        }

        /// <summary>
        /// Invoice status
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Invoice status
        /// </summary>
        public string StatusText {
            get; set;
        }

        ///// <summary>
        ///// There will be 3 type of payment status
        ///// 1 means: Due
        ///// 2 means: Partial
        ///// 3 means: Paid
        ///// </summary>
        //public int PaymentStatus {
        //    get; set;
        //}

        /// <summary>
        /// Invoice created date
        /// </summary>
        public new DateTime? CreatedOn {
            get; set;
        }

        /// <summary>
        /// Updated date of invoice
        /// </summary>
        public new DateTime? UpdatedOn {
            get; set;
        }

        /// <summary>
        /// invoice creation date.
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Update Full Name
        /// </summary>
        public string UpdatedByFullName {
            get; set;

        }

        /// <summary>
        /// Notes on invoice.
        /// </summary>
        public string Remarks {
            get; set;
        }

        public string ShippingTypeText {
            get; set;
        }

        public int TotalLineItems {
            get; set;
        }

        /// <summary>
        /// system generated invoice Id .
        /// </summary>
        public string InvoiceNo {
            get; set;
        }

        public DateTime? DueDate {
            get; set;
        }

        ///// <summary>
        ///// system generated payment Id .
        ///// </summary>
        //public new Guid ID {
        //    get; set;
        //}

        ///// <summary>
        ///// Id of Customer .
        ///// </summary>
        //public Guid CustomerId {
        //    get; set;
        //}

        ///// <summary>
        ///// system generated invoice Id .
        ///// </summary>
        //public string InvoiceNo {
        //    get; set;
        //}

        ///// <summary>
        ///// name of the customer.
        ///// </summary>
        //public string CustomerName {
        //    get; set;
        //}

        ///// <summary>
        ///// Payment creation date and time (in UTC).
        ///// </summary>
        //public DateTime PostingDate {
        //    get; set;
        //}

        ///// <summary>
        ///// Payment Due date and time (in UTC).
        ///// </summary>
        //public DateTime? DueDate {
        //    get; set;
        //}


        ///// <summary>
        ///// total amount due.
        ///// </summary>
        //public Decimal TotalPaymentDue {
        //    get; set;
        //}

        ///// <summary>
        ///// Payment creation date and time (in UTC).
        ///// </summary>
        //public int TotalLineItems {
        //    get; set;
        //}

        ///// <summary>
        ///// Payment status.
        ///// </summary>
        //public string StatusText {
        //    get; set;
        //}
    }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="PartInvoiceReportDTO"/> .
    /// </summary>
    public class PartInvoiceReportDTO :BaseDTO {

        /// <summary>
        /// System generated unique invoice id.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// System generated unique invoice No.
        /// </summary>
        public string InvoiceNo {
            get; set;
        }


        /// <summary>
        /// The CustomerName.
        /// </summary>
        /// <remarks>It's empty if not applicable.</remarks>
        public string CustomerName {
            get; set; 
        }

        /// <summary>
        /// Invoice TotalPaymentDue.
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Invoice BalanceDue.
        /// </summary>
        public decimal BalanceDue {
            get; set;
        }

        /// <summary>
        /// Invoice Due date and time (in UTC).
        /// </summary>
        public DateTime? DueDate {
            get; set;
        }

        /// <summary>
        /// Invoice Posting date and time (in UTC).
        /// </summary>
        public DateTime? PostingDate {
            get; set;
        }

        /// <summary>
        /// Invoice Posting date and time (in UTC).
        /// </summary>
        public DateTime? DocumentDate {
            get; set;
        }

        /// <summary>
        /// Shipping Type.
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Shipping Type.
        /// </summary>
        public string Remarks {
            get; set;
        }


        /// <summary>
        /// Invoice Status.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Invoice TotalLineItems.
        /// </summary>
        public int TotalLineItems {
            get; set;
        }

        /// <summary>
        /// Deleted invoice .
        /// </summary>
        public bool Deleted {
            get; set;
        }

        /// <summary>
        /// Unique Number of Entity
        /// </summary>
        public string ERPDocNum {
            get; set;
        }


        /// <summary>
        /// Invoice Status According to Deleted,Paid OR Partially paid   
        /// </summary>        
        public string StatusText {
            get; set;
        }
    }
}

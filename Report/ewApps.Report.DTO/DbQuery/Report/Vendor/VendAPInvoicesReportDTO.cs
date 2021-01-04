/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 11 Nov 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 11 Nov 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="VendAPInvoicesReportDTO"/> .
    /// </summary>
    public class VendAPInvoicesReportDTO {

        /// <summary>
        /// System generated unique invoice id.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// System generated unique invoice No.
        /// </summary>
        public string ERPApInvoiceKey {
            get; set;
        }

        /// <summary>
        /// System generated unique invoice No.
        /// </summary>
        public string ERPDocNum {
            get; set;
        }


        /// <summary>
        /// The Vendor Name.
        /// </summary>
        /// <remarks>It's empty if not applicable.</remarks>
        public string VendorName {
            get; set;
        }

        /// <summary>
        /// Id of Vendor.
        /// </summary>
        /// <remarks>It's empty if not applicable.</remarks>
        public string ERPVendorKey {
            get; set;
        }

        /// <summary>
        /// Invoice TotalPaymentDue.
        /// </summary>
        public decimal TotalPaymentDue {
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
        /// Deleted invoice .
        /// </summary>
        public bool Deleted {
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

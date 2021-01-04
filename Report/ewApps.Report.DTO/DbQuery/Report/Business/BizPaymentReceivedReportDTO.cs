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
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="BizPaymentReceivedReportDTO"/>.
    /// </summary>
    public class BizPaymentReceivedReportDTO :BaseDTO {

        /// <summary>
        /// System generated unique payment id.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// System generated unique payment number.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// Payment Status.
        /// </summary>
        public string Status {
            get; set;
        }


        /// <summary>
        /// Payment Services.
        /// </summary>
        public string Service {
            get; set;
        }

        /// <summary>
        /// Payment Mode or Payment Type like COD,Card,Internet Banking.
        /// </summary>
        public string Type {
            get; set;
        }

        /// <summary>
        /// Payment Paid date and time (in UTC).
        /// </summary>
        public DateTime PaidOn {
            get; set;
        }

        /// <summary>
        /// Paid AMount.
        /// </summary>
        public decimal CreditAmount {
            get; set;
        }

        /// <summary>
        /// System generated unique invoice no as Invoice id .
        /// </summary>
        public string InvoiceId {
            get; set;
        }

        /// <summary>
        /// name of invoice.
        /// </summary>
        public string InvoiceName {
            get; set;
        }

        /// <summary>
        /// name of invoice.
        /// </summary>
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// name of invoice.
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// payment due date  .
        /// </summary>
        public DateTime? DueDate {
            get; set;
        }

        /// <summary>
        /// Amount According to Invoice.
        /// </summary>
        public decimal OriginalAmount {
            get; set;
        }        

        /// <summary>
        /// Payment Deleted or not.
        /// </summary>
        public bool Deleted {
            get; set;
        }

        /// <summary>
        /// Payment Status According to Deleted Payment and Status Value .
        /// </summary>
        private string _statusText;
        public string StatusText {
            get {
                if(Deleted) {
                    _statusText = "Deleted";
                }
                else if(Status == "A") {
                    _statusText = "Pending";
                }
                else if(Status == "B") {
                    _statusText = "Originated";
                }
                else if(Status == "R") {
                    _statusText = "Returned";
                }
                else {
                    _statusText = "Settled";
                }

                return _statusText;
            }
            private set {
                _statusText = value;
            }
        }

    }
}

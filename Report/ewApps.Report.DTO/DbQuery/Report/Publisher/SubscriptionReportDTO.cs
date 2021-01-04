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
    /// This class is a DTO with consolidate information of <see cref="SubscriptionReportDTO"/> .
    /// </summary>
    public class SubscriptionReportDTO:BaseDTO {

        /// <summary>
        /// System generated unique subscription id.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// System generated subscription number.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// name of the subscription.
        /// </summary>
        public string SubscriptionName {
            get; set;
        }


        /// <summary>
        /// subscription term.
        /// </summary>
        public int Term {
            get; set;
        }

        /// <summary>
        /// Term Unit
        /// </summary>
        public int TermUnit {
            get; set;
        }


        /// <summary>
        /// payment cycle.
        /// </summary>
        public int PaymentCycle {
            get; set;
        }

        /// <summary>
        /// price in dollar.
        /// </summary>
        public Decimal PriceInDollar {
            get; set;
        }

        /// <summary>
        /// name of the application.
        /// </summary>
        public string Application {
            get; set;
        }

        /// <summary>
        /// total no of subscription tenat.
        /// </summary>
        public int Tenants {
            get; set;
        }

        /// <summary>
        /// active flag is to check subscription is active or not.
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// deleted flag is to check subscription is deleted or not.
        /// </summary>
        public new bool Deleted {
            get; set;
        }

        private string _status;
        public string Status {
            get {
                if(Deleted) {
                    _status = "Deleted";
                }
                else if(Active) {
                    _status = "Active";
                }
                else {
                    _status = "Inactive";
                }
                return _status;
            }
            private set {
                _status = value;
            }
        }
    }
}

/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Atul Badgujar <abadgujar @batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    public class PlatSubscriptionReportDTO:BaseDTO {

        public new Guid ID {
            get; set;
        } 

        public string IdentityNumber {
            get; set;
        }

        public string PlanName {
            get; set;
        }

        public decimal PriceInDollar {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public bool Deleted {
            get; set;
        }

        public int BusinessUserCount {
            get; set;
        }

        public int Term {
            get; set;
        }

        //Term unit 
        public int TermUnit {
            get; set;
        }

        public decimal TotalBilling {
            get; set;
        }

        public int CustomerUserCount {
            get; set;
        }

        public DateTime EndDate {
            get; set;
        }

        public string AppName {
            get; set;
        }       

        public int AppServiceCount {
            get; set;
        }

        public bool AutoRenewal {
            get; set;
        }

        public bool AllowUnlimitedShipment {
            get; set;
        }

        public int? UserPerCustomerCount {
            get; set;
        }

        public string PublisherName {
            get; set;
        }


        public string BusinessName {
            get; set;
        }

        public new DateTime CreatedOn {
            get; set;
        }

    }
}


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

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Business Application model for editing the business tenant information.
    /// </summary>
    public class TenantAppSubscriptionDQ: BaseDQ {

        // Application name.
        public string Name {
            get; set;
        }

        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Application id for which a tenant subscribe.
        /// </summary>
        public Guid AppId {
            get; set;
        }

        public Guid ThemeId {
            get; set;
        }
        
        /// <summary>
        /// Number of users can registerd to use application by that business tenantid.
        /// </summary>
        public int BusinessUserCount {
            get; set;
        }

        public int Term {
            get; set;
        }

        /// <summary>
        /// Contains the subscription plan id.
        /// </summary>
        public Guid SubscriptionPlanId {
            get; set;
        }

        public bool AutoRenewal {
            get; set;
        }

        public int PlanSchedule {
            get; set;
        }
        public double PriceInDollar {
            get; set;
        }
        public int GracePeriodInDays {
            get; set;
        }

        public DateTime SubscriptionStartDate {
            get; set;
        }

        public DateTime SubscriptionStartEnd {
            get; set;
        }

        public int AlertFrequency {
            get; set;
        }

        public int PaymentCycle {
            get; set;
        }

        public bool CustomizeSubscription {
            get; set;
        }

        /// <summary>
        /// State of application whether application is active/in-active.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// InactiveComment for application in-active.
        /// </summary>    
        public string InactiveComment {
            get;
            set;
        }


        public bool OneTimePlan {
            get; set;
        }

        public int? UserPerCustomerCount {
            get; set;
        }

        public int? ShipmentCount {
            get; set;
        }

        public int? ShipmentUnit {
            get; set;
        }

    }
}

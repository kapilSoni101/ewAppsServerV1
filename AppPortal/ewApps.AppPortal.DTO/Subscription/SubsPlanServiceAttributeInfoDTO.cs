/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam<anigam@eworkplaceapps.com>
 * Date:07 july 2019
 * 
 */

using System;

namespace ewApps.AppPortal.DTO {
    public class SubsPlanServiceAttributeInfoDTO {

        public Guid SubsPlanServiceAttributeId {
            get; set;
        }

        public Guid SubscriptionPlanId {
            get; set;
        }

        public Guid SubscriptionPlanServiceId {
            get; set;
        }

        public Guid AppServiceAttributeId {
            get; set;
        }

        public string AttributeName {
            get; set;
        }

        public bool Active {
            get; set;
        }


    }
}

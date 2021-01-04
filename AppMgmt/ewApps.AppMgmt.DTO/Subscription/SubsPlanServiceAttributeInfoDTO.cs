/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam<anigam@eworkplaceapps.com>
 * Date:07 july 2019
 * 
 */

using System;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DTO {
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


        //public static SubsPlanServiceAttributeInfoDTO MapFrom(SubscriptionPlanServiceAttribute subscriptionPlanServiceAttribute) {
        //    SubsPlanServiceAttributeInfoDTO subsPlanServiceAttributeInfoDTO = new SubsPlanServiceAttributeInfoDTO();
        //    subsPlanServiceAttributeInfoDTO.SubscriptionPlanId = subscriptionPlanServiceAttribute.SubscriptionPlanId;
        //    subsPlanServiceAttributeInfoDTO.SubscriptionPlanServiceId = subscriptionPlanServiceAttribute.SubscriptionPlanServiceId;
        //    subsPlanServiceAttributeInfoDTO.AppServiceAttributeId = subscriptionPlanServiceAttribute.AppServiceAttributeId;
        //    return subsPlanServiceAttributeInfoDTO;
        //}

    }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam<anigam@eworkplaceapps.com>
 * Date:07 july 2019
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.AppPortal.DTO {

    public class SubsPlanServiceInfoDTO {

        public Guid SubscriptionPlanServiceId {
            get; set;
        }

        public Guid SubscriptionPlanId {
            get; set;
        }

        public Guid AppServiceId {
            get; set;
        }

        public string ServiceName {
            get; set;
        }

        public bool Active {
            get; set;
        }

        [NotMapped]
        public List<SubsPlanServiceAttributeInfoDTO> ServiceAttributeList {
            get; set;
        }

    }
}

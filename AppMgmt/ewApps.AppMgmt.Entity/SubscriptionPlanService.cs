/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam<anigam@eworkplaceapps.com>
 * Date:07 july 2019
 * 
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Entity {

  [Table("SubscriptionPlanService", Schema = "am")]
  public class SubscriptionPlanService:BaseEntity {

    [Required]
    public Guid SubscriptionPlanId {
      get; set;
    }

    [Required]
    public Guid AppServiceId {
    get; set;
    }


}
}

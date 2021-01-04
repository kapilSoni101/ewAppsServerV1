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
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.BaseService {

    public class BaseDQ: BaseEntity {

    [NotMapped]
    public override Guid ID {
      get ; set ;
    }

    [NotMapped]
    public override Guid CreatedBy {
      get ; set ;
    }

    [NotMapped]
    public override DateTime? CreatedOn {
      get ; set ;
    }

    [NotMapped]
    public override Guid UpdatedBy {
      get ; set ;
    }

    [NotMapped]
    public override DateTime? UpdatedOn {
      get ; set ;
    }

    [NotMapped]
    public override bool Deleted {
      get ; set ;
    }

    [NotMapped]
    public override Guid TenantId {
      get ; set ;
    }

  }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ewApps.Core.BaseService {

    public class BaseEntity : ISystemEntityField {

    [Key]
    public virtual Guid ID
    {
      get; set;
    }

    public virtual Guid CreatedBy
    {
      get; set;
    }

    public virtual DateTime? CreatedOn
    {
      get; set;
    }

    public virtual Guid UpdatedBy
    {
      get; set;
    }

    public virtual DateTime? UpdatedOn
    {
      get; set;
    }

    public virtual bool Deleted
    {
      get; set;
    }

    public virtual Guid TenantId
    {
      get; set;
    }

    // Hari Dudani
    public override string ToString() {
      return JsonConvert.SerializeObject(this);
    }
  }

  //public class BaseDTO : BaseEntity {
  //  [NotMapped]
  //  public override Guid CreatedBy
  //  {
  //    get => base.CreatedBy;
  //    set => base.CreatedBy = value;
  //  }

  //  [NotMapped]
  //  public override DateTime? CreatedOn
  //  {//}
  //    get => base.CreatedOn;
  //    set => base.CreatedOn = value;
  //  }
  
}

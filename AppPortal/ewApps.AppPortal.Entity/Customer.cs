/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 08 August 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {

  [Table("Customer", Schema = "ap")]
  public class Customer:BaseEntity {


    /// <summary>
    /// 
    /// </summary>
    public Guid BusinessPartnerTenantId {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool CanUpdateCurrency {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool Configured {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Currency {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string IdentityNumber {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string DateTimeFormat {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int DecimalPrecision {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string DecimalSeperator {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string GroupSeperator {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string GroupValue {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Language {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string TimeZone {
      get; set;
    }

  }
}

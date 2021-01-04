/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 5 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 5 February 2019
 */


using System;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="PlatPublisherReportDTO"/> .
    /// </summary>
    public class PlatPublisherReportDTO : BaseDTO {

    // <summary>
    /// Identity number.
    /// </summary>
    public new Guid ID {
      get; set;
    }
   

    /// <summary>
    /// Identity number.
    /// </summary>
    public string CreatedByName {
      get; set;
    }

    /// <summary>
    /// Identity number.
    /// </summary>
    public new DateTime? CreatedOn {
      get; set;
    }

    

    /// <summary>
    /// 
    /// </summary>
    public new bool Deleted {
      get; set;
    }

    /// <summary>
    /// Identity number.
    /// </summary>
    public string IdentityNumber {
      get; set;
    }

    /// <summary>
    /// The name  of Publisher.
    /// </summary>
    public string Name {
      get; set;
    }   

    /// <summary>
    /// Publisher Inactive Comment
    /// </summary>
    public int ApplicationCount {
      get;
      set;
    }

    /// <summary>
    /// Publisher Inactive Comment
    /// </summary>
    public int TenantCount {
      get;
      set;
    }

    /// <summary>
    /// Publisher Inactive Comment
    /// </summary>
    public int UserCount {
      get;
      set;
    }

    /// <summary>
    /// Publisher Active Tenant
    /// </summary>
    public int ActiveTenantCount {
      get;
      set;
    }

    /// <summary>
    /// Publisher Status
    /// </summary>
    public string Status {
      get;
      set;
    }
  }
}

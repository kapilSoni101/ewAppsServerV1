/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

  /// <summary>
  /// Contains support customer list values.
  /// </summary>
  public class SupportCustomerTicketDTO:BaseDTO {

    /// <summary>
    /// Ticket Id.
    /// </summary>
    public string TicketId {
      get; set;
    }
    /// <summary>
    /// Tenant Name.
    /// </summary>
    public string TenantName {
      get; set;
    }
    /// <summary>
    /// Customer Id.
    /// </summary>
    public string CustomerId {
      get; set;
    }
    /// <summary>
    /// Customer Name.
    /// </summary>
    public string CustomerName {
      get; set;
    }
    /// <summary>
    /// Title.
    /// </summary>
    public string Title {
      get; set;
    }
    /// <summary>
    /// Priority.
    /// </summary>
    public int Priority {
      get; set;
    }
    /// <summary>
    /// Assigned To.
    /// </summary>
    public string AssignedTo {
      get; set;
    }
    /// <summary>
    /// Modified On.
    /// </summary>
    public DateTime ModifiedOn {
      get; set;
    }
    /// <summary>
    /// Modified By.
    /// </summary>
    public string ModifiedBy {
      get; set;
    }
    /// <summary>
    /// Status.
    /// </summary>
    public int Status {
      get; set;
    }
  }
}

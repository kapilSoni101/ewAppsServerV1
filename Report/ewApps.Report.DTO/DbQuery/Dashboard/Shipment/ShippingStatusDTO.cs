//dbquery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 17 April 2019
 * 
 * Contributor/s: Sanjeev khanna 
 * Last Updated On: 17 April 2019
 */


using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {



    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="ShippingStatusDTO"/> .
    /// </summary>
    public class ShippingStatusDTO : BaseDTO {

    /// <summary>
    /// total no of ready to ship orders.
    /// </summary>
    public int PendingShipping {
      get; set;
    }

    /// <summary>
    /// total no of shipped order.
    /// </summary>
    public int TotalShipped {
      get; set;
    }

    
    
  }
}



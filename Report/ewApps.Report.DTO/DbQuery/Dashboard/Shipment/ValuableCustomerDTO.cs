//dbquery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 17 April 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 17 April 2019
 */


using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This DTO Provide 5 Most Valuable Customer Payment Information 
    /// </summary>
    public class ValuableCustomerDTO:BaseDTO {

  
    /// <summary>
    ///  Customer Name 
    /// </summary>
    public string Name {
      get; set;
    }

    /// <summary>
    ///  Quantity of Order
    /// </summary>
    public int Quantity {
      get; set;
    }

    /// <summary>
    ///  Total Amount Value 
    /// </summary>
    public decimal Value {
      get; set;
    }
  }
}




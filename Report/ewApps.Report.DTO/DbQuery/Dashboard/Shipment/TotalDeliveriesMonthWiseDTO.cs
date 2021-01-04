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
    /// This DTO Provide Month name and No of Delievers Added in that month 
    /// </summary>
    public class TotalDeliveriesMonthWiseDTO : BaseDTO {

    /// <summary>
    ///  No of Delievries Added in that month
    /// </summary>
    public int TotalDeliveriesinthisMonth {
      get; set;
    }
    /// <summary>
    ///  Month Name 
    /// </summary>
    public string MonthNames {
      get; set;
    }
  }
}


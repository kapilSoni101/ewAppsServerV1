//dbquery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 20 November 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 November 2018
 */

using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    /// <summary>
    /// This DTO Provide Month name and No of Business Added in that month 
    /// </summary>
    public class BusinessAddedCountAndMonthDTO:BaseDTO
  {

    /// <summary>
    ///  No of Business Added in that month
    /// </summary>
    public int TotalBusinessaddedinthisMonth
    {
      get; set;
    }
    /// <summary>
    ///  Month Name 
    /// </summary>
    public string MonthNames
    {
      get; set;
    }
  }
}

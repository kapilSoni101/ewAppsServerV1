/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 29 August 2019
 */

namespace ewApps.Report.DTO {

  /// <summary>
  /// This class is a DTO with consolidate information of <see cref="InoviceAndMonthNameDTO"/> .
  /// </summary>
  public class InoviceAndMonthNameDTO {

    /// <summary>
    /// total no of business added.
    /// </summary>
    public decimal TotalPaymentDoneinthisMonth
    {
      get; set;
    }

    /// <summary>
    /// name of month.
    /// </summary>
    public string MonthNames
    {
      get; set;
    }
  }
}

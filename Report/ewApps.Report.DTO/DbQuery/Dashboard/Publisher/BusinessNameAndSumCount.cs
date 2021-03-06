﻿//dbquery

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
    /// This class Contain Business Name and Sum of invoice 
    /// </summary>
    public class BusinessNameAndSumCount:BaseDTO
  {
    /// <summary>
    /// Business Name 
    /// </summary>
    public string BusinessName
    {
      get; set;
    }
    /// <summary>
    /// Sum of Invoice for Business
    /// </summary>
    public decimal TotalBusinessSum
    {
      get; set;
    }
  }
}

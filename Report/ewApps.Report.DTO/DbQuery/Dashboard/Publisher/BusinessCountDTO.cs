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

using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    /// <summary>
    /// This class Contain Business Count information 
    /// </summary>
    public class BusinessCountDTO:BaseDTO
  {
    /// <summary>
    /// Total No of Business
    /// </summary>
    public int NoofBusiness
    {
      get; set;
    }
    /// <summary>
    /// Application Name 
    /// </summary>
    public string Application
    {
      get; set;
    }
    /// <summary>
    /// Business Total Percentage
    /// </summary>
    [NotMapped]
    public float PercentageOfBusiness
    {
      get; set;
    }

  }
}

//dbquery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 06 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 06 February 2019
 */

using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class contain apppublisher count 
    /// </summary>
    public class PublisherTenantCountDTO :BaseDTO {
    
    /// <summary>
    /// No of Business in Publisher 
    /// </summary>
    public int NoOfBusiness {
      get; set;
    }
    /// <summary>
    /// Name of the Publisher
    /// </summary>
    public string Publisher {
      get; set;
    }
    /// <summary>
    /// Percentage of Tenants In publisher
    /// </summary>
    [NotMapped]
    public float PercentageOfBusiness {
      get; set;
    } 
  }
}

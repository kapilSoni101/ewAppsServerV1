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
    public class ApplicationPublisherCountDTO : BaseDTO {

    /// <summary>
    /// No of User in Application 
    /// </summary>
    public int NoOfPublisher {
      get; set;
    }
    /// <summary>
    /// Name of the Application
    /// </summary>
    public string Application {
      get; set;
    }
    /// <summary>
    /// Percentage of User In application
    /// </summary>
    [NotMapped]
    public float PercentageOfPublisher {
      get; set;
    }

  }
}

//DbQuery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 6 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 6 February 2019
 */


using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    /// <summary>
    /// This class Contain App And BUsiness Count 
    /// </summary>
    public class PlatAppAndBusinessCountDTO :BaseDTO {
    /// <summary>
    /// Total no of APplication 
    /// </summary>
    public int TotalApplications {
      get; set;
    }
    /// <summary>
    /// Total no of Active APplication 
    /// </summary>
    public int ActiveApplications {
      get; set;
    }
    /// <summary>
    /// Total no of InActive APplication 
    /// </summary>
    public int InActiveApplications {
      get; set;
    }
    /// <summary>
    /// Total no of Business 
    /// </summary>
    public int TotalBusiness {
      get; set;
    }
    /// <summary>
    /// Total no of Business with APplication 
    /// </summary>
    public int BusinessWithApplication {
      get; set;
    }
    /// <summary>
    /// Total no of Business without APplication 
    /// </summary>
    public int BusinessWithOutApplication {
      get; set;
    }

    // <summary>
    /// Total no of Publisher in APplication 
    /// </summary>
    public int TotalPublisher {
      get; set;
    }

  }
}

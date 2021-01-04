// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 5 February 2019
 */


using System;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="PlatApplicationReportDTO"/> .
    /// </summary>
    public class PlatApplicationReportDTO :BaseDTO {

    /// <summary>
    /// System generated unique application id.
    /// </summary>
    public new Guid ID {
      get; set;
    }

    /// <summary>
    /// System generated unique application number.
    /// </summary>
    public string IdentityNumber {
      get; set;
    }

    /// <summary>
    /// name of the application .
    /// </summary>
    public string ApplicationName {
      get; set;
    }    

    /// <summary>
    /// no of subscription .
    /// </summary>
    public Int32 ServiceCount {
      get; set;
    }

    /// <summary>
    /// no of publisher .
    /// </summary>
    public Int32 PublisherCount {
      get; set;
    }


    /// <summary>
    /// no of tenants .
    /// </summary>
    public Int32 TenantCount {
      get; set;
    }

    /// <summary>
    /// it indicate application is active or not.
    /// </summary>
    public bool Active {
      get; set;
    }

    /// <summary>
    /// it indicate application is deleted or not.
    /// </summary>
    public new bool Deleted {
      get; set;
    }

    private string _status;
    public string Status {
      get {
        if (Deleted) {
          _status = "Deleted";
        }
        else if (Active) {
          _status = "Active";
        }
        else {
          _status = "Inactive";
        }
        return _status;
      }
      private set {
        _status = value;
      }
    }

  }
}

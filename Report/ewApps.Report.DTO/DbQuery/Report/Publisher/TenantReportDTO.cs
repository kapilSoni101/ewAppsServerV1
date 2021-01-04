/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */

using System;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    public class TenantReportDTO :BaseDTO {
    public new Guid ID {
      get; set;
    }
    
    public string IdentityNumber {
      get; set;
    }
    public string TenantName {
      get; set;
    }

    public string CreatedBy {
      get; set;
    }

    public DateTime CreatedOn {
      get; set;
    }

    public int Applications {
      get; set;
    }

    public int Subscriptions {
      get; set;
    }

    public DateTime? JoinedDate {
      get; set;
    }

    public int Services {
      get; set;
    }

    public int Users {
      get; set;
    }

    public bool Active
    {
      get; set;
    }

    public new bool Deleted
    {
      get; set;
    }

    private string _status;
    public string Status
    {
      get
      {
        if (Deleted)
        {
          _status = "Deleted";
        }
        else if (Active)
        {
          _status = "Active";
        }
        else
        {
          _status = "Inactive";
        }
        return _status;
      }
      private set
      {
        _status = value;
      }
    }

  }
}

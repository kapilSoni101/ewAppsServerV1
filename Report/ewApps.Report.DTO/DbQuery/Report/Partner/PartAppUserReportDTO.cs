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

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="PartAppUserReportDTO"/> .
    /// </summary>
    public class PartAppUserReportDTO:BaseDTO {

    /// <summary>
    /// System generated unique appuser id.
    /// </summary>
    public new Guid ID {
      get; set;
    }

    /// <summary>
    /// name of user .
    /// </summary>
    public string UserName {
      get; set;
    }

    /// <summary>
    /// Name of the owner who invited User 
    /// </summary>
    public string InvitedBy {
      get; set;
    }

    /// <summary>
    /// User invitation date and time (in UTC).
    /// </summary>
    public DateTime InvitedOn {
      get; set;
    }

    /// <summary>
    /// User joining date and time (in UTC).
    /// </summary>
    public DateTime? JoinedOn {
      get; set;
    }

    /// <summary>
    /// User role.
    /// </summary>
    public bool Role {
      get; set;
    }

    /// <summary>
    /// total User permission bit mask value 
    /// </summary>
    public Int64 PermissionBitMask {
      get; set;
    }

    /// <summary>
    /// Total Number of permission user have 
    /// </summary>
    public int Permissions {
      get; set;
    }

    /// <summary>
    /// Active flag indicate user is active or not 
    /// </summary>
    public bool Active {
      get; set;
    }

    /// <summary>
    /// Active flag indicate user is deleted or not 
    /// </summary>
    public new bool Deleted {
      get; set;
    }

    /// <summary>
    /// Business Partner TenantId.
    /// </summary>
    public Guid? BusinessPartnerTenantId {
      get; set;
    }


    /// <summary>
    /// user Status According to Deleted Payment and active Value .
    /// </summary>
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

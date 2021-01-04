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
    /// This class is a DTO with consolidate information of <see cref="BizAppUserReportDTO"/>.
    /// </summary>
    public class BizAppUserReportDTO:BaseDTO {

    /// <summary>
    /// System generated unique AppUser id.
    /// </summary>
    public new Guid ID {
      get; set;
    }

    /// <summary>
    /// The username.
    /// </summary>
    /// <remarks>It's empty if not applicable.</remarks>
    public string UserName {
      get; set;
    }

    /// <summary>
    /// The name of the user who invited.
    /// </summary>
    public string InvitedBy {
      get; set;
    }

    /// <summary>
    /// AppUser invite date and time (in UTC).
    /// </summary>
    public DateTime InvitedOn {
      get; set;
    }

    /// <summary>
    /// AppUser joining date and time (in UTC).
    /// </summary>
    public DateTime? JoinedOn {
      get; set;
    }

    /// <summary>
    /// AppUser Role.
    /// </summary>
    public bool Role {
      get; set;
    }

    /// <summary>
    /// Total Number of Permissions in BitMask
    /// </summary>
    public Int64 PermissionBitMask {
      get; set;
    }

    /// <summary>
    /// Total Number of Permissions which user have.
    /// </summary>
    public int Permissions {
      get; set;
    }

    /// <summary>
    /// It indicate user is active or not
    /// </summary>
    public bool Active {
      get; set;
    }

    /// <summary>
    /// It indicate user is delete or not
    /// </summary>
    public new bool Deleted {
      get; set;
    }

    /// <summary>
    /// AppUser Status value according to deleted,Active or inactive user 
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

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
    /// This class is a DTO with consolidate information of <see cref="PartSupportTicketReportDTO"/> .
    /// </summary>
    public class PartSupportTicketReportDTO: BaseDTO {

    /// <summary>
    /// System generated unique support id.
    /// </summary>
    public new Guid ID {
      get; set;
    }
    /// <summary>
    /// System generated unique support ticket number.
    /// </summary>
    public string IdentityNumber {
      get; set;
    }

    /// <summary>
    /// support ticket title.
    /// </summary>
    public string Title {
      get; set;
    }

    /// <summary>
    /// Support ticket creation date and time (in UTC).
    /// </summary>
    public DateTime CreatedOn {
      get; set;
    }


    /// <summary>
    /// support ticket owner 
    /// </summary>
    public string AssignTo {
      get; set;
    }

        /// <summary>
        /// Name of Business 
        /// </summary>
        public string BusinessName {
            get; set;
        }

        /// <summary>
        /// Name of Business 
        /// </summary>
        public string CustomerName {
            get; set;
        }



        /// <summary>
        /// Support ticket last modified date and time (in UTC).
        /// </summary>
        public DateTime ModifiedOn {
      get; set;
    }

    /// <summary>
    /// Support ticket last modified by.
    /// </summary>
    public string ModifiedBy {
      get; set;
    }

    /// <summary>
    /// Support ticket status value.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportStatusTypeEnum"/>.</remarks>
    public Int16 Status {
      get; set;
    }

    private string _status;
    public string StatusText {
      get {
        if(Deleted) {
          _status = "Deleted";
        }
        else if(Status == 1) {
          _status = "Open";
        }
        else if(Status == 2) {
          _status = "OnHold";
        }
        else if(Status == 3) {
          _status = "Resolved";
        }
        else if(Status == 4) {
          _status = "Closed";
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


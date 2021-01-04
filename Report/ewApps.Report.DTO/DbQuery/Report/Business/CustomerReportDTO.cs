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
    /// This class is a DTO with consolidate information of <see cref="CustomerReportDTO"/> .
    /// </summary>
    public class CustomerReportDTO :BaseDTO {

    /// <summary>
    /// System generated unique support id.
    /// </summary>
    public new Guid ID {
      get; set;
    }

    /// <summary>
    /// customer Name.
    /// </summary>
    public string Customer {
      get; set;
    }

    /// <summary>
    /// Name of the user who invited customer
    /// </summary>
    public string InvitedBy {
      get; set;
    }

    /// <summary>
    /// customer invitation date and time (in UTC).
    /// </summary>
    public DateTime? InvitedOn {
      get; set;
    }

    /// <summary>
    /// customer joining date and time (in UTC).
    /// </summary>
    public DateTime? JoinedOn {
      get; set;
    }

    /// <summary>
    /// Total Number of Invoices 
    /// </summary>
    public int TotalInvoices {
      get; set;
    }

    /// <summary>
    /// Total Number of Open or Not Paid Invoies  
    /// </summary>
    public int OpenInvoices {
      get; set;
    }

    /// <summary>
    /// customer upcoming due date and time (in UTC).
    /// </summary>
    public DateTime? UpcomingDueDate {
      get; set;
    }

    /// <summary>
    /// customer last payment date and time (in UTC).
    /// </summary>
    public DateTime? LastPaymentOn {
      get; set;
    }

    /// <summary>
    /// Active flag indicate payment is active or not 
    /// </summary>
    public bool Active {
      get; set;
    }

    /// <summary>
    /// Active flag indicate payment is deleted or not 
    /// </summary>
    public new bool Deleted {
      get; set;
    }

    /// <summary>
    /// System Genereated CUstomer Reference Id 
    /// </summary>
    public string CustomerRefId {
      get; set;
    }

    /// <summary>
    /// Payment Status According to Deleted Payment and Status Value .
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

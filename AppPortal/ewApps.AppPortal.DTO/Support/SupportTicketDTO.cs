/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Represents DTO of <see cref="SupportTicket"/> information.
    /// </summary>
    public class SupportTicketDTO {

    /// <summary>
    /// Gets or sets the system generated support ticket id.
    /// </summary>
    /// <value>
    /// The support ticket id.
    /// </value>
    public Guid SupportTicketId {
      get; set;
    }

    /// <summary>
    /// Gets or sets the support ticket title.
    /// </summary>
    /// <value>
    /// The support ticket title.
    /// </value>
    public string Title {
      get; set;
    }

    /// <summary>
    /// Gets or sets the system generated support ticket number.
    /// </summary>
    /// <value>
    /// The system generated support identity number.
    /// </value>
    public string SupportIdentityNumber {
      get; set;
    }

    /// <summary>
    /// Gets or sets the support ticket current support level.
    /// </summary>
    /// <value>
    /// The current support level to whom ticket is assigned.
    /// </value>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportLevelEnum"/>.</remarks>
    public short CurrentLevel {
      get; set;
    }

    /// <summary>
    /// Gets or sets the customer unique id.
    /// </summary>
    /// <value>
    /// The customer unique id.
    /// </value>
    public Guid CustomerId {
      get; set;
    }

    /// <summary>
    /// Gets or sets the priority.
    /// </summary>
    /// <value>
    /// The support ticket priority.
    /// </value>
    /// <remarks>It should be any value of <see cref="SupportPriorityEnum"/>.</remarks>
    public short Priority {
      get; set;
    }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The support ticket status.
    /// </value>
    /// <remarks>It should be any value of <see cref="SupportStatusTypeEnum"/>.</remarks>
    public short Status {
      get; set;
    }

    /// <summary>
    /// Gets or sets the generation level.
    /// </summary>
    /// <value>
    /// The generation level.
    /// </value>
    /// <remarks>It should be any value of <see cref="SupportLevelEnum"/>.</remarks>
    public short GenerationLevel {
      get; set;
    }

    /// <summary>
    /// Gets or sets the support ticket created date and time.
    /// </summary>
    /// <value>
    /// The support ticket created date and time (in UTC).
    /// </value>
    public new DateTime CreatedOn {
      get; set;
    }

    /// <summary>
    /// Gets or sets the support ticket creater full name.
    /// </summary>
    /// <value>
    /// The support ticket creater full name.
    /// </value>
    public string CreaterFullName {
      get; set;
    }

    /// <summary>
    /// Gets or sets the support ticket last updated date and time.
    /// </summary>
    /// <value>
    /// The support ticket last updated date and time (int UTC).
    /// </value>
    public new DateTime UpdatedOn {
      get; set;
    }

    /// <summary>
    /// Gets or sets the user name of the user who last updated support ticket.
    /// </summary>
    /// <value>
    /// The user name of the user who last updated support ticket.
    /// </value>
    public string UpdatedByName {
      get; set;
    }

    /// <summary>
    /// Gets or sets the tenant id.
    /// </summary>
    /// <value>
    /// The tenant id.
    /// </value>
    public new Guid TenantId {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the system generated tenant identity number.
    /// </summary>
    /// <value>
    /// The system generated tenant identity number.
    /// </value>
    public string TenantIdentityNumber {
      get; set;
    }

    /// <summary>
    /// Gets or sets the name of the tenant.
    /// </summary>
    /// <value>
    /// The name of the tenant.
    /// </value>
    public string TenantName {
      get; set;
    }

    /// <summary>
    /// Gets or sets the system generated tenant identity number.
    /// </summary>
    /// <value>
    /// The system generated tenant identity number.
    /// </value>
    public string PublisherIdentityNumber {
      get; set;
    }

    /// <summary>
    /// Gets or sets the name of the tenant.
    /// </summary>
    /// <value>
    /// The name of the tenant.
    /// </value>
    public string PublisherName {
      get; set;
    }

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    /// <value>
    /// The name of the customer.
    /// </value>
    public string CustomerName {
      get; set;
    }

        /// <summary>
        /// Gets or sets the customer reference id.
        /// </summary>
        /// <value>
        /// The customer reference id.
        /// </value>
        /// 
        public string CustomerRefId {
            get; set;
        }

        /// <summary>
        /// Gets the name of the current assignee.
        /// </summary>
        /// <value>
        /// The name of the current assignee.
        /// </value>
        [NotMapped]
    public string AssigneeName {
      //get {
      //  return GetAssigneeName(this.CurrentLevel, this.GenerationLevel);
      //}
      get;
      set;
    }
       // [NotMapped]
        public Guid? PublisherId {
            get; set;
        }
      //  [NotMapped]
        public Guid? BusinessId {
            get; set;
        }

    public string AppName
    {     
      get;
      set;
    }
    public string OriginatedBy
    {
      get;
      set;
    }
    public string SubdomainName {
      get;
      set;
    }

    //// This method returns assignee name corresponding to input support level.
    //private string GetAssigneeName(short currentAssignedLevel, short generationLevel) {
    //  SupportLevelEnum currentAssignedLevelEnum = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), currentAssignedLevel.ToString());
    //  string name = "";
    //  switch(currentAssignedLevelEnum) {
    //    case SupportLevelEnum.None:
    //      name = "";
    //      break;
    //    case SupportLevelEnum.Level1:
    //      name = this.CreaterFullName;
    //      break;
    //    case SupportLevelEnum.Level2:
    //      if(generationLevel == (short)SupportLevelEnum.Level1) {
    //        name = this.TenantName;
    //      }
    //      else {
    //        name = this.CreaterFullName;
    //      }
    //      break;
    //    case SupportLevelEnum.Level3:
    //      name = Constants.SuperAdminKey;
    //      break;
    //    default:
    //      break;
    //  }
    //  return name;
    //}

  }
}

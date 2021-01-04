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
using System.Collections.Generic;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// This class is a DTO that contains <see cref="SupportTicket" and <see cref="SupportComment"/> information to be use for Add and Update operations.
    /// </summary>
    public class SupportAddUpdateDTO {

    /// <summary>
    /// Initializes a new instance and member variables of the <see cref="SupportAddUpdateDTO"/> class.
    /// </summary>
    public SupportAddUpdateDTO() {
      SupportCommentList=  new List<SupportCommentDTO>();
    }

    /// <summary>
    /// System generated unique support ticket .
    /// </summary>
    public Guid? SupportTicketId {
      get; set;
    } = Guid.Empty;


    /// <summary>
    /// Human readable system generated support ticket number.
    /// </summary>
    public string SupportIdentityNumber {
      get; set;
    }


    /// <summary>
    /// A unique customer id.
    /// </summary>
    public Guid CustomerId {
      get; set;
    }

    /// <summary>
    /// A unique tenant id.
    /// </summary>
    public Guid TenantId {
      get; set;
    }

    /// <summary>
    /// Support ticket title.
    /// </summary>
    public string Title {
      get; set;
    }

    /// <summary>
    /// Support ticket description.
    /// </summary>
    public string Description {
      get; set;
    }

    /// <summary>
    /// Support ticket priority value.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportPriorityEnum"/>.</remarks>
    public short Priority {
      get; set;
    }

    /// <summary>
    /// Support ticket status value.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportStatusTypeEnum"/>.</remarks>
    public short Status {
      get; set;
    }

    /// <summary>
    /// Support ticket's current support level enum value.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportLevelEnum"/>.</remarks>
    public short CurrentLevel {
      get; set;
    }

    /// <summary>
    /// User comment list
    /// </summary>
    /// <remarks>Each support comment instance should be set <see cref="ewApps.Core.Common.OperationType"/>.</remarks>
    public List<SupportCommentDTO> SupportCommentList {
      get; set;
    }

        /// <summary>
        /// Support ticket description.
        /// </summary>
        public string AppKey {
            get; set;
        }

    public Guid PortalId
    {
      get; set;
    }
    public Guid AppId
    {
      get; set;
    }
    public Guid? PublisherTenantId
    {
      get; set;
    }
    public Guid? BusinessTenantId
    {
      get; set;
    }
    public Guid? BusinessPartnerTenantId
    {
      get; set;
    }


  }
}

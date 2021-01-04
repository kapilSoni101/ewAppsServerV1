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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {

  /// <summary>
  /// This class represents a Record of SupportTicket table.
  /// </summary>
  [Table("SupportTicket", Schema = "ap")]
  public class SupportTicket:BaseEntity {

    /// <summary>
    /// The entity name.
    /// </summary>
    public const string EntityName = "SupportTicket";

    /// <summary>
    /// A unique support ticket number.
    /// </summary>
    [Required]
    public string IdentityNumber {
      get; set;
    }

    /// <summary>
    /// Support ticket title.
    /// </summary>
    [Required]
    public string Title {
      get; set;
    }

    /// <summary>
    /// Support ticket description.
    /// </summary>
    [Required]
    public string Description {
      get; set;
    }

    /// <summary>
    /// Support ticket priority.
    /// </summary>
    /// <remarks>It should be any value of <see cref="SupportPriorityEnum"/>.</remarks>
    [Required]
    public short Priority {
      get; set;
    }

    /// <summary>
    /// A constant have Status property name.
    /// </summary>
    public const string StatusPropertyName = "Status";

    /// <summary>
    /// Support ticket current status
    /// </summary>
    /// <remarks>It should be any value of <see cref="SupportStatusTypeEnum"/>.</remarks>
    [Required]
    public short Status {
      get; set;
    }

    /// <summary>
    /// Support ticket generation level.
    /// </summary>
    /// <remarks>It should be any value of <see cref="SupportLevelEnum"/>.</remarks>
    [Required]
    public short GenerationLevel {
      get; set;
    }

    /// <summary>
    /// A constant have current level property name.
    /// </summary>
    public const string CurrentLevelPropertyName = "CurrentLevel";

    /// <summary>
    /// Support ticket current level.
    /// </summary>
    /// <remarks>It should be any value of <see cref="SupportLevelEnum"/>.</remarks>
    [Required]
    public short CurrentLevel {
      get; set;
    }

    /// <summary>
    /// Application key of support ticket.
    /// </summary>
    [Required]
    public string AppKey {
      get; set;
    }


    /// <summary>
    /// Parent customer id of support ticket.
    /// </summary>
    [Required]
    public Guid CustomerId {
      get; set;
    }

    /// <summary>
    /// A constant have creator id property name.
    /// </summary>
    public const string CreatorIdPropertyName = "CreatorId";

    /// <summary>
    /// Support ticket creator id.
    /// </summary>
    [Required]
    public Guid CreatorId {
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

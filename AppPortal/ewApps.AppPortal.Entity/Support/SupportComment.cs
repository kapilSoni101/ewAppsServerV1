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
    /// This class represents a record of SupportComment table.
    /// </summary>
    [Table("SupportComment", Schema = "ap")]
  public class SupportComment:BaseEntity {
    /// <summary>
    /// The entity name.
    /// </summary>
    public const string EntityName = "SupportComment";

    /// <summary>
    /// Gets or sets the comment text.
    /// </summary>
    [Required]
    public string CommentText {
      get; set;
    }

    /// <summary>
    /// Gets or sets the creator support level.
    /// </summary>
    /// <remarks>It should be any value of <see cref="SupportLevelEnum"/>.</remarks>
    [Required]
    public short CreatorLevel {
      get; set;
    }

    /// <summary>
    /// Parent support ticket reference id.
    /// </summary>
    [Required]
    public Guid SupportId {
      get; set;
    }

    /// <summary>
    /// Support ticket creator id.
    /// </summary>
    [Required]
    public Guid CreatorId {
      get; set;
    }
  }
}

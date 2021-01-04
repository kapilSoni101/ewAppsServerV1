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
  /// This class is a DTO that contains <see cref="SupportComment"/> information to be use for Add, Update and Delete operations.
  /// </summary>
  public class SupportCommentDTO  {

    /// <summary>
    /// System generated unique id.
    /// </summary>
    public Guid CommentId {
      get; set;
    }

    /// <summary>
    /// The comment text.
    /// </summary>
    public string CommentText {
      get; set;
    }


    /// <summary>
    /// Requester user's support level.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportLevelEnum"/>.</remarks>
    public short CreatorLevel {
      get; set;
    }

    /// <summary>
    /// The display name of the commentor.
    /// </summary>
    /// <remarks>This is not required if it use for add, update and delete operations.</remarks>
    [NotMapped]
    public string CommentorName {
      get; set;
    }

    /// <summary>
    /// The creation date and time in UTC.
    /// </summary>
    /// <remarks>This should be system generated if DTO is use for add, update and delete operations.</remarks>
    public new DateTime CreatedOn {
      get; set;
    }

    /// <summary>
    /// The type of the operation.
    /// </summary>
    /// <remarks>
    /// <ul>
    /// <list type="bullet">
    /// <item>This should be some predefind value of <see cref="OperationType"/>.</item>
    /// <item>An instance with value <see cref="OperationType.None"/> will be ignore.</item>
    /// </list>
    /// </ul>
    /// </remarks>
    [NotMapped]
    public int OperationType {
      get; set;
    }
  }
}

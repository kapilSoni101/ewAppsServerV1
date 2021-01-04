/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Core.DMService {

  /// <summary>
  /// DMDocumentUser table represting document thumbnail  .
  /// </summary>
  [Table("DMDocumentUser", Schema = "core")]
  public class DMDocumentUser :BaseEntity {

    /// <summary>
    /// AppUserId Identifier.
    /// </summary>
    [Required]
    public Guid TenantUserId {
      get; set;
    }

    /// <summary>
    /// DocumentId Identifier.
    /// </summary>
    [Required]
    public Guid DocumentId {
      get; set;
    }

    /// <summary>
    /// RoleId Identifier.
    /// </summary>
    [Required]
    public Guid RoleId {
      get; set;
    }  

    /// <summary>
    /// LinkedEntityId Identifier.
    /// </summary>
    [Required]
    public Guid LinkedEntityId {
      get; set;
    }

    /// <summary>
    /// LinkedEntityType.
    /// </summary>
    [Required]
    public int LinkedEntityType {
      get; set;
    }

    /// <summary>
    /// FolderName 
    /// </summary>
    [Required]
    public string LinkedEntityName {
      get; set;
    }

    /// <summary>
    /// LinkedGroupMemberId Identifier.
    /// </summary>
    public Guid? LinkedGroupMemberId {
      get; set;
    }

    /// <summary>
    /// LinkedGroupMemberType.
    /// </summary>
    public int? LinkedGroupMemberType {
      get; set;
    }

    /// <summary>
    /// LinkedGroupMemberName 
    /// </summary>
    [MaxLength(200)]
    public string LinkedGroupMemberName {
      get; set;
    }
  }
}

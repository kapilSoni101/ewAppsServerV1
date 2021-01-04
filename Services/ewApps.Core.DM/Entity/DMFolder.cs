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
  /// DMFolder table represting all the folder.
  /// </summary>
  [Table("DMFolder", Schema = "core")]
  public class DMFolder :BaseEntity {

    /// <summary>
    /// FolderId Identifier.
    /// </summary>
    [Required]
    public Guid FolderId {
      get; set;
    }

    /// <summary>
    /// AppUserId Identifier.
    /// </summary>
    [Required]
    public Guid TenantUserId {
      get; set;
    }

    /// <summary>
    /// FolderName 
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FolderName {
      get; set;
    }

    /// <summary>
    /// ParentFolderId Identifier.
    /// </summary>
    public Guid? ParentFolderId {
      get; set;
    }

    /// <summary>
    /// The Type of owner.
    /// </summary>
    [Required]
    public int FolderType {
      get; set;
    }  

    /// <summary>
    /// The Type of owner.
    /// </summary>
    public int? FileCount {
      get; set;
    }

    /// <summary>
    /// Deleted count.
    /// </summary>
    [Required]
    public bool Deleted {
      get; set;
    }

    /// <summary>
    /// ParentDeleted count.
    /// </summary>
    [Required]
    public bool ParentDeleted {
      get; set;
    }

    /// <summary>
    /// AppId Identifier.
    /// </summary>
    [Required]
    public Guid AppId {
      get; set;
    }
  }
}

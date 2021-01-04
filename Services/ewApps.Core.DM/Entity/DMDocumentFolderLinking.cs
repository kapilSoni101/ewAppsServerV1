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
  /// DMDocumentFolderLinking table represting linking of document and folder   .
  /// </summary>
  [Table("DMDocumentFolderLinking", Schema = "core")]
  public class DMDocumentFolderLinking : BaseEntity{

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
    /// Folder Identifier.
    /// </summary>
    [Required]
    public Guid FolderId {
      get; set;
    }

    /// <summary>
    /// The Type of folder.
    /// </summary>
    [Required]
    public int FolderType {
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

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
  /// DMDocumentVersion table represting document version  .
  /// </summary>
  [Table("DMDocumentVersion", Schema = "core")]
  public class DMDocumentVersion : BaseEntity{

    /// <summary>
    /// DocumentId Identifier.
    /// </summary>
    [Required]
    public Guid DocumentId {
      get; set;
    }

    /// <summary>
    /// PhysicalPath 
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string PhysicalPath {
      get; set;
    }

    /// <summary>
    /// VersionNumber.
    /// </summary>
    [Required]
    public int VersionNumber {
      get; set;
    }

    /// <summary>
    /// The Type of owner.
    /// </summary>
    [Required]
    public int OwnerEntityType {
      get; set;
    }

    /// <summary>
    /// Owner Identifier.
    /// </summary>
    [Required]
    public Guid OwnerEntityId {
      get; set;
    }

    /// <summary>
    /// thumbnail Identifier.
    /// </summary>   
    public Guid ThumbnailId {
      get; set;
    }

    /// <summary>
    /// Filename with extension
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FileName {
      get; set;
    }

    /// <summary>
    /// File Extension.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string FileExtension {
      get; set;
    }

    /// <summary>
    /// File size in kb.
    /// </summary>
    [Required]
    public float FileSizeinKB {
      get; set;
    }

    /// <summary>
    /// FileStorage Identifier.
    /// </summary> 
    [Required]
    public Guid FileStorageId {
      get; set;
    }

    /// <summary>
    /// The Type of storage.
    /// </summary>
    [Required]
    public int StorageType {
      get; set;
    }

    /// <summary>
    /// Title
    /// </summary>
    [MaxLength(200)]
    public string Title {
      get; set;
    }  
  }
}

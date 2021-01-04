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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Core.DMService {

    /// <summary>
    /// DMDocument entity represting all the document related properties.
    /// </summary>
    [Table("DMDocument", Schema = "core")]
  public class DMDocument:BaseEntity {

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
    /// The Current Version number  of document.
    /// </summary>
    [Required]
    public int CurrentVersionNumber {
      get; set;
    }

    /// <summary>
    /// Description of Document
    /// </summary>   
    [MaxLength(4000)]
    public string Description {
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
    /// AppId Identifier.
    /// </summary>
    [Required]
    public Guid AppId {
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
    /// document title.
    /// </summary>
    [MaxLength(200)]
    public string Title {
      get; set;
    }

  }
}

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
  /// DMDocumentFolderLinking table represting linking of document and folder   .
  /// </summary>
  [Table("DMThumbnail", Schema = "core")]
  public class DMThumbnail:BaseEntity {

    /// <summary>
    /// DocumentId Identifier.
    /// </summary>
    [Required]
    public Guid DocumentId {
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
    [MaxLength(50)]
    public string FileExtension {
      get; set;
    }

    /// <summary>
    /// File size in kb.
    /// </summary>
    [Required]
    public double FileSizeinKB {
      get; set;
    }

    /// <summary>
    /// Height.
    /// </summary>
    [Required]
    public int Height {
      get; set;
    }

    /// <summary>
    /// Width.
    /// </summary>
    [Required]
    public int Width {
      get; set;
    }

    /// <summary>
    /// Duration.
    /// </summary>
    [Required]
    public double Duration {
      get; set;
    }

    /// <summary>
    /// MediaType.
    /// </summary>
    [Required]
    public int MediaType {
      get; set;
    }


    /// <summary>
    /// Document File Name.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string DocumentFileName {
      get; set;
    }

  }
}

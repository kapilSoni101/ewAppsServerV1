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
  /// DMFileStorage table represting document storage  .
  /// </summary>
  [Table("DMFileStorage", Schema = "core")]
  public class DMFileStorage: BaseEntity{

    /// <summary>
    /// Filename with extension
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FileName {
      get; set;
    }

    /// <summary>
    /// File size in kb.
    /// </summary>
    [Required]
    public float Size {
      get; set;
    }

    /// <summary>
    /// FilePath
    /// </summary> 
    [Required]
    [MaxLength(200)]
    public string FilePath {
      get; set;
    }

    /// <summary>
    /// The Type of storage.
    /// </summary>
    public int? StorageType {
      get; set;
    }

    /// <summary>
    /// MimeType
    /// </summary>    
    [MaxLength(200)]
    public string MimeType {
      get; set;
    }
  }
}

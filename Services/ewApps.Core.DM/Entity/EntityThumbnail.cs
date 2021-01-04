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
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Core.DMService {

  /// <summary>
  /// Thumbnail entity represting all the Thumbnail related properties.
  /// </summary>
  [Table("EntityThumbnail", Schema = "core")]
  public class EntityThumbnail: BaseEntity
  {

    /// <summary>
    /// The Type of owner.
    /// </summary>
    public int OwnerEntityType
    {
      get; set;
    }

    /// <summary>
    /// Owner Identifier.
    /// </summary>
    public Guid OwnerEntityId
    {
      get; set;
    }

    /// <summary>
    /// Filename with extension
    /// </summary>
    public string FileName
    {
      get; set;
    }

    /// <summary>
    /// File Extension.
    /// </summary>
    public string FileExtension
    {
      get; set;
    }

    /// <summary>
    /// File size in kb.
    /// </summary>
    public double FileSizeinKB
    {
      get; set;
    }


    /// <summary>
    /// File Heights.
    /// </summary>
    public int Height
    {
      get; set;
    }

    /// <summary>
    /// File Width.
    /// </summary>
    public int Width
    {
      get; set;
    }

    /// <summary>
    /// Duration.
    /// </summary>
    public double Duration
    {
      get; set;
    }

    /// <summary>
    /// MediaType.
    /// </summary>
    public int MediaType
    {
      get; set;
    }

    /// <summary>
    /// Name of the Document .
    /// </summary>
    public string DocumentFileName
    {
      get; set;
    }

    /// <summary>
    /// Id of Tenant.
    /// </summary>
    public override Guid TenantId
    {
      get; set;
    }
      
  }
}

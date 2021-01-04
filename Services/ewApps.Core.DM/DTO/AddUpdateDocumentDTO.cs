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

namespace ewApps.Core.DMService {

  /// <summary>
  /// Represents model properties to perform Add update operations on document entity.
  /// </summary>
  public class AddUpdateDocumentDTO {

    /// <summary>
    /// Document id to uniquely identify document.
    /// </summary>
    public Guid DocumentId;

    /// <summary>
    /// Owner Entity Type of document.
    /// </summary>
    public Int32 DocOwnerEntityType {
      get;
      set;
    }

    /// <summary>
    /// Owner Entity identifier of document.
    /// </summary>
    public Guid DocOwnerEntityId {
      get;
      set;
    }

    /// <summary>
    /// Title of document.
    /// </summary>
    public string Title {
      get;
      set;
    }

    /// <summary>
    /// File name of document.
    /// </summary>
    public string DocFileName {
      get;
      set;
    }

    public string Description {
      get;
      set;
    }

    /// <summary>
    /// File size of document in KB.
    /// </summary>
    public float DocFileSizeInKB {
      get;
      set;
    }

    /// <summary>
    /// Height of the thumbnail requested.
    /// </summary>
    public Int32 ReqThumbnailHeight {
      get;
      set;
    }

    /// <summary>
    /// Width of the thumbnail requested.
    /// </summary>
    public Int32 ReqThumbnailWidth {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the type of the media.
    /// </summary>
    public int MediaType {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the type of the media.
    /// </summary>
    public double Duration {
      get;
      set;
    }

    /// <summary>
    /// Folder id for document
    /// </summary>
    public Guid FolderId {
      get; set;
    }

  }
}


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
  /// This model defines properties for doc response.
  /// </summary>
  public class DocumentResponseModel
  {

    /// <summary>
    /// Document id to uniqely identify the document.
    /// </summary>
    public Guid DocumentId
    {
      get; set;
    }

    /// <summary>
    /// Owner entity id for document.
    /// </summary>
    public Guid OwnerEntityId
    {
      get; set;
    }

    /// <summary>
    /// File name of document.
    /// </summary>
    public string FileName
    {
      get; set;
    }

    /// <summary>
    /// Thumbnail id of document.
    /// </summary>
    public Guid ThumbnailId
    {
      get; set;
    }

    /// <summary>
    /// Mime type of document.
    /// </summary>
    public string Mimetype
    {
      get; set;
    }

    /// <summary>
    /// Media type for document.
    /// </summary>
    public int MediaType
    {
      get; set;
    }

    /// <summary>
    /// Thumbnail URL for doccument's thumbnail.
    /// </summary>
    public string ThumbnailURL
    {
      get; set;
    }

    /// <summary>
    /// Document URL 
    /// </summary>
    public string DocumentURL
    {
      get; set;
    }

    /// <summary>
    /// Extension of document file.
    /// </summary>
    public string FileExtension
    {
      get; set;
    }

    /// <summary>
    /// File storage id for document.
    /// </summary>
    public Guid FileStorageId
    {
      get; set;
    }

    /// <summary>
    /// Size of file in KB.
    /// </summary>
    public float FileSizeinKB
    {
      get; set;
    }

    /// <summary>
    /// Tenant Id for document.
    /// </summary>
    public Guid TenantId
    {
      get; set;
    }

    /// <summary>
    /// File name for thumbnail
    /// </summary>
    public string ThumbnailFileName
    {
      get; set;
    }

  }
}
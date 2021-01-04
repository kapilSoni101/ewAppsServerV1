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
using ewApps.Core.BaseService;

namespace ewApps.Core.DMService {
  public class ThumbnailIdDetailDTO : BaseDTO
  {

    /// <summary>
    /// Unique identifier of thumbnail.
    /// </summary>
    public new Guid ID
    {
      get;
      set;
    }

    ///// <summary>
    ///// Unique identifier of thumbnail.
    ///// </summary>
    //public Guid ThumbnailId
    //{
    //  get;
    //  set;
    //}

    /// <summary>
    /// Modified Date of thumbnail.
    /// </summary>
    public DateTime ThumbnailModifiedDate
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the name of the thumbnail fil.
    /// </summary>
    /// <value>
    /// The name of the thumbnail fil.
    /// </value>
    public string ThumbnailFileName
    {
      get;
      set;
    }

  }
}

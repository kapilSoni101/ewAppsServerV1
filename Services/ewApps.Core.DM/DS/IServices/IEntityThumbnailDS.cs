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
    public interface IEntityThumbnailDS : IBaseDS<EntityThumbnail>
  {

    /// <summary>
    /// Adds the thumbnail.
    /// </summary>
    /// <param name="thumbnailAddModel">The thumbnail add model.</param>
    /// <returns>Thumbnail Id Detail model.</returns>
    ThumbnailIdDetailDTO AddThumbnail(ThumbnailAddAndUpdateDTO thumbnailAddModel);

    /// <summary>
    /// Get Thumbnail Detail By OwnerEntityId 
    /// </summary>
    /// <param name="OwnerEntityId"></param>
    /// <returns></returns>
    ThumbnailAddAndUpdateDTO GetThumbnailInfoByOwnerEntityId(Guid ownerEntityId);

    /// <summary>
    /// Get Update Thumbnail Detail 
    /// </summary>
    /// <param name="thumbnailUpdateModel"></param>
    /// <returns></returns>
    ThumbnailIdDetailDTO UpdateThumbnail(ThumbnailAddAndUpdateDTO thumbnailUpdateModel);

    /// <summary>
    /// Deletes the thumbnail.
    /// </summary>
    /// <param name="thumbnailId">The thumbnail identifier.</param>
    void DeleteThumbnail(Guid thumbnailId);




  }
}

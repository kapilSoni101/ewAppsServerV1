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
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.Core.DMService {
    public class EntityThumbnailRepository : BaseRepository<EntityThumbnail, DMDBContext>, IEntityThumbnailRepository
  {
    #region Constructor 
    public EntityThumbnailRepository(DMDBContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }
    #endregion

    #region public methods 
    /// <summary>
    /// Get Thumbnail Information by OwnerEntityId
    /// </summary>
    /// <param name="ownerEntityId"></param>
    /// <returns></returns>
    public EntityThumbnail GetThumbnailByOwnerEntityId(Guid ownerEntityId) {
            EntityThumbnail thumbnailDTO;
            //thumbnailDTO = _context.EntityThumbnail.Where(a => a.OwnerEntityId == appUserId) as EntityThumbnail;
            thumbnailDTO = _context.EntityThumbnail.FirstOrDefault(a => a.OwnerEntityId == ownerEntityId);
            if(thumbnailDTO == null) {
                thumbnailDTO = _context.EntityThumbnail.FirstOrDefault(a => a.ID == ownerEntityId);
            }
            return thumbnailDTO;
    } 
    #endregion

  }
}

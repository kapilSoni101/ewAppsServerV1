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
    public interface  IEntityThumbnailRepository : IBaseRepository<EntityThumbnail>
  {
        EntityThumbnail GetThumbnailByOwnerEntityId(Guid appUserId);
    


  }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {
    /// <summary>
    /// This class implements standard database logic and operations for support group.
    /// </summary>
    /// 
    public class SupportGroupRepository:BaseRepository<SupportGroup, AppPortalDbContext>, ISupportGroupRepository {

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// 
    public SupportGroupRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context,sessionManager) {
    }
  }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 21 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 21 Aug 2019
 */


using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {
    /// <summary>
    /// This interface defines repository methods to get <see cref="ewApps.Core.Entity.AppUserTypeLinking"/> entity related data.
    /// </summary>
    /// <seealso cref="BaseRepository{AppUserTypeLinking, CoreDbContext}" />
    /// <seealso cref="IAppUserTypeLinkingRepository" />
    public class AppUserTypeLinkingRepository :BaseRepository<AppUserTypeLinking, AppMgmtDbContext>, IAppUserTypeLinkingRepository {

    /// <summary>
    /// Initializes a new instance of the <see cref="AppUserTypeLinkingRepository"/> class.
    /// </summary>
    public AppUserTypeLinkingRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

  }
}

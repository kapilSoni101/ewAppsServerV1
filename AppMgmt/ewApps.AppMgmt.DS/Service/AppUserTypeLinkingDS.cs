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


using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {
    /// <summary>
    /// This class implementes <see cref="IAppUserTypeLinkingDS"/> to provide standard business logic and operations for <see cref="AppUserTypeLinking"/>.
    /// </summary>
    public class AppUserTypeLinkingDS:BaseDS<AppUserTypeLinking>, IAppUserTypeLinkingDS {

        IAppUserTypeLinkingRepository _appUserTypeLinkingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserTypeLinkingDS"/> class with its dependencies.
        /// </summary>
        /// <param name="appUserTypeLinkingRepository">An instance of <see cref="IAppUserTypeLinkingRepository"/> to communicate with storage.</param>
        /// <param name="cacheService">An instance of <see cref="ICacheService"/> to get and set data to/from Cache.</param>
        public AppUserTypeLinkingDS(IAppUserTypeLinkingRepository appUserTypeLinkingRepository) : base(appUserTypeLinkingRepository) {
            _appUserTypeLinkingRepository = appUserTypeLinkingRepository;
        }


    }
}

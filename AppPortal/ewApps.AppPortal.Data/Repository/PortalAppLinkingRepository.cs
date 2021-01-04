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

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// This interface defines repository methods to get <see cref="ewApps.Core.Entity.PortalAppLinking"/> entity related data.
    /// </summary>
    /// <seealso cref="BaseRepository{PortalAppLinking, CoreDbContext}" />
    /// <seealso cref="IPortalAppLinkingRepository" />
    public class PortalAppLinkingRepository:BaseRepository<PortalAppLinking, AppPortalDbContext>, IPortalAppLinkingRepository {

            /// <summary>
            /// Initializes a new instance of the <see cref="AppUserTypeLinkingRepository"/> class.
            /// </summary>
            public PortalAppLinkingRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            }
        }
}

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
    /// This interface defines repository methods to get <see cref="ewApps.Core.Entity.Portal"/> entity related data.
    /// </summary>
    /// <seealso cref="BaseRepository{Portal, CoreDbContext}" />
    /// <seealso cref="IPortalRepository" />
    public class PortalRepository:BaseRepository<Portal, AppPortalDbContext>, IPortalRepository {

        /// <summary>
        /// Initializes a new instance of the <see cref="PortalRepository"/> class.
        /// </summary>
        public PortalRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
    }
}

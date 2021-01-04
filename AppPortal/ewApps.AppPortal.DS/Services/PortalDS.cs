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

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implementes <see cref="IPortalDS"/> to provide standard business logic and operations for <see cref="Portal"/>.
    /// </summary>
    public class PortalDS:BaseDS<Portal>, IPortalDS {

        IPortalRepository _portalRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortalDS"/> class with its dependencies.
        /// </summary>
        /// <param name="portalRepository">An instance of <see cref="IPortalAppLinkingRepository"/> to communicate with storage.</param>
        /// <param name="cacheService">An instance of <see cref="ICacheService"/> to get and set data to/from Cache.</param>
        public PortalDS(IPortalRepository portalRepository) : base(portalRepository) {
            _portalRepository = portalRepository;
        }
    }
}

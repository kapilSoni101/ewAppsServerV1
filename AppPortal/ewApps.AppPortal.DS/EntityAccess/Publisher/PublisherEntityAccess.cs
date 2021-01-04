/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* 
* Author: Rajesh Thakur <rtakur@eworkplaceapps.com>
* Date: 24 September 2018
* 
* Contributor/s: Nitin Jain
* Last Updated On: 10 October 2018
*/

using System;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class enables permission relatrd logic for platform entities.
    /// </summary>
    public abstract class PublisherEntityAccess:IPublisherEntityAccess {

        #region  Local variables

        protected IUserSessionManager _userSessionManager;
        protected IRoleDS _roleDS;
        protected Role _permission;

        #endregion Local variables

        #region Constructor

        public PublisherEntityAccess(IUserSessionManager sessionManager, IRoleDS roleDS) {
            _userSessionManager = sessionManager;
            _roleDS = roleDS;
        }

        #endregion Constructor

        /// <summary>
        /// GEts the permission bits for given entity id.
        /// </summary>
        /// <param name="entityId">Entity id to get permission.</param>
        /// <returns>Permission list array.</returns>
        public abstract bool[] AccessList(Guid entityId);

        // Gets the permission records from database.
        protected void InitPermission() {
            _permission = GetLoginUserPermissionSet();
        }

        // remove this method
        public virtual bool CheckAccess(int operation, Guid entityId) {
            UserSession session = _userSessionManager.GetSession();
            InitPermission();

            PlatformUserPlatformAppPermissionEnum bitmask = (PlatformUserPlatformAppPermissionEnum)_permission.PermissionBitMask;
            return (bitmask & (PlatformUserPlatformAppPermissionEnum)operation) == (PlatformUserPlatformAppPermissionEnum)operation;
        }

        protected virtual Role GetLoginUserPermissionSet() {
            return _roleDS.GetEntityByAppUserAndAppId();
        }

    }
}
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using ewApps.AppPortal.Common;
using ewApps.AppPortal.DS;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using System;

namespace ewApps.AppPortal.DS {

    public class PlatformTenantSubscriptionAccess:PlatformEntityAccess, IPlatformTenantSubscriptionAccess {

        PlatformUserPlatformAppPermissionEnum permEnum;

        public PlatformTenantSubscriptionAccess(IUserSessionManager sessionManager, IRoleDS roleDS) : base(sessionManager, roleDS) {
        }

        public override bool[] AccessList(Guid entityId) {
            throw new NotImplementedException();
        }

        public bool[] AccessList() {
            bool[] accessVector = new bool[2];
            accessVector[(int)PlatformUserPlatformAppPermissionEnum.ViewSubscription] = CheckAccess((int)PlatformUserPlatformAppPermissionEnum.ViewSubscription, Guid.Empty);
            accessVector[(int)PlatformUserPlatformAppPermissionEnum.ManageSubscription] = CheckAccess((int)PlatformUserPlatformAppPermissionEnum.ManageSubscription, Guid.Empty);

            return accessVector;
        }

        public override bool CheckAccess(int operation, Guid entityId) {
            bool hasPermission = false;
            UserSession session = _userSessionManager.GetSession();
            InitPermission();

            // Get the permission bit mask.
            PlatformUserPlatformAppPermissionEnum bitmask = (PlatformUserPlatformAppPermissionEnum)_permission.PermissionBitMask;

            switch(operation) {

                case (int)OperationType.Add:
                case (int)OperationType.Update:
                case (int)OperationType.Delete:
                    hasPermission = (bitmask & PlatformUserPlatformAppPermissionEnum.ManageSubscription) == PlatformUserPlatformAppPermissionEnum.ManageSubscription;
                    break;
            }

            return hasPermission;
        }

    }
}

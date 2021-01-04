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

using System;
using ewApps.AppPortal.Common;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS  {

  public class PlatformAppUserAccess:PlatformEntityAccess , IPlatformAppUserAccess {
        PlatformUserPlatformAppPermissionEnum permEnum;

    public PlatformAppUserAccess(IUserSessionManager sessionManager, IRoleDS roleDS) : base(sessionManager, roleDS) {
    }

    public override bool[] AccessList(Guid entityId) {
      bool[] accessVector = new bool[1];
     // accessVector[0] = CheckAccess((int)PlatformPermissionEnum.ViewMyPlatformSettings, Guid.Empty);
      accessVector[0] = CheckAccess((int)PlatformUserPlatformAppPermissionEnum.ManagePlatformPortalSettings, Guid.Empty);

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
          hasPermission = (bitmask & PlatformUserPlatformAppPermissionEnum.ManagePlatformPortalSettings) == PlatformUserPlatformAppPermissionEnum.ManagePlatformPortalSettings;
          break;
      }

      return hasPermission;
    }
  }
}

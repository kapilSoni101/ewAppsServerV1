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

using System;
using ewApps.AppPortal.Common;

namespace ewApps.AppPortal.DS {
    public class SupportSession {

    public Guid ID {
      get; set;
    }

    public string UserName {
      get; set;
    }

    public Guid AppUserId {
      get; set;
    }

    public Guid TenantId {
      get; set;
    }

    public string TenantName {
      get; set;
    }

    public Guid AppId {
      get; set;
    }

    public string IdentityToken {
      get; set;
    }

    public SupportLevelEnum SupportLevel {
      get; set;
    } = SupportLevelEnum.None;

        public int UserType {
            get; set; }
  }
}
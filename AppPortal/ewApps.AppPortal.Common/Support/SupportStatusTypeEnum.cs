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
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.Common {
  public enum SupportStatusTypeEnum :short {
    None = 0,
    Open = 1,
    OnHold = 2,
    Resolved = 3,
    Closed = 4,
    //  Delete = 5,
  }
}

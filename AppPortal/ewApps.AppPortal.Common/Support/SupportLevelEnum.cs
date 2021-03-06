﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
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
  public enum SupportLevelEnum:short {
    None = 0,
    Level1 = 1, // Customer
    Level2 = 2, // Business
    Level3 = 3,  // Publisher
    Level4 = 4  // Platform
  }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 07 October 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 07 October 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.Common {

    /// <summary>
    /// SyncResponseEnum to identify the Sync Response.
    /// </summary>
    [Flags]
    public enum SyncResponseEnum {

        None = 0,
        Success = 1,
        Failed = 2
    }
    }

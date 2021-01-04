/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    // This class contain account detail of appservice
    public class AppServiceAccDetailIdDTO {
        public Guid Id {
            get; set;
        }
        public Guid SourceEntityId {
            get; set;
        }
        public string AccountJson {
            get; set;
        }
        public Guid ServiceId {
            get; set;
        }
        public OperationType operationType {
            get; set;
        }

    }
}

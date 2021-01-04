//DbQuery

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
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    public class PublisherAppServiceDQ: BaseDQ {

        public new Guid ID {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        public Guid AppServiceId {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string AttributeName {
            get; set;
        }

        public Guid AttributeId {
            get; set;
        }

        public string AccountJson {
            get; set;
        }

    }
}

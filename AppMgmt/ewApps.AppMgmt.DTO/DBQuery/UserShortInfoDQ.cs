//DbQuery

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    public class UserShortInfoDQ:BaseDQ {

        public Guid ID {
            get; set;
        }

        public string Email {
            get; set;
        }
        public string FirstName {
            get; set;
        }
        public string LastName {
            get; set;
        }

        public Guid AppId {
            get; set;
        }


        public DateTime? UserActivationDate {
            get; set;
        }

    }
}

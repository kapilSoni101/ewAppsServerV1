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
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Contains attribute detail.
    /// </summary>
    public class AppServiceAttributeRequestDTO {

        public Guid ID {
            get; set;
        }

        public string JsonData {
            get; set;
        }

        public string Name {
            get; set;
        }

        public object buinessVCACHPayAttrDTO {
            get; set;
        }
    }
}

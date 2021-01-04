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
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// This class is a DTO for adding user data.
    /// </summary>
    public class UserAppRelationDTO {

        /// <summary>
        /// User appp identifier.
        /// </summary>
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// ClientAppType for user to be added on identity server.
        /// </summary>
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// Operation type of the applications.
        /// </summary>
        [NotMapped]
        public OperationType OperationType {
            get; set;
        }

        public bool Active {
            get; set;
        }
    }
}

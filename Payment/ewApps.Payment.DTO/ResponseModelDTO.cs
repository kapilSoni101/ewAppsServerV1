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

namespace ewApps.Payment.DTO {
    /// <summary>
    /// This class is a DTO for response of the API.
    /// </summary>
    public class ResponseModelDTO {

        /// <summary>
        /// Entity identityfier for the entity on which operation is performed.
        /// </summary>
        public Guid Id;

        /// <summary>
        /// Success flag.
        /// </summary>
        public bool IsSuccess = true;

        /// <summary>
        /// Additional message.
        /// </summary>
        public string Message = "";

    }
}

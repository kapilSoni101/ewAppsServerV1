/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 27 March 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 27 March 2019
 */

namespace ewApps.Core.ScheduledJobService {

    /// <summary>
    /// This class represents a general response object that constains response type and response as raw string.
    /// </summary>
    public class ResponseDTO {

        /// <summary>
        /// The type of the reponse.
        /// </summary>
        public string ReponseType {
            get; set;
        }

        /// <summary>
        /// The response in form of string.
        /// </summary>
        public string Response {
            get; set;
        }
    }
}

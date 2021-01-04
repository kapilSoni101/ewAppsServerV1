/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */



using System;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="AppDTO"/> .
    /// </summary>
    public class AppDTO {

        /// <summary>
        /// Key of Application.
        /// </summary>
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// ID of Application.
        /// </summary>
        public Guid ID {
            get; set;
        }

    }
}

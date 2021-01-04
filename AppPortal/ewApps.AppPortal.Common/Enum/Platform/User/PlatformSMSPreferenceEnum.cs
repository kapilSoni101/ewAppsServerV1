/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

namespace ewApps.AppPortal.Common {

    /// <summary>
    /// This class provides publisher prefernces list.
    /// </summary>
    [System.Flags]
    public enum PlatformSMSPreferenceEnum:long {

        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,

        /// <summary>
        /// All the prefernece are true.
        /// </summary>
        All = None

    }
}

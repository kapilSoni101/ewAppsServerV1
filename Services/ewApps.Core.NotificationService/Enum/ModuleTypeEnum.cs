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

namespace ewApps.Core.NotificationService {

    /// <summary>
    /// Enum used to define the module for the notification.
    /// </summary>
    public enum ModuleTypeEnum {

        /// <summary>
        /// Refers to publisher
        /// </summary>
        Platform = 1,

        /// <summary>
        /// Refers to publisher
        /// </summary>
        Publisher = 2,

        /// <summary>
        /// Refers to payment business.
        /// </summary>
        Business = 3,

        /// <summary>
        /// Refers to customer users.
        /// </summary>
        Customer = 4,

        Vendor = 5
    }
}

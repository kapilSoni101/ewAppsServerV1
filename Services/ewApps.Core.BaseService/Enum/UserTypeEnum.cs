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

namespace ewApps.Core.BaseService {

    /// <summary>
    /// Defines the portal of the user.
    /// </summary>
    [System.Flags]
    public enum UserTypeEnum {

        /// <summary>
        /// Platform user.
        /// </summary>
        Platform = 1,

        /// <summary>
        /// Publisher user.
        /// </summary>
        Publisher = 2,

        /// <summary>
        /// Vendor user.
        /// </summary>
        Business = 3,

        /// <summary>
        /// Customer user.
        /// </summary>
        Customer = 4,

        /// <summary>
        /// Vendor user.
        /// </summary>
        Vendor = 5,

        /// <summary>
        /// Employee user.
        /// </summary>
        Employee = 6


        // platform
        // publisher
        // business
        // business partner

    }
}
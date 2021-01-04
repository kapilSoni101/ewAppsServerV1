/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna<skhanna@eworkplaceapps.com>
 * Date: 14 January 2019
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.UserSessionService {

    /// <summary>
    /// Session manager configuration options
    /// </summary>
    public class UserSessionOptions {

        /// <summary>
        /// Gets or sets a value indicating whether [light session].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [light session]; otherwise, <c>false</c>.
        /// </value>
        public bool LightSession {
            get; set;
        } = false;

        public bool RemoteSession {
            get; set;
        } = false;

    }
}

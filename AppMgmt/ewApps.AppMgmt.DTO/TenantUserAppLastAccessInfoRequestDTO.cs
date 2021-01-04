/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 14 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 Aug 2019
 */

using System;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// Data Trasfer object user for request from other portals for setting/Update the user last acces info for the particular application of a tenant.
    /// </summary>
    public class TenantUserAppLastAccessInfoRequestDTO {

        /// <summary>
        /// Tenant Identifier of the user tenant.
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// AppId UniqueIdentifier to indentify the application for which user whats to set/update the login time.
        /// </summary>
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// TenantUserId UniqueIdentifier for user identification.
        /// </summary>
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// TenantUserId UniqueIdentifier for user identification.
        /// </summary>
        public Guid IdentityUserId {
            get; set;
        }

        /// <summary>
        /// Login Data Time of the user (last time user login).
        /// </summary>
        public DateTime LoginDateTime {
            get; set;
        }

        /// <summary>
        /// Region from user is login in.
        /// </summary>
        public string Region {
            get; set;
        }

        /// <summary>
        /// Current laguage of the user.
        /// </summary>
        public string Language {
            get; set;
        }

        /// <summary>
        /// Browser from which user is login in.
        /// </summary>
        public string Browser {
            get; set;
        }

        /// <summary>
        /// Current time zone of the user.
        /// </summary>
        public string TimeZone {
            get; set;
        }

        
    }
}
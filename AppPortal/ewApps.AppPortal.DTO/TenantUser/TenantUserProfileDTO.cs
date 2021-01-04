/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 9 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 9 September 2019
 */


using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.DMService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// This class is a DTO that contains tenant user profile information to be use for Get and Update operations.
    /// </summary>
    public class TenantUserProfileDTO {
        /// <summary>
        /// User unique identifier
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// USer first name.
        /// </summary>
        public string FirstName {
            get; set;
        }

        /// <summary>
        /// USer last name.
        /// </summary>
        public string LastName {
            get; set;
        }

        /// <summary>
        ///  App user email.
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// USer phone number name.
        /// </summary>
        public string Phone {
            get; set;
        }

        /// <summary>
        /// isAddThumbnail bit to indicate a thumbnail is first TimeAdding
        /// </summary>
        public bool IsAddThumbnail {
            get;
            set;
        }

        /// <summary>
        /// Thumbnail Id
        /// </summary>
        public Guid ThumbnailId {
            get; set;
        }

        /// <summary>
        /// The thumbnail add and update model.
        /// </summary>
        public ThumbnailAddAndUpdateDTO ThumbnailAddUpdateModel {
            get;
            set;
        }
    }
}


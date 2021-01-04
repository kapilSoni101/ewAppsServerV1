//response

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

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// This class is a DTO that contains ap user information to be use for Add and Update operations.
    /// </summary>
    public class AppUserDTO {

        /// <summary>
        /// User unique identifier
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Portal type.
        /// </summary>
        public int UserType {
            get; set;
        }

        /// <summary>
        /// Fullname
        /// </summary>
        public string FullName {
            get; set;
        }

        /// <summary>
        ///  App user email.
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// Role of the user.
        /// </summary>
        public string Role {
            get; set;
        }

        /// <summary>
        /// USer role Id.
        /// </summary>
        public Guid RoleId {
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
        /// USer phone number name.
        /// </summary>
        public string Phone {
            get; set;
        }

        /// <summary>
        /// Admin bit to indicate a user is admin
        /// </summary>
        public bool Admin {
            get;
            set;
        }

        /// <summary>
        /// Invitor name
        /// </summary>
        public string InvitedBy {
            get;
            set;
        }

        /// <summary>
        /// Invitor name
        /// </summary>
        public string AppKey {
            get;
            set;
        }

        /// <summary>
        /// bit mast
        /// </summary>
        public long PermissionBitMask {
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
        /// Invitation status of the user.
        /// </summary>
        public int Status {
            get; set;
        }

        /// TODO: Ishwar to check        
        /// <summary>
        /// Gets or sets the thumbnail add and update model.
        /// </summary>
        /// <value>
        /// The thumbnail add and update model.
        /// </value>
        //public ThumbnailAddAndUpdateDTO ThumbnailAddUpdateModel {
        //    get;
        //    set;
        //}

        /// <summary>
        /// joined date of the user
        /// </summary>
        [NotMapped]
        public DateTime? JoinedDate {
            get;
            set;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class TenantUserUpdateRequestDTO {

        /// <summary>
        /// User unique identifier.
        /// </summary>
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// User unique identifier.
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// USer first name.
        /// </summary>
        public string FirstName {
            get; set;
        }

        /// <summary>
        /// User last name.
        /// </summary>
        public string LastName {
            get; set;
        }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string FullName {
            get; set;
        }

        /// <summary>
        ///  App user email login user id of the user.
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// User phone number of the tenant user.
        /// </summary>
        public string Phone {
            get; set;
        }

        /// <summary>
        /// User details about app and its permissions.
        /// </summary>
        public List<UserAppRelationDTO> UserAppRelationDTOList {
            get; set;
        }
    }
}


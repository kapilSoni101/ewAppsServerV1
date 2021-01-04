using System;
using ewApps.Core.ExceptionService;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// This data trasnfer object 
    /// </summary>
    public class IdentityServerAddUserResponseDTO {

        /// <summary>
        /// User Id of the newly created user.
        /// </summary>
        public Guid UserId {
            get; set;
        }

        /// <summary>
        /// Code for setting the password of the user.
        /// </summary>
        public string Code {
            get; set;
        }

        /// <summary>
        /// Ewp error if any occured while creating the user.
        /// </summary>
        public EwpError EwpError {
            get; set;
        }

    }
}

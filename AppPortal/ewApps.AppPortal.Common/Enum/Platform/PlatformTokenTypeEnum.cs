using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.Common {
    public enum PlatformTokenTypeEnum {

        /// <summary>
        /// Platform other user  with new email.
        /// </summary>
        PlatformUserWithNewEmailInvite = 1,

        /// <summary>
        /// Platform other user email.
        /// </summary>
        PlatformUserExistingEmailInvite = 2,

        /// <summary>
        /// Platform user forgot password email .
        /// </summary>
        PlatformUserForgotPassword = 3,

        /// <summary>
        /// Publisher primary user with new email.
        /// </summary>
        PublisherPrimaryUserWithNewEmailInvite = 4,

        /// <summary>
        /// Publisher primary user with new email.
        /// </summary>
        PublisherPrimaryUserWithExistingEmailInvite = 5,

    }
}

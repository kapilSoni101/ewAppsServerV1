using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.CommonService;

namespace ewApps.AppMgmt.Common {

    /// <summary>
    /// Contains constants values.
    /// </summary>
    public sealed class Constants {

        public const string DomainNameDuplicateMessage = "Sub-domain name already exist.";

        #region IDPrefix

        public const string TenantIdPrefix = "TNT";

        #endregion IDPrefix

        #region Default Region and language

        public const string DefaultLanguage = "en";

        public const string DefaultRegion = "US";

        #endregion Default Region and language

        #region Default Encryption Algorithm 

        /// <summary>
        /// The default encryption algorigthm
        /// </summary>
        public const CryptoHelper.EncryptionAlgorithm DefaultEncryptionAlgo = CryptoHelper.EncryptionAlgorithm.AES;

        #endregion

        public const string UserIdPrefix = "USR";
        public const int UserIdstartnumber = 100002;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.CommonService {

    /// <summary>
    /// Contain constants value.
    /// </summary>
    public sealed class Constants {

        #region Default Encryption Algorithm 

        /// <summary>
        /// The default encryption algorigthm
        /// </summary>
        public const CryptoHelper.EncryptionAlgorithm DefaultEncryptionAlgo = CryptoHelper.EncryptionAlgorithm.AES;

        #endregion

        public const string DefaultTimeFormat = "hh:mm tt";

        #region Payment

        public const string ACHPaymentsAttributeKey = "ACHPayments";

        public const string CreditCardPaymentsAttributeKey = "CreditCardPayments";

        #endregion Payment

        public const string CanceledInvoiceText = "Canceled";

    }
}

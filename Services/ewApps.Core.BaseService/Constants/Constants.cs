using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {

    public sealed class Constants {

        #region AppKey Constants

        /// <summary>
        /// Value of admin role key.
        /// </summary>
        public const string AppKey = "pub";

        /// <summary>
        /// Its default entery for Publisher Tenant application.
        /// </summary>
        public const string PublisherApplicationId = "67D09A6F-CE95-498C-BF69-33C7D38F9041";

        /// <summary>
        /// Its default entery for business tenant application.
        /// </summary>
        public const string BusinessApplicationId = "F4952EF3-F1BD-4621-A5F9-290FD09BC81B";

        #endregion AppKey Constants      


        #region PaymentServiceAndAttributeKeys

        public const string VeriCheckServiceKey = "VeriCheck";
        public const string CBCServiceKey = "CBC";
        public const string FedExServiceKey = "FedEx";

        public const string FedExSameDayCityAttributeKey = "FedExSameDayCity";
        public const string ACHPaymentsAttributeKey = "ACHPayments";

        public const string FedExPriorityOvernightAttributeKey = "FedExPriorityOvernight";
        public const string FedExSameDayAttributeKey = "FedExSameDay";
        public const string CreditCardPaymentsAttributeKey = "CreditCardPayments";
        public const string FedExFirstOvernightAttributeKey = "FedExFirstOvernight";


        #endregion

        public const string DefaultLanguage = "en";

        #region Invoice Status 
        /// <summary>
        /// Open Status of Invoice 
        /// </summary>
        public const string Open = "Open";

        /// <summary>
        /// Open Status of Invoice 
        /// </summary>
        public const string Closed = "Closed";

        /// <summary>
        /// Open Status of Invoice 
        /// </summary>
        public const string Partial = "Partial"; 
        #endregion
    }
}

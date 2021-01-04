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
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.Common {

    /// <summary>
    /// This class provides all the constants used in the application.
    /// </summary>
    public sealed class Constants {

        #region Exception Constants

        /// <summary>
        /// Original Exception flag key.
        /// </summary>  
        public const string IsOriginaleKey = "IsOriginal";

        /// <summary>
        /// Wrapper Exception flag key.
        /// </summary>
        public const string IsWrapperKey = "IsWrapper";

        /// <summary>
        /// Pass Through Exception flag key.
        /// </summary>
        public const string IsPassThroughKey = "IsPassThrough";

        /// <summary>
        /// Re-throw Exception flag key.
        /// </summary>
        public const string IsRethrowKey = "IsRethrow";

        /// <summary>
        /// New Exception flag key.
        /// </summary>
        public const string IsNewKey = "IsNew";

        /// <summary>
        /// Exception Severity key.
        /// </summary>
        public const string SeverityKey = "Severity";

        /// <summary>
        /// Additional message key.
        /// </summary>
        public const string AdditionalMsgKey = "AdditionalMsg";

        /// <summary>
        /// Exception is logged flag key.
        /// </summary>
        public const string IsLoggedKey = "IsLogged";

        /// <summary>
        /// Http status code.
        /// </summary>
        public const string HttpStatusCodeKey = "HttpStatusCode";

        /// <summary>
        /// PassThrough Policy Key Constant. 
        /// </summary>
        public const string PassThroughPolicyKey = "PassThroughPolicy";

        /// <summary>
        /// New Policy Key Constant. 
        /// </summary>
        public const string NewPolicyKey = "NewPolicy";

        /// <summary>
        /// Log Policy Key Constant. 
        /// </summary>
        public const string LogPolicyKey = "LogPolicy";

        /// <summary>
        /// Data Policy Key Constant. 
        /// </summary>
        public const string DataPolicyKey = "DataPolicy";

        /// <summary>
        /// DataService Policy Key Constant. 
        /// </summary>
        public const string DataServicePolicyKey = "DataServicePolicy";

        /// <summary>
        /// DataService Policy Key Constant. 
        /// </summary>
        public const string SyncPolicyKey = "SyncPolicy";

        /// <summary>
        /// Admin role name list.
        /// </summary>
        public const string AdminRoleNames = "'AccountAdmin','ITAdmin','ITIssueAdmin'";

        public const string BusinessCurrencyNotSet = "Business currency not set.";


        #endregion Exception Constants.

        #region Role Key Constants

        /// <summary>
        /// Value of admin role key.
        /// </summary>
        public const string AdminRoleKey = "Admin";

        #endregion Role Key Constants

        #region AppKey Constants

        /// <summary>
        /// Value of admin role key.
        /// </summary>
        public const string AppKey = "pay";

        #endregion Role Key Constants

        #region PaymentServiceAndAttributeKeys

        public const string VeriCheckServiceKey = "VeriCheck";
        public const string TSYSCheckServiceKey = "TSYS";
        public const string CBCServiceKey = "CBC";
        public const string FedExServiceKey = "FedEx";

        public const string FedExSameDayCityAttributeKey = "FedExSameDayCity";
        public const string ACHPaymentsAttributeKey = "ACHPayments";       

        public const string FedExPriorityOvernightAttributeKey = "FedExPriorityOvernight";
        public const string FedExSameDayAttributeKey = "FedExSameDay";
        public const string CreditCardPaymentsAttributeKey = "CreditCardPayments";
        public const string FedExFirstOvernightAttributeKey = "FedExFirstOvernight";

        public const string SavingAccount = "Saving";


        #endregion

        #region Payment Status

        public const string PaymentStatusSettled = "Settled";

        public const string PaymentStatusPartialSettled = "PartialSettled";

        public const string PaymentStatusPending = "Pending";

        #endregion Payment Status

        public const string DefaultLanguage = "en";

        public const string OpenStatus = "Open";

    }
}



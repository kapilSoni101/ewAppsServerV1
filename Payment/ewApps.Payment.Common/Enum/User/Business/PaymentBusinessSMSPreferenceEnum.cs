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

namespace ewApps.Payment.Common {

    /// <summary>
    /// This class provides payment business user sms prefernces list.
    /// </summary>
    [System.Flags]
    public enum PaymentBusinessSMSPreferenceEnum:long {

        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,

        /// <summary>
        /// Invoice payment is received.
        /// </summary>
        InvoicePaymentReceived = 1,

        /// <summary>
        /// Invoice payment status is changed.
        /// </summary>
        InvoicePaymentStatusChanged = 2,


        /// <summary>
        /// All the prefernece are true.
        /// </summary>
        All = None | InvoicePaymentReceived | InvoicePaymentStatusChanged

    }
}

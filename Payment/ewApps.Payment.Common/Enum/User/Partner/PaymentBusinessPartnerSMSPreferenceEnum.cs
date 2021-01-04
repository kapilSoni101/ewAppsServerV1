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
    /// This class provides payment business partner user sms prefernces list.
    /// </summary>
    [System.Flags]
    public enum PaymentBusinessPartnerSMSPreferenceEnum:long {

        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,

        /// <summary>
        /// New Invoice is received.
        /// </summary>
        NewInvoiceReceived = 1,

        /// <summary>
        /// Invoice Payment is initiated.
        /// </summary>
        InvoicePaymentInitiated = 2,

        /// <summary>
        /// Invoice payment status is changed.
        /// </summary>
        InvoicePaymentStatusChanged = 4,

        /// <summary>
        /// All the prefernece are true.
        /// </summary>
        All = None | NewInvoiceReceived | InvoicePaymentInitiated | InvoicePaymentStatusChanged
    }
}

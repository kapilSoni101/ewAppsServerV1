using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.Common
{
    /// <summary>
    /// Contains all payment status.    
    /// </summary>
    [System.Flags]
    public enum PaymentStatusEnum
    {
        /// <summary>
        /// Pending 
        /// </summary>
        Pending,
        /// <summary>
        /// Originated
        /// </summary>
        Originated,
        /// <summary>
        /// Error
        /// </summary>
        Error,
        /// <summary>
        /// VoidRequested
        /// </summary>
        VoidRequested,
        /// <summary>
        /// Voided
        /// </summary>
        Voided,
        /// <summary>
        /// RefundRequested
        /// </summary>
        RefundRequested,
        /// <summary>
        /// Refunded
        /// </summary>
        Refunded,
        /// <summary>
        /// Payment sattled.
        /// </summary>
        Settled
    }
}

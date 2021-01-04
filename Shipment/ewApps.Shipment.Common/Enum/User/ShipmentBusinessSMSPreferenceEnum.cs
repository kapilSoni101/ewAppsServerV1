using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Shipment.Common {


    /// <summary>
    /// SMS preferece of the business user for the shipment application.
    /// </summary>
    [System.Flags]
    public enum ShipmentBusinessSMSPreferenceEnum:long {

        /// <summary>
        /// No prefernces.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// All the perfrences are on.
        /// </summary>
        All = None
    }
}
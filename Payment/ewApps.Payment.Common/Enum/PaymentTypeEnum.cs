using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.Common {

    /// <summary>
    /// Contains all payment type like.    
    /// </summary>
    [System.Flags]
    public enum PaymentTypeEnum {
        Invoice = 1,
        Advance = 2,
        Vendor = 3
    }

}

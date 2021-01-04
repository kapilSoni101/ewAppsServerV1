using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// Payment detail.
    /// </summary>
   public class TSysCardResponse {

        /// <summary>
        /// Payment detail object.
        /// </summary>
        public TSysSalesDetailDTO SaleResponse {
            get;set;
        }

    }
}

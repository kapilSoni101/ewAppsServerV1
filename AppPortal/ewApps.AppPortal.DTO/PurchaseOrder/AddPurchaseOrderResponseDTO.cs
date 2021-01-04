using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// A response model object of a invoice.
    /// </summary>
    public class AddPurchaseOrderResponseDTO {

        public Guid PurchaseOrderId {
            get; set;
        }

        public int PurchaseOrderEntityType {
            get; set;
        }

        public string ERPPurchaseOrderKey {
            get; set;
        }

        public Guid CustomerId {
            get; set;
        }

    }
}

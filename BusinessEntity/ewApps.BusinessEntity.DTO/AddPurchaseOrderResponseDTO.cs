using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// A response model object of a invoice.
    /// </summary>
    public class AddPurchaseOrderResponseDTO {

        public Guid PurchaseOrderId {
            get;set;
        }

        public int PurchaseOrderEntityType {
            get;set;
        }

        public string ERPPurchaseOrderKey {
            get; set;
        }

        public Guid VendorId {
            get; set;
        }

    }
}

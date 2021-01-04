using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BusBADeliveryItemDTO {

        /// <summary>
        /// The DeliveryItem Id.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Represents the Serial Or BatchNo
        /// </summary>
        public string SerialOrBatchNo {
            get; set;
        }

        /// <summary>
        /// Represents the ERP   item key.
        /// </summary>
        public string ERPItemKey {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item name.
        /// </summary>
        public string ItemName {
            get; set;
        }

        /// <summary>
        /// Represents the delivery item Quantity.
        /// </summary>
        public decimal Quantity {
            get; set;
        }

        /// <summary>
        /// Represents the delivery item UnitPrice.
        /// </summary>
        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// Represents the delivery item DiscountPercent.
        /// </summary>
        public decimal DiscountPercent {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery TaxCode.
        /// </summary>
        public string TaxCode {
            get; set;
        }

        /// <summary>
        /// Represents the delivery TaxPercent
        /// </summary>
        public decimal TaxPercent {
            get; set;
        }

        /// <summary>
        /// Represents the Blanket Agreement No
        /// </summary>
        public string BlanketAgreementNo {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery Freight.
        /// </summary>
        public decimal Freight {
            get; set;
        }

    }
}

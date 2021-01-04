using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Represents the delivery item related properties.
    /// </summary>
    public class BADeliveryItemSyncDTO {
        

        /// <summary>
        /// Represents the ERP delivery item key.
        /// </summary>
        public string ERPDeliveryKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERP connector key
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery id
        /// </summary>
        public Guid DeliveryID {
            get; set;
        }

        /// <summary>
        /// Represents the ERP   item key.
        /// </summary>
        public string ERPItemKey {
            get; set;
        }

        /// <summary>
        /// Represents the lot number.
        /// </summary>
        public string LotNo {
            get; set;
        }

        /// <summary>
        /// Represents the  item id.
        /// </summary>
        public Guid ItemID {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item name.
        /// </summary>
        public string ItemName {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item type.
        /// </summary>
        public int ItemType {
            get; set;
        }

        /// <summary>
        /// Represents the  Item Type Text
        /// </summary>
        public string ItemTypeText {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item Quantity.
        /// </summary>
        public decimal Quantity {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item UnitPrice.
        /// </summary>
        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item UnitPrice.
        /// </summary>
        public decimal UnitPriceFC {
            get; set;
        }


        /// <summary>
        /// Represents the   delivery item DiscountPercent.
        /// </summary>
        public decimal DiscountPercent {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery   TaxCode.
        /// </summary>
        public string TaxCode {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery TaxPercent
        /// </summary>
        public decimal TaxPercent {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery TotalLC
        /// </summary>
        public decimal TotalLC {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery TotalLC
        /// </summary>
        public decimal TotalLCFC {
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

        /// <summary>
        /// Represents the ERP delivery Freight.
        /// </summary>
        public decimal FreightFC {
            get; set;
        }

        ///// <summary>
        ///// Represents the ERP Delivery ItemKey 
        ///// </summary>
        //[Required]
        //[MaxLength(100)]
        //public string ERPDeliveryItemKey {
        //  get; set;
        //}

        /// <summary>
        /// Represents the Serial Or BatchNo
        /// </summary>
        public string SerialOrBatchNo {
            get; set;
        }
      
    }
}


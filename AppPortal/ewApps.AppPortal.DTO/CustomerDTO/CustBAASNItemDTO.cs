using System;

namespace ewApps.AppPortal.DTO
{

  public class CustBAASNItemDTO
  {
        public Guid ID {
            get; set;
        }

        public Guid ASNId {
            get; set;
        }


        /// <summary>
        /// Represents the ERP delivery item key.
        /// </summary>
        public string ERPASNKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERP contract key
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERP   item key.
        /// </summary>
        public string ERPItemKey {
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
        /// Represents the   delivery item Quantity.
        /// </summary>
        public decimal Quantity {
            get; set;
        }

        /// <summary>
        /// Unit of item.
        /// </summary>    
        public string QuantityUnit {
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
        /// Represents the   delivery item UnitPrice.
        /// </summary>
        public string Unit {
            get; set;
        }

        public decimal?  Tax {
            get; set;
        }


        /// <summary>
        /// Represents the ERP delivery TaxCode.
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
        /// Represents the Serial Or BatchNo
        /// </summary>
        public string SerialOrBatchNo {
            get; set;
        }
     
    }
}

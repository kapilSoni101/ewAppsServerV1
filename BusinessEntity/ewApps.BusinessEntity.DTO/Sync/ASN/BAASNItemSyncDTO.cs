using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DTO{
    public class BAASNItemSyncDTO {
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
        /// Represents the ERP delivery id
        /// </summary>
        public Guid ASNId {
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

        /// <summary>
        /// Represents the   delivery item DiscountPercent.
        /// </summary>
        public decimal DiscountPercent {
            get; set;
        }
        /// <summary>
        /// Discount amount after applied discount on actual amount.
        /// </summary>
        public decimal DiscountAmount {
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

        /// <summary>
        /// Represents the Serial Or BatchNo
        /// </summary>
        public string SerialOrBatchNo {
            get; set;
        }
        /// <summary>
        /// TotalLC
        /// </summary>
        public decimal? GLAmount {
            get; set;
        }

        public string Whse {
            get; set;
        }
        /// <summary>
        /// Ship from address.
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }


        /// <summary>
        /// ShipToAddress
        /// </summary>
        public string ShipToAddress {
            get; set;
        }


        /// <summary>
        /// Bill to address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }
        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BAASNItem MapToEntity(BAASNItemSyncDTO model) {
            BAASNItem entity = new BAASNItem();
            entity.ERPASNKey = model.ERPASNKey;
            entity.ERPItemKey = model.ERPItemKey;
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ASNId = model.ASNId;
            entity.BlanketAgreementNo = model.BlanketAgreementNo;
            entity.DiscountPercent = model.DiscountPercent;
            entity.DiscountAmount = model.DiscountAmount;
            entity.Freight = model.Freight;
            entity.FreightFC = model.FreightFC;
            entity.ItemName = model.ItemName;
            entity.ItemID = model.ItemID;
            entity.Quantity = model.Quantity;
            entity.QuantityUnit = model.QuantityUnit;
            entity.SerialOrBatchNo = model.SerialOrBatchNo;
            entity.TaxCode = model.TaxCode;
            entity.TaxPercent = model.TaxPercent;
            entity.TotalLC = model.TotalLC;
            entity.TotalLCFC = model.TotalLCFC;
            entity.UnitPrice = model.UnitPrice;
            entity.UnitPriceFC = model.UnitPriceFC;
            entity.ShipFromAddress = model.ShipFromAddress;
            entity.ShipToAddress = model.ShipToAddress;
            entity.BillToAddress = model.BillToAddress;
            entity.Unit = model.Unit;
            entity.GLAmount = model.GLAmount;
            entity.Whse = model.Whse;
            return entity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.DMService;

namespace ewApps.BusinessEntity.DTO {

    public class BusBAItemMasterViewDTO {

        /// <summary>
        /// 
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ERPItemKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ItemType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ItemName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BarCode {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int PriceUnit {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PriceUniText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingTypeText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ManagedItem {
            get; set;
        }

        /// <summary>
        /// Application active flag.
        /// </summary>
        public string Active {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseLength {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseLengthUnit {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PurchaseLengthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseWidth {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseWidthUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PurchaseWidthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseHeight {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseHeightUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PurchaseHeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseVolume {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseVolumeUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PurchaseVolumeUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseWeight {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseWeightUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PurchaseWeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal SalesLength {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int SalesLengthUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SalesLengthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal SalesWidth {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SalesWidthUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SalesWidthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal SalesHeight {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SalesHeightUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SalesHeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal SalesVolume {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int SalesVolumeUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SalesVolumeUnitText {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SalesWeight {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int SalesWeightUnit {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string SalesWeightUnitText {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Remarks {
            get; set;
        }

        public DateTime UpdatedOn {
            get; set;
        }

        public string UpdatedByName {
            get; set;
        }

        [NotMapped]
        public List<BusBAItemAttachmentDTO> AttachmentList {
            get; set;
        }

        [NotMapped]
        public List<AddUpdateDocumentModel> listAttachment {
            get; set;
        }

        public static BAItemMaster MapToEntity(BusBAItemMasterViewDTO busBAItemMasterViewDTO) {
            BAItemMaster itemMaster = new BAItemMaster();

            itemMaster.Active = busBAItemMasterViewDTO.Active;
            itemMaster.BarCode = busBAItemMasterViewDTO.BarCode;
            //itemMaster.CreatedBy= busBAItemMasterViewDTO
            //itemMaster.CreatedOn = busBAItemMasterViewDTO
            //itemMaster.Deleted= busBAItemMasterViewDTO
            itemMaster.ERPConnectorKey = busBAItemMasterViewDTO.ERPConnectorKey;
            itemMaster.ERPItemKey = busBAItemMasterViewDTO.ERPItemKey;
            //itemMaster.ID= busBAItemMasterViewDTO
            itemMaster.ItemName = busBAItemMasterViewDTO.ItemName;
            itemMaster.ItemType = busBAItemMasterViewDTO.ItemType;
            itemMaster.ManagedItem = busBAItemMasterViewDTO.ManagedItem;
            itemMaster.Price = busBAItemMasterViewDTO.Price;
            //itemMaster.PriceFC = busBAItemMasterViewDTO.
            itemMaster.PriceUnit = busBAItemMasterViewDTO.PriceUnit;
            itemMaster.PriceUniText = busBAItemMasterViewDTO.PriceUniText;
            itemMaster.PurchaseHeight = busBAItemMasterViewDTO.PurchaseHeight;
            itemMaster.PurchaseHeightUnit = busBAItemMasterViewDTO.PurchaseHeightUnit;
            itemMaster.PurchaseHeightUnitText = busBAItemMasterViewDTO.PurchaseHeightUnitText;
            itemMaster.PurchaseLength = busBAItemMasterViewDTO.PurchaseLength;
            itemMaster.PurchaseLengthUnit = busBAItemMasterViewDTO.PurchaseLengthUnit;
            itemMaster.PurchaseLengthUnitText = busBAItemMasterViewDTO.PurchaseLengthUnitText;
            itemMaster.PurchaseVolume = busBAItemMasterViewDTO.PurchaseVolume;
            itemMaster.PurchaseVolumeUnit = busBAItemMasterViewDTO.PurchaseVolumeUnit;
            itemMaster.PurchaseVolumeUnitText = busBAItemMasterViewDTO.PurchaseVolumeUnitText;
            itemMaster.PurchaseWeight = busBAItemMasterViewDTO.PurchaseWeight;
            itemMaster.PurchaseWeightUnit = busBAItemMasterViewDTO.PurchaseWeightUnit;
            itemMaster.PurchaseWeightUnitText = busBAItemMasterViewDTO.PurchaseWeightUnitText;
            itemMaster.PurchaseWidth = busBAItemMasterViewDTO.PurchaseWidth;
            itemMaster.PurchaseWidthUnit = busBAItemMasterViewDTO.PurchaseWidthUnit;
            itemMaster.PurchaseWidthUnitText = busBAItemMasterViewDTO.PurchaseWidthUnitText;
            itemMaster.Remarks = busBAItemMasterViewDTO.Remarks;
            itemMaster.SalesHeight = busBAItemMasterViewDTO.SalesHeight;
            itemMaster.SalesHeightUnit = busBAItemMasterViewDTO.SalesHeightUnit;
            itemMaster.SalesHeightUnitText = busBAItemMasterViewDTO.SalesHeightUnitText;
            itemMaster.SalesLength = busBAItemMasterViewDTO.SalesLength;
            itemMaster.SalesLengthUnit = busBAItemMasterViewDTO.SalesLengthUnit;
            itemMaster.SalesLengthUnitText = busBAItemMasterViewDTO.SalesLengthUnitText;
            itemMaster.SalesVolume = busBAItemMasterViewDTO.SalesVolume;
            itemMaster.SalesVolumeUnit = busBAItemMasterViewDTO.SalesVolumeUnit;
            itemMaster.SalesVolumeUnitText = busBAItemMasterViewDTO.SalesVolumeUnitText;
            itemMaster.SalesWeight = busBAItemMasterViewDTO.SalesWeight;
            itemMaster.SalesWeightUnit = busBAItemMasterViewDTO.SalesWeightUnit;
            itemMaster.SalesWeightUnitText = busBAItemMasterViewDTO.SalesWeightUnitText;
            itemMaster.SalesWidth = busBAItemMasterViewDTO.SalesWidth;
            itemMaster.SalesWidthUnit = busBAItemMasterViewDTO.SalesWidthUnit;
            itemMaster.SalesWidthUnitText = busBAItemMasterViewDTO.SalesWidthUnitText;
            itemMaster.ShippingType = busBAItemMasterViewDTO.ShippingType;
            itemMaster.ShippingTypeText = busBAItemMasterViewDTO.ShippingTypeText;
            //itemMaster.TenantId= busBAItemMasterViewDTO.
            //itemMaster.UpdatedBy= busBAItemMasterViewDTO.
            itemMaster.UpdatedOn = busBAItemMasterViewDTO.UpdatedOn;            
            
            return itemMaster;
        }

        public static BAItemMaster UpdateMapToEntity(BusBAItemMasterViewDTO busBAItemMasterViewDTO,  BAItemMaster itemMaster) {
            
            itemMaster.Active = busBAItemMasterViewDTO.Active;
            itemMaster.BarCode = busBAItemMasterViewDTO.BarCode;
            //itemMaster.CreatedBy= busBAItemMasterViewDTO
            //itemMaster.CreatedOn = busBAItemMasterViewDTO
            //itemMaster.Deleted= busBAItemMasterViewDTO
            itemMaster.ERPConnectorKey = busBAItemMasterViewDTO.ERPConnectorKey;
            itemMaster.ERPItemKey = busBAItemMasterViewDTO.ERPItemKey;
            //itemMaster.ID= busBAItemMasterViewDTO
            itemMaster.ItemName = busBAItemMasterViewDTO.ItemName;
            itemMaster.ItemType = busBAItemMasterViewDTO.ItemType;
            itemMaster.ManagedItem = busBAItemMasterViewDTO.ManagedItem;
            itemMaster.Price = busBAItemMasterViewDTO.Price;
            //itemMaster.PriceFC = busBAItemMasterViewDTO.
            itemMaster.PriceUnit = busBAItemMasterViewDTO.PriceUnit;
            itemMaster.PriceUniText = busBAItemMasterViewDTO.PriceUniText;
            itemMaster.PurchaseHeight = busBAItemMasterViewDTO.PurchaseHeight;
            itemMaster.PurchaseHeightUnit = busBAItemMasterViewDTO.PurchaseHeightUnit;
            itemMaster.PurchaseHeightUnitText = busBAItemMasterViewDTO.PurchaseHeightUnitText;
            itemMaster.PurchaseLength = busBAItemMasterViewDTO.PurchaseLength;
            itemMaster.PurchaseLengthUnit = busBAItemMasterViewDTO.PurchaseLengthUnit;
            itemMaster.PurchaseLengthUnitText = busBAItemMasterViewDTO.PurchaseLengthUnitText;
            itemMaster.PurchaseVolume = busBAItemMasterViewDTO.PurchaseVolume;
            itemMaster.PurchaseVolumeUnit = busBAItemMasterViewDTO.PurchaseVolumeUnit;
            itemMaster.PurchaseVolumeUnitText = busBAItemMasterViewDTO.PurchaseVolumeUnitText;
            itemMaster.PurchaseWeight = busBAItemMasterViewDTO.PurchaseWeight;
            itemMaster.PurchaseWeightUnit = busBAItemMasterViewDTO.PurchaseWeightUnit;
            itemMaster.PurchaseWeightUnitText = busBAItemMasterViewDTO.PurchaseWeightUnitText;
            itemMaster.PurchaseWidth = busBAItemMasterViewDTO.PurchaseWidth;
            itemMaster.PurchaseWidthUnit = busBAItemMasterViewDTO.PurchaseWidthUnit;
            itemMaster.PurchaseWidthUnitText = busBAItemMasterViewDTO.PurchaseWidthUnitText;
            itemMaster.Remarks = busBAItemMasterViewDTO.Remarks;
            itemMaster.SalesHeight = busBAItemMasterViewDTO.SalesHeight;
            itemMaster.SalesHeightUnit = busBAItemMasterViewDTO.SalesHeightUnit;
            itemMaster.SalesHeightUnitText = busBAItemMasterViewDTO.SalesHeightUnitText;
            itemMaster.SalesLength = busBAItemMasterViewDTO.SalesLength;
            itemMaster.SalesLengthUnit = busBAItemMasterViewDTO.SalesLengthUnit;
            itemMaster.SalesLengthUnitText = busBAItemMasterViewDTO.SalesLengthUnitText;
            itemMaster.SalesVolume = busBAItemMasterViewDTO.SalesVolume;
            itemMaster.SalesVolumeUnit = busBAItemMasterViewDTO.SalesVolumeUnit;
            itemMaster.SalesVolumeUnitText = busBAItemMasterViewDTO.SalesVolumeUnitText;
            itemMaster.SalesWeight = busBAItemMasterViewDTO.SalesWeight;
            itemMaster.SalesWeightUnit = busBAItemMasterViewDTO.SalesWeightUnit;
            itemMaster.SalesWeightUnitText = busBAItemMasterViewDTO.SalesWeightUnitText;
            itemMaster.SalesWidth = busBAItemMasterViewDTO.SalesWidth;
            itemMaster.SalesWidthUnit = busBAItemMasterViewDTO.SalesWidthUnit;
            itemMaster.SalesWidthUnitText = busBAItemMasterViewDTO.SalesWidthUnitText;
            itemMaster.ShippingType = busBAItemMasterViewDTO.ShippingType;
            itemMaster.ShippingTypeText = busBAItemMasterViewDTO.ShippingTypeText;
            //itemMaster.TenantId= busBAItemMasterViewDTO.
            //itemMaster.UpdatedBy= busBAItemMasterViewDTO.
            itemMaster.UpdatedOn = busBAItemMasterViewDTO.UpdatedOn;

            return itemMaster;
        }

    }
}

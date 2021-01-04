using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// 
    /// </summary>
    public class BAContractItemSyncDTO {

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPContractKey {
            get; set;
        }

        public Guid ContractId {
            get; set;
        }
        public Guid ItemId {
            get; set;
        }
        /// <summary>
        /// Unique key of item coming from ERP connector.
        /// </summary>        
        public string ERPItemKey {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int ItemNo {
            get; set;
        }

        public string ItemDescription {
            get; set;
        }

        public string ItemGroup {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal PlannedQuantity {
            get; set;
        }

        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int CumulativeCommittedQuantity {
            get; set;
        }

        public decimal CumulativeCommittedAmount {
            get; set;
        }

        public int CumulativeQuantity {
            get; set;
        }

        public decimal CumulativeAmountLC {
            get; set;
        }

        public int OpenQuantity {
            get; set;
        }

        public decimal OpenAmountLC {
            get; set;
        }

        public string Project {
            get; set;
        }

        public string FreeText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndOfWarranty {
            get; set;
        }

        public string UoMCode {
            get; set;
        }

        public string UoMName {
            get; set;
        }

        public string UoMGroup {
            get; set;
        }

        public decimal ItemsPerUnit {
            get; set;
        }

        public string PortionofReturnsPerc {
            get; set;
        }

        public int ShippingType {
            get; set;
        }

        public string ShippingTypeText {
            get; set;
        }

        public int ItemRowStatus {
            get; set;
        }

        public string ItemRowStatusText {
            get; set;
        }
        // <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BAContractItem MapToEntity(BAContractItemSyncDTO model) {
            BAContractItem entity = new BAContractItem();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPContractKey = model.ERPContractKey;
            entity.ContractId = model.ContractId;
            entity.ERPItemKey = model.ERPItemKey;
            entity.ItemId = model.ItemId;
            entity.CumulativeAmountLC = model.CumulativeAmountLC;
            entity.CumulativeCommittedAmount = model.CumulativeCommittedAmount;
            entity.CumulativeCommittedQuantity = model.CumulativeCommittedQuantity;
            entity.CumulativeQuantity = model.CumulativeQuantity;
            entity.EndOfWarranty = model.EndOfWarranty;
            entity.FreeText = model.FreeText;
            entity.ItemDescription = model.ItemDescription;
            entity.ItemGroup = model.ItemGroup;
            entity.ItemNo = model.ItemNo;
            entity.ItemRowStatus = model.ItemRowStatus;
            entity.ItemRowStatusText = model.ItemRowStatusText;
            entity.ItemsPerUnit = model.ItemsPerUnit;
            entity.OpenAmountLC = model.OpenAmountLC;
            entity.OpenQuantity = model.OpenQuantity;

            entity.PlannedQuantity = model.PlannedQuantity;
            entity.PortionofReturnsPerc = model.PortionofReturnsPerc;
            entity.Project = model.Project;
            entity.ShippingType = model.ShippingType;
            entity.ShippingTypeText = model.ShippingTypeText;
            entity.UnitPrice = model.UnitPrice;
            entity.UoMCode = model.UoMCode;
            entity.UoMGroup = model.UoMGroup;
            entity.UoMName = model.UoMName;
            return entity;
        }
    }
}

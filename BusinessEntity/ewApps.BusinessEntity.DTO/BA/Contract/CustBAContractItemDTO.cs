using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// 
    /// </summary>
    public class CustBAContractItemDTO
  {

        public string ERPConnectorKey {
            get; set;
        }

        public Guid ContractID {
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
    }
}

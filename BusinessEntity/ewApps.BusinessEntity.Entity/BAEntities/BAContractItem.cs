/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 08 August 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {


    /// <summary>
    /// Represents properties for BAContract entity.
    /// </summary>
    [Table("BAContractItem", Schema = "be")]
    public class BAContractItem:BaseEntity {

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPContractKey {
            get; set;
        }

        [Required]
        public Guid ItemId {
            get; set;
        }
        /// <summary>
        /// Unique key of item coming from ERP connector.
        /// </summary>    
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPItemKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid ContractId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ItemNo {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ItemDescription {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ItemGroup {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal PlannedQuantity {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int CumulativeCommittedQuantity {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal CumulativeCommittedAmount {
            get; set;
        }

        public int CumulativeQuantity {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal CumulativeAmountLC {
            get; set;
        }

        public int OpenQuantity {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal OpenAmountLC {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Project {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public string UoMName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string UoMGroup {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal ItemsPerUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PortionofReturnsPerc {
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
        public int ItemRowStatus {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ItemRowStatusText {
            get; set;
        }
    }
}

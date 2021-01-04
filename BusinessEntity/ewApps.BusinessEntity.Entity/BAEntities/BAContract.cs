/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 05 September 2019
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
    [Table("BAContract", Schema = "be")]
    public class BAContract:BaseEntity {

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
        public string ERPCustomerKey {
            get; set;
        }

        [MaxLength(50)]
        [Required]
        public string ERPContractKey {
            get; set;
        }

        /// <summary>
        /// Unique number of Contract .
        /// </summary>
        [MaxLength(100)]
        public string ERPDocNum {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string BPCurrency {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string TelephoneNo {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string Email {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int DocumentNo {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AgreementMethod {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDate {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BPProject {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? TerminationDate {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? SigningDate {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AgreementType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PaymentTerms {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PaymentMethod {
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
        public int Status {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks {
            get; set;
        }
        
        public string Owner {
            get; set;
        }

    }
}

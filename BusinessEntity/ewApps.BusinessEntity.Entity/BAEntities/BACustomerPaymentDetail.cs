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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {

    /// <summary>
    /// Represents properties for CustomerPaymentDetail entity.
    /// </summary>
    [Table("BACustomerPaymentDetail", Schema = "be")]
    public class BACustomerPaymentDetail :BaseEntity{

        /// <summary>
        /// SAP connector key .
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// unique key for customer .
        /// </summary> 
        [Required]
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// SAP customer key .
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int CreditCardType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string CreditCardTypeText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string CreditCardNo {
            get; set;
        }

        /// <summary>
        /// Card expiration date .
        /// </summary>
        public DateTime? ExpirationDate {
            get; set;
        }

        /// <summary>
        /// IDNumber
        /// </summary>
        public int IDNumber {
            get; set;
        }

        /// <summary>
        /// customer bank country .
        /// </summary>
        [MaxLength(200)]
        public string BankCountry {
            get; set;
        }

        /// <summary>
        /// customer bank name .
        /// </summary>
        [MaxLength(200)]
        public string BankName {
            get; set;
        }

        /// <summary>
        /// customer bank code .
        /// </summary>
        [MaxLength(100)]
        public string BankCode {
            get; set;
        }

        /// <summary>
        /// customer bank account .
        /// </summary>
        [MaxLength(200)]
        public string Account {
            get; set;
        }

        /// <summary>
        /// customer BICSWIFTCode .
        /// </summary>
        [MaxLength(200)]
        public string BICSWIFTCode {
            get; set;
        }

        /// <summary>
        /// customer bank account name .
        /// </summary>
        [MaxLength(200)]
        public string BankAccountName {
            get; set;
        }

        /// <summary>
        /// customer banl branch name .
        /// </summary>
        [MaxLength(200)]
        public string Branch {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Default {
            get; set;
        }

        /// <summary>
        /// customer route number . 
        /// </summary>
        [MaxLength(20)]
        public string ABARouteNumber {
            get; set;
        }

        /// <summary>
        /// customer accont type .
        /// </summary>
        public int AccountType {
            get; set;
        }

        /// <summary>
        /// string value of customer accont type.
        /// </summary>
        [MaxLength(100)]
        public string AccountTypeText {
            get; set;
        }
    }
}

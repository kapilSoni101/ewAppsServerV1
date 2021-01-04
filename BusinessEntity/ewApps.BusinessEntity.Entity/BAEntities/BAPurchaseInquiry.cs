/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Anil Nigam<anigam @eworkplaceapps.com>
 * Date:07 july 2019
 * 
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
    /// Theme entity represting all PurchaseInquiry properties.
    /// </summary>
     [Table("BAPurchaseInquiry", Schema = "be")]
    public class BAPurchaseInquiry : BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "BAPurchaseInquiry";

        /// <summary>
        /// Represents the ERP Customer key.
        /// </summary>
        [MaxLength(100)]
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// Represents the customer name.
        /// </summary>
        [MaxLength(100)]
        public string CustomerName {
            get; set;
        }


        /// <summary>
        /// Represents the contact person name of customer.
        /// </summary>
        [MaxLength(100)]
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Represents the customer ref no.
        /// </summary>
        [MaxLength(100)]
        public string CustomerRefNo {
            get; set;
        }


        /// <summary>
        /// Represents the status.
        /// </summary>
        public char Status {
            get; set;
        }

        /// <summary>
        /// Represents the status text.
        /// </summary>
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Represents the posting date of purchase inquiry.
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// Represents the Valid date of purchase inquiry
        /// </summary>
        public DateTime ValidUntil {
            get; set;
        }


        /// <summary>
        /// Represents the document date
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Represents the total
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Total {
            get; set;
        }

        /// <summary>
        /// Represents the remarks
        /// </summary>
        [MaxLength(4000)]
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Represents the ERPShipToAddressKey
        /// </summary>
        [MaxLength(100)]
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the ShipToAddress
        /// </summary>
        [MaxLength(4000)]
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ERPPayToAddressKey
        /// </summary>
        [MaxLength(100)]
        public string ERPPayToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the PayToAddress
        /// </summary>
        [MaxLength(4000)]
        public string PayToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingText
        /// </summary>
        public string ShippingText {
            get; set;
        }


    }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 07 July 2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {

    [Table("BASalesOrder", Schema = "be")]
    public class BASalesOrder:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "BASalesOrder";

        [MaxLength(100)]
        public string ERPSalesOrderKey {
            get; set;
        }

        /// <summary>
        /// Unique number of SalesOrder .
        /// </summary>
        [MaxLength(100)]
        public string ERPDocNum {
            get; set;
        }

        [MaxLength(100)]
        public string ERPConnectorKey {
            get; set;
        }

        [MaxLength(100)]
        public string ERPCustomerKey {
            get; set;
        }

        public Guid CustomerID {
            get; set;
        }

        [MaxLength(100)]
        public string CustomerName {
            get; set;
        }

        [MaxLength(100)]
        public string ContactPerson {
            get; set;
        }

        [MaxLength(100)]
        public string CustomerRefNo {
            get; set;
        }

        [MaxLength(100)]
        public string LocalCurrency {
            get; set;
        }

        public int Status {
            get; set;
        }

        [MaxLength(100)]
        public string StatusText {
            get; set;
        }

        public DateTime PostingDate {
            get; set;
        }

        public DateTime DeliveryDate {
            get; set;
        }

        public DateTime DocumentDate {
            get; set;
        }

        [MaxLength(4000)]
        public string PickAndPackRemarks {
            get; set;
        }

        [MaxLength(100)]
        public string SalesEmployee {
            get; set;
        }

        [MaxLength(100)]
        public string Owner {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalBeforeDiscount {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalBeforeDiscountFC {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Discount {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal DiscountFC {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Freight {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal FreightFC {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Tax {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal TaxFC {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalPaymentDue {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalPaymentDueFC {
            get; set;
        }

        [MaxLength(40000)]
        public string Remarks {
            get; set;
        }       

        [MaxLength(4000)]
        public string ShipFromAddress {
            get; set;
        }

        [MaxLength(100)]
        public string ShipFromAddressKey {
            get; set;
        }

        [MaxLength(100)]
        public string ERPShipToAddressKey {
            get; set;
        }

        [MaxLength(4000)]
        public string ShipToAddress {
            get; set;
        }

        [MaxLength(100)]
        public string ERPBillToAddressKey {
            get; set;
        }

        [MaxLength(4000)]
        public string BillToAddress {
            get; set;
        }

        public int ShippingType {
            get; set;
        }

        [MaxLength(100)]
        public string ShippingTypeText {
            get; set;
        }

    }
}

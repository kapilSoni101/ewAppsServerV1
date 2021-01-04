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
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {

    [Table("BASalesQuotation", Schema = "be")]
    public class BASalesQuotation:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "BASalesQuotation";

        /// <summary>
        /// ERPSalesQuotationKey will be not null if comingfrom ERP connector.
        /// It unique id of SalesQuotation table of ERP connector table.
        /// </summary>
        [MaxLength(100)]
        public string ERPSalesQuotationKey {
            get; set;
        }

        /// <summary>
        /// Unique number of SalesQuotation .
        /// </summary>
        [MaxLength(100)]
        public string ERPDocNum {
            get; set;
        }

        /// <summary>
        /// ERP connector key.        
        /// </summary>
        [MaxLength(100)]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// ERPCustomerKey will be not null if coming from ERP connector.
        /// It unique id of Customer table of ERP connector table.
        /// </summary>
        [MaxLength(100)]
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// Unique id of Customet table.
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// Customer name.
        /// </summary>
        [MaxLength(100)]
        public string CustomerName {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// Name of Contact person.
        /// </summary>
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Reference id of customer.
        /// </summary>
        [MaxLength(100)]
        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// Local currency.
        /// </summary>
        [MaxLength(100)]
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Current status of SalesQuotation.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Status display text.
        /// </summary>
        [MaxLength(100)]
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Posting date.
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// SalesQuote Valid untill.
        /// </summary>
        public DateTime ValidUntil {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Sales Employees.
        /// </summary>
        [MaxLength(100)]
        public string SalesEmployee {
            get; set;
        }

        /// <summary>
        /// Owner
        /// </summary>
        [MaxLength(100)]
        public string Owner {
            get; set;
        }

        /// <summary>
        /// Total amount before discount.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Total amount before discount.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalBeforeDiscountFC {
            get; set;
        }

        /// <summary>
        /// Discount on  quote.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Discount on  quote.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal DiscountFC {
            get; set;
        }

        /// <summary>
        /// Frright charges.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Freight {
            get; set;
        }

        /// <summary>
        /// Frright charges.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal FreightFC {
            get; set;
        }

        /// <summary>
        /// Tax on quote.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Tax on quote.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal TaxFC {
            get; set;
        }

        /// <summary>
        /// Total payment due.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Total payment due.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Comment on notes.
        /// </summary>
        [MaxLength(4000)]
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// ERPShipToAddressKey will be not null if coming from ERP connector.
        /// It unique id of address table of ERP connector table.
        /// </summary>
        [MaxLength(100)]
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// ShipFromAddress 
        /// </summary>
        [MaxLength(4000)]
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// ShipFromAddressKey will be not null if coming from ERP connector.
        /// </summary>
        [MaxLength(100)]
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        [MaxLength(4000)]
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// ERPBillToAddressKey will be not null if coming from ERP connector.
        /// It unique id of address.
        /// </summary>
        [MaxLength(100)]
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// bill to address.
        /// </summary>
        [MaxLength(4000)]
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// TYpe of shipping.
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// shipping text.
        /// </summary>
        public string ShippingTypeText {
            get; set;
        }

        #region IValidator<App> Members

        /// <inheritdoc />
        public bool Validate(out IList<EwpErrorData> brokenRules) {
            brokenRules = BrokenRules(this).ToList<EwpErrorData>();
            return brokenRules.Count > 0;
        }

        /// <inheritdoc />
        public IEnumerable<EwpErrorData> BrokenRules(BASalesQuotation entity) {
            //Check for application name is required.
            if(entity.CustomerId == Guid.Empty) {
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "Customer",
                    Message = "Customer is required."
                };
            }
        }

        #endregion

    }
}

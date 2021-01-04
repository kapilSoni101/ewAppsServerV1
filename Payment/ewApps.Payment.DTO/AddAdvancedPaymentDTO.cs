/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda, Amit Mundra
 * Date: 16 October 2019
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

    public class AddAdvancedPaymentDTO {

        public Guid ID {
            get; set;
        }

        public Guid BusinessId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }


        public Guid PartnerId {
            get; set;
        }

        public Guid InvoiceId {
            get; set;
        }

        public decimal TotalAmount {
            get; set;
        }

        public decimal AmountPaid {
            get; set;
        }

        public decimal AmountPaidFC {
            get; set;
        }        


        // Business currency
        public string LocalCurrency {
            get; set;
        }

        public string CustomerCurrency {
            get; set;
        }

        // 'credit' or 'debit'
        public string PaymentType {
            get; set;
        }

        // blank right now
        public string PaymentEntryType {
            get; set;
        }

        public int CheckNumber {
            get; set;
        }

        /// <summary>
        /// Front image copy of the check associated with the account as Base64 encoded string
        /// Needed for POP, ICL, BOC
        /// </summary>
        public string CheckImageFront {
            get; set;
        }
        /// <summary>
        /// Back image copy of the check associated with the account as Base64 encoded string
        /// Needed for POP, ICL, BOC
        /// </summary>
        public string CheckImageBack {
            get; set;
        }

        public string Note {
            get; set;
        }

        public DateTime OriginationDate {
            get; set;
        }

        /// <summary>
        /// Adding customer DTO to get customer detail 
        /// from application when actual payment is made intead of creating it when it is added to application.
        /// COnnector has responsibility to add and update it whenever required.
        /// </summary>
        public UserPaymentInfoModel userPaymentInfoModel {
            get; set;
        }

        /// <summary>
        /// Serviceid from which source payment is going to make.
        /// </summary>
        public Guid AppServiceId {
            get; set;
        }

        /// <summary>
        /// Service attrubute id from which source payment is going to make.
        /// </summary>
        public Guid AppServiceAttributeId {
            get; set;
        }



        /// <summary>
        /// Client Ip address where Payment done 
        /// </summary>
        public string ClientIP {
            get; set;
        }

        /// <summary>
        /// Payment account id.
        /// </summary>
        public Guid CustomerAccountId {
            get; set;
        }

        /// <summary>
        /// Save customer account info.
        /// </summary>
        public bool SaveAccountInfo {
            get; set;
        } = false;

        public string PayeeName {
            get; set;
        }

        /// <summary>
        /// Client Browser Information where Payment Done 
        /// </summary>
        public string ClientBrowser {
            get; set;
        }


        /// <summary>
        /// Client Operating System Information where Payment Done 
        /// </summary>
        public string ClientOS {
            get; set;
        }

        /// <summary>
        /// Whether want to pay amount in Business or customer currency.
        /// </summary>
        public bool PaymentAmountInBusinessCurrency {
            get; set;
        }

        /// <summary>
        /// Payment currency.
        /// </summary>
        public string PaymentTransectionCurrency {
            get; set;
        }

        /// <summary>
        /// Payment can be Advance, Invoice
        /// </summary>
        public int TransectionPaymentType {
            get; set;
        }

    }
}

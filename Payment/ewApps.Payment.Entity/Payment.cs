/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Payment.Entity {

    /// <summary>
    /// Contains payment table property.
    /// </summary>
    [Table("Payment", Schema = "pay")]
    public class Payment:BaseEntity {
        // TODO: Complete documentation of Transaction entity class.

        #region ewApps data

        // ewApps Data
        /// <summary>
        /// ewApps Business Id (corresponds to MerchantId)
        /// </summary>
        public Guid BusinessId {
            get; set;
        }
        /// <summary>
        /// ewApps Partner Id (corresponds to CustomerId)
        /// </summary>
        public Guid PartnerId {
            get; set;
        }

        [MaxLength(100)]
        [Required]
        public string IdentityNumber {
            get; set;
        }

        #endregion ewApps data

        #region Transaction Data

        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// The transaction value (or amount)
        /// </summary>
        public decimal Amount {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// The transaction value (or amount) in foreign currency.
        /// </summary>
        public decimal AmountFC {
            get; set;
        }

        [MaxLength(4000)]
        /// <summary>
        /// Description of the transaction
        /// </summary>
        public string Description {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// String ('debit', 'credit')
        /// Debit is ACH transaction that is intended to withdraw funds from a Receiver’s account 
        /// for deposit into Merchant’s Settlement Account
        /// Credit is ACH transaction that is intended to deposit funds into a Receiver’s account 
        /// which has been withdrawn from Merchant’s Settlement Account.
        /// </summary>
        public string Type {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// The check_number is the identifying number of a check
        /// </summary>
        public string CheckNumber {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// Front image copy of the check associated with the account as Base64 encoded string
        /// Needed for POP, ICL, BOC
        /// </summary>
        public string CheckImageFront {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// Back image copy of the check associated with the account as Base64 encoded string
        /// Needed for POP, ICL, BOC
        /// </summary>
        public string CheckImageBack {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// Full customer credentials which includes name, routing number, account number and account type, or customer id token that begins with prefix 'cus_xxxxxxxxxxx'.
        /// An object: either id or whole object with fields: name, account_number, routing_number, account_type
        /// </summary>
        public string CustomerName {
            get; set;
        }

        [MaxLength(100)]
        public string CustomerAccountNumber {
            get; set;
        }

        [MaxLength(100)]
        public string CustomerRoutingNumber {
            get; set;
        }

        public int CustomerAccountType {
            get; set;
        }

        public string CardType {
            get;set;
        }

        public string AuthCode {
            get; set;
        }

        public string TransactionId {
            get; set;
        }

        public string PayeeName {
            get;set;
        }

        #endregion Transaction Data

        /// <summary>
        /// Payment currency.
        /// </summary>
        public string PaymentTransectionCurrency {
            get;set;
        }

        /// <summary>
        /// Payment can be Advance, Invoice
        /// </summary>
        public int PaymentType {
            get;set;
        }

        /// <summary>
        /// Payment done by PreAuth or not. 
        /// </summary>
        public Guid? PreAuthPaymentID {
            get;set;
        }

        #region Status Data

        /// <summary>
        /// Date of transaction origination
        /// </summary>
        public DateTime OriginationDate {
            get; set;
        }

        /// <summary>
        /// Last known status update date/time
        /// </summary>
        public DateTime LastStatusUpdateDate {
            get; set;
        }
        /// <summary>
        /// Last Known Status record Id
        /// </summary>
        public Guid LastTransactionStatusId {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// Last known status value
        /// 
        /// Possible values:
        /// 'A', Pending, 
        /// The transaction has been initiated and accepted for processing by VeriCheck.
        /// Transactions can be voided while in 'Pending' status.
        /// Transactions automatically change from 'Pending' status at 6PM Eastern Time.
        /// 
        /// 'B', Originated
        /// Transaction has been pulled from the system for processing through the Federal Reserve.
        /// Transaction may not be reversed or voided in this state
        /// 
        /// 'R', Returned
        /// Transaction has been returned by the customer's bank.
        /// For more information on types and reasons for returns, see: http://www.vericheck.com/ach-return-codes/
        /// 
        /// 'S', Settled
        /// Transaction has been funded to the Merchant's Bank.
        /// Transaction can be reversed/refunded in this state*/
        /// </summary>
        public string Status {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// Last known status reason, if any
        /// </summary>
        public string Reason {
            get; set;
        }

        #endregion Stasus Data

        #region Payment Service Id and Attribute 

        /// <summary>
        /// Id of AppService table.
        /// </summary>
        public Guid AppServiceId {
            get; set;
        }

        /// <summary>
        /// Id of AppServiceAttribute table. 
        /// </summary>
        public Guid AppServiceAttributeId {
            get; set;
        }

        [MaxLength(20)]
        public string ReturnCode {
            get; set;
        }

        [MaxLength(100)]
        public string ReturnString {
            get; set;
        }

        #endregion Payment Service Id and Attribute 


    }
}
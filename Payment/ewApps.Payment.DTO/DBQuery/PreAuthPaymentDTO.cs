using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// Detail of pre-auth-payment.
    /// </summary>
    public class PreAuthPaymentDTO {

        #region ewApps data

        // ewApps Data
        /// <summary>
        /// ewApps Business Id (corresponds to MerchantId)
        /// </summary>
        public Guid BusinessId {
            get; set;
        }
        /// <summary>
        /// CustomerId
        /// </summary>
        public Guid BACustomerId {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        ///  saved cutomer account table id.
        /// </summary>
        public Guid CustomerAccountDetailId {
            get; set;
        }

        #endregion ewApps data

        #region Transaction Data

        /// <summary>
        /// The transaction value (or amount)
        /// </summary>
        public decimal Amount {
            get; set;
        }

        /// <summary>
        /// The transaction value (or amount) in foreign currency.
        /// </summary>
        public decimal AmountFC {
            get; set;
        }

        /// <summary>
        /// How much amount remained after using some amount from actual Amount.
        /// </summary>
        public decimal RemainingAmount {
            get; set;
        }

        /// <summary>
        /// How much amount remained after using some amount from actual Amount.
        /// </summary>
        public decimal RemainingAmountFC {
            get; set;
        }

        /// <summary>
        /// Description of the transaction
        /// </summary>
        public string Description {
            get; set;
        }

        /// <summary>
        /// Full customer credentials which includes name, routing number, account number and account type, or customer id token that begins with prefix 'cus_xxxxxxxxxxx'.
        /// An object: either id or whole object with fields: name, account_number, routing_number, account_type
        /// </summary>
        public string NameOnCard {
            get; set;
        }

        public string CardNumber {
            get; set;
        }

        /// <summary>
        /// Cardtype like Vise, Master, American Express etc.
        /// </summary>
        public string CardType {
            get; set;
        }

        /// <summary>
        /// Card auth code after authorized payment.
        /// </summary>
        public string AuthCode {
            get; set;
        }

        /// <summary>
        /// Card transection id.
        /// </summary>
        public string TransactionId {
            get; set;
        }

        public string PayeeName {
            get; set;
        }

        /// <summary>
        /// Payment currency.
        /// </summary>
        public string PaymentTransectionCurrency {
            get; set;
        }

        #endregion Transaction Data        

        #region Status Data

        /// <summary>
        /// Date of transaction origination
        /// </summary>
        public DateTime OriginationDate {
            get; set;
        }

        public DateTime ExpirationDate {
            get; set;
        }

        public string Status {
            get; set;
        }

        public string Captured {
            get;set;
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

        #endregion Payment Service Id and Attribute 


        /// <summary>
        /// Number of times can be use for payment.
        /// </summary>
        public int MaxTotalPaymentCount {
            get; set;
        }

        public int CurrentPaymentSequenceNumber {
            get; set;
        }

        public string AccountJson {
            get;set;
        }
    }
}

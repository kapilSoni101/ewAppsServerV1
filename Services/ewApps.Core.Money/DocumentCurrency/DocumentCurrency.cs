using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Core.Money {
    /// <summary>
    /// Entity class for document currency
    /// </summary>
    [Table("DocumentCurrency", Schema = "core")]
    public class DocumentCurrency {
        /// <summary>
        /// Unique identity fire
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }
        /// <summary>
        /// Amount used in document in Base currency
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal DocumentAmount {
            get; set;
        }
        /// <summary>
        /// Document base currency code
        /// </summary>
        public int DocumentCurrencyCode {
            get; set;
        }
        /// <summary>
        /// Document foreign currency code
        /// </summary>
        public int AgentCurrencyCode {
            get; set;
        }
        /// <summary>
        /// Conversion rate used when document is created/Updated
        /// </summary>
        private decimal _approxConversionRate;
        [Column(TypeName = "decimal (18,5)")]
        public decimal ApproxConversionRate {
            get {
                return _approxConversionRate;
            }
            set {
                if(DocumentCurrencyCode == AgentCurrencyCode)
                    _approxConversionRate = 1;
                else
                    _approxConversionRate = value;
            }
        }

        private decimal _finalConversionRate;
        /// <summary>
        /// Currency cnversion rate used when document is settled
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal FinalConversionRate {
            get {
                return _finalConversionRate;
            }
            set {
                if(DocumentCurrencyCode == AgentCurrencyCode)
                    _finalConversionRate = 1;
                else
                    _finalConversionRate = value;
            }
        }

        /// <summary>
        /// Document amont in Foreign currency
        /// </summary>
        private decimal _agentAmount;
        [Column(TypeName = "decimal (18,5)")]
        public decimal AgentAmount {
            get {
                return _agentAmount;
            }
            set {
                if(DocumentCurrencyCode == AgentCurrencyCode)
                    _agentAmount = DocumentAmount;
                else
                    _agentAmount = value;
            }
        }

        /// <summary>
        /// Document type to which this data is referred like transaction,payment etc.
        /// </summary>
        [MaxLength(100)]
        public string DocumentType {
            get; set;
        }
        /// <summary>
        /// Source id of the document by which this data can be extracted uniquelly
        /// </summary>
        public Guid DocumentId {
            get; set;
        }
        /// <summary>
        /// Datetime when the documentis created and the conversion rate is used for Approx conversion rate
        /// </summary>
        public DateTime CreationDate {
            get; set;
        }
        /// <summary>
        /// Datetime when the documentis created and the conversion rate is used for final conversion rate
        /// </summary>
        public DateTime SettlementDate {
            get; set;
        }

    }
}

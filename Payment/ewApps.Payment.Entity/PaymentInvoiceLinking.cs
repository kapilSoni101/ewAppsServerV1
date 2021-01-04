/* Date: 24 September 2018
*
* Contributor/s: Amit
* Last Updated On: 19 December 2018
*/
using ewApps.Core.BaseService;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ewApps.Payment.Entity {
    [Table("PaymentInvoiceLinking", Schema = "pay")]
    /// <summary>
    /// Entity will contains the all invoice payment history.
    /// </summary>    
    public class PaymentInvoiceLinking:BaseEntity {

        [Required]
        /// <summary>
        /// Invoice payment transection id.
        /// </summary>
        public Guid PaymentId {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// The transaction value (or amount)
        /// </summary>
        public decimal Amount {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// The transaction value (or amount)
        /// </summary>
        public decimal AmountFC {
            get; set;
        }

        [Required]
        /// <summary>
        /// ewApps Invoice Id for which the transaction is issued
        /// </summary>
        public Guid InvoiceId {
            get; set;
        }

    }
}

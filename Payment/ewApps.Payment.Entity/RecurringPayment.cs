/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.Payment.Entity {

    /// <summary>
    /// Contains recurring payment table property.
    /// </summary>
    [Table("RecurringPayment", Schema = "pay")]
    public class RecurringPayment:BaseEntity {

        /// <summary>
        /// ewApps recurring payment primary Id 
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// ewApps recurring period 
        /// </summary>
        public int RecurringPeriod {
            get; set;
        }

        /// <summary>
        /// ewApps recurring terms
        /// </summary>
        public int RecurringTerms {
            get; set;
        }


        /// <summary>
        /// start date 
        /// </summary>
        public DateTime StartDate {
            get; set;
        }

        /// <summary>
        /// end date 
        /// </summary>
        public DateTime EndDate {
            get; set;
        }

        /// <summary>
        /// remaining term count
        /// </summary>
        public int RemainingTermCount {
            get; set;
        }


        /// <summary>
        /// next payment date 
        /// </summary>
        public DateTime NextPaymentdate {
            get; set;
        }

        [Required]
        /// <summary>
        /// customer Id 
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// order Id 
        /// </summary>
        public string OrderId {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// Amount for each pay.
        /// </summary>
        public decimal TermAmount {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// Total amount to pay
        /// </summary>
        public decimal TotalAmount {
            get; set;
        }

        /// <summary>
        /// Active/Inactive
        /// </summary>
        public int Status {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// Tax apply on each pay
        /// </summary>
        public decimal InvoiceTax {
            get; set;
        }

        /// <summary>
        /// Customer Account detail id.
        /// </summary>
        public Guid CustomerAccountId {
            get; set;
        }

        /// <summary>
        /// Json of recurring detail.
        /// </summary>
        [MaxLength(4000)]
        public string Payload {
            get; set;
        }

        #region Validatation

        /// <summary>
        /// Validating Recurring Payment.
        /// </summary>
        /// <param name="brokenRules"></param>
        /// <returns></returns>
        public bool Validate(out IList<EwpErrorData> brokenRules) {
            brokenRules = BrokenRules(this).ToList<EwpErrorData>();
            return brokenRules.Count > 0;
        }

        /// <inheritdoc />
        public IEnumerable<EwpErrorData> BrokenRules(RecurringPayment entity) {
            //Check for employee's first name is required.
            if(string.IsNullOrEmpty(entity.OrderId)) {
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "OrderId",
                    Message = "OrderId is required."
                };
            }
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

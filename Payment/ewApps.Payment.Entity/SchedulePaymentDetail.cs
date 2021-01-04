/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@batchmaster.com>
 * Date: 16 October 2019
 * 
 * Contributor/s: 
 * Last Updated On: 16 October 2019
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
    /// Contains Schedule payment table property.
    /// </summary>
    [Table("SchedulePaymentDetail", Schema = "pay")]
    public class SchedulePaymentDetail:BaseEntity {

        /// <summary>
        /// Reference Id of SchedulePayment.
        /// </summary>
        public Guid SchedulePaymentId {
            get; set;
        }

        /// <summary>
        /// Customer unique id.
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// Configured payment account id.
        /// </summary>
        public Guid CustomerAccountId {
            get; set;
        }

        /// <summary>
        /// Unique invoiceId
        /// </summary>
        public Guid InvoiceID {
            get; set;
        }

        /// <summary>
        /// Amount to pay for invoice.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// Amount for each pay.
        /// </summary>
        public decimal InvoicePayableAmount {
            get; set;
        }
       

        #region Validatation

        #endregion Validatation

    }
}

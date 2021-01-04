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
    [Table("SchedulePayment", Schema = "pay")]
    public class SchedulePayment:BaseEntity {          

        /// <summary>
        /// start date 
        /// </summary>
        public DateTime ScheduleDate {
            get; set;
        }

        [MaxLength(100)]
        /// <summary>
        /// order Id 
        /// </summary>
        public string IdentityNo {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        /// <summary>
        /// Amount for each pay.
        /// </summary>
        public decimal PayableAmount {
            get; set;
        }

        /// <summary>
        /// Active/Inactive
        /// </summary>
        public int Status {
            get; set;
        }

        #region Validatation

        #endregion Validatation

    }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 7 May 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 7 May 2019
 */

using ewApps.Core.BaseService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Payment.Entity {
    /// <summary>
    /// Contains PaymentLog table property.
    /// </summary>
    [Table("PaymentLog", Schema = "pay")]
    public class PaymentLog:BaseEntity {
        // TODO: Complete documentation of Payment Log entity class.

        #region Client Information 
        /// <summary>
        /// Client Ip address where Payment done 
        /// </summary>
        public string ClientIP {
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
        /// payment transection id.
        /// </summary>
        public Guid PaymentId {
            get; set;
        }

        #endregion
    }
}

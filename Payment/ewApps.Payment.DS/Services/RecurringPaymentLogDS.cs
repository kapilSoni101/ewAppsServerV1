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
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Payment.Data;
using ewApps.Payment.Entity;

namespace ewApps.Payment.DS {

    /// <summary>
    /// Enum for payment state, whether payment sucessfully processed or error occured.
    /// </summary>
    public enum RecurringPaymentLogStateEnum {
        Processed = 1, Error = 2, AccountNotFound = 3, RecurringInActivate = 4
    }

    public class RecurringPaymentLogDS:BaseDS<RecurringPaymentLog>, IRecurringPaymentLogDS {

        #region Member variable 

        IRecurringPaymentLogRepository _recurringPaymentLogRepository;

        #endregion Member variable 

        #region Contructor

        /// <summary>Initializing local variablesInitializing local variables</summary>        
        /// <param name="recurringPaymentLogRepository"></param>       
        public RecurringPaymentLogDS(IRecurringPaymentLogRepository recurringPaymentLogRepository) : base(recurringPaymentLogRepository) {
            _recurringPaymentLogRepository = recurringPaymentLogRepository;
        }

        #endregion

    }
}

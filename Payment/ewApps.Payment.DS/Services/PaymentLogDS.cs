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
using ewApps.Payment.Data;
using ewApps.Payment.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Payment.DS {

    /// <summary>
    /// Class is used to pay  payment log.
    /// 
    /// </summary>
    public class PaymentLogDS:BaseDS<PaymentLog>, IPaymentLogDS {

        #region Local Member

        IPaymentLogRepository _paymentLogRepository;

        #endregion

        #region Constructor

        /// <param name="paymentLogRepository"></param>    
        public PaymentLogDS(IPaymentLogRepository paymentLogRepository) : base(paymentLogRepository) {
            _paymentLogRepository = paymentLogRepository;
        }

        #endregion Constructor

    }
}

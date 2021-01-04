/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.Data {

    /// <summary>
    /// Contains payment to add pament entery for a invoice, also support method to get paid payment history for a payment.
    /// </summary>
    public class PaymentRepository:BaseRepository<ewApps.Payment.Entity.Payment, PaymentDbContext>, IPaymentRepository {

        #region Constructor

        /// <summary>
        /// Payment data container constructor.
        /// </summary>
        /// <param name="context">A dbcontext.</param>
        /// <param name="sessionManager">Session manager.</param>
        public PaymentRepository(PaymentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor        

    }
}

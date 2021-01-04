using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;

namespace ewApps.Payment.Data {

    /// <summary>
    /// Contains payment information to authorize/block some amount for later use.
    /// </summary>
    public class PreAuthPaymentRepository:BaseRepository<PreAuthPayment, PaymentDbContext>, IPreAuthPaymentRepository {

        #region Constructor

        /// <summary>
        /// PreAuthPayment data container constructor.
        /// </summary>
        /// <param name="context">A dbcontext.</param>
        /// <param name="sessionManager">Session manager.</param>
        public PreAuthPaymentRepository(PaymentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor  

    }
}

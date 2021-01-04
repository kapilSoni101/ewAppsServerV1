using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Entity;

namespace ewApps.Payment.Data {

    /// <summary>
    /// It will create link for each invoice transection and payment.
    /// </summary>
    public class PaymentInvoiceLinkingRepository:BaseRepository<PaymentInvoiceLinking, PaymentDbContext>, IPaymentInvoiceLinkingRepository {

        /// <summary>
        /// Default construction with necessary DI object.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        /// <param name="connectionManager"></param>
        public PaymentInvoiceLinkingRepository(PaymentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }      

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Entity;

namespace ewApps.Payment.Data {

  public class RecurringPaymentLogRepository:BaseRepository<RecurringPaymentLog, PaymentDbContext>, IRecurringPaymentLogRepository {

    public RecurringPaymentLogRepository(PaymentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

    }
  }
}

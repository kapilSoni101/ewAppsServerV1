using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.Data {

    /// <summary>
    /// Contains payment to add pament entery for a invoice, also support method to get paid payment history for a payment.
    /// </summary>
    public interface IPaymentRepository:IBaseRepository<ewApps.Payment.Entity.Payment> {        

    }
}

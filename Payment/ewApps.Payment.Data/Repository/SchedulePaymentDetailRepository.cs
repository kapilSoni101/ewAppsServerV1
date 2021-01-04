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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Entity;
using ewApps.Core.DbConProvider;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.Data {

    /// <summary>
    /// Dataclass for recurring payment.
    /// </summary>
    public class SchedulePaymentDetailRepository:BaseRepository<SchedulePaymentDetail, PaymentDbContext>, ISchedulePaymentDetailRepository {

        /// <summary>
        /// Init local variables.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public SchedulePaymentDetailRepository(PaymentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #region Get

        #endregion Get

    }
}

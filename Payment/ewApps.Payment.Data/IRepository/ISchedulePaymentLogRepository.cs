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
  /// Provide support method for add/update recurring schedule.
  /// </summary>
  public interface ISchedulePaymentLogRepository: IBaseRepository<SchedulePaymentLog> {

    }
}

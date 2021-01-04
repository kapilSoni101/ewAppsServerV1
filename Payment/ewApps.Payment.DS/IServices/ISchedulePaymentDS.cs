/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@batchmaster.com>
 * Date: 16 October 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 16 October 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.ScheduledJobService;
using ewApps.Payment.Entity;

namespace ewApps.Payment.DS {

  public interface ISchedulePaymentDS: IBaseDS<SchedulePayment> {

  }
}

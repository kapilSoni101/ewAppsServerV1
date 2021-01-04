/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 27 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 27 February 2019
 */

using System;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Core.ScheduledJobService {
    /// <summary>
    /// This class contains scheduler master data to be initialized on new database setup.
    /// </summary>
    public class SchedulerMasterData {
        /// <summary>
        /// Generates scheduler master data.
        /// </summary>
        /// <param name="builder">The model builder instance to generate database records..</param>
        public static void Init(ModelBuilder builder) {
            #region Scheduler
            //DateTime nextExecutionTime=  DateTime.SpecifyKind(DateTime.Parse(DateTime.UtcNow.ToString("dd-MMM-yyyy hh:mm")), DateTimeKind.Utc);
            builder.Entity<Scheduler>().HasData(
                        new {
                            ID = new Guid("72504d45-3128-45f2-aa68-078a3d5eb20f"),
                            SchedulerName = "ScheduledPaymentScheduler",
                            FrequencyType = (int)FrequencyType.Minute,
                            FrequencyValue = 1,
                            EndDate = DateTime.UtcNow.AddYears(20),
                            Active = Convert.ToBoolean(true),
                            NextExecutionTime = DateTime.SpecifyKind(DateTime.Parse(DateTime.UtcNow.ToString("dd-MMM-yyyy hh:mm")), DateTimeKind.Utc),
                            UpdatedOn = DateTime.UtcNow,
                        });
            #endregion
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.ExceptionService;

namespace ewApps.Core.ScheduledJobService {
    public class SchedulerHelper {

        public static DateTime CalculateNextExecutionDateTime(DateTime currentScheduleDateTime, int frequencyType, int frequencyValue) {
            DateTime calculatedExecutionDateTime;
            switch(frequencyType) {
                case (int)FrequencyType.Minute:
                    calculatedExecutionDateTime = currentScheduleDateTime.AddMinutes(frequencyValue);
                    break;
                case (int)FrequencyType.Hour:
                    calculatedExecutionDateTime = currentScheduleDateTime.AddHours(frequencyValue);
                    break;
                case (int)FrequencyType.Day:
                    calculatedExecutionDateTime = currentScheduleDateTime.AddDays(frequencyValue);
                    break;
                case (int)FrequencyType.Month:
                    calculatedExecutionDateTime = currentScheduleDateTime.AddMonths(frequencyValue);
                    break;
                case (int)FrequencyType.Year:
                    calculatedExecutionDateTime = currentScheduleDateTime.AddYears(frequencyValue);
                    break;
                default:
                    throw new EwpInvalidOperationException("Invalid Frequency Type.");
            }
            return calculatedExecutionDateTime;
        }
    }
}

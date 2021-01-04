using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.ScheduledJobService {
    [Table("Scheduler", Schema = "core")]
    public class Scheduler {
        #region Constructor

        /// <summary>
        /// Default contstructor to initialize variables (if any).
        /// </summary>
        public Scheduler() {
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Scheduler Id.
        /// </summary>
        public Guid ID {
            get;
            set;
        }

        /// <summary>
        /// Job Name
        /// </summary>
        [MaxLength(100)]
        public string SchedulerName {
            get; set;
        }

        /// <summary>
        /// Job Active Status
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// Frequency Type.
        /// </summary>
        public int FrequencyType {
            get; set;
        }

        /// <summary>
        /// Frequency Value.
        /// </summary>
        public int FrequencyValue {
            get;
            set;
        }

        /// <summary>
        /// End Date (in UTC).
        /// </summary>
        public DateTime? EndDate {
            get; set;
        }

        /// <summary>
        /// Last Execution Time (in UTC).
        /// </summary>
        public DateTime? LastExecutionTime {
            get; set;
        }


        /// <summary>
        /// Next Execution Time (in UTC).
        /// </summary>
        public DateTime NextExecutionTime {
            get; set;
        }

        /// <summary>
        /// The modified date-time (in UTC).
        /// </summary>
        public DateTime UpdatedOn {
            get; set;
        }

        #endregion

    }
}

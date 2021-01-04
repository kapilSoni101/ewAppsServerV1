namespace ewApps.Core.ScheduledJobService {
    /// <summary>
    /// This enum defines scheduled job's possible status.
    /// </summary>
    public enum ScheduledJobStatusEnum {
        NotProcessed = 1,
        Success = 2,
        Failed = 3,
        Unknown = 4
    }

    /// <summary>
    /// This enum defines workflow possible step.
    /// </summary>
    public enum WorkflowStepEnum {
        InProcess = 1,
        CalledSource = 2,
        Completed = 3
    }

    /// <summary>
    /// This enum defines frequency type.
    /// </summary>
    public enum FrequencyType {
        Minute = 1,
        Hour = 2,
        Day = 3,
        Month = 4,
        Year = 5
    }
}

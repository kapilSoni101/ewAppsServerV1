using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.NotificationService {
    // ToDo: Notification Review: Nitin: Correct Class Name.
    public class GenerateNotificationDTO {

        public int ModuleId {
            get; set;
        }

        public long EventId {
            get; set;
        }

        public bool UseCacheForTemplate {
            get; set;
        }

        public Dictionary<string, object> EventInfo {
            get; set;
        }

        public Guid LoggedinUserId {
            get; set;
        }

        public bool NotificationToLoginUser {
            get; set;
        } = true;

    }
}

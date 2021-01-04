using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class TenantUpdateForPublisherDTO {
        public Guid UserId {
            get; set;
        }

        public string FirstName {
            get; set;
        }

        public string LastName {
            get; set;
        }

        public string FullName {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public string TenantName {
            get; set;
        }

        public bool TenantActiveState {
            get; set;
        }

    }
}

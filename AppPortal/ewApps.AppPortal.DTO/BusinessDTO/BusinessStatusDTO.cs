using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class BusinessStatusDTO {

        public Guid TenantId {
            get; set;
        }
        public bool Configured {
            get; set;
        }
        public bool IntegratedMode {
            get; set;
        }
    }
}

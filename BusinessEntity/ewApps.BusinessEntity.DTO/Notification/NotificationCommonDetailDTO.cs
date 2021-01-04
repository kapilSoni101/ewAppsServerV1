using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class NotificationCommonDetailDTO {
        public string SubDomainName {
            get; set;
        }

        public string PublisherName {
            get; set;
        }

        public string BusinessName {
            get; set;
        }

        public string Copyright {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        public string AppKey {
            get; set;
        }

        public string AppName {
            get; set;
        }

        public Guid BusinessTenantId {
            get; set;
        }

        public Guid PublisherTenantId {
            get; set;
        }
    }
}

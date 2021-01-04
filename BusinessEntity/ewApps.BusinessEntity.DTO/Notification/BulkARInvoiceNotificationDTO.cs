using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BulkARInvoiceNotificationDTO {
         
        public int NewInvoiceCount {
            get; set;
        }

        public int UpdatedInvoiceCount {
            get; set;
        }

        public string UpdatedByName {
            get; set;
        }

        public string UpdatedByUserNo {
            get; set;
        }

        public DateTime UpdatedDateTime {
            get; set;
        }

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

        public Guid BusinessTenantId {
            get; set;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class AttachmentReqDTO {

        public int BAEntityType {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }
        public string FileName {
            get; set;
        }

        public string ERPEntityKey {
            get; set;
        }
        public string ERPEntityAttachmentKey {
            get; set;
        }
    }
}

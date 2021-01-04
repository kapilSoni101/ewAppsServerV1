using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class PullERPDataReqDTO {

        public List<int> SelectedBAEntityTypes {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public string ERPEntityKey {
            get; set;
        }
    }
}

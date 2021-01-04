using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

    public class PullERPDataReqDTO {

        public List<int> SelectedBAEntityTypes {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public List<string> ERPEntityKey {
            get; set;
        }
    }
}

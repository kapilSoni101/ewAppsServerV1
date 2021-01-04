using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

    public class BAAddConnectionReqDTO {

        public string TenantId {
            get; set;
        }
        
        public string SAPDBName {
            get; set;
        }
        public string SAPBaseURL {
            get; set;
        }
        public string SAPAccessToken {
            get; set;
        }
        public string SAPServer {
            get; set;
        }
        public string ConnectorKey {
            get; set;
        }
    }
}

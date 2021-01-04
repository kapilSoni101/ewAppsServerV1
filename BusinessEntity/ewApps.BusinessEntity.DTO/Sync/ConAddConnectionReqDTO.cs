using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

    public class ConAddConnectionReqDTO {

        public string TenantId {
            get; set;
        }
        public string AppIdentityServerURL {
            get; set;
        }
        public string AppBaseURLName {
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

        /// <summary>
        /// Id that SAP B1 may need likeewapps uses tenantId
        /// </summary>
        public string SAPRefId {
            get; set;
        }
        public bool Active {
            get; set;
        }
        public DateTime CreatedOn {
            get; set;
        }
        public DateTime UpdatedOn {
            get; set;
        }
        public DateTime LastSyncOn {
            get; set;
        }
    }
}

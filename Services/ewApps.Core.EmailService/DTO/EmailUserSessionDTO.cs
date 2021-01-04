using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.EmailService {
    public class EmailUserSessionDTO {


        public Guid ID {
            get; set;
        }


        public string UserName {
            get; set;
        }


        public Guid TenantUserId {
            get; set;
        }


        public Guid TenantId {
            get; set;
        }


        public string TenantName {
            get; set;
        }

        public Guid AppId {
            get; set;
        }


        public string IdentityToken {
            get; set;
        }

        public int UserType {
            get;
            set;
        }

        public string Subdomain {
            get;
            set;
        }

        public string IdentityServerId {
            get;
            set;
        }
    }
}

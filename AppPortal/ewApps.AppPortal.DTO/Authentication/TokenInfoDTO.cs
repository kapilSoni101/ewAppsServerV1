using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
   public class TokenInfoDTO {

       
        public Guid TokenId {
            get;
            set;
        }
        
        public int TokenType {
            get;
            set;
        }
        
        public Guid TenantUserId {
            get;
            set;
        }

        public string AppKey {
            get;
            set;
        }
        public int UserType {
            get;
            set;
        }
    }
}

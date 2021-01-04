using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class ForgotPasswordDTO {

        public Guid TenantId {
            get;
            set;
        }
        public string Email {
            get;
            set;
        }
        public string Code {
            get;
            set;
        }
        public int userType {
            get;
            set;
        }
    }
}

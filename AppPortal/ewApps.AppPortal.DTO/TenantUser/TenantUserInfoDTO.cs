using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
  public  class TenantUserInfoDTO {
        public Guid TenantUserId {
            get; set;
        }

        public Guid IdentityUserId {
            get; set;
        }

        public string FirstName {
            get; set;
        }

        public string LastName {
            get; set;
        }

        public string FullName {
            get; set;
        }

        public string Email {
            get; set;
        }

        public string Code {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public string Phone {
            get; set;
        }

        public Guid TenantId {
            get;
            set;
        }

        public bool Active {
            get;
            set;
        } = true;

        public Tuple<string, bool> NewUser {
            get; set;
        }
    }
}

using System;
using System.Collections.Generic;

namespace ewApps.AppPortal.DTO {
    public class PublisherSignUpDTO {
        public Guid PublisherTenantId {
            get; set;
        }

        public string PublisherName {
            get; set;
        }

        public string SubDomain {
            get; set;
        }

        public Guid PrimaryTenantUserId {
            get; set;
        }

        public string PrimaryUserEmail {
            get; set;
        }

        public string PrimaryUserFirstName {
            get; set;
        }

        public string PrimaryUserLastName {
            get; set;
        }

        public string Phone {
            get; set;
        }


        public string PrimaryUserFullName {
            get; set;
        }



        public List<AppInfoDTO> AppInfoList {
            get; set;
        }
    }
}

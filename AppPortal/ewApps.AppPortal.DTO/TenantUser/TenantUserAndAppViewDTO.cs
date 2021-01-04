using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class TenantUserAndAppViewDTO {

        public Guid TenantUserId {
            get; set;
        }

        public string FirstName {
            get; set;
        }

        public string LastName {
            get; set;
        }

        public string Email {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public string CreatedBy {
            get; set;
        }

        public string Phone {
            get; set;
        }

        public DateTime? JoinedDate {
            get; set;
        }

        [NotMapped]
       public List<TenantUserAppPermissionDTO> TenantUserAppPermissionDTOs {
            get; set;
        }
    }
}

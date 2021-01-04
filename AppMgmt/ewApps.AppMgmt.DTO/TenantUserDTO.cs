using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DTO {
    public class TenantUserDTO {

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

        public static TenantUserDTO MapFromTenantUser(TenantUser tenantUser) {
            TenantUserDTO tenantUserDTO = new TenantUserDTO();
            tenantUserDTO.TenantUserId = tenantUser.ID;
            tenantUserDTO.Code = tenantUser.Code;
            tenantUserDTO.Email = tenantUser.Email;
            tenantUserDTO.FirstName = tenantUser.FirstName;
            tenantUserDTO.FullName = tenantUser.FullName;
            tenantUserDTO.IdentityNumber = tenantUser.IdentityNumber;
            tenantUserDTO.IdentityUserId = tenantUser.IdentityUserId;
            tenantUserDTO.LastName = tenantUser.LastName;
            tenantUserDTO.Phone = tenantUser.Phone;
            tenantUserDTO.TenantId = tenantUser.TenantId;
            return tenantUserDTO;
        }
    }
}

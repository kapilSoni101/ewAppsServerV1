using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DTO {
    public class TenantDTO {

        public string IdentityNumber {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string VarId {
            get; set;
        }

        public string SubDomainName {
            get; set;
        }

        public string LogoUrl {
            get; set;
        }

        public string Language {
            get; set;
        }

        public string TimeZone {
            get; set;
        }

        public string Currency {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public int TenantType {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }


        public DateTime? InvitedOn {
            get; set;
        }
        
        public DateTime? JoinedOn {
            get; set;
        }


        public Guid? InvitedBy {
            get; set;
        }

        public static TenantDTO MapFromTenant(Tenant tenant) {
            TenantDTO tenantDTO = new TenantDTO();
            tenantDTO.Active = tenant.Active;
            tenantDTO.Currency = tenant.Currency;
            tenantDTO.IdentityNumber = tenant.IdentityNumber;
            tenantDTO.InvitedBy = tenant.InvitedBy;
            tenantDTO.InvitedOn = tenant.InvitedOn;
            tenantDTO.JoinedOn = tenant.JoinedOn;
            tenantDTO.Language = tenant.Language;
            tenantDTO.LogoUrl = tenant.LogoUrl;
            tenantDTO.Name = tenant.Name;
            tenantDTO.SubDomainName = tenant.SubDomainName;
            tenantDTO.TenantId = tenant.ID;
            tenantDTO.TenantType = tenant.TenantType;
            tenantDTO.TimeZone = tenant.TimeZone;
            tenantDTO.VarId = tenant.VarId;
            return tenantDTO;
        }
    }
}

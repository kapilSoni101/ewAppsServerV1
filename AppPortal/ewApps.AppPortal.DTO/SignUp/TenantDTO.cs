using System;

namespace ewApps.AppPortal.DTO {
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


        public DateTime? joinedOn {
            get; set;
        }


        public Guid? InvitedBy {
            get; set;
        }

    }
}

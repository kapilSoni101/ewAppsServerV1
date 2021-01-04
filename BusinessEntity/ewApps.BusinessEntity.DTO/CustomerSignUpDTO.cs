using System;

namespace BusinessEntity.DTO {

  public class CustomerSignUpDTO {
        public Guid TenantId {
            get;
            set;
        }
        public Guid BusinesPartnerTenantId {
            get;
            set;
        }
        public Guid BusinesPrimaryUserId {
            get;
            set;
        }
        public string CutomerName {
            get;
            set;
        }
        public string Currency {
            get;
            set;
        }
    }
}

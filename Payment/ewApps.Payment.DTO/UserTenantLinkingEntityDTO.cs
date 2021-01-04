using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// A clone of UserTenantLinking entity.
    /// </summary>
    public class UserTenantLinkingEntityDTO: BaseDTO {

        /// <summary>
        /// The Id of TenantUser for which this linking belowngs.
        /// </summary>
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// The Id of SubTenant if the user is of partner type.
        /// </summary>
        public Guid? BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// User type of the user.
        /// </summary>
        public int UserType {
            get; set;
        }

        /// <summary>
        /// Partner type of the user.
        /// </summary>
        public int? PartnerType {
            get; set;
        }

        /// <summary>
        /// Tenant's primary user flag.
        /// </summary>
        public bool IsPrimary {
            get; set;
        }

    }
}

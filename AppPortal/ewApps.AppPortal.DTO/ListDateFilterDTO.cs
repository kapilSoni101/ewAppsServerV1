using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Contains the properties to filter item list by date/id and deleted.
    /// </summary>
    public class ListDateFilterDTO {

        /// <summary>
        /// From date.
        /// </summary>
        public DateTime FromDate {
            get; set;
        }

        /// <summary>
        /// From and ToDate range.
        /// </summary>
        public DateTime ToDate {
            get; set;
        }

        /// <summary>
        /// ID of entity to filter object like TenantId/CustomerId etc.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// SHow only deleted entity or not.
        /// </summary>
        public bool Deleted {
            get;
            set;
        }

        public DateTime FromDateMoment {
            get; set;
        }
        public DateTime ToDateMoment {
            get; set;
        }
    }
}

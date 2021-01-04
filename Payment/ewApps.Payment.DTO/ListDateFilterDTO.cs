using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

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

        /// <summary>
        /// Gets or sets from date moment.
        /// </summary>
        /// <value>
        /// From date moment.
        /// </value>
        public DateTime FromDateMoment {
            get; set;
        }

        /// <summary>
        /// Converts to datemoment.
        /// </summary>
        /// <value>
        /// To date moment.
        /// </value>
        public DateTime ToDateMoment {
            get; set;
        }
    }

}

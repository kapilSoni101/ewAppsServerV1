using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// After updateding business, folowwing business model return some properties.
    /// </summary>
    public class BusinessResponseModelDTO {

        //
        // Summary:
        //     Entity identityfier for the entity on which operation is performed.
        public Guid Id {
            get; set;
        }
        //
        // Summary:
        //     Success flag.
        public bool IsSuccess {
            get; set;
        }
        //
        // Summary:
        //     Additional message.
        public string Message {
            get; set;
        }

        /// <summary>
        /// Whether tenant active/inactive.
        /// </summary>
        public bool IsActive {
            get; set;
        }

    }
}

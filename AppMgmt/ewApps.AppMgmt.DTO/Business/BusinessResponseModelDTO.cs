using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// After updateding business, folowwing business model return some properties.
    /// </summary>
    //ToDo: nitin- class name is not correct.
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

        public List<UserAppRelationDTO> userAppRelationDTOs {
            get;
            set;
        }

    }
}

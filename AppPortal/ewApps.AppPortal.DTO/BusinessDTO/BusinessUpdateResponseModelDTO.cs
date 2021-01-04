using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// After updateding business, folowwing business model return some properties.
    /// </summary>
    public class BusinessUpdateResponseModelDTO {

        /// <summary>
        /// Entity identityfier for the entity on which operation is performed.
        /// </summary>
        public Guid Id {
            get; set;
        }

        /// <summary>
        /// Success flag.
        /// </summary>
        public bool IsSuccess {
            get; set;
        }


        /// <summary>
        /// Additional message.
        /// </summary>
        public string Message {
            get; set;
        }

        /// <summary>
        /// Whether tenant active/inactive.
        /// </summary>
        public bool IsActive {
            get; set;
        }

        //ToDo: nitin-Propety Name-userAppRelationDTOs 
        public List<UserAppRelationDTO> userAppRelationDTOs {
            get;
            set;
        }
    }
}

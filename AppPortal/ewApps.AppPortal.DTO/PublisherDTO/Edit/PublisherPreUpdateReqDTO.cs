using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// This class contains publisher signup related information.
    /// </summary>
    public class PublisherPreUpdateReqDTO {

        /// <summary>
        /// The application key list.
        /// </summary>
        public IEnumerable<Guid> SuscriptionPlanIdList {
            get; set;
        }

        /// <summary>
        /// The application identifier list.
        /// </summary>
        public IEnumerable<Guid> AppIdList {
            get; set;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// This class contains publisher signup related information.
    /// </summary>
    public class PublisherPreSignUpReqDTO {

        /// <summary>
        /// The sub domain of requested publisher tenant.
        /// </summary>
        public string SubDomain {
            get; set;
        }

        /// <summary>
        /// The application key list.
        /// </summary>
        public List<string> AppKeyList {
            get; set;
        }

        /// <summary>
        /// The application identifier list.
        /// </summary>
        public List<Guid> AppIdList {
            get;set;
        }

        /// <summary>
        /// The publisher's primary user email.
        /// </summary>
        public string UserEmail {
            get; set;
        }

    }
}

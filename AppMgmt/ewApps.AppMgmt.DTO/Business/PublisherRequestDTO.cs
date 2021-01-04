using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    ///  A request model to get publisher and user info.
    /// </summary>
    public class PublisherRequestDTO {

        public Guid PublisherTenantId {
            get; set;
        }

        public  Guid PubHomeAppId {
            get; set;
        }

        public int UserType {
            get; set;
        }

        public string UserEmail {
            get; set;
        }

    }
}

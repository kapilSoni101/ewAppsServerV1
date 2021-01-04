using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// Contains basic required information of a connector postal.
    /// </summary>
    public class ConnectorConfigDTO {

        /// <summary>
        /// Unqiue ID.
        /// </summary>
        public Guid ID {
            get;set;
        }

        /// <summary>
        /// Connactor json.
        /// </summary>
        public string json {
            get;set;
        }

        /// <summary>
        /// Connection is valid or not.
        /// </summary>
        public string Status {
            get; set;
        }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message {
            get; set;
        }

        /// <summary>
        /// A unque key to identify the connector.
        /// </summary>
        public string ConnectorKey {
            get;set;
        }

    }
}

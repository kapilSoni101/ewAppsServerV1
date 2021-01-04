using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    public class ConnectorConfigDQ:BaseDQ {
        public new Guid ID {
            get; set;
        }
       
        public string SettingJson {
            get; set;
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
            get; set;
        }

    }

}

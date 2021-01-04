using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    public class ConnectorConfigDQ: BaseDQ {

        public new Guid ID {
            get;
            set;
        }

        /// <summary>
        /// Setting Json
        /// </summary>
        public string SettingJson {
            get;
            set;
        }

        /// <summary>
        // Connection is valid or not.
        /// </summary>
        public string Status {
            get;
            set;
        }
     
        /// <summary>
        /// Mesage
        /// </summary>
        public string Message {
            get;
            set;
        }
        /// <summary>
        ///     A unque key to identify the connector. 
        /// </summary>        
        public string ConnectorKey {
            get;
            set;
        }
    }
}

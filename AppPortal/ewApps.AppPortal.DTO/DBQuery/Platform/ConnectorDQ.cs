using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO.DBQuery {
    public class ConnectorDQ :BaseDQ {
        
        /// <summary>
        /// Connector name
        /// </summary>
        public string ConnectorName {
            get;
            set;
        }
        
        /// <summary>
        /// Connector key 
        /// </summary>
        public string ConnectorKey {
            get;
            set;
        }

        /// <summary>
        /// Active
        /// </summary>
        public bool Active {
            get;
            set;
        }

    }
}

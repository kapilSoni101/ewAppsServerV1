using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// 
    /// </summary>
    public class ConnectorDQ :BaseDQ {

        /// <summary>
        /// 
        /// </summary>
        public string ConnectorName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectorKey {
            get; set;
        }

    }
}

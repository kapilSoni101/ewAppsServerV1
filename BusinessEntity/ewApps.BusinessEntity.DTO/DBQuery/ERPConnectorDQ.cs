using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// 
    /// </summary>
    public class ERPConnectorDQ:BaseDQ {

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

        /// <summary>
        /// Active
        /// </summary>
        public bool Active {
            get;
            set;
        }

    }
}

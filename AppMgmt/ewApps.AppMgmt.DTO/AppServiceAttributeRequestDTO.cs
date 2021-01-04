using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// Contains attribute detail.
    /// </summary>
    public class AppServiceAttributeRequestDTO {

        public Guid ID {
            get; set;
        }

        public string JsonData {
            get; set;
        }

        public string Name {
            get; set;
        }

        public object buinessVCACHPayAttrDTO {
            get; set;
        }
    }
}

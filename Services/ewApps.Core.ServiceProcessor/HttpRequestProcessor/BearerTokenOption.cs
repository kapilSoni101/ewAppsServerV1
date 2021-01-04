using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.ServiceProcessor {
    public class BearerTokenOption {

        /// <summary>
        /// Application key
        /// </summary>
        public string AppClientName {
            get; set;
        }

        /// <summary>
        /// Identity server url.
        /// </summary>
        public string AuthServiceUrl {
            get; set;
        }

        public string AuthScope {
            get; set;
        } = "authapi sapb1api payapi connvcapi";


    }
}

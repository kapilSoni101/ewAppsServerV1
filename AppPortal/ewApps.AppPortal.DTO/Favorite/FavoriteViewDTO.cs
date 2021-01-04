using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class FavoriteViewDTO {
       
        /// <summary>
        /// MenuId
        /// </summary>
        public string MenuKey {
            get; set;
        }

        /// <summary>
        /// URL
        /// </summary>
        public string Url {
            get; set;
        }

        /// <summary>
        /// Portal key
        /// </summary>
        public string PortalKey {
            get; set;
        }

        /// <summary>
        /// AppId
        /// </summary>        
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// System show default menu items
        /// </summary>
        public bool System {
            get;set;
        }

    }
}

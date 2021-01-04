using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
   public class FavoriteUpdateDTO {

        /// <summary>
        /// MenuId
        /// </summary>
        public string MenuKey {
            get; set;
        }

        /// <summary>
        /// AppId
        /// </summary>        
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFavorite {
            get;set;
        }
        
    }
}

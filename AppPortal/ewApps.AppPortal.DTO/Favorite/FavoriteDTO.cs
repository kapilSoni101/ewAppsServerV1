using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Is favorite item or system favorite menu item
    /// </summary>
    public class FavoriteDTO {

        /// <summary>
        /// System favorite
        /// </summary>
        public bool SystemFavorite {
            get; set;
        }

        /// <summary>
        /// Is favorite or not
        /// </summary>
        public bool IsFavorite {
            get; set;
        }

    }
}

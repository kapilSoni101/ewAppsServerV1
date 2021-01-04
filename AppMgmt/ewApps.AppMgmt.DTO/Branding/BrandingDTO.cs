using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;

namespace ewApps.AppMgmt.DTO {
    public class BrandingDTO :BaseDTO {


        /// <summary>
        /// 
        /// </summary>
        public new Guid ID {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public new Guid TenantId {
            get;
            set;
        }


        /// <summary>
        /// Name of the application.
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// PoweredBy of the application.
        /// </summary>
        public string PoweredBy {
            get;
            set;
        }

        /// <summary>
        /// CopyrightsText of the application.
        /// </summary>
        public string Copyright {
            get;
            set;
        }

        /// <summary>
        /// Theme Unique identifier of the application.
        /// </summary>
        public Guid ThemeId {
            get;
            set;
        }

        /// <summary>
        /// Themekey of the application.
        /// </summary>
        public string ThemeKey {
            get;
            set;
        }


        /// <summary>
        /// Thumbnail Unique identifier of the application.
        /// </summary>
        [NotMapped]
        public new Guid ThumbnailId {
            get;
            set;
        }

        /// <summary>
        /// ThumbnailUrl of the application.
        /// </summary>
        [NotMapped]
        public string ThumbnailUrl {
            get;
            set;
        }

        /// <summary>
        /// Thumbnail details model of the application
        /// </summary>
        [NotMapped]
        public ThumbnailAddAndUpdateDTO ThumbnailAddUpdateModel {
            get;
            set;
        }


    }
}

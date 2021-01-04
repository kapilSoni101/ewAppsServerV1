using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;

namespace ewApps.AppMgmt.DTO {
    public class BusinessBrandingDTO:BaseDTO {

        /// <summary>
        /// Unique identifier for the TenantAppSetting table.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Unique indentifer for Tenant Table
        /// </summary>
        public new Guid TenantId {
            get;
            set;
        }

        /// <summary>
        /// Unique indentifer for TenantSubscription Table
        /// </summary>
        public new Guid AppId {
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

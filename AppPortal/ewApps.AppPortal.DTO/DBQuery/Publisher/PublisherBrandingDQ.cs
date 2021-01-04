using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;

namespace ewApps.AppPortal.DTO.DBQuery {

    /// <summary>
    /// Publisher branding model for Get and Update
    /// </summary>
    public class PublisherBrandingDQ :BaseDQ {

            /// <summary>
            /// Unique identifier for the Publisher table.
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
            public Guid ThumbnailId {
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

            /// <summary>
            /// Thumbnail Unique identifier of the platform.
            /// </summary>                
            public Guid PlatformThumbnailId {
                get;
                set;
            }

            /// <summary>
            /// Thumbnail details model of the application
            /// </summary>
            [NotMapped]
            public ThumbnailAddAndUpdateDTO PlatThumbnailAddUpdateModel {
                get;
                set;
            }

            /// <summary>
            /// Publisher Customerzied Logo Thumbnail Flag.
            /// </summary>  
            [NotMapped]
            public bool CustomizedLogoThumbnail {
                get; set;
            }

            /// <summary>
            /// CanUpdateCopyRight Flag Indicate The Right Of Publisher To change CopyRight Tag.
            /// </summary>        
            public bool CanUpdateCopyright {
                get; set;
            }

        /// <summary>
        /// ApplyPoweredBy Flag Indicate The Right Of Publisher To get PoweredBy Tag.
        /// </summary>        
        public bool ApplyPoweredBy {
            get; set;
        }

    }
}

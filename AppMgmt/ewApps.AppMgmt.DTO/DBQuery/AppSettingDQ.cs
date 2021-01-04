using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// Contains application specific setting info.
    /// </summary>
    public class AppSettingDQ {

        public Guid AppId {
            get; set;
        }

        public string Name {
            get; set;
        }

        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// Tenant Language
        /// </summary>
        public string Language {
            get; set;
        }

        /// <summary>
        /// Tenant Currency
        /// </summary>
        public string Currency {
            get; set;
        }

        /// <summary>
        /// Tenant TimeZone
        /// </summary>
        public string TimeZone {
            get; set;
        }

        /// <summary>
        /// Tenant DateTimeFormat
        /// </summary>
        public string DateTimeFormat {
            get;
            set;
        }

    }
}

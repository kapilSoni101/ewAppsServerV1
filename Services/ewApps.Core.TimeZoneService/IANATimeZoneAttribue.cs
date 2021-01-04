using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.Helper {
    /// <summary>
    /// This class implements standard business logic and operations for IANA time zone attribute.
    /// </summary>    
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]

    public class IANATimeZoneAttribue:Attribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="IANATimeZoneAttribue"/> class.
        /// </summary>
        /// <param name="ianaTimeZoneId">The iana time zone identifier.</param>
        /// <param name="windowsTimeZoneId">The windows time zone identifier.</param>
        /// <param name="region">The region.</param>
        public IANATimeZoneAttribue(string ianaTimeZoneId, string windowsTimeZoneId, string region) {
            Initialize(ianaTimeZoneId, windowsTimeZoneId, region);
        }

        // Initializes local members of class.
        /// <summary>
        /// Initializes the specified iana time zone identifier.
        /// </summary>
        /// <param name="ianaTimeZoneId">The iana time zone identifier.</param>
        /// <param name="windowsTimeZoneId">The windows time zone identifier.</param>
        /// <param name="region">The region.</param>
        private void Initialize(string ianaTimeZoneId, string windowsTimeZoneId, string region) {
            IANATimeZoneId = ianaTimeZoneId;
            WindowsTimeZoneId = windowsTimeZoneId;
            Region = region;
        }

        /// <summary>
        /// Gets or sets the iana time zone identifier.
        /// </summary>
        /// <value>
        /// The iana time zone identifier.
        /// </value>
        public string IANATimeZoneId {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the windows time zone identifier.
        /// </summary>
        /// <value>
        /// The windows time zone identifier.
        /// </value>
        public string WindowsTimeZoneId {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region {
            get;
            set;
        }
    }
}

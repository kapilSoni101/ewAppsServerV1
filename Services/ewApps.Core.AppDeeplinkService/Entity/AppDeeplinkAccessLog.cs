using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Core.AppDeeplinkService {

    /// <summary>
    /// Contains log of clicked deeplink. 
    /// </summary>
    [Table("AppDeeplinkAccessLog", Schema = "core")]
    public class AppDeeplinkAccessLog {

        /// <summary>
        /// Uniue key of the table row
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Foreighn key, Key of AppDeppLink.
        /// </summary>
        public Guid AppDeeplinkId {
            get; set;
        }

        /// <summary>
        /// Time when the link is accessed
        /// </summary>
        public DateTime AccessTimestamp {
            get; set;
        }

        //Asha -???
        /// <summary>
        ///  Url used, (ActionEndpointUrl or Default Url)
        /// </summary>
        [MaxLength(100)]
        public string AccessUrl {
            get; set;
        }

        /// <summary>
        /// if stale or password wrong or error
        /// </summary>
        public bool AccessGranted {
            get; set;
        }

        /// <summary>
        /// Error text of not generated or error log for accessing the url.
        /// </summary>
        [MaxLength(100)]
        public string AccessNotGrantedReason {
            get; set;
        }

        /// <summary>
        /// IPAddress of machine from where deeplink is accessing.
        /// </summary>
        [MaxLength(100)]
        public string AccessorIPAddress {
            get; set;
        }

    }
}

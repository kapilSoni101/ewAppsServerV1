using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Core.AppDeeplinkService {

    [Table("AppDeeplink", Schema = "core")]
    public class AppDeeplink {

        /// <summary>
        /// Uniue key of the table row
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Rendom number generated for url.
        /// </summary>
        public long NumberId {
            get; set;
        }

        /// <summary>
        /// short url key.
        /// </summary>
        public string ShortUrlKey {
            get; set;
        }

        /// <summary>
        /// UserId, May be, who allowed to access or who generated the deeplink.
        /// </summary>
        public Guid? UserId {
            get; set;
        }

        /// <summary>
        /// TenantId  to which the link is relevent
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        ///  Max number of time user can access the url.
        /// </summary>
        public int MaxUseCount {
            get; set;
        }

        /// <summary>
        /// Numbe rof time user accessed the url.
        /// </summary>
        public int UserAccessCount {
            get; set;
        }

        /// <summary>
        /// Url expiration date, its optional.
        /// </summary>
        public DateTime? ExpirationDate {
            get; set;
        }

        /// <summary>
        /// User Password to access url.Not mendatory
        /// </summary>
        [MaxLength(20)]
        public string Password {
            get; set;
        }

        /// <summary>
        /// Payload contain a object json.
        /// Json data, contain all necessary information required for showing the data on clicked deep link.
        /// </summary>
        [MaxLength(4000)]
        public string ActionData {
            get; set;
        }

        /// <summary>
        /// The module can assign assign any name, preferably for example, <namespace>.<action-name>.
        /// Using ActionName, Can identify the module name, from where deeplink generated. 
        /// </summary>
        [MaxLength(100)]
        public string ActionName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string ActionEndpointUrl {
            get; set;
        }

    }
}

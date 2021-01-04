using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.Payment.Entity {

    [Table("ASNotification", Schema = "pay")]
    public class ASNotification:BaseEntity {

        /// <summary>
        /// The entity name publisher.
        /// </summary>
        public const string EntityName = "ASNotification";

        public Guid RecipientTenantUserId {
            get; set;
        }
        public string HtmlContent {
            get; set;
        }
        public string TextContent {
            get; set;
        }
        public string MetaData {
            get; set;
        }
        public bool ReadState {
            get; set;
        }
        public Guid EntityId {
            get; set;
        }
        public int EntityType {
            get; set;
        }
        public Guid? LinkNotificationId {
            get; set;
        }
        public Guid AppId {
            get; set;
        }
        //public Guid PortalId {
        //    get; set;
        //}
        public long LogType {
            get; set;
        }
        public string AdditionalInfo {
            get; set;
        }
    }
}
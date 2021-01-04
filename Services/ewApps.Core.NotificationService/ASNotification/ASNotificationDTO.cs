using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.NotificationService {
    public class ASNotificationDTO {
        //Id
        public Guid ID {
            get; set;
        }

        //RecipientUserId
        public Guid RecipientUserId {
            get; set;
        }

        //HtmlContent
        public string HtmlContent {
            get; set;
        }

        //TextContent
        public string TextContent {
            get; set;
        }

        //MetaData
        public string MetaData {
            get; set;
        }

        //Read
        public bool Read {
            get; set;
        }

        //SourceEntityType
        public int SourceEntityType {
            get; set;
        }

        //SourceEntityId
        public Guid SourceEntityId {
            get; set;
        }

        //LogType
        public long ASNotificationType {
            get; set;
        }

        //TenantId
        public Guid TenantId {
            get; set;
        }

        //PartnerTenantId
        public Guid PartnerTenantId {
            get; set;
        }


        //ApplicationId
        public Guid AppId {
            get; set;
        }


        //LinkNotificationId
        public Guid LinkNotificationId {
            get; set;
        }

        public string AdditionalInfo {
            get; set;
        }



        //CreatedOn
        //CreatedBy
        //UpdatedBy
        //UpdatedOn



    }
}

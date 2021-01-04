using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.NotificationService {
    public class NotificationRecipient {
        // LoginEmail,EmailPreference,PushPreference,DesktopPreference,RegionLanguage

        public Guid TenantUserId {
            get; set;
        }

        public string FullName {
            get; set;
        }

        public string Email {
            get; set;
        }

        public bool EmailPreference {
            get; set;
        }


        public string RegionLanguage {
            get; set;
        }

        public Guid ApplicationId {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        public int UserType {
            get; set;
        }

        [DefaultValue(false)]
        public bool SMSPreference {
            get; set;
        } = true;

        [DefaultValue("")]
        public string SMSRecipient {
            get; set;
        }

        //[NotMapped]
        [DefaultValue(true)]
        public bool ASPreference {
            get; set;
        } = true;

        //[NotMapped]
        //[DefaultValue("")]
        //public Guid ASRecipientUserId  {
        //    get; set;
        //}
    }
}


//AppUserId  -- appuser

//FullName  -- appuser

//Email -- appuser

//EmailPreference -- appuserpreference

//RegionLanguage -- tenantappsetting

//ApplicationId -- 

//TenantId -- tenantuserlinking

//UserType -- tenantuserlinking

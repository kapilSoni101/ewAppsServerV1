using System;

namespace ewApps.Core.NotificationService {
    public class SMSNotificationRecipient  {

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

    public string Phone {
            get; set;
        }
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

namespace ewApps.Core.NotificationService {
    /// <summary>This class defines notification constant.</summary>
    public static class NotificationConstants {
        //Engg - Should come from Config file
        /// <summary>Defines default language used in notification.</summary>
        public const string Language = "en-US";


        /// <summary>The business portal default URL</summary>
        public const string BusinessPortalDefaultUrl = "/businessportal";
        //public const string BusinessUserDefaultAction = "businessuser";
        //public const string EmployeeDefaultURL = "/employee";
        //public const string DocumentDefaultAction = "Document";

        #region Public Constants

        public static string DeeplinkInfoKey = "DeeplinkInfo";

        public static string TenantIdKey = "tenantid";

        public static string BusinessPartnerTenantIdKey = "businesspartnertenantid";

        public static string AppUserIdKey = "appuserid";

        public static string IdentityServerUserIdKey = "identityserveruserid";

        public static string UserTypeKey = "usertype";

        public static string CodeKey = "code";

        public static string TokenInfoIdKey = "tokeninfoid";

        public static string TokenTypeKey = "tokentype";

        public static string TenantLanguageKey = "tenantLanguage";

        public static string BranchKey = "branchkey";

        public static string TrackingIdKey = "TrackingId";

        public static string LinkNotificationIdKey = "LinkNotificationId";

        public static string EventDataXmlKey = "EventDataXml";

        public static string EventDataKey = "EventData";

        public static string AppIdKey = "appid";

        public static string UserSessionKey = "UserSession";

        public static string RecipientFullNameKey = "RecipientFullName";

        public static string EventNumberKey = "EventNumber";

        public static string CreatedOnKey = "createdOn";
        public static string CreatedByNameKey = "createdByName";
        public static string UpdatedByNameKey = "updatedByName";
        public static string UpdatedOnKey  = "updatedOn";
        public static string CopyrightKey = "copyright";

        public static string CustomerNameKey = "customerName";

        public static string SubDomainKey = "subDomain";

        public static string EntityIdKey = "entityId";

        public static string PublisherNameKey = "publisherName";
        public static string BusinessNameKey  = "businessName";

        public static string ASAdditionalInfoKey = "ASAdditionalInfo";
        #endregion
    }
}

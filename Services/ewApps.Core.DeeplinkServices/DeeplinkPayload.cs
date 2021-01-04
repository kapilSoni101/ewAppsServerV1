using System;
using System.Collections.Generic;

namespace ewApps.Core.DeeplinkServices {
    // It contains the payload to generate the deeplink, some of the fields are common for all links like moduleId and eventId
    // while other fields are link specific, which are stored in dictionary DeepLinkInfo

    public class DeeplinkPayload {
        // Set of common fields for all deeplinks.

        /// <summary>
        /// The parent module id.
        /// </summary>
        public int ModuleId;

        /// <summary>
        /// The event id.
        /// </summary>
        public long EventId;

        public Guid TenantId;

        public Guid LoggedinUserId;

        public Guid NotificationId;

        public Guid LinkNotificationId;

        // For each Deeplink generated:It keeps inforation abt Actions ,RelativeURL and other event parameters
        //it is a dictionary of required data for each link.    

        /// <summary>
        /// The deeplink information.
        /// </summary>
        /// <remarks>
        /// 1) This property is use when a single notification has multiple url but target app/url is same 
        /// and can be generate using same branch key.
        /// 2) This type (Dictionary&lt;string, Dictionary&lt;string, string&gt;&gt;) is required so that 
        /// if single email has multiple url like forgor password has two url one is for set password 
        /// and another for contact us so this dictionary will have two entries.
        /// </remarks>
        public Dictionary<string, Dictionary<string, string>> DeeplinkInfo;

        /// <summary>
        /// This property contains information to generate branch deeplink that are targeting multiple applications.
        /// </summary>
        /// <remarks>
        /// 1) When we are having different deeplink for different user in recipient list we will create list of deeplinks and 
        /// find suitable deeplink for users in abstract method.
        /// </remarks>
        public List<DeeplinkInfo> DeeplinkInfoList;
    }

     
    public class DeeplinkInfo {
        /// <summary>
        /// This is decision parameter that help to differenciate between information required to generate deeplink.
        /// </summary>
        /// <remarks>Like UserType</remarks>
        public string DeeplinkKey;

        /// <summary>
        /// This property contains information required to generate branch deeplink url.
        /// </summary>
        /// <remarks>Key in dictionary is a unique key to identify the placeholder of generated url in xsl/template file.</remarks>
        public Dictionary<string, Dictionary<string, string>> DeeplinkInfoList;
    }
}

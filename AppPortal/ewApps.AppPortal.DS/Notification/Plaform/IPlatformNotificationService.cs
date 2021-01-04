/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {
  public interface IPlatformNotificationService:INotificationService<NotificationRecipient> {
    //DeeplinkPayload GetDeeplinkPayload(NotificationPayload notificationPayload);
    //EmailPayload GetEmailPayload(NotificationPayload notificationPayload, NotificationRecipient recepientRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments);
    //Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload notificationPayload, DeeplinkResultSet deeplinkResultSet);
  }
}
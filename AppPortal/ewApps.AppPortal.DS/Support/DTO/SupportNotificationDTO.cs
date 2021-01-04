/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System.Collections.Generic;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This DTO is collection of the data required to genarate support notifications.
    /// </summary>
    public class SupportNotificationDTO {

    /// <summary>
    /// Support ticket.
    /// </summary>
    public SupportTicket SupportTicket {
      get; set;
    }

    /// <summary>
    /// Support comment.
    /// </summary>
    public SupportCommentDTO SupportCommentDTO {
      get; set;
    }

    /// <summary>
    /// Support comment.
    /// </summary>
    public UserSession UserSession {
      get; set;
    }

    /// <summary>
    /// Support comment.
    /// </summary>
    public string PublisherName {
      get; set;
    }

    /// <summary>
    /// Support comment.
    /// </summary>
    public Guid AppId {
      get; set;
    }

    /// <summary>
    /// Support comment.
    /// </summary>
    public string AppName {
      get; set;
    }

    /// <summary>
    /// Support comment.
    /// </summary>
    public bool IsPublisherAssinged {
      get; set;
    }

    /// <summary>
    /// Notifaication related data.
    /// </summary>
    public Dictionary<string, string> NotificationEventData {
      get; set;
    } = new Dictionary<string, string>();

  }
}
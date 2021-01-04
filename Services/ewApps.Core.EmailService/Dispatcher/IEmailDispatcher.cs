/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 03 OCT 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

namespace ewApps.Core.EmailService {
  public interface IEmailDispatcher
  {
    void SendEmail(string body, string to, string subject, bool isBodyHTML = false);
  }
}
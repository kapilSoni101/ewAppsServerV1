/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 9 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 9 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// This class is a DTO that contains about us infomation to be use for Get operations.
    /// </summary>
    public class AboutUsDTO {

        public string LogoURL {
            get;
            set;
        }
        public string CompanyName {
            get;
            set;
        }
        public string PortalName {
            get;
            set;
        }
        public string ApplicationName {
            get;
            set;
        }
        public string VersionNumber {
            get;
            set;
        }
        public string CopyRightText {
            get;
            set;
        }
        public string TermsURL {
            get;
            set;
        }
        public string PolicyURL {
            get;
            set;
        }
    }
}

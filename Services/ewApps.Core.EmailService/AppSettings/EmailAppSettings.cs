/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date:  28 May 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 28 May 2019
 */

namespace ewApps.Core.EmailService {

    public class EmailAppSettings {

        public string SenderKey {
            get; set;
        }

        public string SenderEmail {
            get; set;
        }

        public string SenderDisplayName {
            get; set;
        }

        public string SenderUserId {
            get; set;
        }

        public string SenderPwd {
            get; set;
        }

        public string SMTPServer {
            get; set;
        }

        public string SMTPPort {
            get; set;
        }

        public string Default {
            get; set;
        }

        public string EnableSSL {
            get; set;
        }

        public string ConnectionString {
            get; set;
        }

        public ushort DispatcherType {
            get; set;
        }
    }
}

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

namespace ewApps.Core.NotificationService {

    public class NotificationAppSettings {

        public string SupportEmail {
            get; set;
        }
        public string CompanyLogo {
            get; set;
        }
        public string RedirectUrl {
            get; set;
        }
        public string PaymentUrl {
            get; set;
        }
        public string PlatformBranchApiKey {
            get; set;
        }
        public string PublisherBranchApiKey {
            get; set;
        }
        public string BusinessBranchApiKey {
            get; set;
        }
        public string PaymentBranchApiKey {
            get; set;
        }
        public string ShipmentBranchApiKey {
            get; set;
        }
        public string CustomerBranchApiKey {
            get; set;
        }

        public string VendorBranchApiKey {
            get; set;
        }

        public string BranchApiUrl {
            get; set;
        }
        public string XslTemplateRootPath {
            get; set;
        }

        public string NotificationServiceUrl {
            get; set;
        }

    }
}

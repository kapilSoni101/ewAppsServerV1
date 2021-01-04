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
using System.Text;

namespace ewApps.Payment.Common {

    /// <summary>
    /// This class contains application settings.
    /// Note that AppSettings objects are parsed from json.
    /// </summary>
    public class PaymentAppSettings {

        public string AppName {
            get; set;
        }

        public string ConnectionString {
            get; set;
        }

        public string QConnectionString {
            get; set;
        }

        public string IdentityServerUrl {
            get; set;
        }

        public bool EnableSubdomain {
            get; set;
        }

        public string[] CrossOriginsUrls {
            get; set;
        }

        public string AppVersion {
            get; set;
        }


        public string Deployment {
            get; set;
        }


        public string MinimumLoggingLevel {
            get; set;
        }

        public string LogPortalUrl {
            get; set;
        }

        public string PlatformPortalURL {
            get; set;
        }

        public string PublisherPortalSubDomainURL {
            get; set;
        }

        public string PublisherPortalDefaultURL {
            get; set;
        }

        public string PaymentPortalSubDomainBizURL {
            get;set;
        }

        public string PaymentPortalSubDomainCustomerURL {
            get; set;
        }
        
        public string QuickPayUrl {
            get;set;
        }

        public string VeriCheckApiUrl {
            get; set;
        }

        public string CreditCardApiUrl {
            get; set;
        }

        public string BusinessConnectorApiUrl {
            get;set;
        }

        public string BusinessEntityApiUrl {
            get;set;
        }

        public string AppClientName {
            get; set;
        }

        public string CreditCardAPIMID {
            get; set;
        }

        public string CreditCardAPIUserID {
            get; set;
        }

        public string CreditCardAPIPassword {
            get; set;
        }

        public string CreditCardAPIdeviceID {
            get;set;
        }

        //public string QuickPayUrl {
        //    get; set;
        //}

        //public string RecurringSchedularCallBackEndPoint {
        //    get; set;
        //}

        public string VCServerHostEndPoint {
            get; set;
        }

        public string VCSubscriberCallBackEndPoint {
            get; set;
        }

        //public string PublisherApiUrl {
        //    get; set;
        //}


        //public bool EnableSubdomain
        //{
        //  get; set;
        //}

        //public string[] CrossOriginsUrls {
        //  get; set;
        //}
        //public string SAPApiUrl {
        //    get; set;
        //}

        //public string AppVersion {
        //  get; set;
        //}


        //public string Deployment {
        //  get; set;
        //}


        //public string MinimumLoggingLevel {
        //  get; set;
        //}

        //public string LogPortalUrl {
        //    get; set;
        //}


        //public string BusinessPortalSubDomainURL {
        //    get; set;
        //}

        //public string BusinessPortalDefaultURL {
        //    get; set;
        //}

        //public string PaymentPortalSubDomainURL {
        //    get; set;
        //}

        //public string PlatformPortalURL {
        //    get; set;
        //}

        //public string PublisherPortalSubDomainURL {
        //    get; set;
        //}

        //public string PublisherPortalDefaultURL {
        //    get; set;
        //}

        //public string PaymentPortalDefaultURL {
        //    get; set;
        //}
    }
}

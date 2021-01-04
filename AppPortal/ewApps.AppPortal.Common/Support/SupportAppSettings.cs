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

namespace ewApps.AppPortal.Common {
  public class SupportAppSettings {

    public string AppName {
      get; set;
    }

    //public string AppClientName {
    //  get; set;
    //}

    //public bool EnableSubdomain {
    //  get; set;
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


    public string ConnectionString {
      get; set;
    }


    public string[] CrossOriginsUrls {
      get; set;
    }

    public string LogPortalUrl {
      get; set;
    }


    //public string PlatformPortalURL {
    //  get; set;
    //}

    //public string PublisherPortalSubDomainURL {
    //  get; set;
    //}

    //public string PublisherPortalDefaultURL {
    //  get; set;
    //}

    public string IdentityServerUrl {
      get; set;
    }

    //public string BusinessPortalSubDomainURL {
    //  get; set;
    //}

    //public string BusinessPortalDefaultURL {
    //  get; set;
    //}

    //public string PaymentPortalSubDomainURL {
    //  get; set;
    //}

    //public string PaymentPortalDefaultURL {
    //  get; set;
    //}

  }
}
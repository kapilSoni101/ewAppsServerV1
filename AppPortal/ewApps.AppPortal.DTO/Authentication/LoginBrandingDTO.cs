// DbQuery

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
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    public class LoginBrandingDTO:BaseDTO {

    public string SubDomainName {
      get; set;
    }

    public string LogoUrl {
      get; set;
    }

    /// <summary>Tenant identifier</summary>
    public new Guid TenantId {
      get; set;
    }

    public string TenantName {
      get; set;
    }
    public Guid? TenantExtensionId {
      get; set;
    }
    public string PublisherName {
      get; set;
    }

    public string AppKey {
      get; set;
    }
    public string Copyright {
      get; set;
    }
    public string ApplicationName {
      get; set;
    }
    public Guid? AppID {
      get; set;
    }
    public string Language {
      get; set;
    }
    [NotMapped]
    public string InactiveComment {
      get; set;
    }
   
    public bool Active {
      get; set;
    }
    public bool Deleted {
      get; set;
    }
    [NotMapped]
// validation type like Inactive, delete
    public string ValidationType {
      get; set;
    }

    // ValidationTypeData like publisher, publisheruser,business, businessuser,application, applicationuser
    [NotMapped]
    public string ValidationTypeData {
      get; set;
    }


  }
}

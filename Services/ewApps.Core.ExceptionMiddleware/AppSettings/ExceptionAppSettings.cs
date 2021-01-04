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

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// This class contains application settings.
  /// Note that AppSettings objects are parsed from json.
  /// </summary>
  public class ExceptionAppSettings {

    public string AppName {
      get; set;
    }

    public string SMTPServer {
      get; set;
    }


    public string SenderUserId {
      get; set;
    }


    public string SenderPwd {
      get; set;
    }


    public int SMTPPort {
      get; set;
    }



    public bool EnableSSL {
      get; set;
    }

    public string SenderEmail {
      get; set;
    }


    public string ReceiverEmail {
      get; set;
    }

    public string ServerName {
      get; set;
    }

    public bool EnableErrorEmail {
      get; set;
    }
  }
}

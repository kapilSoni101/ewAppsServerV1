/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 24 Nov 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 NOv 2018
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.ThumbnailService
{

  public class ThumbnailServiceSettings {

    public string ThumbnailUrl
    {
      get; set;
    }    

    public string ThumbnailRootPath {
      get; set;
    }

        public string TempDocumentRootPath {
            get; set;
        }

        public string DocumentRootPath {
            get; set;
        }
        public string DefaultImagePath {
            get; set;
        }

        public string DocumentUrl {
            get; set;
        }

    }
}

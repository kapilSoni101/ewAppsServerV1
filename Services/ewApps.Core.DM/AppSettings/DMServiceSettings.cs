/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.DMService {

    /// <summary>
    /// This class contains application settings.
    /// Note that AppSettings objects are parsed from json.
    /// </summary>
    public class DMServiceSettings {

        public string ConnectionString {
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

        public string ThumbnailUrl {
            get; set;
        }

        public string ThumbnailRootPath {
            get; set;
        }


    }
}

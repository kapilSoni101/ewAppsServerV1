﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

namespace ewApps.Core.UniqueIdentityGeneratorService {

    /// <summary>
    /// This class contains application settings.
    /// Note that AppSettings objects are parsed from json.
    /// </summary>
    public class UniqueIdentityGeneratorAppSettings {

        public string ConnectionString {
            get; set;
        }

        //public string AppManagmentURL {
        //    get; set;
        //}

        //public string[] AnonymousUrls {
        //    get; set;
        //}

    }
}

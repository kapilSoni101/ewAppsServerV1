// Response

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 14 Aug 2018
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 14 Aug 2018
 */

using System;


namespace ewApps.AppPortal.DTO {
    public class ThemeResponseDTO {

        public Guid Id {
            get; set;
        }
        public string ThemeName {
            get; set;
        }
        public string ThemeKey {
            get; set;
        }
    }
}

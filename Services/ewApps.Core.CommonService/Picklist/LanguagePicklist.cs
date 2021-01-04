/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 30 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.CommonService {

    //This Helper Class is Used For Language
    public class LanguagePicklist {

        public string LanguageId {
            get; set;
        }

        public string LanguageName {
            get; set;
        }

        public List<LanguagePicklist> GetLanguagePL() {
            List<LanguagePicklist> list = new List<LanguagePicklist>();
            list.Add(new LanguagePicklist() { LanguageId = "en", LanguageName = "en-NZ" });
            list.Add(new LanguagePicklist() { LanguageId = "hi-IN", LanguageName = "Hindi" });
            list.Add(new LanguagePicklist() { LanguageId = "en", LanguageName = "English" });
            list.Add(new LanguagePicklist() { LanguageId = "en", LanguageName = "English (UK)" });
            list.Add(new LanguagePicklist() { LanguageId = "en", LanguageName = "en-MX" });
            return list;

        }
    }
}

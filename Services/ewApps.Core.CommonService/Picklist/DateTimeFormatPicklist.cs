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
using System.Linq;
using System.Text;

namespace ewApps.Core.CommonService {

    //This Helper Class is Used For Date Time Format
    public class DateTimeFormatPicklist {

        public string DateTimeFormatId {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

        public string JSDateTimeFormat {
            get; set;
        }


        public List<DateTimeFormatPicklist> GetDateTimeFormatPL() {
            return InitializeDateTimeFormatPL();
        }

        private static List<DateTimeFormatPicklist> InitializeDateTimeFormatPL() {
            List<DateTimeFormatPicklist> list = new List<DateTimeFormatPicklist>();
            list.Add(new DateTimeFormatPicklist() { DateTimeFormatId = "MM/DD/YYYY", DateTimeFormat = "MM/DD/YYYY", JSDateTimeFormat = "MM/dd/yyyy" });
            list.Add(new DateTimeFormatPicklist() { DateTimeFormatId = "YYYY-MM-DD", DateTimeFormat = "YYYY-MM-DD", JSDateTimeFormat = "yyyy-MM-dd" });
            list.Add(new DateTimeFormatPicklist() { DateTimeFormatId = "DD/MM/YYYY", DateTimeFormat = "DD/MM/YYYY", JSDateTimeFormat = "dd/MM/yyy" });
            list.Add(new DateTimeFormatPicklist() { DateTimeFormatId = "DD MMMM YYYY", DateTimeFormat = "DD MMMM YYYY", JSDateTimeFormat = "dd MMMM yyyy" });
            list.Add(new DateTimeFormatPicklist() { DateTimeFormatId = "MMMM DD, YYYY", DateTimeFormat = "MMMM DD, YYYY", JSDateTimeFormat = "MMMM dd, yyyy" });
            return list;
        }

        public static DateTimeFormatPicklist GetById(string dateTimeFormatId) {
            return InitializeDateTimeFormatPL().FirstOrDefault(i => i.DateTimeFormatId == dateTimeFormatId);
        }
    }
}

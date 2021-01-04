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

    //This Helper Class is Used For Currency Group
   public class CurrencyGroupingPicklist {

        public string CurrencyGroupingId {
            get; set;
        }

        public string CurrencyGrouping {
            get; set;
        }

        public List<CurrencyGroupingPicklist> GetCurrencyGroupingPL() {
            List<CurrencyGroupingPicklist> list = new List<CurrencyGroupingPicklist>();
            list.Add(new CurrencyGroupingPicklist() { CurrencyGroupingId = "None", CurrencyGrouping = "Select from the list" });
            list.Add(new CurrencyGroupingPicklist() { CurrencyGroupingId = "Three", CurrencyGrouping = "3 eg. 100,000" });
            list.Add(new CurrencyGroupingPicklist() { CurrencyGroupingId = "Four", CurrencyGrouping = "4 eg. 1000,0000" });
            list.Add(new CurrencyGroupingPicklist() { CurrencyGroupingId = "TwoThree", CurrencyGrouping = "2-3 eg. 10,000" });
            return list;
        }
    }
}

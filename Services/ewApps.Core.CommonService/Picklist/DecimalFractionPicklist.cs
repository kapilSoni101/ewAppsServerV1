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

    //This Helper Class is Used For Decimal Fraction
    public class DecimalFractionPicklist {

        public string DecimalFractionId {
            get; set;
        }

        public string DecimalFraction {
            get; set;
        }

        public List<DecimalFractionPicklist> GetDecimalFractionPL() {
            List<DecimalFractionPicklist> list = new List<DecimalFractionPicklist>();
            list.Add(new DecimalFractionPicklist() { DecimalFractionId = "None", DecimalFraction = "Select from the list" });
            list.Add(new DecimalFractionPicklist() { DecimalFractionId = "Zero", DecimalFraction = "0 eg. 10" });
            list.Add(new DecimalFractionPicklist() { DecimalFractionId = "One", DecimalFraction = "1 eg. 10.2" });
            list.Add(new DecimalFractionPicklist() { DecimalFractionId = "Two", DecimalFraction = "2 eg. 10.20" });
            list.Add(new DecimalFractionPicklist() { DecimalFractionId = "Three", DecimalFraction = "3 eg. 10.201" });
            return list;
        }
    }
}

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

    //This Helper Class is Used For Currency Seprator
    public class CurrencySeperatorPicklist {

        public string CurrencySeperatorId {
            get; set;
        }

        public string CurrencySeperator {
            get; set;
        }

        public List<CurrencySeperatorPicklist> GetCurrencySepratorPL() {
            List<CurrencySeperatorPicklist> list = new List<CurrencySeperatorPicklist>();
            list.Add(new CurrencySeperatorPicklist() { CurrencySeperatorId =((int) CurrencySeperatorEnum.None).ToString(), CurrencySeperator = "Select from the list" });
            list.Add(new CurrencySeperatorPicklist() { CurrencySeperatorId = ((int)CurrencySeperatorEnum.CommaAndDot).ToString(), CurrencySeperator = "‘,’ (comma) & ‘.’ (dot)" });
            list.Add(new CurrencySeperatorPicklist() { CurrencySeperatorId = ((int)CurrencySeperatorEnum.DotAndComma).ToString(), CurrencySeperator = "‘.’ (dot) & ‘,’ (comma)" });            
            return list;
        }
    }
}

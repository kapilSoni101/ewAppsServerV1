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

namespace ewApps.Core.BaseService {

    /// <summary>
    /// Application identification key to identify the application uniquely.
    /// </summary>
    //[Flags]
    public enum AppKeyEnum {
        plat = 1,
        pub = 2,
        biz = 3,
        pay = 4,
        ship = 5,
        cust = 6,
        report = 7,
        dsd = 8,
        doc = 9,
        crm = 10,
        vend = 11,
        custsetup = 12,
        vendsetup = 13
    }
}
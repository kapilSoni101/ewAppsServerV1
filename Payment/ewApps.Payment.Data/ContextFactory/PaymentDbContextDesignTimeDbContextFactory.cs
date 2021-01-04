﻿///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
// * Date: 24 September 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 10 October 2018
// */
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Payment.Data {
    public class PaymentDbContextDesignTimeDbContextFactory:BaseDesignTimeDbContextFactory<PaymentDbContext> {
        protected override PaymentDbContext CreateNewInstance(DbContextOptions<PaymentDbContext> options) {
            return new PaymentDbContext(options, _conString);
        }
    }
}

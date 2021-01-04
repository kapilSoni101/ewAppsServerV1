/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 29 August 2019
 */


using Microsoft.EntityFrameworkCore;

namespace ewApps.Report.QData {

    public class CQRSReportDesignTimeDbContextFactory:BaseDesignTimeDbContextFactory<QReportDbContext> {

        protected override QReportDbContext CreateNewInstance(DbContextOptions<QReportDbContext> options) {
            return new QReportDbContext(options, _conString);
        }

    }
}

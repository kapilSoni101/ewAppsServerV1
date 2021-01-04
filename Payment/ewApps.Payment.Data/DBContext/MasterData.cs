/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 16 November 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 20 November 2018
 */
using System;
using ewApps.Core.BaseService;
using ewApps.Payment.Entity;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Payment.Data {

    // Hari Sir Review

    /// <summary>
    /// This class is responsible to generate master data at the time of database creation
    /// </summary>
    public class MasterData {

        /// <summary>
        /// Startup method to generate master data. It is called from DB Context on database creation.
        /// </summary>
        /// <param name="builder">The model builder</param>
        public static void Init(ModelBuilder builder) {

            #region Role

            builder.Entity<Role>().HasData(
                   new {
                       ID = new Guid("6327a668-e7d7-451c-bf86-722f027d5d79"),
                       RoleName = "Admin",
                       RoleKey = "admin",
                       AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                       Active = Convert.ToBoolean(true),
                       PermissionBitMask = Convert.ToInt64(BusinessUserPaymentAppPermissionEnum.All),
                       UserType = (int)UserTypeEnum.Business,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)

                   },
                    new {
                        ID = new Guid("1903EB0F-F116-45E5-8701-686E626ED869"),
                        RoleName = "Admin",
                        RoleKey = "admin",
                        AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                        Active = Convert.ToBoolean(true),
                        PermissionBitMask = Convert.ToInt64(CustomerUserPaymentAppPermissionEnum.All),
                        UserType = (int)UserTypeEnum.Customer,
                        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        Deleted = Convert.ToBoolean(false)

                    }
                   );



            #endregion

        }
    }
}

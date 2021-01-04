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
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {

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
            #region Connector
            builder.Entity<ERPConnector>().HasData(
                   
                    new {
                        ID = new Guid("aaea4427-53f8-4ef8-b821-caff358cbd92"),
                        Name = "SAP Business One",
                        Active = Convert.ToBoolean(true),
                        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        Deleted = Convert.ToBoolean(false),
                        ConnectorKey = "sap",
                        TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda")
                    },
                    new {
                        ID = new Guid("706200a5-bdaa-482e-ad17-8fc6fc39c8b3"),
                        Name = "Acumatica",
                        Active = Convert.ToBoolean(false),
                        ConnectorKey = "acumatica",
                        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        Deleted = Convert.ToBoolean(false),
                        TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda")
                    },
                    new {
                        ID = new Guid("7d43c277-2d8a-4e68-aace-6cc8af26bf9b"),
                        Name = "BatchMaster",
                        Active = Convert.ToBoolean(false),
                        ConnectorKey = "batchmaster",
                        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        Deleted = Convert.ToBoolean(false),
                        TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda")
                    },
                    new {
                        ID = new Guid("a140ddc9-2bbf-477c-9052-c285118c2326"),
                        Name = "OptiPro ERP",
                        Active = Convert.ToBoolean(false),
                        ConnectorKey = "optiproeRP",
                        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        Deleted = Convert.ToBoolean(false),
                        TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda")
                    }
            );
            #region Role
            builder.Entity<Role>().HasData(
                   new {
                       ID = new Guid("da5421d7-dfc6-425a-93e9-a3272e740727"),
                       RoleName = "Admin",
                       RoleKey = "admin",
                       AppId = new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                       Active = Convert.ToBoolean(true),
                       PermissionBitMask = Convert.ToInt64(511),
                       UserType = (int)UserTypeEnum.Business,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },
              new {
                  ID = new Guid("8cecec11-2352-431d-86bc-ba78322649a7"),
                  RoleName = "Admin",
                  RoleKey = "admin",
                  AppId = new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                  Active = Convert.ToBoolean(true),
                  PermissionBitMask = Convert.ToInt64(4095),
                  UserType = (int)UserTypeEnum.Customer,
                  CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                  UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                  Deleted = Convert.ToBoolean(false)
            });
            #endregion
            #endregion
        }
    }
}

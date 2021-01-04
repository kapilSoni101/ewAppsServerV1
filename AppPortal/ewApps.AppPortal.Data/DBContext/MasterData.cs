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
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {

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
            #region Portal

            builder.Entity<Portal>().HasData(
                new {
                    ID = new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"),
                    Name = "Platform Portal",
                    PortalKey = "PlatPortal",
                    UserType = (int)UserTypeEnum.Platform,
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                  new {
                      ID = new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"),
                      Name = "Publisher Portal",
                      PortalKey = "PubPortal",
                      UserType = (int)UserTypeEnum.Publisher,
                      CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                      CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                      UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                      UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                      Deleted = Convert.ToBoolean(false)
                  },
                      new {
                          ID = new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"),
                          Name = "Business Portal",
                          PortalKey = "BizPortal",
                          UserType = (int)UserTypeEnum.Business,
                          CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                          CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                          UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                          UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                          Deleted = Convert.ToBoolean(false)
                      },
                       new {
                           ID = new Guid("0919810e-536c-42f5-a130-1cb62605508d"),
                           Name = "Customer Portal",
                           PortalKey = "CustPortal",
                           UserType = (int)UserTypeEnum.Customer,
                           CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           Deleted = Convert.ToBoolean(false)
                       },
                        new {
                            ID = new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"),
                            Name = "Vendor Portal",
                            PortalKey = "VendPortal",
                            UserType = (int)UserTypeEnum.Vendor,
                            CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                            CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                            UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            Deleted = Convert.ToBoolean(false)
                        },
                         new {
                             ID = new Guid("0fe68284-fbc8-472a-8efc-b7914c89a5e1"),
                             Name = "Employee Portal",
                             PortalKey = "EmployeePortal",
                             UserType = (int)UserTypeEnum.Employee,
                             CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                             CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                             UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                             UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                             Deleted = Convert.ToBoolean(false)

                         });

            #endregion

            #region PortalAppLinking

            builder.Entity<PortalAppLinking>().HasData(
                          new {
                              ID = new Guid("8bfbd8af-5f27-4eb9-a508-be87a84c7963"),
                              PortalId = new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"),
                              AppId = new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                              CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                              CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                              UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                              UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                              Deleted = Convert.ToBoolean(false)
                          },
                            new {
                                ID = new Guid("7c9a4607-9410-4e61-b4ab-f1d8df7305cc"),
                                AppId = new Guid("67D09A6F-CE95-498C-BF69-33C7D38F9041"),
                                PortalId = new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)
                            },
                            new {
                                ID = new Guid("0a826d78-14af-469f-8f78-9a7b068c574c"),
                                AppId = new Guid("F4952EF3-F1BD-4621-A5F9-290FD09BC81B"),
                                PortalId = new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)
                            },
                            new {
                                ID = new Guid("7a128e85-c2e2-4650-85bd-cc433a4e532e"),
                                AppId = new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                                PortalId = new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)
                            },
                            new {
                                ID = new Guid("caf59a3e-fb1e-4fb9-8129-9383230477bb"),
                                AppId = new Guid("6118A7FF-742B-25DB-A9C1-8E252C39BB73"),
                                PortalId = new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)
                            },
                             new {
                                 ID = new Guid("a4e51eea-81dc-4345-b9b1-76262b5c4f0e"),
                                 AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                                 PortalId = new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"),
                                 CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                 CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                 UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                 UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                 Deleted = Convert.ToBoolean(false)
                             },
                            new {
                                ID = new Guid("803ea4b6-f380-4d9f-97b0-82faa212f48b"),
                                AppId = new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                                PortalId = new Guid("0919810e-536c-42f5-a130-1cb62605508d"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)

                            },

                            new {
                                ID = new Guid("4af1430a-468f-4e5b-9786-9e542dc9a14a"),
                                AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                                PortalId = new Guid("0919810e-536c-42f5-a130-1cb62605508d"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)

                            },

                            new {
                                ID = new Guid("2657f722-c6dd-44b7-8c3b-432ecf797cdc"),
                                AppId = new Guid("3252c1cf-c74a-4d0d-b0ce-a6271aefc0a2"),
                                PortalId = new Guid("0919810e-536c-42f5-a130-1cb62605508d"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)

                            },
                            new {
                                ID = new Guid("5d404aad-7235-41f1-b760-bd414ed9e3fa"),
                                AppId = new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"),
                                PortalId = new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)
                            },
                            new {
                                ID = new Guid("a8e9a7c5-57d1-43a4-afd3-e70a8ce59608"),
                                AppId = new Guid("283259b7-952c-4f9b-9399-16a28ed08580"),
                                PortalId = new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"),
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)

                            }
                            //new {
                            //    ID = new Guid("e1fbd5df-69e7-48d3-92ee-40f7fd9d2c99"),
                            //    AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                            //    PortalId = new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"),
                            //    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                            //    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            //    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                            //    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            //    Deleted = Convert.ToBoolean(false)
                            //}

              );

            #endregion

            #region TenantUserAppPreference
            builder.Entity<TenantUserAppPreference>().HasData(
                       new {
                           ID = new Guid("c31433d7-107d-4fc0-b30d-baaf22112635"),
                           EmailPreference = Convert.ToInt64(31),
                           SMSPreference = Convert.ToInt64(0),
                           TenantUserId = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           UserAppLinkingId = new Guid("8260b697-e605-46e2-92e2-2f27935a8547"),
                           CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           Deleted = Convert.ToBoolean(false),
                           TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                           AppId = new Guid("8ded3502-01e5-469a-909b-5424d50d00d6")

                       });
            #endregion

            #region RoleLinking
            builder.Entity<RoleLinking>().HasData(
                 new {
                     ID = new Guid("0b03bbe7-b3f7-4978-9411-2b9cb8976579"),
                     TenantUserId = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                     AppId = new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                     RoleId = new Guid("df435b1a-7dbd-4e8f-b0f9-c07ba6e84f80"),
                     TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                     CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                     CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                     UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                     UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                     Deleted = Convert.ToBoolean(false)

                 });
            #endregion

            #region Role
            builder.Entity<Role>().HasData(
                   new {
                       ID = new Guid("df435b1a-7dbd-4e8f-b0f9-c07ba6e84f80"),
                       RoleName = "Admin",
                       RoleKey = "admin",
                       AppId = new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                       Active = Convert.ToBoolean(true),
                       PermissionBitMask = Convert.ToInt64(511),
                       UserType = (int)UserTypeEnum.Platform,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },
              new {
                  ID = new Guid("FB8BE172-2C7F-4F04-B40A-487FDA92E323"),
                  RoleName = "Admin",
                  RoleKey = "admin",
                  AppId = new Guid("67D09A6F-CE95-498C-BF69-33C7D38F9041"),
                  Active = Convert.ToBoolean(true),
                  PermissionBitMask = Convert.ToInt64(4095),
                  UserType = (int)UserTypeEnum.Publisher,
                  CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                  UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                  Deleted = Convert.ToBoolean(false)
              },

               new {
                   ID = new Guid("958d4943-124b-4467-b345-395dcd37e2fe"),
                   RoleName = "Admin",
                   RoleKey = "admin",
                   AppId = new Guid("F4952EF3-F1BD-4621-A5F9-290FD09BC81B"),
                   Active = Convert.ToBoolean(true),
                   PermissionBitMask = Convert.ToInt64(3),
                   UserType = (int)UserTypeEnum.Business,
                   CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                   CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                   UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                   UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                   Deleted = Convert.ToBoolean(false)
               },


            new {
                ID = new Guid("2fb520ad-6655-44eb-ba5f-c4abe5af01da"),
                RoleName = "Admin",
                RoleKey = "admin",
                AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                Active = Convert.ToBoolean(true),
                PermissionBitMask = Convert.ToInt64(63),
                UserType = (int)UserTypeEnum.Customer,
                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                Deleted = Convert.ToBoolean(false)

            });

            #endregion

            #region Favorite

            builder.Entity<Favorite>().HasData(
                   new {
                       ID = new Guid("eeec0c47-9c5e-4bde-914b-39a3e3a387ee"),
                       MenuKey = "Transaction History",
                       PortalKey = "BizPortal",
                       Url = "/feature/payapp/transaction/history",
                       AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },

                   new {
                       ID = new Guid("c810c5d9-a559-4242-89a7-7283adb4ca7a"),
                       MenuKey = "Receive Payment",
                       PortalKey = "BizPortal",
                       Url = "arinvoices",
                       AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },


                   new {
                       ID = new Guid("8b5bb0f6-6e57-4529-93bc-9a8b45651b73"),
                       MenuKey = "Refund",
                       PortalKey = "BizPortal",
                       Url = "arinvoices",
                       AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },


                   new {
                       ID = new Guid("083972f2-8eeb-4a99-92f1-6097a18cb094"),
                       MenuKey = "Void a Payment",
                       PortalKey = "BizPortal",
                       Url = "transaction/void",
                       AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },

                   new {
                       ID = new Guid("4a5519b6-e2c9-42e2-9715-cae5af638b15"),
                       MenuKey = "Transaction History",
                       PortalKey = "BizPortal",
                       Url = "/feature/custapp/transaction/history",
                       AppId = new Guid("8C6FA8CE-6B94-428F-95DE-5ED8859260D2"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },
                   new {
                       ID = new Guid("8120877e-3351-4f1b-924e-9934551f176e"),
                       MenuKey = "Receive Payment",
                       PortalKey = "BizPortal",
                       Url = "arinvoices",
                       AppId = new Guid("8C6FA8CE-6B94-428F-95DE-5ED8859260D2"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },
                   new {
                       ID = new Guid("c4285a44-32e9-4c4a-b186-843b6c31b4fb"),
                       MenuKey = "Refund",
                       PortalKey = "BizPortal",
                       Url = "arinvoices",
                       AppId = new Guid("8C6FA8CE-6B94-428F-95DE-5ED8859260D2"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },
                   new {
                       ID = new Guid("4e5a3aa9-86bc-4dea-9acf-136678ef8cfe"),
                       MenuKey = "Void a Payment",
                       PortalKey = "BizPortal",
                       Url = "/feature/custapp/transaction/history",
                       AppId = new Guid("8C6FA8CE-6B94-428F-95DE-5ED8859260D2"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },

                   new {
                       ID = new Guid("fcd4ccc7-eeba-49de-a1aa-83f1cd57a3e8"),
                       MenuKey = "Transaction History",
                       PortalKey = "CustPortal",
                       Url = "payment/history",
                       AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },

                   new {
                       ID = new Guid("98f338c6-fada-4c44-8f09-298f4c6cc292"),
                       MenuKey = "Make a Payment",
                       PortalKey = "CustPortal",
                       Url = "payment",
                       AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },
                    new {
                        ID = new Guid("b2bd694f-004e-42c3-bad8-3478c37bcec4"),
                        MenuKey = "Create Order",
                        PortalKey = "CustPortal",
                        Url = "Orders",
                        AppId = new Guid("8C6FA8CE-6B94-428F-95DE-5ED8859260D2"),
                        System = Convert.ToBoolean(true),
                        TenantId = Guid.Empty,
                        TenantUserId = Guid.Empty,
                        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        Deleted = Convert.ToBoolean(false)
                    },
                   new {
                       ID = new Guid("c75c4053-8b86-445b-819f-0fbe2028b74b"),
                       MenuKey = "Receive Payment",
                       PortalKey = "CustPortal",
                       Url = "apayment",
                       AppId = new Guid("8C6FA8CE-6B94-428F-95DE-5ED8859260D2"),
                       System = Convert.ToBoolean(true),
                       TenantId = Guid.Empty,
                       TenantUserId = Guid.Empty,
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   },

                new {
                    ID = new Guid("9d74d0f6-d07c-4a90-884f-92e454c78349"),
                    MenuKey = "Transaction History",
                    PortalKey = "CustPortal",
                    Url = "payment/history",
                    AppId = new Guid("8C6FA8CE-6B94-428F-95DE-5ED8859260D2"),
                    System = Convert.ToBoolean(true),
                    TenantId = Guid.Empty,
                    TenantUserId = Guid.Empty,
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("438a6e60-4b05-4896-8344-f7f660b3b629"),
                    MenuKey = " Invite Business",
                    PortalKey = "PubPortal",
                    Url = "payment/history",
                    AppId = new Guid("67D09A6F-CE95-498C-BF69-33C7D38F9041"),
                    System = Convert.ToBoolean(true),
                    TenantId = Guid.Empty,
                    TenantUserId = Guid.Empty,
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("e406a4db-030d-4864-908e-08332bb6ab98"),
                    MenuKey = "Open Tickets",
                    PortalKey = "PubPortal",
                    Url = "payment/history",
                    AppId = new Guid("67D09A6F-CE95-498C-BF69-33C7D38F9041"),
                    System = Convert.ToBoolean(true),
                    TenantId = Guid.Empty,
                    TenantUserId = Guid.Empty,
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


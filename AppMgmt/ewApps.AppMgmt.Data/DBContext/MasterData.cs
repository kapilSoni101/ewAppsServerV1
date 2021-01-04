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
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {

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

            #region Tenant

            builder.Entity<Tenant>().HasData(
                          new {
                              ID = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                              Phone = "",
                              Name = "Platform",
                              IdentityNumber = "0",
                              LogoUrl = "",
                              SubDomainName = "69ABE04E-E9D7-499D-A3C1-2593ABEF8959",
                              Language = "en",
                              Currency = "$",
                              TimeZone = "America/Los_Angeles",
                              TenantType = (int)TenantType.Platform,
                              VarId = "",
                              Active = Convert.ToBoolean(true),
                              CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                              CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                              UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                              UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                              Deleted = Convert.ToBoolean(false)
                              // },
                              //new {
                              //  ID = new Guid("31d22478-a57c-45b2-8181-085bbd66e617"),
                              //  Phone = "",
                              //  Name = "Publisher",
                              //  IdentityNumber = "0",
                              //  LogoUrl = "",
                              //  SubDomainName = "eworkPlace",
                              //  Language = "en",
                              //  Currency = "$",
                              //  TimeZone = "America/Los_Angeles",
                              //  TenantType = (int)TenantType.Publisher,
                              //  VarId = "",
                              //  Active = Convert.ToBoolean(true),
                              //  CreatedBy = new Guid("D899632C-A7EE-405B-AA71-0AD0779EDFD2"),
                              //  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                              //  UpdatedBy = new Guid("D899632C-A7EE-405B-AA71-0AD0779EDFD2"),
                              //  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                              //  Deleted = Convert.ToBoolean(false)
                          });
            #endregion

            #region TenantSubscription
            builder.Entity<TenantSubscription>().HasData(
                        new {
                            ID = new Guid("f524047f-1389-412f-904d-0492858c0194"),
                            TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                            AppId = new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                            SystemConfId = new Guid("00000000-0000-0000-0000-000000000000"),
                            ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                            SubscriptionStartDate = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            SubscriptionStartEnd = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            SubscriptionPlanId = new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"),
                            PriceInDollar = (decimal)(0.0),
                            Status = 0,
                            BusinessUserCount = 0,
                            Term = 0,
                            InactiveComment = "",
                            PaymentCycle = (int)PaymentCycleEnum.Monthly,
                            CustomizeSubscription = Convert.ToBoolean(false),
                            AutoRenewal = Convert.ToBoolean(false),
                            LogoThumbnailId = new Guid("00000000-0000-0000-0000-000000000000"),
                            OneTimePlan = Convert.ToBoolean(true),
                            UserPerCustomerCount = 0,
                            ShipmentCount = 0,
                            ShipmentUnit = 0,
                            CustomerUserCount = 0,
                            CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                            CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                            UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            Deleted = Convert.ToBoolean(false)
                            //},
                            //  new {
                            //    ID = new Guid("a9f12d7d-08fa-4225-acc9-600fd387c2c6"),
                            //    TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                            //    AppId = new Guid("67D09A6F-CE95-498C-BF69-33C7D38F9041"),
                            //    SystemConfId = new Guid("00000000-0000-0000-0000-000000000000"),
                            //    SubscriptionStartDate = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            //    SubscriptionStartEnd = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            //    UserLicenses = 15,
                            //    PrimaryAccountNo = "",
                            //    SubscriptionPlanId = new Guid("d390a264-f0db-49ce-9415-daee1ed4b445"),
                            //    CreditCardNo = "",
                            //    PriceInDollar = Convert.ToDouble(0),
                            //    PlanSchedule = 1,
                            //    GracePeriodInDays = 58,
                            //    Status = 0,
                            //    State = 1,
                            //    PaymentCycle = (int)PaymentCycleEnum.Monthly,
                            //    AlertFrequency = 4,
                            //    CustomizeSubscription = Convert.ToBoolean(false),
                            //    AutoRenew = Convert.ToBoolean(false),
                            //    LogoThumbnailId = new Guid("00000000-0000-0000-0000-000000000000"),
                            //    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                            //    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            //    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                            //    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                            //    Deleted = Convert.ToBoolean(false)
                        });
            #endregion

            #region App
            builder.Entity<App>().HasData(

                     new {
                         ID = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                         Name = "iSmartPayment",
                         AppKey = "pay",
                         AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                         AppScope = (int)AppScopeEnum.Public,
                         Constructed = Convert.ToBoolean(true),
                         Active = Convert.ToBoolean(true),
                         ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                         IdentityNumber = "APP100001",
                         InactiveComment = "",
                         CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                         CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                         UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                         UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                         Deleted = Convert.ToBoolean(false)
                     },
                    new {
                        ID = new Guid("6118A7FF-742B-25DB-A9C1-8E252C39BB73"),
                        Name = "iFastShipment",
                        AppKey = "ship",
                        AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                        AppScope = (int)AppScopeEnum.Public,
                        Constructed = Convert.ToBoolean(true),
                        Active = Convert.ToBoolean(true),
                        ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                        IdentityNumber = "APP100002",
                        InactiveComment = "",
                        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        Deleted = Convert.ToBoolean(false)
                    },

                new {
                    ID = new Guid("e5080257-c602-42cd-aedb-30b33757c382"),
                    Name = "Document",
                    AppKey = "doc",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(false),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100003",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },

                new {
                    ID = new Guid("f629c9e6-93d8-4e6b-8050-26fd89e9af5c"),
                    Name = "DSD",
                    AppKey = "dsd",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(false),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100004",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("e4ecf0e7-1bc7-49d4-a95d-1bdb1b77715e"),
                    Name = "Report and Dashboard",
                    AppKey = "report",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(false),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100005",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },

                new {
                    ID = new Guid("a33315c5-fd8b-45cd-99df-fbbeb88597f6"),
                    Name = "POS",
                    AppKey = "pos",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(false),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100006",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("4b643cfc-26d4-41f4-abda-a656682ceb50"),
                    Name = "Fixed Assets",
                    AppKey = "fixed",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(false),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100007",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("74d886db-ee46-41c2-acbe-9fdc7bd182fa"),
                    Name = "CRM",
                    AppKey = "crm",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(false),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100008",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                    Name = "Customer App",
                    AppKey = "cust",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(true),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100009",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"),
                    Name = "Vendor App",
                    AppKey = "vend",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(true),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100010",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("F4952EF3-F1BD-4621-A5F9-290FD09BC81B"),
                    Name = "BusinessSetUp App",
                    AppKey = "biz",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Free,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(true),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100011",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("3252c1cf-c74a-4d0d-b0ce-a6271aefc0a2"),
                    Name = "CustomerSetUp App",
                    AppKey = "custsetup",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Free,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(true),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100012",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("67D09A6F-CE95-498C-BF69-33C7D38F9041"),
                    Name = "Publisher App",
                    AppKey = "pub",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Free,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(true),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100013",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                new {
                    ID = new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                    Name = "Platform App",
                    AppKey = "plat",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Free,
                    AppScope = (int)AppScopeEnum.Public,
                    Constructed = Convert.ToBoolean(true),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100014",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                },
                 new {
                     ID = new Guid("283259b7-952c-4f9b-9399-16a28ed08580"),
                     Name = "VendorSetUp App",
                     AppKey = "vendsetup",
                     AppSubscriptionMode = (int)AppSubscriptionModeEnum.Free,
                     AppScope = (int)AppScopeEnum.Public,
                     Constructed = Convert.ToBoolean(true),
                     Active = Convert.ToBoolean(true),
                     ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                     IdentityNumber = "APP100015",
                     InactiveComment = "",
                     CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                     CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                     UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                     UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                     Deleted = Convert.ToBoolean(false)
                 },
                new {
                    ID = new Guid("f6ad4bf0-c302-4eb1-ab13-35ff9a832add"),
                    Name = "BankPayment",
                    AppKey = "bankpay",
                    AppSubscriptionMode = (int)AppSubscriptionModeEnum.Paid,
                    AppScope = (int)AppScopeEnum.Private,
                    Constructed = Convert.ToBoolean(true),
                    Active = Convert.ToBoolean(true),
                    ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                    IdentityNumber = "APP100016",
                    InactiveComment = "",
                    CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                    UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                    Deleted = Convert.ToBoolean(false)
                }
                );
            #endregion

            #region AppUser
            builder.Entity<TenantUser>().HasData(

                            new {
                                ID = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                FirstName = "Hari",
                                LastName = "Dudani",
                                FullName = "Hari Dudani",
                                TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                                Phone = "+918965321451",
                                Email = "hdudani@gmail.com",
                                Status = 2,
                                Region = "",
                                ParentRefId = new Guid("00000000-0000-0000-0000-000000000000"),
                                IdentityUserId = new Guid("d26eb1db-1c04-4a14-b922-e7cabbf1366f"),
                                Active = Convert.ToBoolean(true),
                                UserType = (int)UserTypeEnum.Platform,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)
                            });
            #endregion

            #region AppUserTypeLinking
            builder.Entity<AppUserTypeLinking>().HasData(
                          new {
                              ID = new Guid("9974f09e-ce64-460d-b1e9-7967dcc2ee00"),
                              AppId = new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"),
                              UserType = (int)UserTypeEnum.Platform,
                              CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                              CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                              UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                              UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                              Deleted = Convert.ToBoolean(false),
                              PartnerType = 0
                          },
                            new {
                                ID = new Guid("6db9d8c9-2a8b-4b7c-8dba-cda7338d1424"),
                                AppId = new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"),
                                UserType = (int)UserTypeEnum.Publisher,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false),
                                PartnerType = 0
                            },
                            new {
                                ID = new Guid("f62a5cb5-36c9-43e7-8dfb-af92eb114712"),
                                AppId = new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"),
                                UserType = (int)UserTypeEnum.Business,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false),
                                PartnerType = 0
                            },
                            new {
                                ID = new Guid("b21da138-837b-483a-bb95-0a7c19f02331"),
                                AppId = new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                                UserType = (int)UserTypeEnum.Business,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false),
                                PartnerType = 0
                            },
                            new {
                                ID = new Guid("51ac7ff1-083b-4def-8ff4-ee89e2e98041"),
                                AppId = new Guid("71D183B4-B17B-4458-92C9-A545FF775C13"),
                                UserType = (int)UserTypeEnum.Business,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false),
                                PartnerType = 0
                            },
                            new {
                                ID = new Guid("87d0b552-8973-4f28-a383-532611c5db06"),
                                AppId = new Guid("6118A7FF-742B-25DB-A9C1-8E252C39BB73"),
                                UserType = (int)UserTypeEnum.Business,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false),
                                PartnerType = 0
                            },
                             new {
                                 ID = new Guid("b43d29e0-28ab-4c4b-8293-8ad90bc83f29"),
                                 AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                                 UserType = (int)UserTypeEnum.Business,
                                 CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                 CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                 UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                 UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                 Deleted = Convert.ToBoolean(false),
                                 PartnerType = 0
                             },
                            new {
                                ID = new Guid("67f40404-084c-4170-baca-9a6868f5b6c2"),
                                AppId = new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                                UserType = (int)UserTypeEnum.Customer,
                                PartnerType = 0,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)

                            },

                            new {
                                ID = new Guid("c8ae2650-f7f5-44a4-ac52-851f01e8ed5d"),
                                AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                                UserType = (int)UserTypeEnum.Customer,
                                PartnerType = 0,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)

                            },

                            new {
                                ID = new Guid("809cc1eb-2234-46b7-9829-dd66edac9b60"),
                                AppId = new Guid("6118A7FF-742B-25DB-A9C1-8E252C39BB73"),
                                UserType = (int)UserTypeEnum.Customer,
                                PartnerType = 0,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)

                            },
                            new {
                                ID = new Guid("b9e705cd-d5a3-44a8-9672-e916871f8d38"),
                                AppId = new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"),
                                UserType = (int)UserTypeEnum.Vendor,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false),
                                PartnerType = 0
                            },
                            new {
                                ID = new Guid("14a88b03-da38-422e-a58f-ed71e2243445"),
                                AppId = new Guid("6118A7FF-742B-25DB-A9C1-8E252C39BB73"),
                                UserType = (int)UserTypeEnum.Vendor,
                                PartnerType = 0,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false)

                            },
                            new {
                                ID = new Guid("58b5ab4b-3921-42f3-8d44-600398d7c68d"),
                                AppId = new Guid("2B031B59-5276-589C-9D75-2A7AE1C799C8"),
                                UserType = (int)UserTypeEnum.Vendor,
                                CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                                UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                                Deleted = Convert.ToBoolean(false),
                                PartnerType = 0
                            }

              );

            #endregion

            #region Theme
            builder.Entity<Theme>().HasData(
             new {
                 ID = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560"),
                 ThemeName = "Business",
                 ThemeKey = "business",
                 PreviewImageUrl = "1",
                 Active = Convert.ToBoolean(true),
                 CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                 CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                 UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                 UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                 Deleted = Convert.ToBoolean(false)
             },
              new {
                  ID = new Guid("5270BC92-13FE-5299-0A24-752070424BC8"),
                  ThemeName = "Elegant",
                  ThemeKey = "elegant",
                  PreviewImageUrl = "1",
                  Active = Convert.ToBoolean(true),
                  CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                  UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                  Deleted = Convert.ToBoolean(false)
              },
               new {
                   ID = new Guid("2A438A5C-7AC6-41BD-66CB-9A771B60F9DD"),
                   ThemeName = "Light",
                   ThemeKey = "light",
                   PreviewImageUrl = "1",
                   Active = Convert.ToBoolean(true),
                   CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                   CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                   UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                   UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                   Deleted = Convert.ToBoolean(false)
               });
            #endregion

            #region SubscriptionPlan
            builder.Entity<SubscriptionPlan>().HasData(
                      new {
                          ID = new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"),
                          IdentityNumber = "SS#1",
                          PlanName = "Trial Plan",
                          Term = 15,
                          PriceInDollar = (decimal)(0.0),
                          AppId = new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                          Active = Convert.ToBoolean(true),
                          PaymentCycle = (int)PaymentCycleEnum.Monthly,
                          TermUnit = (int)PaymentCycleEnum.Monthly,
                          BusinessUserCount = 0,
                          CustomerUserCount = 0,
                          TransactionCount = 0,
                          AllowUnlimitedTransaction = true,
                          OtherFeatures = "",
                          StartDate = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                          EndDate = new DateTime(2020, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                          AutoRenewal = Convert.ToBoolean(true),
                          OneTimePlan = Convert.ToBoolean(true),
                          UserPerCustomerCount = 0,
                          ShipmentCount = 0,
                          ShipmentUnit = 0,
                          AllowUnlimitedShipment = Convert.ToBoolean(false),
                          TenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                          CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                          CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                          UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                          UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                          Deleted = Convert.ToBoolean(false)
                      },

                  new {
                      ID = new Guid("d390a264-f0db-49ce-9415-daee1ed4b445"),
                      IdentityNumber = "SS#2",
                      PlanName = "Trial Plan",
                      Term = 15,
                      PriceInDollar = (decimal)(0.0),
                      AppId = new Guid("67D09A6F-CE95-498C-BF69-33C7D38F9041"),
                      Active = Convert.ToBoolean(true),
                      PaymentCycle = (int)PaymentCycleEnum.Monthly,
                      TermUnit = (int)PaymentCycleEnum.Monthly,
                      BusinessUserCount = 0,
                      CustomerUserCount = 0,
                      TransactionCount = 0,
                      AllowUnlimitedTransaction = true,
                      OtherFeatures = "",
                      StartDate = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                      EndDate = new DateTime(2020, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                      AutoRenewal = Convert.ToBoolean(true),
                      OneTimePlan = Convert.ToBoolean(true),
                      UserPerCustomerCount = 0,
                      ShipmentCount = 0,
                      ShipmentUnit = 0,
                      AllowUnlimitedShipment = Convert.ToBoolean(false),
                      TenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                      CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                      CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                      UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                      UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                      Deleted = Convert.ToBoolean(false)
                  },
                   new {
                       ID = new Guid("8d5e65ca-1b62-4bc3-b0f0-9b3703aabebf"),
                       IdentityNumber = "SS#3",
                       PlanName = "Trial Plan",
                       Term = 15,
                       PriceInDollar = (decimal)(0.0),
                       AppId = new Guid("F4952EF3-F1BD-4621-A5F9-290FD09BC81B"),
                       Active = Convert.ToBoolean(true),
                       PaymentCycle = (int)PaymentCycleEnum.Monthly,
                       TermUnit = (int)PaymentCycleEnum.Monthly,
                       BusinessUserCount = 0,
                       CustomerUserCount = 0,
                       TransactionCount = 0,
                       AllowUnlimitedTransaction = true,
                       OtherFeatures = "",
                       StartDate = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       EndDate = new DateTime(2020, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       AutoRenewal = Convert.ToBoolean(true),
                       OneTimePlan = Convert.ToBoolean(true),
                       UserPerCustomerCount = 0,
                       ShipmentCount = 0,
                       ShipmentUnit = 0,
                       AllowUnlimitedShipment = Convert.ToBoolean(false),
                       TenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                       CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                       UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                       Deleted = Convert.ToBoolean(false)
                   }
                   
                   

                   );
            #endregion

            #region UserAppLinking
            builder.Entity<TenantUserAppLinking>().HasData(
                    new {
                        ID = new Guid("8260b697-e605-46e2-92e2-2f27935a8547"),
                        Active = Convert.ToBoolean(true),
                        TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                        AppId = new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                        TenantUserId = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        BusinessPartnerTenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                        IsPrimary = Convert.ToBoolean(true),
                        JoinedDate = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        Deleted = Convert.ToBoolean(false),
                        InvitedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                        InvitationBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                        UserType = (int)UserTypeEnum.Platform,
                        Status = (int)TenantUserInvitaionStatusEnum.Invited,

                    });
            #endregion

            #region AppService

            //builder.Entity<AppService>().HasData(
            //      new {
            //        ID = new Guid("14646abb-5bb7-4685-a0aa-2147f266415c"),
            //        Name = "VeriCheck",
            //        AppId = new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"),
            //        AppServiceData = "",
            //        ServiceKey = "VeriCheck",
            //        Active = Convert.ToBoolean(true),
            //        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        Deleted = Convert.ToBoolean(false)
            //      },
            //      new {
            //        ID = new Guid("f26cafe0-b4aa-4463-b9f7-5eb843419cec"),
            //        Name = "CBC",
            //        AppId = new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"),
            //        AppServiceData = "",
            //        ServiceKey = "CBC",
            //        Active = Convert.ToBoolean(true),
            //        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        Deleted = Convert.ToBoolean(false)
            //      },
            //      new {
            //        ID = new Guid("fe135fab-7b1e-47e0-ac9d-8aeeb718827d"),
            //        Name = "FedEx",
            //        AppId = new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"),
            //        AppServiceData = "",
            //        ServiceKey = "FedEx",
            //        Active = Convert.ToBoolean(true),
            //        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        Deleted = Convert.ToBoolean(false)
            //      });
            #endregion

            #region AppServiceAttribute

            //builder.Entity<AppServiceAttribute>().HasData(
            //      new {
            //        ID = new Guid("fa962e89-ab79-4ea9-9184-631f0aee1fff"),
            //        Name = "ACH Payments",
            //        AppServiceId = new Guid("14646abb-5bb7-4685-a0aa-2147f266415c"),
            //        Active = Convert.ToBoolean(true),
            //        AttributeKey = "ACHPayments",
            //        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        Deleted = Convert.ToBoolean(false)
            //      },
            //      new {
            //        ID = new Guid("bd0dce41-6c7b-4d8a-9f91-960aac945081"),
            //        Name = "CreditCard Payments",
            //        AppServiceId = new Guid("f26cafe0-b4aa-4463-b9f7-5eb843419cec"),
            //        Active = Convert.ToBoolean(true),
            //        AttributeKey = "CreditCard Payments",
            //        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        Deleted = Convert.ToBoolean(false)
            //      },
            //      new {
            //        ID = new Guid("6982c083-83de-42b2-a901-710bd94c7862"),
            //        Name = "FedEx SameDay",
            //        AppServiceId = new Guid("fe135fab-7b1e-47e0-ac9d-8aeeb718827d"),
            //        Active = Convert.ToBoolean(true),
            //        AttributeKey = "FedEx SameDay",
            //        CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //        UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //        Deleted = Convert.ToBoolean(false)
            //      },
            //new {
            //  ID = new Guid("88c16909-6070-4cf0-9993-1cfa460ecc8e"),
            //  Name = "FedEx SameDay City",
            //  AppServiceId = new Guid("fe135fab-7b1e-47e0-ac9d-8aeeb718827d"),
            //  Active = Convert.ToBoolean(true),
            //  AttributeKey = "FedEx SameDay City",
            //  CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //  UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //  Deleted = Convert.ToBoolean(false)
            //},
            //new {
            //  ID = new Guid("4d8ef15b-bc42-48d1-bfb4-f0ea883db850"),
            //  Name = "FedEx First Overnight",
            //  AppServiceId = new Guid("fe135fab-7b1e-47e0-ac9d-8aeeb718827d"),
            //  Active = Convert.ToBoolean(true),
            //  AttributeKey = "FedEx First Overnight",
            //  CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //  UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //  Deleted = Convert.ToBoolean(false)
            //},
            //new {
            //  ID = new Guid("8fcac6ef-2e88-4822-a4fd-6f3d2d0d5958"),
            //  Name = "FedEx Priority Overnight",
            //  AppServiceId = new Guid("fe135fab-7b1e-47e0-ac9d-8aeeb718827d"),
            //  Active = Convert.ToBoolean(true),
            //  AttributeKey = "FedEx Priority Overnight",
            //  CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //  UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
            //  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
            //  Deleted = Convert.ToBoolean(false)
            //});
            #endregion

            #region TenantLinking
            builder.Entity<TenantLinking>().HasData(
                       new {
                           ID = new Guid("30b3a49f-ae51-4a67-b610-bfcb935ae77a"),
                           PlatformTenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                           //PublisherTenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                           //BuisnessTenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                           //BusinessPartnerTenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                           CreatedBy = new Guid("D899632C-A7EE-405B-AA71-0AD0779EDFD2"),
                           CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           Deleted = Convert.ToBoolean(false),
                           // },
                           //new {
                           //  ID = new Guid("df1f175a-3599-4855-9e7f-e19f2cf7299f"),
                           //  PlatformTenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                           //  PublisherTenantId = new Guid("31d22478-a57c-45b2-8181-085bbd66e617"),
                           //  BuisnessTenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                           //  BusinessPartnerTenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                           //  CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           //  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           //  UpdatedBy = new Guid("D899632C-A7EE-405B-AA71-0AD0779EDFD2"),
                           //  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           //  Deleted = Convert.ToBoolean(false),
                       });
            #endregion

            #region TenantUserLinking
            builder.Entity<UserTenantLinking>().HasData(
                       new {
                           ID = new Guid("601622f9-9e52-4dd7-acaa-b8d44087b486"),
                           TenantId = new Guid("18571765-24b5-4c36-a957-416eaec38fda"),
                           TenantUserId = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           IsPrimary = Convert.ToBoolean(true),
                           CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"),
                           UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           Deleted = Convert.ToBoolean(false),
                           UserType = (int)UserTypeEnum.Platform,
                           // },
                           //new {
                           //  ID = new Guid("9fa1b75a-7f83-4eb1-a541-4f762c1d1d05"),
                           //  TenantId = new Guid("31d22478-a57c-45b2-8181-085bbd66e617"),
                           //  TenantUserId = new Guid("D899632C-A7EE-405B-AA71-0AD0779EDFD2"),
                           //  SubTenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                           //  Default = Convert.ToBoolean(true),
                           //  CreatedBy = new Guid("D899632C-A7EE-405B-AA71-0AD0779EDFD2"),
                           //  CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           //  UpdatedBy = new Guid("D899632C-A7EE-405B-AA71-0AD0779EDFD2"),
                           //  UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc),
                           //  Deleted = Convert.ToBoolean(false),
                           //  UserType = (int)UserTypeEnum.Publisher,
                       });
            #endregion

        }
    }
}

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// Business Application model for editing the business tenant information.
    /// </summary>
    //ToDo: nitin- basedto is not reqruied.
    public class UpdateBusinessAppSubscriptionDTO:BaseDTO {

        // Application name.
        public string Name {
            get; set;
        }

        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Application id for which a tenant subscribe.
        /// </summary>
        public Guid AppId {
            get; set;
        }

        /*
        /// <summary>
        /// Will contains App services ids list. 
        /// For example, If a App has 3 subservices (AppServices), then a Buusiness teant may subscribe any number of subservices out of 3 services.
        /// </summary>
        [NotMapped]
        public List<TenantAppServiceDTO> AppSubServices {
          get; set;
        }*/

        [NotMapped]
        /// <summary>
        /// Will contains App services ids list. 
        /// For example, If a App has 3 subservices (AppServices), then a Buusiness teant may subscribe any number of subservices out of 3 services.
        /// </summary>
        public List<AppServiceRequestDTO> AppSubServices {
            get; set;
        }

        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// Number of users can registerd to use application by that business tenantid.
        /// </summary>
        public int BusinessUserCount {
            get; set;
        }

        /// <summary>
        /// Contains the subscription plan id.
        /// </summary>
        public Guid SubscriptionPlanId {
            get; set;
        }

        public bool AutoRenewal {
            get; set;
        }
       
        public decimal PriceInDollar {
            get; set;
        }

        public int Term {
            get; set;
        }

        public DateTime SubscriptionStartDate {
            get; set;
        }

        public DateTime SubscriptionStartEnd {
            get; set;
        }

        public int PaymentCycle {
            get; set;
        }

        public bool CustomizeSubscription {
            get; set;
        }

        /// <summary>
        /// State of application whether application is active/in-active.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// InactiveComment for application in-active.
        /// </summary>    
        public string InactiveComment {
            get;
            set;
        }

        [NotMapped]
        public int opType {
            get; set;
        }      

        [NotMapped]
        /// <summary>
        /// Business application primary userid assoicated with a business.
        /// </summary>
        public Guid UserId {
            get; set;
        }       

        [NotMapped]
        public DateTime? UserActivationDate {
            get; set;
        }

        public Guid AppSettingId {
            get; set;
        }

        /// <summary>
        /// AppLication Language
        /// </summary>
        public string Language {
            get;
            set;
        }

        /// <summary>
        /// Application TimeZone
        /// </summary>
        public string TimeZone {
            get;
            set;
        }

        /// <summary>
        /// AppLication Currency
        /// </summary>
        public string Currency {
            get;
            set;
        }

        /// <summary>
        /// AppLication dateFormat
        /// </summary>    
        public string DateTimeFormat {
            get;
            set;
        }

        public bool OneTimePlan {
            get; set;
        }

        public int? CustomerUserCount {
            get; set;
        }
        public int? UserPerCustomerCount {
            get; set;
        }

        public int? ShipmentCount {
            get; set;
        }

        public int? ShipmentUnit {
            get; set;
        }

        /// <summary>
        /// Map tenant app subscription properties  to UpdateBusinessAppSubscriptionDTO model.
        /// </summary>
        /// <param name="tenantAppSubscription">Tenant app subscription model.</param>
        /// <returns></returns>
        public static UpdateBusinessAppSubscriptionDTO MapProperties(TenantAppSubscriptionDQ tenantAppSubscription) {
            UpdateBusinessAppSubscriptionDTO udSubsModel = new UpdateBusinessAppSubscriptionDTO();
            udSubsModel.AppId = tenantAppSubscription.AppId;
            udSubsModel.ID = tenantAppSubscription.ID;
            udSubsModel.InactiveComment = tenantAppSubscription.InactiveComment;
            //udSubsModel.Language = tenantAppSubscription.Language;
            //udSubsModel.TimeZone = tenantAppSubscription.TimeZone;
            //udSubsModel.Currency = tenantAppSubscription.Currency;
            udSubsModel.AutoRenewal = tenantAppSubscription.AutoRenewal;
            udSubsModel.CreatedBy = tenantAppSubscription.CreatedBy;
            udSubsModel.CreatedOn = tenantAppSubscription.CreatedOn;
            udSubsModel.CustomizeSubscription = tenantAppSubscription.CustomizeSubscription;
            //udSubsModel.DateTimeFormat = tenantAppSubscription.DateTimeFormat;
            udSubsModel.Deleted = tenantAppSubscription.Deleted;
            udSubsModel.Name = tenantAppSubscription.Name;
            udSubsModel.PaymentCycle = tenantAppSubscription.PaymentCycle;
            udSubsModel.PriceInDollar = tenantAppSubscription.PriceInDollar;
            udSubsModel.Status = tenantAppSubscription.Status;
            udSubsModel.Term = tenantAppSubscription.Term;
            udSubsModel.SubscriptionPlanId = tenantAppSubscription.SubscriptionPlanId;
            udSubsModel.SubscriptionStartDate = tenantAppSubscription.SubscriptionStartDate;
            udSubsModel.SubscriptionStartEnd = tenantAppSubscription.SubscriptionStartEnd;
            udSubsModel.TenantId = tenantAppSubscription.TenantId;
            udSubsModel.ThemeId = tenantAppSubscription.ThemeId;
            udSubsModel.UpdatedBy = tenantAppSubscription.UpdatedBy;
            udSubsModel.UpdatedOn = tenantAppSubscription.UpdatedOn;
            udSubsModel.BusinessUserCount = tenantAppSubscription.BusinessUserCount;

            udSubsModel.OneTimePlan = tenantAppSubscription.OneTimePlan;
            udSubsModel.UserPerCustomerCount = tenantAppSubscription.UserPerCustomerCount;
            udSubsModel.ShipmentCount = tenantAppSubscription.ShipmentCount;
            udSubsModel.ShipmentUnit = tenantAppSubscription.ShipmentUnit;

            return udSubsModel;
        }


    }
}

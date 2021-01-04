using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// Use for subscribe application.
    /// </summary>
    public class BusinessAppSubscriptionDTO {

        /// <summary>
        /// Unique id.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Application id for which a tenant subscribe.
        /// </summary>
        public Guid AppId {
            get; set;
        }

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

        [NotMapped]
        public int opType {
            get; set;
        }        

        [NotMapped]
        public string AdminFirstName {
            get; set;
        }

        [NotMapped]
        public string AdminLastName {
            get; set;
        }

        [NotMapped]
        public string AdminPassword {
            get; set;
        }

        [NotMapped]
        public string AdminEmail {
            get; set;
        }

        /// <summary>
        /// Tenant Language
        /// </summary>
        public string Language {
            get; set;
        }

        /// <summary>
        /// Tenant Currency
        /// </summary>
        public string Currency {
            get; set;
        }

        /// <summary>
        /// Tenant TimeZone
        /// </summary>
        public string TimeZone {
            get; set;
        }

        /// <summary>
        /// Tenant DateTimeFormat
        /// </summary>
        public string DateTimeFormat {
            get;
            set;
        }

        public int State {
            get; set;
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
        /// Map model object propeties to Entity property.
        /// </summary>
        /// <returns></returns>
        public TenantSubscription MapPropertieToEntity() {
            TenantSubscription sub = new TenantSubscription();
            sub.Term = this.Term;
            sub.AppId = this.AppId;
            sub.AutoRenewal = this.AutoRenewal;
            sub.CustomizeSubscription = this.CustomizeSubscription;
            sub.PaymentCycle = this.PaymentCycle;
            sub.PriceInDollar = this.PriceInDollar;
            sub.SubscriptionPlanId = this.SubscriptionPlanId;
            sub.SubscriptionStartDate = this.SubscriptionStartDate;
            sub.SubscriptionStartEnd = this.SubscriptionStartEnd;
            sub.ThemeId = this.ThemeId;
            sub.BusinessUserCount = this.BusinessUserCount;
            sub.Status = this.State;

            sub.OneTimePlan = this.OneTimePlan;
            sub.UserPerCustomerCount = this.UserPerCustomerCount;
            sub.ShipmentCount = this.ShipmentCount;
            sub.ShipmentUnit = this.ShipmentUnit;
            sub.CustomerUserCount = this.CustomerUserCount;

            return sub;
        }

        /// <summary>
        /// Map subscription model object to TenantSubscription Entity object array.
        /// </summary>
        /// <param name="listModel">List of subscription object.</param>
        /// <returns>return subscription array after mapping properties.</returns>
        public static TenantSubscription[] MapModelArrayToEntityArray(List<BusinessAppSubscriptionDTO> listModel) {
            List<TenantSubscription> listTS = new List<TenantSubscription>();
            for(int i = 0; i < listModel.Count; i++) {
                listTS.Add(listModel[i].MapPropertieToEntity());
            }

            return listTS.ToArray();
        }

    }
}

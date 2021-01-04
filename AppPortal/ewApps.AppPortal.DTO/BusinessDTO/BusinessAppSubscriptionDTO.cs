using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {


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


        public int Term {
            get; set;
        }

        public decimal PriceInDollar {
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
        public List<ConnectorConfigDQ> ConnectorConfig {
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

        public int? CustomerUserCount {
            get; set;
        }

        public bool OneTimePlan {
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

    }

}

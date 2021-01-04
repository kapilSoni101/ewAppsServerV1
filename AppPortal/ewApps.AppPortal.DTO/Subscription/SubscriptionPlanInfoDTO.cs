using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    public class SubscriptionPlanInfoDTO {

        public Guid Id {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public string PlanName {
            get; set;
        }

        public decimal PriceInDollar {
            get; set;
        }

        public int Term {
            get; set;
        }

        public int TermUnit {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public int PaymentCycle {
            get; set;
        }

        public bool AllowUnlimitedTransaction {
            get; set;
        }

        public int BusinessUserCount {
            get; set;
        }

        public int CustomerUserCount {
            get; set;
        }

        public DateTime StartDate {
            get; set;
        }

        public DateTime EndDate {
            get; set;
        }

        public string OtherFeatures {
            get; set;
        }

        public int TransactionCount {
            get; set;
        }

        public Guid AppId {
            get; set;
        }

        public string AppName {
            get; set;
        }

        public int AppServiceCount {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public Guid CreatedBy {
            get; set;
        }

        public bool AutoRenewal {
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


        [NotMapped]
        public List<SubsPlanServiceInfoDTO> ServiceInfoDTO {
            get; set;
        }

        [NotMapped]
        public OperationType OpType {
            get; set;
        }

        public bool AllowUnlimitedShipment {
            get; set;
        }

        public Guid? UpdatedBy {
          get; set;
        }

        public string UpdatedByName {
          get; set;
        }

        public DateTime? UpdatedOn {
          get; set;
        }

        public bool? CanDelete {
          get; set;
        }

    public string AppKey { get; set; }

    public static PubBusinessSubsPlan MapToPubBusinessSubsPlan(SubscriptionPlanInfoDTO subscriptionPlanInfoDTO) {
            PubBusinessSubsPlan pubBusinessSubsPlan = new PubBusinessSubsPlan();

            pubBusinessSubsPlan.Active = subscriptionPlanInfoDTO.Active;
            pubBusinessSubsPlan.AllowUnlimitedTransaction = subscriptionPlanInfoDTO.AllowUnlimitedTransaction;
            pubBusinessSubsPlan.AppId = subscriptionPlanInfoDTO.AppId;
            pubBusinessSubsPlan.BusinessUserCount = subscriptionPlanInfoDTO.BusinessUserCount;
            pubBusinessSubsPlan.CustomerUserCount = subscriptionPlanInfoDTO.CustomerUserCount;
            pubBusinessSubsPlan.EndDate = subscriptionPlanInfoDTO.EndDate;
            pubBusinessSubsPlan.OtherFeatures = subscriptionPlanInfoDTO.OtherFeatures;
            pubBusinessSubsPlan.PaymentCycle = subscriptionPlanInfoDTO.PaymentCycle;
            pubBusinessSubsPlan.PlanName = subscriptionPlanInfoDTO.PlanName;
            pubBusinessSubsPlan.PriceInDollar = subscriptionPlanInfoDTO.PriceInDollar;
            pubBusinessSubsPlan.StartDate = subscriptionPlanInfoDTO.StartDate;
            pubBusinessSubsPlan.SubscriptionPlanId = subscriptionPlanInfoDTO.Id;
            pubBusinessSubsPlan.Term = subscriptionPlanInfoDTO.Term;
            pubBusinessSubsPlan.TermUnit = subscriptionPlanInfoDTO.TermUnit;
            pubBusinessSubsPlan.TransactionCount = subscriptionPlanInfoDTO.TransactionCount;
            pubBusinessSubsPlan.AutoRenewal = subscriptionPlanInfoDTO.AutoRenewal;
            pubBusinessSubsPlan.OneTimePlan = subscriptionPlanInfoDTO.OneTimePlan;
            pubBusinessSubsPlan.UserPerCustomerCount = subscriptionPlanInfoDTO.UserPerCustomerCount;
            pubBusinessSubsPlan.ShipmentCount = subscriptionPlanInfoDTO.ShipmentCount;
            pubBusinessSubsPlan.ShipmentUnit = subscriptionPlanInfoDTO.ShipmentUnit;
            pubBusinessSubsPlan.AllowUnlimitedShipment = subscriptionPlanInfoDTO.AllowUnlimitedShipment;

            return pubBusinessSubsPlan;
        }

    }
}

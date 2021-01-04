using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DTO {
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

        public Guid AppId {
            get; set;
        }

        public string AppName {
            get; set;
        }

        public int TransactionCount {
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

        public bool AllowUnlimitedShipment
        {
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

    public static SubscriptionPlanInfoDTO MapFromSuscriptionPlan(SubscriptionPlan suscriptionPlan) {
            SubscriptionPlanInfoDTO subscriptionPlanInfoDTO = new SubscriptionPlanInfoDTO();

            subscriptionPlanInfoDTO.Active = suscriptionPlan.Active;
            subscriptionPlanInfoDTO.AllowUnlimitedTransaction = suscriptionPlan.AllowUnlimitedTransaction;
            subscriptionPlanInfoDTO.AppId = suscriptionPlan.AppId;
            subscriptionPlanInfoDTO.BusinessUserCount = suscriptionPlan.BusinessUserCount.Value;
            subscriptionPlanInfoDTO.CustomerUserCount = suscriptionPlan.CustomerUserCount.Value;
            subscriptionPlanInfoDTO.StartDate = suscriptionPlan.StartDate;
            subscriptionPlanInfoDTO.EndDate = suscriptionPlan.EndDate;
            subscriptionPlanInfoDTO.OtherFeatures = suscriptionPlan.OtherFeatures;
            subscriptionPlanInfoDTO.PaymentCycle = suscriptionPlan.PaymentCycle;
            subscriptionPlanInfoDTO.PlanName = suscriptionPlan.PlanName;
            subscriptionPlanInfoDTO.PriceInDollar = suscriptionPlan.PriceInDollar;
            subscriptionPlanInfoDTO.Term = suscriptionPlan.Term;
            subscriptionPlanInfoDTO.TermUnit = suscriptionPlan.TermUnit;
            subscriptionPlanInfoDTO.TransactionCount = suscriptionPlan.TransactionCount;
            subscriptionPlanInfoDTO.AutoRenewal = suscriptionPlan.AutoRenewal;
            subscriptionPlanInfoDTO.OneTimePlan = suscriptionPlan.OneTimePlan;
            subscriptionPlanInfoDTO.UserPerCustomerCount = suscriptionPlan.UserPerCustomerCount;
            subscriptionPlanInfoDTO.ShipmentCount = suscriptionPlan.ShipmentCount;
            subscriptionPlanInfoDTO.ShipmentUnit = suscriptionPlan.ShipmentUnit;
            subscriptionPlanInfoDTO.AllowUnlimitedShipment = suscriptionPlan.AllowUnlimitedShipment;

            return subscriptionPlanInfoDTO;
        }
    }
}

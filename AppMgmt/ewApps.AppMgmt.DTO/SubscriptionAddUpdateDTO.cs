using System;
using System.Collections.Generic;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// Provides properties for Add/update subscription plan related data.
    /// </summary>
    public class SubscriptionAddUpdateDTO : BaseDTO {

        public new Guid ID {
            get; set;
        }

    public new Guid AppId
    {
      get; set;
    }

    public string PlanName { get; set; }

    public decimal PriceInDollar { get; set; }

    public int PaymentCycle { get; set; }

    public int PlanTerm { get; set; }

    public int TermUnit { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool Active { get; set; }

    public List<AppServiceDTO> Services { get; set; }

    public int BusinessUserCount { get; set; }

    public int CustomerUserCount { get; set; }

    public int TransactionCount { get; set; }

    public bool AllowUnlimitedTransaction { get; set; }

    public string OtherFeatures { get; set; }

    public bool AutoRenewal { get; set; }

    public bool OneTimePlan { get; set; }

    public int UserPerCustomerCount { get; set; }

    public int ShipmentCount { get; set; }

    public int ShipmentUnit { get; set; }

    public bool AllowUnlimitedShipment { get; set; }

    public static SubscriptionPlan MapToEntity(SubscriptionAddUpdateDTO dto, SubscriptionPlan entity) {
      entity.Active = dto.Active;
      entity.AllowUnlimitedTransaction = dto.AllowUnlimitedTransaction;
      entity.AppId = dto.AppId;
      entity.BusinessUserCount = dto.BusinessUserCount;
      entity.CustomerUserCount = dto.CustomerUserCount;
      entity.StartDate = dto.StartDate;
      entity.EndDate = dto.EndDate;
      entity.OtherFeatures = dto.OtherFeatures;
      entity.PaymentCycle = dto.PaymentCycle;
      entity.PlanName = dto.PlanName;
      entity.PriceInDollar = dto.PriceInDollar;
      entity.Term = dto.PlanTerm;
      entity.TermUnit = entity.PaymentCycle;
      entity.TransactionCount = dto.TransactionCount;
      entity.AutoRenewal = dto.AutoRenewal;
      entity.OneTimePlan = dto.OneTimePlan;
      entity.UserPerCustomerCount = dto.UserPerCustomerCount;
      entity.ShipmentCount = dto.ShipmentCount;
      entity.ShipmentUnit = dto.ShipmentUnit;
      entity.AllowUnlimitedShipment = dto.AllowUnlimitedShipment;


      return entity;
    }

  }
}

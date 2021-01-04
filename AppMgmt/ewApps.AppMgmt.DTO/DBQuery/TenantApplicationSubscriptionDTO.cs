//DbQuery 

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    // Model of ApplicationSubscriptionDTO.
    public class TenantApplicationSubscriptionDTO: BaseDTO
    {

        public string Name { get; set; }

        public Guid SubscriptionPlanId { get; set; }

        public bool AutoRenew { get; set; }

        public int PlanSchedule { get; set; }

        public double PriceInDollar { get; set; }

        public int GracePeriodInDays { get; set; }

        public int AlertFrequency { get; set; }

        public int FreeUserLicenseCount { get; set; }

        public int PaymentCycle { get; set; }

        public int Term { get; set; }

        public int NumberOfUsers { get; set; }

        /// <summary>
        /// Map subscription plan entity to Tenant app subscription.
        /// </summary>
        /// <param name="entity">SubscriptionPlan entity</param>
        /// <returns></returns>
        public static TenantApplicationSubscriptionDTO MapEntityToTenantApplicationSubscriptionDTO(SubscriptionPlan entity) {
            TenantApplicationSubscriptionDTO dto = new TenantApplicationSubscriptionDTO();
            //dto.AlertFrequency = entity.AlertFrequency;
            //dto.AutoRenew = entity.AutoRenewal;
            //dto.CreatedBy = entity.CreatedBy;
            //dto.CreatedOn = entity.CreatedOn;
            //dto.Deleted = entity.Deleted;
            //dto.FreeUserLicenseCount = entity.FreeUserLicenseCount;
            //dto.GracePeriodInDays = entity.GracePeriodInDays;
            //dto.ID = entity.ID;
            //dto.Name = entity.PlanName;
            //dto.NumberOfUsers = entity.NumberOfUsers;
            //dto.PaymentCycle = (int)entity.PaymentCycle;
            //dto.PlanSchedule = entity.PlanSchedule;
            //dto.PriceInDollar = entity.PriceInDollar;
            //dto.SubscriptionPlanId = entity.ID;
            //dto.TenantId = entity.TenantId;
            //dto.Term = entity.Term;
            //dto.UpdatedBy = entity.UpdatedBy;
            //dto.UpdatedOn = entity.UpdatedOn;

            return dto;
        }

    }
}

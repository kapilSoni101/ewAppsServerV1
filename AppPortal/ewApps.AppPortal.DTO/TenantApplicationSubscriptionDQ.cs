/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 04 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Contains subscription application plan detail.
    /// </summary>
    public class TenantApplicationSubscriptionDQ:BaseDTO {

        public string Name {
            get; set;
        }

        public Guid SubscriptionPlanId {
            get; set;
        }        

        public decimal PriceInDollar {
            get; set;
        }        

        public int PaymentCycle {
            get; set;
        }

        public int Term {
            get; set;
        }

        public int TermUnit {
            get; set;
        }

        public int NumberOfUsers {
            get; set;
        }        

        public int BusinessUserCount {
            get; set;
        }

        public int CustomerUserCount {
            get;set;
        }

        public int TransactionCount {
            get;set;
        }

        public bool AllowUnlimitedTransaction {
            get;set;
        }

        [NotMapped]
        public int AppServiceCount {
            get;set;
        }

        [NotMapped]
        public bool AutoRenew {
            get; set;
        }

        [NotMapped]
        public int PlanSchedule {
            get; set;
        }

        [NotMapped]
        public int GracePeriodInDays {
            get; set;
        }

        [NotMapped]
        public int AlertFrequency {
            get; set;
        }

        public static List<TenantApplicationSubscriptionDQ> MapProperties(List<SubscriptionPlanInfoDTO> listPlan) {
            List<TenantApplicationSubscriptionDQ> list = new List<TenantApplicationSubscriptionDQ>();
            for(int i = 0; i < listPlan.Count; i++) {
            }

            return list;
        }

        public static TenantApplicationSubscriptionDQ MapProperties(SubscriptionPlanInfoDTO plan) {
            TenantApplicationSubscriptionDQ item = new TenantApplicationSubscriptionDQ();
            item.ID = plan.Id;
            item.SubscriptionPlanId = plan.Id;
            item.Name = plan.PlanName;
            item.NumberOfUsers = plan.BusinessUserCount;
            item.AllowUnlimitedTransaction = plan.AllowUnlimitedTransaction;
            //item.AutoRenew = plan.Aut;
            item.BusinessUserCount = plan.BusinessUserCount;
            item.CustomerUserCount = plan.CustomerUserCount;
            item.AppServiceCount = plan.AppServiceCount;
            item.PaymentCycle = plan.PaymentCycle;
            item.PriceInDollar = plan.PriceInDollar;
            item.TermUnit = plan.TermUnit;
            item.Term = plan.Term;
            

            return item;
        }

    }
}

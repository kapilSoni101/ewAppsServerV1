using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    public class AppServiceDTO:BaseDTO {

        public AppServiceDTO() {
            AppServiceAttributeList = new List<AppServiceAttributeDTO>();
        }

        public new Guid ID {
            get; set;
        }

        public string Name {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public string ServiceKey {
            get; set;
        }

        [NotMapped]
        public bool Checked {
          get; set;
        }

        [NotMapped]
        public string ServiceAccountDetail {
            get; set;
        }

        [NotMapped]
        public List<AppServiceAcctDetailDTO> CarrierServiceAccountDetailList {
            get; set;
        }

        [NotMapped]
        public List<AppServiceAttributeDTO> AppServiceAttributeList {
            get; set;
        }

        /// <summary>
        /// Map AppService entity to dto.
        /// </summary>
        /// <param name="appService"></param>
        /// <returns></returns>
        public static AppServiceDTO MapAppServiceToDTO(AppService appService) {
            AppServiceDTO dto = new AppServiceDTO();
            dto.Active = appService.Active;
            dto.CreatedBy = appService.CreatedBy;
            dto.CreatedOn = appService.CreatedOn;
            dto.Deleted = appService.Deleted;
            dto.ID = appService.ID;
            dto.Name = appService.Name;
            dto.ServiceKey = appService.ServiceKey;
            dto.TenantId = appService.TenantId;
            dto.UpdatedBy = appService.UpdatedBy;
            dto.UpdatedOn = appService.UpdatedOn;

            return dto;
        }

        public static SubscriptionPlanService MapToSubscriptionPlanService(AppServiceDTO appServiceDTO, SubscriptionPlanService entity, Guid subPlanId) {
          entity.SubscriptionPlanId = subPlanId;
          entity.AppServiceId = appServiceDTO.ID;

          return entity;
        }

  }
}

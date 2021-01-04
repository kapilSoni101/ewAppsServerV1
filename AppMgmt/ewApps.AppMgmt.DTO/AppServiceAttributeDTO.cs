using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    // Contains chhild attribute for AppService.
    /// </summary>
    public class AppServiceAttributeDTO:BaseDTO {

        public AppServiceAttributeDTO() {
            AppServiceAcctDetailList = new List<AppServiceAcctDetailDTO>();
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

        public string AttributeKey {
            get; set;
        }

        [NotMapped]
        public bool Checked {
          get; set;
        }

        [NotMapped]
        public List<AppServiceAcctDetailDTO> AppServiceAcctDetailList {
            get; set;
        }

        /// <summary>
        /// Map attribute entity to model object.
        /// </summary>
        /// <param name="entity">Attribute entity.</param>
        /// <returns>return modle object.</returns>
        public static AppServiceAttributeDTO MapAppServiceAttributeToDTO(AppServiceAttribute entity) {
            AppServiceAttributeDTO dto = new AppServiceAttributeDTO();
            dto.Active = entity.Active;
            dto.AttributeKey = entity.AttributeKey;
            dto.CreatedBy = entity.CreatedBy;
            dto.CreatedOn = entity.CreatedOn;
            dto.Deleted = entity.Deleted;
            dto.ID = entity.ID;
            dto.Name = entity.Name;
            dto.TenantId = entity.TenantId;
            dto.UpdatedBy = entity.UpdatedBy;
            dto.UpdatedOn = entity.UpdatedOn;

            return dto;
        }

    public static SubscriptionPlanServiceAttribute MapToSubscriptionPlanServiceAttribute(AppServiceAttributeDTO appServiceAttributeDTO, SubscriptionPlanServiceAttribute entity, Guid subPlanId, Guid subPlanServId) {
      entity.SubscriptionPlanId = subPlanId;
      entity.SubscriptionPlanServiceId = subPlanServId;
      entity.AppServiceAttributeId = appServiceAttributeDTO.ID;

      return entity;
    }
  }
}

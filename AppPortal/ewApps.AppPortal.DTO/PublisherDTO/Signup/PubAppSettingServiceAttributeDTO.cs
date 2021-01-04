using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Contains chhild attribute for AppService.
    /// </summary>
    public class PubAppSettingServiceAttributeDTO {

        public PubAppSettingServiceAttributeDTO() {
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
    

        ///// <summary>
        ///// Map attribute entity to model object.
        ///// </summary>
        ///// <param name="entity">Attribute entity.</param>
        ///// <returns>return modle object.</returns>
        //public static PubAppSettingServiceAttributeDTO MapAppServiceAttributeToDTO(AppServiceAttribute entity) {
        //    PubAppSettingServiceAttributeDTO dto = new PubAppSettingServiceAttributeDTO();
        //    dto.Active = entity.Active;
        //    dto.AttributeKey = entity.AttributeKey;
        //    dto.CreatedBy = entity.CreatedBy;
        //    dto.CreatedOn = entity.CreatedOn;
        //    dto.Deleted = entity.Deleted;
        //    dto.ID = entity.ID;
        //    dto.Name = entity.Name;
        //    dto.TenantId = entity.TenantId;
        //    dto.UpdatedBy = entity.UpdatedBy;
        //    dto.UpdatedOn = entity.UpdatedOn;

        //    return dto;
        //}

    }
}

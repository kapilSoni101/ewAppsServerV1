using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.AppPortal.DTO {
    public class PubAppSettingServiceDTO {

        public PubAppSettingServiceDTO() {
            AppServiceAttributeList = new List<PubAppSettingServiceAttributeDTO>();
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

        //public bool Checked {
        //  get; set;
        //}
        [NotMapped]
        public string ServiceAccountDetail {
            get; set;
        }
        [NotMapped]
        public List<AppServiceAcctDetailDTO> CarrierServiceAccountDetailList {
            get; set;
        }
        [NotMapped]
        public List<PubAppSettingServiceAttributeDTO> AppServiceAttributeList {
            get; set;
        }

        ///// <summary>
        ///// Map AppService entity to dto.
        ///// </summary>
        ///// <param name="appService"></param>
        ///// <returns></returns>
        //public static PubAppSettingServiceDTO MapAppServiceToDTO(AppService appService) {
        //    PubAppSettingServiceDTO dto = new PubAppSettingServiceDTO();
        //    dto.Active = appService.Active;
        //    dto.CreatedBy = appService.CreatedBy;
        //    dto.CreatedOn = appService.CreatedOn;
        //    dto.Deleted = appService.Deleted;
        //    dto.ID = appService.ID;
        //    dto.Name = appService.Name;
        //    dto.ServiceKey = appService.ServiceKey;
        //    dto.TenantId = appService.TenantId;
        //    dto.UpdatedBy = appService.UpdatedBy;
        //    dto.UpdatedOn = appService.UpdatedOn;

        //    return dto;
        //}

    }
}

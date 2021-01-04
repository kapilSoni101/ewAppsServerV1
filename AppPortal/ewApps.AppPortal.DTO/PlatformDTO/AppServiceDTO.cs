using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
  public class AppServiceDTO :BaseDTO {

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

    //public bool Checked {
    //  get; set;
    //}
    [NotMapped]
    public string ServiceAccountDetail
    {
      get; set;
    }
    [NotMapped]
    public List<AppServiceAcctDetailDTO> CarrierServiceAccountDetailList
    {
      get; set;
    }
    [NotMapped]
    public List<AppServiceAttributeDTO> AppServiceAttributeList {
      get; set;
    }

  }
}

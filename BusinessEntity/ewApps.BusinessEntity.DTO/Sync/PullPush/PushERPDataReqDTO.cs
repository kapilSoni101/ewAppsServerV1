using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

  public class PushERPDataReqDTO {

    public List<BABaseSyncDTO> Entities {
      get; set;
    }

    public Guid TenantId {
      get; set;
    }

    //public string ERPEntityKey {
    //  get; set;
    //}
  }
}

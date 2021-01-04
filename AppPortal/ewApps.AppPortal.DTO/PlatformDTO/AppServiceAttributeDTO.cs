using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
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

    //public bool Checked {
    //  get; set;
    //}

    [NotMapped]
    public List<AppServiceAcctDetailDTO> AppServiceAcctDetailList {
      get; set;
    }

  }
}

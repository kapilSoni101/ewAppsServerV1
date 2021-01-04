using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
  /// <summary>
  /// Contains chhild attribute for AppService.
  /// </summary>
  public class BusAppServiceDTO :BaseDTO {

    public BusAppServiceDTO() {
      BusAppServiceAttributeList = new List<BusAppServiceAttributeDTO>();
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
    public bool Checked  {
      get; set;
    }

    [NotMapped]
    public List<BusAppServiceAttributeDTO> BusAppServiceAttributeList {
      get; set;
    }



  }
}

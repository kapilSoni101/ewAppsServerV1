using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

  public class CustomerSignUpResDTO {

    public Guid BusinessTenantId {
      get; set;
    }

    public Guid CustomerTenantId {
      get; set;
    }

  }

}

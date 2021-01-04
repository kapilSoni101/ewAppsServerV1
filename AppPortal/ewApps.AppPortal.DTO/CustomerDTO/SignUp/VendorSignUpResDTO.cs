using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO
{
 public class VendorSignUpResDTO
  {
    public Guid BusinessTenantId
    {
      get; set;
    }

    public Guid VendorTenantId
    {
      get; set;
    }
  }
}

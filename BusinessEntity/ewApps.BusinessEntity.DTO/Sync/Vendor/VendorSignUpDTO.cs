using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
 public  class VendorSignUpDTO
  {
    public Guid TenantId
    {
      get;
      set;
    }
    public Guid BusinesPartnerTenantId
    {
      get;
      set;
    }
    public Guid BusinesPrimaryUserId
    {
      get;
      set;
    }
    public string CutomerName
    {
      get;
      set;
    }
    public string Currency
    {
      get;
      set;
    }
  }
}

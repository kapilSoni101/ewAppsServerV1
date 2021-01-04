using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO
{
  public class TenantInfo
  {

    public Guid Id
    {
      get; set;
    }

    public string Name
    {
      get; set;
    }

    public bool IntegratedMode
    {
      get; set;
    }

    public string Subdomain
    {
      get; set;
    }

  }
}

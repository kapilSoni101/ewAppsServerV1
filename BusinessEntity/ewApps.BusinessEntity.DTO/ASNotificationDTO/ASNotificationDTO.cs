using ewApps.Core.BaseService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
  public class ASNotificationDTO
  {
    public string TextContent
    {
      get; set;
    }
    public Guid AppId
    {
      get; set;
    }

    public Guid Id
    {
      get; set;
    }
    public Guid RecipientTenantUserId
    {
      get; set;
    }

    public Guid TenantId
    {
      get; set;
    }

    public bool ReadState
    {
      get; set;
    }
    public Guid EntityId
    {
      get; set;
    }
    public int EntityType
    {
      get; set;
    }

    public DateTime CreatedOn
    {
      get; set;
    }
    public string AdditionalInfo {
      get; set;
    }
    
  }
}

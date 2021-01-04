using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Contains basic required information of a connector postal.
    /// </summary>
    public class ConnectorConfigDTO {

    /// <summary>
    /// Unqiue ID.
    /// </summary>
    public Guid ID {
      get; set;
    }

    /// <summary>
    /// Connactor json.
    /// </summary>
    public string Json {
      get; set;
    }

    /// <summary>
    /// Connection is valid or not.
    /// </summary>
    public string Status {
      get; set;
    }

    /// <summary>
    /// Message.
    /// </summary>
    public string Message {
      get; set;
    }

    /// <summary>
    /// A unque key to identify the connector.
    /// </summary>
    public string ConnectorKey {
      get; set;
    }

    public virtual Guid CreatedBy {
      get;
      set;
    }
    public DateTime? CreatedOn {
      get;
      set;
    }
    public Guid UpdatedBy {
      get;
      set;
    }
    public DateTime? UpdatedOn {
      get;
      set;
    }

    public Guid TenantId {
      get;
      set;
    }

  }
}

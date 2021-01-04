using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO {
  
  /// <summary>
  /// Represents a request DTO with to request support ticket list
  /// </summary>
  public class PartReportSupportTicketParamDTO {
    /// <summary>
    /// Gets or sets the application key.
    /// </summary>
    /// <value>
    /// The application key to get specific application support tickets.
    /// </value>
    public string AppKey {
      get; set;
    }

    /// <summary>
    /// Gets or sets the tenant id.
    /// </summary>
    /// <value>
    /// The tenant id to get specific tenant's support tickets.
    /// </value>
    public Guid TenantId {
      get; set;
    }

    /// <summary>
    /// Gets or sets the generation level.
    /// </summary>
    /// <value>
    /// The generation level.
    /// </value>
    /// <remarks>It should be any value of <see cref="SupportLevelEnum"/>.</remarks>
    public int GenerationLevel {
      get; set;
    }

    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    /// <value>
    /// The customer id to filter specific customer support ticket.
    /// </value>
    public Guid? CustomerId {
      get; set;
    }

    /// <summary>
    /// Gets or sets the creator user id.
    /// </summary>
    /// <value>
    /// The user id of support ticket creator.
    /// </value>
    public Guid CreatorId {
      get; set;
    }

    public DateTime FromDate {
      get; set;
    }

    public DateTime ToDate {
      get; set;
    }

  }
}

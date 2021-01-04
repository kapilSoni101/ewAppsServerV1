using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.Common {

  public enum SalesOrderStatusEnum {
    /// <summary>
    /// Shows that order is open.
    /// </summary>
    Open = 1,

    /// <summary>
    /// Shows that order is incomplete.
    /// </summary>
    Incomplete = 2,

    /// <summary>
    /// Shows that order is completed.
    /// </summary>
    Completed = 3,

    Cancelled = 4,
  }
}

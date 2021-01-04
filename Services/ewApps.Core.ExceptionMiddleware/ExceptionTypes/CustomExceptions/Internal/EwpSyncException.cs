//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 14 june 2015
//===============================================================================

using System;
using System.Collections.Generic;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// The exception that is thrown by exception handling block to replace the original exception,
  /// occurred in DataService layer, with a user friendly message.
  /// </summary>
  [Serializable]
  public class EwpSyncException : BaseException {

    private static readonly string _message = "An error occurred during data sync operation.";

    #region Constructors

    /// <inheritdoc />
    public EwpSyncException()
      : base(_message) {
    }

    /// <inheritdoc />
    public EwpSyncException(string message)
      : base(message) {
    }

    /// <inheritdoc />
    public EwpSyncException(string message, System.Exception inner)
      : base(message, inner) {    
    }
       /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>    
    /// <param name="message">Error message.</param>
    /// <param name="dataList">Validation data.</param>
    public EwpSyncException(string message, IList<EwpErrorData> dataList)
      : base(message, null) {     
      ErrorDataList = dataList;
    }

    #endregion



    /// <summary>
    /// Gets the type of the ewp error.
    /// </summary>
    /// <returns></returns>
    /// <value>
    /// The type of the ewp error.
    /// </value>
    public override ErrorType GetEwpErrorType()
    {
     return ErrorType.SyncException;
    }
  }
}

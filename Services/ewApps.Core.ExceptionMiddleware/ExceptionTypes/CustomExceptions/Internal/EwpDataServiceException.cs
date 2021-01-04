//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 14 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// The exception that is thrown by exception handling block to replace the original exception,
  /// occurred in DataService layer, with a user friendly message.
  /// </summary>
  [Serializable]
  public class EwpDataServiceException : BaseException {

    private static readonly string _message = "An error occurred during data service operation.";

    #region Constructors

    /// <inheritdoc />
    public EwpDataServiceException()
      : base(_message) {
    }

    /// <inheritdoc />
    public EwpDataServiceException(string message)
      : base(message) {
    }

    /// <inheritdoc />
    public EwpDataServiceException(string message, System.Exception inner)
      : base(message, inner) {    
    }

    #endregion

    /// <summary>
    /// Gets the type of the ewp error.
    /// </summary>
    /// <value>
    /// The type of the ewp error.
    /// </value>
    public override ErrorType GetEwpErrorType()
    {
      return ErrorType.DataServiceException;
    }

  }
}

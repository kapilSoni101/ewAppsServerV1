//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 14 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// The exception that is thrown by exception handling block.It logs the original message and replace the original exception,
  /// occurred in Data layer, with a user friendly message.
  /// </summary>
  [Serializable]
  public class EwpDataException : BaseException {

    private static readonly string _message = "An error occurred in database operation.";

    #region Constructors

    /// <inheritdoc />
    public EwpDataException()
      : base(_message) {
    }

    /// <inheritdoc />
    public EwpDataException(string message)
      : base(message) {
    }

    /// <inheritdoc />
    public EwpDataException(string message, System.Exception inner)
      : base(message, inner) {   
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
      return ErrorType.DataException;
    }
  }
}

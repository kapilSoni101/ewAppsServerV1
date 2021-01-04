//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 14 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// It is a base exception class inherited from System.ApplicationException class.
  /// All other custom exception will be inherited from this class.
  /// </summary>
  [Serializable]
  public class EwpRecoverableException : BaseException {

    #region Local Members

    private static readonly string _message = "Invalid entity.";

    #endregion Local Members

    #region Constructor

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    public EwpRecoverableException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    public EwpRecoverableException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="innerException">inner exception.</param>
    public EwpRecoverableException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">inner exception.</param>
    public EwpRecoverableException(string message, Exception innerException)
      : base(message, innerException) {
    }

    #endregion Constructor

    /// <summary>
    /// Gets the type of the ewp error.
    /// </summary>
    /// <value>
    /// The type of the ewp error.
    /// </value>
    public override ErrorType GetEwpErrorType()
    {
      return ErrorType.RecoverableException;
    }
  }
}

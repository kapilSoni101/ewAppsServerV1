//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 14 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// It is a custom exception class inherited from base exception class.
  /// It is used when login password is wrong.
  /// </summary>
  [Serializable]
  public class EwpInvalidPasswordException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "Invalid password.";

    #endregion Local Members

    #region Constructors

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    public EwpInvalidPasswordException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    public EwpInvalidPasswordException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidPasswordException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidPasswordException(string message, Exception innerException)
      : base(message, innerException) {
    }

    #endregion Constructors


    /// <summary>
    /// Gets the type of the ewp error.
    /// </summary>
    /// <value>
    /// The type of the ewp error.
    /// </value>
    public override ErrorType GetEwpErrorType()
    {
      return ErrorType.Authentication;
    }
  }
}

//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 14 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// It is a custom exception class inherited from base exception class.
  /// It is used to when user is locked due to wrong authentication.
  /// </summary>
  [Serializable]
  public class EwpUserLockedException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "User is locked out due to 4 wrong password attempts.";

    #endregion Local Members

    #region Constructors

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    public EwpUserLockedException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    public EwpUserLockedException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpUserLockedException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpUserLockedException(string message, Exception innerException)
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

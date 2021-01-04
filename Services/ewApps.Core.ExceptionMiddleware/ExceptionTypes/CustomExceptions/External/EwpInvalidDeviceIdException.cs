//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {


  /// <summary>
  /// It is a custom exception class inherited to base exception. It is use for invalid or unregister device for Sync name found.
  /// </summary>
  [Serializable]
  public class EwpInvalidDeviceIdException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "The given device is invalid or not register on server.";

    #endregion Local Members

    #region Constructor

    /// <inheritdoc />
    public EwpInvalidDeviceIdException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message.</param>
    public EwpInvalidDeviceIdException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidDeviceIdException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidDeviceIdException(string message, Exception innerException)
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
      return ErrorType.InvalidDeviceId;
    }
  }
}

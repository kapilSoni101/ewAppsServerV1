//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// It is a custom exception class inherited from base exception class.
  /// It is used to ignore.
  /// </summary>
  [Serializable]
  public class EwpIgnoredException : BaseException {

    #region Local Members

    private static readonly string _message = "Ignore this error.";

    #endregion Local Members

    #region Constructors

    /// <summary>
    /// Intialize class constructor.
    /// </summary>
    public EwpIgnoredException()
      : base(_message, null) {
    }

    /// <summary>
    /// Intitialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Message from exception.</param>
    public EwpIgnoredException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Intitialize constructor with required parameter.
    /// </summary>
    /// <param name="innerException">Inner exception message.</param>
    public EwpIgnoredException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Intitialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpIgnoredException(string message, Exception innerException)
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
      return ErrorType.IgnoreException;
    }
  }
}

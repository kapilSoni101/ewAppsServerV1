//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// It is a custom exception class inherited from base exception class.
  /// It is used when login emial id is wrong.
  /// </summary>
  [Serializable]
  public class EwpInvalidLoginEmailIdException : EwpRecoverableException {

 #region Local Members

    

    #endregion Local Members

    #region Constructors

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    public EwpInvalidLoginEmailIdException()
      : base("Invlid Email", null) {
    }

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    /// <param name="message">Error message.</param>
    public EwpInvalidLoginEmailIdException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidLoginEmailIdException(Exception innerException)
      : base("Invalid email", innerException) {
    }

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidLoginEmailIdException(string message, Exception innerException)
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

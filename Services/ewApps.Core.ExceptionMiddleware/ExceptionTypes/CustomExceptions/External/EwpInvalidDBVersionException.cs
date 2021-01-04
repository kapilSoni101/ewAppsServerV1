//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================

using System;

namespace ewApps.Core.ExceptionService {


  /// <summary>
  /// It is a custom exception class inherited to base exception. It is use for invalid database version.
  /// </summary>
  [Serializable]
  public class EwpInvalidDBVersionException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "The given database version is invalid.";

    #endregion Local Members

    #region Constructor

    /// <inheritdoc />
    public EwpInvalidDBVersionException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message.</param>
    public EwpInvalidDBVersionException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidDBVersionException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidDBVersionException(string message, Exception innerException)
      : base(message, innerException) {
    }

    /// <summary>
    /// Latest DB verion on server
    /// </summary>  
    public Int32 LatestDBVersion {
      get;
      set;
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
      return ErrorType.InvalidVersion;
    }
  }
}

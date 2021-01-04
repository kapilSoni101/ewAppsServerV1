//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 14 june 2015
//===============================================================================
using System;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// The exception that is thrown by the exception handling block to replace the exception from data layer.
  /// </summary>
  [Serializable]
  public class EwpPassThroughException : BaseException {

    #region Local Members

    private static readonly string _message = "Pass this exception to an outer level.";

    #endregion Local Members

    #region Constructors

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    public EwpPassThroughException()
      : base(_message) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    public EwpPassThroughException(string message)
      : base(message) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="inner">Inner exception.</param>
    public EwpPassThroughException(string message, System.Exception inner)
      : base(message, inner) {
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
      return ErrorType.PassThroughException;
    }
  }
}

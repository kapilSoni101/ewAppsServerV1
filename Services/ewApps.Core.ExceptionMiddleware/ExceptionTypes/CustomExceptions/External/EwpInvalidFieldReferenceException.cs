//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================

using System;
using System.Collections.Generic;

namespace ewApps.Core.ExceptionService {
  /// <summary>
  /// It is a custom exception class inherited to base exception. It is use for invalid Reference.
  /// </summary>
  [Serializable]
  public class EwpInvalidFieldReferenceException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "Invalid Field Reference Exception.";
   
    #endregion Local Members

    #region Constructor

    /// <inheritdoc />
    public EwpInvalidFieldReferenceException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">InvalidReference message.</param>
    public EwpInvalidFieldReferenceException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidFieldReferenceException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpInvalidFieldReferenceException(string message, Exception innerException)
      : base(message, innerException) {
    }

 /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>    
    /// <param name="message">Error message.</param>
    /// <param name="dataList">Validation data.</param>
    public EwpInvalidFieldReferenceException(string message, IList<EwpErrorData> dataList)
      : base(message, null) {
      ErrorDataList = dataList;
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
      return ErrorType.InvalidFieldReference;
    }
  }
}

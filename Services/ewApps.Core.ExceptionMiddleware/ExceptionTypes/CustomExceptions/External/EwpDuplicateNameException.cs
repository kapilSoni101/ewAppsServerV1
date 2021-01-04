//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================
using System;
using System.Collections.Generic;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// It is a custom exception class inherited to base exception. It is use for duplicate name found.
  /// </summary>
  [Serializable]
  public class EwpDuplicateNameException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "Duplicate names are not allowed.";

    #endregion Local Members

    #region Constructor   

    /// <summary>
    /// Initializes constructor with no parameter.
    /// </summary>
    public EwpDuplicateNameException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message.</param>
    public EwpDuplicateNameException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpDuplicateNameException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message.</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpDuplicateNameException(string message, Exception innerException)
      : base(message, innerException) {
    }


    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>    
    /// <param name="message">Error message.</param>
    /// <param name="dataList">Validation data.</param>
    public EwpDuplicateNameException(string message, IList<EwpErrorData> dataList)
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
      return ErrorType.Duplicate;
    }
  }
}

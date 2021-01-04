//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 14 june 2015
//===============================================================================
using System;
using System.Collections.Generic;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// It is a custom exception class inherited from base exception class. 
  /// It is used to pass exception when operation is invalid.
  /// </summary>
  [Serializable]
  public class EwpSecurityException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "Unauthorized user.";

    #endregion Local Members

    #region Constructors

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    public EwpSecurityException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    public EwpSecurityException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="innerException"></param>
    public EwpSecurityException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>    
    /// <param name="message">Error message.</param>
    /// <param name="dataList">Validation data.</param>
    public EwpSecurityException(string message, IList<EwpErrorData> dataList)
      : base(message, null) {
      ErrorDataList = dataList;
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
      return ErrorType.Security;
    }

  }
}

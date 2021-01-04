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
  public class EwpInvalidOperationException : BaseException {

    #region Local Members

    private static readonly string _message = "Invalid operation.";

    #endregion Local Members

    #region Constructors

    /// <summary>
    /// Initialize constructor.
    /// </summary>
    public EwpInvalidOperationException()
      : base(_message, null) {
    }

    ///// <summary>
    ///// Initialize constructor with required parameter.
    ///// </summary>
    ///// <param name="message">Error message.</param>
    //public EwpInvalidOperationException(string message)
    //  : base(message, null) {
    //}

    ///// <summary>
    ///// Initialize constructor with required parameter.
    ///// </summary>
    ///// <param name="innerException">Inner exception.</param>
    //public EwpInvalidOperationException(Exception innerException)
    //  : base(_message, innerException) {
    //}

    ///// <summary>
    ///// Initialize constructor with required parameter.
    ///// </summary>
    ///// <param name="message">Error message.</param>
    ///// <param name="innerException">Inner exception.</param>
    ///// <param name="opName">operation name.</param>
    //public EwpInvalidOperationException(string message, Exception innerException, string opName = "")
    //  : base(message, innerException) {
    //  SetData(opName);
    //}

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="subType">Inner exception.</param>
    /// <param name="data">operation name.</param>
    public EwpInvalidOperationException(string message, InvalidOperationExceptionSubType subType = InvalidOperationExceptionSubType.MethodNotSupported, object data = null)
      : base(message, null) {      
      EwpErrorData errorData = new EwpErrorData() {
        Message = message,
        ErrorSubType = (int)subType,
        Data = data
      };
      ErrorDataList = new List<EwpErrorData>() { errorData };
    }

    #endregion Constructors

    #region Public

    /// <summary>
    /// This method set operation name in excetpion.
    /// </summary>
    /// <param name="opName">Operation name.</param>
    public void SetData(string opName) {
      if (!string.IsNullOrEmpty(opName))
        SetData("OperationName", opName);
    }

    /// <summary>
    /// This Method get operation name from exception.
    /// </summary>
    public void GetData() {
      GetData("OperationName");
    }

    #endregion Public

    /// <summary>
    /// Gets the type of the ewp error.
    /// </summary>
    /// <value>
    /// The type of the ewp error.
    /// </value>
    public override ErrorType GetEwpErrorType() {
      return ErrorType.InvalidOperation;
    }
  }
}

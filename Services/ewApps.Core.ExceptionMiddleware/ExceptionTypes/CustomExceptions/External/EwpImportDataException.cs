//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================

using System;
using System.Collections.Generic;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// It is a custom exception class inherited from base exception class. 
  /// It is use for import data opration 
  /// </summary>
  [Serializable]
  public class EwpImportDataException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "Exception occured to import data.";

    #endregion Local Members

    #region Constructors

    /// <summary>
    /// Initialize default constructor.
    /// </summary>
    public EwpImportDataException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initialize parameterized constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public EwpImportDataException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initialize parameterized constructor.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpImportDataException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initialize parameterized constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="erros">The erros.</param>
    public EwpImportDataException(string message, List<EwpErrorData> erros)
      : base(message, null) {
        ErrorDataList = erros;
    }

    /// <summary>
    /// Initialize parameterized constructor.
    /// </summary>
    /// <param name="erros">The erros.</param>
    public EwpImportDataException(List<EwpErrorData> erros)
      : base(_message, null) {
        ErrorDataList = erros;
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
      return ErrorType.ImportData;
    }
  }
}

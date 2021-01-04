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
  public class EwpDataConcurrencyException : EwpRecoverableException {

    #region Local Members

    //private static readonly string _message = "This item has been updated or deleted by another user. Please reload the item and then retry your operation.";
    private static readonly string _message = "The data for this item has been changed by another user. The item data has been refreshed on your screen.Please re-enter your data.";
    private static Dictionary<string, string> _concurrentfieldList;

    #endregion Local Members

    #region Constructor

    /// <inheritdoc />
    public EwpDataConcurrencyException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message.</param>
    public EwpDataConcurrencyException(string message)
      : base(message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpDataConcurrencyException(Exception innerException)
      : base(_message, innerException) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpDataConcurrencyException(string message, Exception innerException)
      : base(message, innerException) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message.</param>
    /// <param name="fields">Concurrent field dictionary.</param>
    public EwpDataConcurrencyException(string message, Dictionary<string, string> fields)
      : base(message, null) {
      _concurrentfieldList = fields;
    }

    /// <summary>
    /// It provide field collection.
    /// </summary>
    public Dictionary<string, string> FieldList {
      get {
        return _concurrentfieldList;
      }
    }

    /// <summary>
    /// Add new field in field list.
    /// </summary>
    /// <param name="fieldName">Name of the concurrent field.</param>
    /// <param name="message">Concurrency message.</param>
    public void AddConcurrency(string fieldName, string message) {
      Dictionary<string, string> fields = _concurrentfieldList;
      if (fields == null)
        fields = new Dictionary<string, string>();
      fields.Add(fieldName, message);
      _concurrentfieldList = fields;
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
      return ErrorType.Concurrency;
    }
  }
}

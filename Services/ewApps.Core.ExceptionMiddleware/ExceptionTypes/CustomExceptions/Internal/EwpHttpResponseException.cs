//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================
using System;

namespace ewApps.Core.ExceptionService
{

  /// <summary>
  /// It is a custom exception class inherited to base exception. It is use for service call.
  /// </summary>
  [Serializable]
  public class EwpHttpResponseException : EwpRecoverableException
  {

    #region Local Members

    //private static readonly string _message = "This item has been updated or deleted by another user. Please reload the item and then retry your operation.";
    private static readonly string _message = "An error occurred during service operation.";
    private static string _responseExMessage;
    private static int _statuscode;

    #endregion Local Members

    #region Constructor

    /// <inheritdoc />
    public EwpHttpResponseException()
      : base(_message, null)
    {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message.</param>
    public EwpHttpResponseException(string message)
      : base(message, null)
    {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="innerException">Inner exception.</param>
    public EwpHttpResponseException(Exception innerException)
      : base(_message, innerException)
    {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="innerException">Inner exception.</param>
    public EwpHttpResponseException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Error mesage.</param>
    /// <param name="responseExMessage">The response ex message.</param>
    public EwpHttpResponseException(string message, string responseExMessage)
      : base(message, null)
    {
      _responseExMessage = responseExMessage;
    }



    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Error mesage.</param>
    /// <param name="responseExMessage">The response ex message.</param>
    public EwpHttpResponseException(string message, string responseExMessage, int statuscode)
      : base(message, null)
    {
      _responseExMessage = responseExMessage;
      _statuscode = statuscode;
    }

    /// <summary>
    /// Gets the response exception message.
    /// </summary>
    /// <value>
    /// The response exception message.
    /// </value>
    public string ResponseExceptionMessage
    {
      get
      {
        return _responseExMessage;
      }
    }

    /// <summary>
    /// Gets the response exception message.
    /// </summary>
    /// <value>
    /// The response exception message.
    /// </value>
    public int ResponseStatusCode
    {
      get
      {
        return _statuscode;
      }

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
      return ErrorType.ResponseException;
    }
  }
}

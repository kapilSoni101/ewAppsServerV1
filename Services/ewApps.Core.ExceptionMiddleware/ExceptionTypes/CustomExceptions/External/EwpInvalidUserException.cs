////===============================================================================
//// © 2015 eWorkplace Apps.  All rights reserved.
//// Original Author: Sanjeev Khanna
//// Original Date: 14 june 2015
////===============================================================================

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Runtime.Serialization;

//namespace ewApps.Core.ExceptionService {


//  /// <summary>
//  /// It is a custom exception class inherited to base exception. It is use for invalid user.
//  /// </summary>
//  [Serializable]
//  public class EwpInvalidUserException : EwpRecoverableException {

//    #region Local Members

//    private static readonly string _message = "The given user id is invalid.";

//    #endregion Local Members

//    #region Constructor

//    /// <inheritdoc />
//    public EwpInvalidUserException()
//      : base(_message, null) {
//    }

//    /// <summary>
//    /// Initializes constructor with required parameter.
//    /// </summary>
//    /// <param name="message">Concurrency message.</param>
//    public EwpInvalidUserException(string message)
//      : base(message, null) {
//    }

//    /// <summary>
//    /// Initializes constructor with required parameter.
//    /// </summary>
//    /// <param name="innerException">Inner exception.</param>
//    public EwpInvalidUserException(Exception innerException)
//      : base(_message, innerException) {
//    }

//    /// <summary>
//    /// Initializes constructor with required parameter.
//    /// </summary>
//    /// <param name="message">Concurrency message</param>
//    /// <param name="innerException">Inner exception.</param>
//    public EwpInvalidUserException(string message, Exception innerException)
//      : base(message, innerException) {
//    }

//    #endregion Constructor

//    /// <summary>
//    /// Gets the type of the ewp error.
//    /// </summary>
//    /// <value>
//    /// The type of the ewp error.
//    /// </value>
//    public override ErrorType GetEwpErrorType()
//    {
//      return ErrorType.Authentication;
//    }
//  }
//}

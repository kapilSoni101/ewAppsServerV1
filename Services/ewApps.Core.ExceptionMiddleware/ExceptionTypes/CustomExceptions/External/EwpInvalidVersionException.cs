//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================
using System;

namespace ewApps.Core.ExceptionService
{

    /// <summary>
    /// It is a custom exception class inherited to base exception. It is use for Invalid Version Exception.
    /// </summary>
    public class EwpInvalidVersionException : BaseException
    {

        #region Local Members

        private static readonly string _message = "Invalid Version Exception.";

        #endregion Local Members

        #region Constructor

        /// <inheritdoc />
        public EwpInvalidVersionException()
            : base(_message, null)
        {
        }

        /// <inheritdoc />
        public EwpInvalidVersionException(string message)
            : base(message, null)
        {
        }


        /// <inheritdoc />
        public EwpInvalidVersionException(Exception innerException)
            : base(_message, innerException)
        {
        }

        /// <inheritdoc />
        public EwpInvalidVersionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion Constructor

        
        /// <summary>
        /// Gets the type of the ewp error.
        /// </summary>
        /// <returns></returns>
        /// <value>
        /// The type of the ewp error.
        /// </value>
        public override ErrorType GetEwpErrorType()
        {
            return ErrorType.InvalidVersion;
        }
    }
}

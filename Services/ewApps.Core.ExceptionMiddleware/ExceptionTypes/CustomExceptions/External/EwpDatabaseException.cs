//===============================================================================
// © 2015 eWorkplace Apps.  All rights reserved.
// Original Author: Sanjeev Khanna
// Original Date: 11 june 2015
//===============================================================================
using System;

namespace ewApps.Core.ExceptionService
{

    /// <summary>
    /// It is a custom exception class inherited to base exception. It is use for Database Exception.
    /// </summary>
    public class EwpDatabaseException : BaseException
    {

        #region Local Members

        private static readonly string _message = "Database Exception.";

        #endregion Local Members

        #region Constructor

        /// <inheritdoc />
        public EwpDatabaseException()
            : base(_message, null)
        {
        }

        /// <inheritdoc />
        public EwpDatabaseException(string message)
            : base(message, null)
        {
        }

      
        /// <inheritdoc />
        public EwpDatabaseException(Exception innerException)
            : base(_message, innerException)
        {
        }

        /// <inheritdoc />
        public EwpDatabaseException(string message, Exception innerException)
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
            return ErrorType.Database;
        }
    }
}

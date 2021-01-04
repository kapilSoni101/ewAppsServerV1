//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
////===============================================================================
//// © 2015 eWorkplace Apps.  All rights reserved.
//// Original Author: Sanjeev Khanna
//// Original Date: 11 june 2015
////===============================================================================

//using System;

//namespace ewApps.Core.ExceptionService
//{

//    /// <summary>
//    /// It is a custom exception class inherited from base exception class.
//    /// It is used when login emial id is wrong.
//    ///// </summary>
//    [Serializable]
//    public class EwpInvalidEmailDomainException : EwpRecoverableException
//    {

//        #region Local Members

//        #endregion Local Members

//        #region Constructors

//        /// <summary>
//        /// Initialize constructor.
//        /// </summary>
//        public EwpInvalidEmailDomainException()
//          : base(ServerMessages.InvalidEmail, null)
//        {
//        }

//        /// <summary>
//        /// Initialize constructor.
//        /// </summary>
//        /// <param name="message">Error message.</param>
//        public EwpInvalidEmailDomainException(string message)
//          : base(message, null)
//        {
//        }

//        /// <summary>
//        /// Initialize constructor.
//        /// </summary>
//        /// <param name="innerException">Inner exception.</param>
//        public EwpInvalidEmailDomainException(Exception innerException)
//          : base(ServerMessages.InvalidEmailDomain, innerException)
//        {
//        }

//        /// <summary>
//        /// Initialize constructor.
//        /// </summary>
//        /// <param name="message">Error message.</param>
//        /// <param name="innerException">Inner exception.</param>
//        public EwpInvalidEmailDomainException(string message, Exception innerException)
//          : base(message, innerException)
//        {
//        }

//        #endregion Constructors

//        /// <summary>
//        /// Gets the type of the ewp error.
//        /// </summary>
//        /// <value>
//        /// The type of the ewp error.
//        /// </value>
//        public override ErrorType GetEwpErrorType()
//        {
//            return ErrorType.Authentication;
//        }
//    }
//}

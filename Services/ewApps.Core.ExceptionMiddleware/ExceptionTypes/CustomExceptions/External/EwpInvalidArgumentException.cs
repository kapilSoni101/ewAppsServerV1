//////===============================================================================
////// © 2015 eWorkplace Apps.  All rights reserved.
////// Original Author: Sanjeev Khanna
////// Original Date: 11 june 2015
//////===============================================================================

////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Runtime.Serialization;

////namespace ewApps.Core.ExceptionService
////{

////    /// <summary>
////    /// It is a custom exception class inherited from base exception class. 
////    /// It is used to pass exception when operation is invalid.
////    /// </summary>
////    [Serializable]
////    public class EwpInvalidArgumentException : EwpRecoverableException
////    {

////        #region Local Members

////        private static readonly string _message = "Invalid Argument.";

////        #endregion Local Members

////        #region Constructors

////        /// <summary>
////        /// Initialize constructor.
////        /// </summary>
////        public EwpInvalidArgumentException()
////            : base(_message, null)
////        {
////        }

////        /// <summary>
////        /// Initialize constructor with required parameter.
////        /// </summary>
////        /// <param name="message">Error message.</param>
////        public EwpInvalidArgumentException(string message)
////            : base(message, null)
////        {
////        }

////        /// <summary>
////        /// Initialize constructor with required parameter.
////        /// </summary>
////        /// <param name="innerException">Inner exception.</param>
////        public EwpInvalidArgumentException(Exception innerException)
////            : base(_message, innerException)
////        {
////        }

////        /// <summary>
////        /// Initialize constructor with required parameter.
////        /// </summary>
////        /// <param name="message">Error message.</param>
////        /// <param name="innerException">Inner exception.</param>
////        /// <param name="opName">operation name.</param>
////        public EwpInvalidArgumentException(string message, Exception innerException, string opName = "")
////            : base(message, innerException)
////        {
////            SetData(opName);
////        }

////        #endregion Constructors

////        #region Public

////        /// <summary>
////        /// This method set operation name in excetpion.
////        /// </summary>
////        /// <param name="opName">Operation name.</param>
////        public void SetData(string opName)
////        {
////            if (!string.IsNullOrEmpty(opName))
////                SetData("OperationName", opName);
////        }

////        /// <summary>
////        /// This Method get operation name from exception.
////        /// </summary>
////        public void GetData()
////        {
////            GetData("OperationName");
////        }

////        #endregion Public

////        /// <summary>
////        /// Gets the type of the ewp error.
////        /// </summary>
////        /// <value>
////        /// The type of the ewp error.
////        /// </value>
////        public override ErrorType GetEwpErrorType()
////        {
////            return ErrorType.InvalidFieldReference;
////        }
////    }
////}

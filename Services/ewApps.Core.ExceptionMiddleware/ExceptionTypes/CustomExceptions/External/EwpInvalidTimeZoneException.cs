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
    /// It is used to invalid time zone .
    /// </summary>
    [Serializable]
    public class EwpInvalidTimeZoneException : EwpRecoverableException {

        #region Local Members

        private static readonly string _message = "Timze zone is not valid.";

        #endregion Local Members

        #region Constructors

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        public EwpInvalidTimeZoneException()
            : base(_message, null) {
        }

        /// <summary>
        /// Initialize constructor with required parameter.
        /// </summary>
        /// <param name="message">Error message.</param>
        public EwpInvalidTimeZoneException(string message)
            : base(message, null) {
        }

        /// <summary>
        /// Initialize constructor with required parameter.
        /// </summary>    
        /// <param name="message">Error message.</param>
        /// <param name="dataList">Validation data.</param>
        public EwpInvalidTimeZoneException(string message, IList<EwpErrorData> dataList)
            : base(message, null) {
            ErrorDataList = dataList;
        }


        /// <summary>
        /// Initialize constructor with required parameter.
        /// </summary>
        /// <param name="innerException">inner exception.</param>
        public EwpInvalidTimeZoneException(Exception innerException)
            : base(_message, innerException) {
        }

        /// <summary>
        /// Initialize constructor with required parameter.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner exception.</param>
        public EwpInvalidTimeZoneException(string message, Exception innerException)
            : base(message, innerException) {
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
          return ErrorType.InvalidTimeZone;
        }
    }
}

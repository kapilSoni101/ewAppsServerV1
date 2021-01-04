using System;
using System.Collections.Generic;
using System.Net;

namespace ewApps.Core.ExceptionService {
    /// <summary>
    /// This is custom exception raised on Service call.
    /// </summary>
    /// <seealso cref="ewApps.Core.ExceptionService.BaseException" />
    public class EwpServiceException:BaseException {

        private string _message = "";
        private HttpStatusCode _statusCode;
        private ErrorType _errorType;



        /// <summary>
        /// Initializes a new instance of the <see cref="EwpServiceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="errorDataList">The ewp error data list.</param>
        public EwpServiceException(string message, HttpStatusCode statusCode, ErrorType errorType, IList<EwpErrorData> errorDataList) : base(message) {
            _message = message;
            _statusCode = statusCode;
            ErrorDataList = errorDataList;
            _errorType = errorType;
        }

        /// <summary>
        ///  The type of the ewp error.
        /// </summary>
        /// <returns>Returns ErrorType mapped to <see cref="EwpServiceException"/>.</returns>
        public override ErrorType GetEwpErrorType() {
            //return ErrorType.ServiceProcessException;
            return _errorType;
        }
    }
}

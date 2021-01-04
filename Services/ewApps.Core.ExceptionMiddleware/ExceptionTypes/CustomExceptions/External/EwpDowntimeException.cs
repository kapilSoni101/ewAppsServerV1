using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ewApps.Core.ExceptionService {
  public class EwpDowntimeException : EwpRecoverableException {

    #region Local Members

    private static readonly string _message = "Down time error";

    #endregion Local Members

    #region Constructor   

    /// <summary>
    /// Initializes constructor with no parameter.
    /// </summary>
    public EwpDowntimeException()
      : base(_message, null) {
    }

    /// <summary>
    /// Initializes constructor with required parameter.
    /// </summary>
    /// <param name="message">Concurrency message.</param>
    public EwpDowntimeException(string message)
      : base(message, null) {
    }

    ///// <summary>
    ///// Initializes constructor with required parameter.
    ///// </summary>
    ///// <param name="innerException">Inner exception.</param>
    //public EwpDowntimeException(Exception innerException)
    //  : base(_message, innerException) {
    //}

    ///// <summary>
    ///// Initializes constructor with required parameter.
    ///// </summary>
    ///// <param name="message">Concurrency message.</param>
    ///// <param name="innerException">Inner exception.</param>
    //public EwpDowntimeException(string message, Exception innerException)
    //  : base(message, innerException) {
    //}


    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="subType">Inner exception.</param>
    /// <param name="data">operation name.</param>
    public EwpDowntimeException(string message, DowntimeExceptionSubType subType = DowntimeExceptionSubType.DowntimeError, object data = null)
      : base(message, null) {
      EwpErrorData errorData = new EwpErrorData() {
        Message = message,
        ErrorSubType = (int)subType,
        Data = data
      };
      ErrorDataList = new List<EwpErrorData>() { errorData };
    }

    /// <summary>
    /// Initialize constructor with required parameter.
    /// </summary>    
    /// <param name="message">Error message.</param>
    /// <param name="dataList">Validation data.</param>
    public EwpDowntimeException(string message, IList<EwpErrorData> dataList)
      : base(message, null) {
      ErrorDataList = dataList;
    }

    #endregion Constructor

    /// <summary>
    /// Gets the type of the ewp error.
    /// </summary>
    /// <value>
    /// The type of the ewp error.
    /// </value>
    public override ErrorType GetEwpErrorType() {
      return ErrorType.DowntimeException;
    }


  }
}

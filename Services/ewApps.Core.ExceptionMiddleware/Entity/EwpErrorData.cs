//===============================================================================
// Copyright © eWorkplace Apps.  All rights reserved.
// eWorkplace Apps Common Tools
// Main Author: Sanjeev Khanna
// Original Date: Apr. 02, 2012
//===============================================================================

using System.Runtime.Serialization;

namespace ewApps.Core.ExceptionService
{

  /// <summary>
  /// This class contains the error properties for service. 
  /// It is used to handle webfault exception with custom xml element.
  /// </summary>
  [DataContract]
  public class EwpErrorData {

    #region Constructor

    /// <summary>
    /// Default constructor.
    /// </summary>
    public EwpErrorData() {
    }

    /// <summary>
    /// Initializes a new instance of the Error Data class.  
    /// </summary>
    /// <param name="errorSubType">Error sub type enum value.</param>
    /// <param name="data">Error data.</param>
    /// <param name="msg">Error message.</param>
    public EwpErrorData(int errorSubType, object data, string msg) {
      ErrorSubType = errorSubType;
      Data = data;
      Message = msg;
    }

    #endregion Constructor

    /// <summary>
    /// Eexception message.
    /// </summary>
    [DataMember]
    public int ErrorSubType {
      get;
      set;
    }


    /// <summary>
    /// Detail information of exception.
    /// </summary>
    [DataMember]
    public object Data {
      get;
      set;
    }

  
    /// <summary>
    /// Http status code.
    /// </summary>
    [DataMember]
    public string Message {
      get;
      set;
    }

  }
}
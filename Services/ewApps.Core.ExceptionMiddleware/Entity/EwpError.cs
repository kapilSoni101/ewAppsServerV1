//===============================================================================
// Copyright © eWorkplace Apps.  All rights reserved.
// eWorkplace Apps Common Tools
// Main Author: Sanjeev Khanna
// Original Date: Nov. 28 2014
//===============================================================================
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace ewApps.Core.ExceptionService
{

  /// <summary>
  /// Struct hold the structure of error. It contain all necessary information of generated error.
  /// </summary>
  [DataContract]
  public class EwpError {

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the EwpError class.  
    /// </summary>
    public EwpError() {
      MessageList = new List<string>();
      EwpErrorDataList = new List<EwpErrorData>();
    }

    /// <summary>
    /// Initializes a new instance of the EwpError class.  
    /// </summary>
    /// <param name="errorType">Error Type</param>
    /// <param name="msgs">Message List</param>
    /// <param name="dataList">Data List</param>
    public EwpError(ErrorType errorType, List<string> msgs, IList<EwpErrorData> dataList) {
      ErrorType = errorType;
      MessageList = msgs;
      EwpErrorDataList = dataList;
    }

    /// <summary>
    /// To the json.
    /// </summary>
    /// <param name="ewpError">The ewp error.</param>
    /// <returns>Returns a srting of Json type </returns>
    public static string ToJSON(EwpError ewpError)
    {
      string jsonError = string.Empty;
      jsonError = Newtonsoft.Json.JsonConvert.SerializeObject(ewpError);
      return jsonError;
    }

    public String ToJson()
    {
      return JsonConvert.SerializeObject(this);
    }

    public class Exception
    {
      public Exception(EwpError ewpError)
      {
        this.ewpError = ewpError;
      }

      EwpError ewpError;
    }

    #endregion Constructor

    #region Properties

    /// <summary>
    /// Error type indicate the type of error. It may be validation, system error.
    /// </summary>
    [DataMember]
    public ErrorType ErrorType {
      get;
      set;
    }

    /// <summary>
    /// Message array hold the array of messages to show.
    /// </summary>   
    [DataMember]
    public List<string> MessageList {
      get;
      set;
    }

    /// <summary>
    /// The ewp error data list.
    /// </summary>
    [DataMember]
    public IList<EwpErrorData> EwpErrorDataList {
      get;
      set;
    }

    public override string ToString() {
      return JsonConvert.SerializeObject(this);
    }

    #endregion Properties

    #region XML

    // -------------------- XML --------------------

    /* XML:
       <EwpError>
         <ErrorType></ErrorType>
         <MessageList>
          <Message></Message>
          <Message></Message>
           ...
         </MessageList>
         <DataList>
           <Data Key=""  Value=""></Data>
           <Data Key=""  Value=""></Data>
           ...
         </DataList>
       </EwpError>
   */

    /// <summary>
    /// To the XML writer.
    /// </summary>
    /// <param name="ewpError">The ewp error.</param>
    /// <returns>Returns the xml documents </returns>
    public static XmlDocument ToXmlWriter(EwpError ewpError) {

      XmlDocument xmlDoc = new XmlDocument();

      XmlElement rootNode = xmlDoc.CreateElement("EwpServiceErrorData");
      xmlDoc.AppendChild(rootNode);

      XmlElement node = xmlDoc.CreateElement("ErrorType");
      node.InnerText = Convert.ToString(ewpError.ErrorType);
      rootNode.AppendChild(node);

      XmlElement DataListNode = xmlDoc.CreateElement("DataList");
      rootNode.AppendChild(DataListNode);
      foreach (EwpErrorData data in ewpError.EwpErrorDataList) {
        XmlElement dataNode = xmlDoc.CreateElement("Data");

        XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Key");
        nameAttribute.Value = Convert.ToString((int)data.ErrorSubType);
        dataNode.Attributes.Append(nameAttribute);

        XmlAttribute valueAttribute = xmlDoc.CreateAttribute("Value");
        valueAttribute.Value = Convert.ToString(data.Data);
        dataNode.Attributes.Append(valueAttribute);

        DataListNode.AppendChild(dataNode);
      }

      XmlElement messageListNode = xmlDoc.CreateElement("MessageList");
      rootNode.AppendChild(messageListNode);
      foreach (string msg in ewpError.MessageList) {
        XmlElement msgNode = xmlDoc.CreateElement("Message");
        msgNode.InnerText = msg;
        messageListNode.AppendChild(msgNode);
      }

      return xmlDoc;
    }

    #endregion XML

    #region JSON

    // -------------------- XML --------------------

    /* XML:
       <EwpError>
         <ErrorType></ErrorType>
         <MessageList>
          <Message></Message>
          <Message></Message>
           ...
         </MessageList>
         <DataList>
           <Data Key=""  Value=""></Data>
           <Data Key=""  Value=""></Data>
           ...
         </DataList>
       </EwpError>
   */



    #endregion JSON
  }

}
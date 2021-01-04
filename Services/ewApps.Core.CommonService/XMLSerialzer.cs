/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul badgujar <abadgujar@batchmaster.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ewApps.Core.CommonService {
    /// <summary>
    /// It provides object serialization and de-serialization into xml.
    /// </summary>  
    public class XMLSerialzer {

        #region public methods 
        /// <summary>
        /// Serizlize the object into xml string.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object.</param>
        /// <returns>String.</returns>
        public static string SerialzeObject<T>(T obj) {

            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            //DataContractSerializer   xsSubmit=new DataContractSerializer(typeof(call));

            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);

            xsSubmit.Serialize(writer, obj);
            return sww.ToString();

        }

        /// <summary>
        /// Deserialize string into object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="xml">XML String.</param>
        /// <returns>Object.</returns>
        public static T DeSerializeObject<T>(string xml) {
            if(string.IsNullOrEmpty(xml))
                return default(T);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));

            return (T)xsSubmit.Deserialize(new MemoryStream(GetBytes(xml)));
        }



        /// <summary>
        /// Converts a list of KeyValuePair&lt;string,string&gt; to xml document in below mention format. <br/>
        /// &lt;DataList&gt;
        ///   &lt;Data Key="Key1String" Value="Key1Value" /&gt;
        ///   &lt;Data Key="Key2String" Value="Key2Value" /&gt;
        /// &lt;DataList/&gt;
        /// </summary>
        /// <param name="keyValueList">The key value list to be convert in xml.</param>
        /// <returns>Returns converted xml document.</returns>
        public static XmlDocument ToXmlWriter(List<KeyValuePair<string, string>> keyValueList) {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement dataListNode = xmlDoc.CreateElement("DataList");
            xmlDoc.AppendChild(dataListNode);

            //XmlElement DataListNode = xmlDoc.CreateElement("DataList");
            //rootNode.AppendChild(DataListNode);
            foreach(KeyValuePair<string, string> data in keyValueList) {
                XmlElement dataNode = xmlDoc.CreateElement("Data");

                XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Key");
                nameAttribute.Value = data.Key.ToString();
                dataNode.Attributes.Append(nameAttribute);

                XmlAttribute valueAttribute = xmlDoc.CreateAttribute("Value");
                valueAttribute.Value = data.Value;
                dataNode.Attributes.Append(valueAttribute);

                dataListNode.AppendChild(dataNode);
            }

            return xmlDoc;
        }

        /// <summary>
        /// To the XML writer.
        /// </summary>
        /// <param name="keyValueList">The key value list.</param>
        /// <returns></returns>
        public static XmlDocument ToXmlWriter(List<Dictionary<string, string>> keyValueList) {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement dataListNode = xmlDoc.CreateElement("DataList");
            xmlDoc.AppendChild(dataListNode);

            foreach(Dictionary<string, string> data in keyValueList) {
                XmlElement dataNode = xmlDoc.CreateElement("Data");
                dataListNode.AppendChild(dataNode);
                foreach(KeyValuePair<string, string> information in data) {
                    XmlElement informationNode = xmlDoc.CreateElement("Information");

                    XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Key");
                    nameAttribute.Value = information.Key.ToString();
                    informationNode.Attributes.Append(nameAttribute);

                    XmlAttribute valueAttribute = xmlDoc.CreateAttribute("Value");
                    valueAttribute.Value = information.Value;
                    informationNode.Attributes.Append(valueAttribute);

                    dataNode.AppendChild(informationNode);
                }
            }

            return xmlDoc;
        }

        /// <summary>
        /// Serizlize the object into xml string.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="value">Object.</param>
        /// <returns>String.</returns>
        public static string Serialize<T>(T value) {

            if(value == null) {
                return null;
            }

            // Nitin:Please uncomment below this code and comment out your code.
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using(MemoryStream mStream = new MemoryStream()) {
                using(XmlTextWriter xmlTextWriter = new XmlTextWriter(mStream, new UTF8Encoding(false, false))) {
                    serializer.Serialize(xmlTextWriter, value);
                    mStream.Position = 0;
                    // return mStream.StreamToString();
                    return Encoding.UTF8.GetString(mStream.GetBuffer());
                    //return "";
                }
            }

            //XmlSerializer serializer = new XmlSerializer(typeof(T));
            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Encoding = new UnicodeEncoding(false, false);
            //settings.Indent = false;
            //settings.OmitXmlDeclaration = false;

            //using (StringWriter textWriter = new StringWriter()) {
            //  using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings)) {
            //    serializer.Serialize(xmlWriter, value);
            //  }
            //  return textWriter.ToString();
            //}
        }

        /// <summary>
        /// Deserialize string into object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="xml">XML String.</param>
        /// <returns>Object.</returns>
        public static T Deserialize<T>(string xml) {

            if(string.IsNullOrEmpty(xml)) {
                return default(T);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlReaderSettings settings = new XmlReaderSettings();
            // No settings need modifying here

            using(StringReader textReader = new StringReader(xml)) {
                using(XmlReader xmlReader = XmlReader.Create(textReader, settings)) {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }
        #endregion

        #region private member 
        /// <summary>
        /// Returns byte array.
        /// </summary>
        /// <param name="str">XML String.</param>
        /// <returns>byte[].</returns>
        private static byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Returns string.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        /// <returns>string of characters.</returns>
        private static string GetString(byte[] bytes) {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        #endregion
    }
}

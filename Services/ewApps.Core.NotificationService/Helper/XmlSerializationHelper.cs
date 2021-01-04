using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ewApps.Core.NotificationService {
    /// <summary>
    /// This class provides helper methods to serialize Dictionary objects to the xml string.
    /// </summary>
    public static class XmlSerializationHelper {

        /// <summary>Serializes the input dictionary into xml. Each dictionary item's key is used as xml tag and value as xml tag value.</summary>
        /// <param name="itemList">Dictionary to be serialized into xml string.</param>
        /// <param name="rootElementName">Serialized xml rool element name. If empty use EventDataList as root element name.</param>
        public static string ToXML(Dictionary<string, object> itemList, string rootElementName = "EventDataList") {
            XmlDocument xml = new XmlDocument();
            //xml.PreserveWhitespace = true;
            // Create an XML declaration. 
            XmlDeclaration xmldecl;
            xmldecl = xml.CreateXmlDeclaration("1.0", "utf-16", "yes");

            // Add the new node to the document.
            XmlElement root = xml.DocumentElement;
            xml.InsertBefore(xmldecl, root);


            XmlElement rootElement = xml.CreateElement(rootElementName);

            foreach(KeyValuePair<string, object> item in itemList) {
                if(item.Value == null) {
                    XmlElement itemElement = xml.CreateElement(item.Key);
                    itemElement.InnerText = Convert.ToString(item.Value);
                    rootElement.AppendChild(itemElement);
                }
                else if(item.Value.GetType().Name.Equals("Dictionary`2")) {
                    if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("Int32")) {
                        AppendChildElement<string, int>((Dictionary<string, int>)item.Value, xml, ref rootElement, item.Key);
                    }
                    else if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("String")) {
                        AppendChildElement<string, string>((Dictionary<string, string>)item.Value, xml, ref rootElement, item.Key);
                    }
                    else if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("Object")) {
                        AppendChild((item.Value as Dictionary<string, object>), xml, ref rootElement, item.Key);
                    }
                }
                else if(item.Value.GetType().Name.Equals("List`1")) {
                    if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("Dictionary`2")) {
                        List<Dictionary<string, string>> dictList = (List<Dictionary<string, string>>)item.Value;
                        for(int i = 0; i < dictList.Count; i++) {
                            AppendChildElement<string, string>((Dictionary<string, string>)dictList[i], xml, ref rootElement, item.Key);
                        }
                    }
                }
                else if(item.Value.GetType().Name.Equals("String")) {
                    XmlElement itemElement = xml.CreateElement(item.Key);
                    itemElement.InnerText = Convert.ToString(item.Value);
                    rootElement.AppendChild(itemElement);
                }
            }
            xml.AppendChild(rootElement);
            return xml.OuterXml;
        }


        public static void AppendChild(Dictionary<string, object> itemList, XmlDocument xml, ref XmlElement rootElement, string currentRootElementName) {

            XmlElement listStartElement = xml.CreateElement(currentRootElementName);
            foreach(KeyValuePair<string, object> item in itemList) {
                if(item.Value.GetType().Name.Equals("Dictionary`2")) {
                    if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("Int32")) {
                        AppendChildElement<string, int>((Dictionary<string, int>)item.Value, xml, ref listStartElement, item.Key);
                    }
                    else if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("String")) {
                        AppendChildElement<string, string>((Dictionary<string, string>)item.Value, xml, ref listStartElement, item.Key);
                    }
                    else if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("Object")) {
                        AppendChild((item.Value as Dictionary<string, object>), xml, ref listStartElement, item.Key);
                    }
                    else if(item.Value.GetType().Name.Equals("List`1")) {
                        if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("Dictionary`2")) {
                            List<Dictionary<string, string>> dictList = (List<Dictionary<string, string>>)item.Value;
                            for(int i = 0; i < dictList.Count; i++) {
                                AppendChildElement<string, string>((Dictionary<string, string>)dictList[i], xml, ref rootElement, item.Key);
                            }
                        }
                    }
                }
                else if(item.Value.GetType().Name.Equals("String")) {
                    XmlElement itemElement = xml.CreateElement(item.Key);
                    itemElement.InnerText = Convert.ToString(item.Value);
                    listStartElement.AppendChild(itemElement);
                }
                rootElement.AppendChild(listStartElement);
            }
        }

        /// <summary>Serializes the input dictionary into xml. Each dictionary item's key is used as xml tag and value as xml tag value.</summary>
        /// <param name="itemList">Dictionary to be serialized into xml string.</param>
        /// <param name="rootElementName">Serialized xml rool element name. If empty use EventDataList as root element name.</param>
        public static string ToXML(Dictionary<string, string> itemList, string rootElementName = "EventDataList") {
            XmlDocument xml = new XmlDocument();
            //xml.PreserveWhitespace = true;
            // Create an XML declaration. 
            XmlDeclaration xmldecl;
            xmldecl = xml.CreateXmlDeclaration("1.0", "utf-16", "yes");

            // Add the new node to the document.
            XmlElement root = xml.DocumentElement;
            xml.InsertBefore(xmldecl, root);

            //if (string.IsNullOrEmpty(rootElementName)) {
            //  rootElementName = "EventDataList";
            //}

            XmlElement rootElement = xml.CreateElement(rootElementName);

            foreach(KeyValuePair<string, string> item in itemList) {
                XmlElement itemElement = xml.CreateElement(Convert.ToString(item.Key));
                itemElement.InnerText = Convert.ToString(item.Value);
                rootElement.AppendChild(itemElement);
                //Console.WriteLine(item.Value.GetType().Name);
            }

            //AppendChildElement(itemList, xml, ref  rootElement);
            xml.AppendChild(rootElement);
            return xml.OuterXml;
        }

        // Append child element into root element to complete xml string.
        private static void AppendChildElement<T, U>(Dictionary<T, U> childItemList, XmlDocument xml, ref XmlElement rootElement, string currentRootElementName) {
            XmlElement listStartElement = xml.CreateElement(currentRootElementName);

            foreach(KeyValuePair<T, U> childItem in childItemList) {
                XmlElement itemElement = xml.CreateElement(Convert.ToString(childItem.Key));
                itemElement.InnerText = Convert.ToString(childItem.Value);
                listStartElement.AppendChild(itemElement);
            }

            rootElement.AppendChild(listStartElement);
        }

    }
}



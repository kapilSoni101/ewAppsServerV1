using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace ewApps.Core.CommonService {
    public class DictionaryXMLSerializHelper {

        public static string SampleDictionary() {
            //  Create notification information
            Dictionary<string, object> eventData = new Dictionary<string, object>();

            Dictionary<string, string> shippedFromAddress = new Dictionary<string, string>();
            shippedFromAddress.Add("Address1", "Gali");


            Dictionary<string, string> shippedToAddress = new Dictionary<string, string>();
            shippedToAddress.Add("Address1", "Gali1");

            List<Dictionary<string, object>> packageList = new List<Dictionary<string, object>>();
            Dictionary<string, object> package = new Dictionary<string, object>();
            package.Add("ElementName", "Package");
            package.Add("PackageType", "Box");
            package.Add("PackageClass", "Standard Box");

            List<Dictionary<string, string>> itemList = new List<Dictionary<string, string>>();

            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("ElementName", "Item");
            item.Add("ItemCode", "I001");
            item.Add("ItemName", "ABC");
            itemList.Add(item);
            item = new Dictionary<string, string>();
            item.Add("ElementName", "Item");
            item.Add("ItemCode", "I002");
            item.Add("ItemName", "ZXY");
            itemList.Add(item);

            package.Add("Items", itemList);

            packageList.Add(package);

            package = new Dictionary<string, object>();
            package.Add("ElementName", "Package");
            package.Add("PackageType", "Bottle");
            package.Add("PackageClass", "Standard Bottle");


            List<Dictionary<string, string>> itemList1 = new List<Dictionary<string, string>>();

            item = new Dictionary<string, string>();
            item.Add("ElementName", "Item");
            item.Add("ItemCode", "I001");
            item.Add("ItemName", "ABC");
            itemList.Add(item);
            item = new Dictionary<string, string>();

            item.Add("ElementName", "Item");
            item.Add("ItemCode", "I002");
            item.Add("ItemName", "ZXY");
            itemList1.Add(item);

            package.Add("Items", itemList1);

            packageList.Add(package);

            eventData.Add("ShippedToAddress", shippedToAddress);
            eventData.Add("ShippedFromAddress", shippedFromAddress);
            eventData.Add("CustomerName", "Bizchat");
            eventData.Add("BusinessName", "opti");
            eventData.Add("PublisherName", "Publisher_Name");
            eventData.Add("DeliveryID", "D0001");
            eventData.Add("TrackingID", "T0001");
            eventData.Add("CarrierServiceName", "UPS");
            eventData.Add("CarrierServiceType", "UPS Carrier");
            eventData.Add("DeliveryInitiatedDateTime", "10-01-2019 05:00");
            eventData.Add("SalesOrderID", "SO-01");
            eventData.Add("SubTotal", "5000");
            eventData.Add("FreightCharges", "2000");
            eventData.Add("Total", "7000");

            eventData.Add("Packages", packageList);

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            string xml = ToXml<string, object>(eventData, "EventData");
            return xml;
        }



        public static string ToXml<Type1, Type2>(Dictionary<Type1, Type2> sourceDictionary, string rootElementName) {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xml.CreateXmlDeclaration("1.0", "utf-16", "yes");

            // Add the new node to the document.
            XmlElement root = xml.DocumentElement;
            xml.InsertBefore(xmldecl, root);
            XmlElement rootElement = xml.CreateElement(rootElementName);
            xml.AppendChild(rootElement);

            XmlElement lastRootElement = rootElement;

            SerializeDictionary(sourceDictionary, lastRootElement);

            return xml.InnerXml;
        }

        private static void SerializeDictionary<Type1, Type2>(Dictionary<Type1, Type2> sourceDictionary, XmlElement lastRootElement) {
            foreach(KeyValuePair<Type1, Type2> item in sourceDictionary) {
                XmlElement itemElement = lastRootElement.OwnerDocument.CreateElement(item.Key.ToString());

                if(item.Value.GetType().Name.Equals("String")) {
                    //  XmlElement itemElement = lastRootElement.OwnerDocument.CreateElement(item.Key.ToString());
                    itemElement.InnerText = Convert.ToString(item.Value);
                    //lastRootElement.AppendChild(itemElement);
                }
                else if(item.Value.GetType().Name.Equals("Dictionary`2")) {

                    if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("String")) {
                        ToXml(item.Value as Dictionary<string, string>, itemElement);
                    }
                    else if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("Int32")) {
                        ToXml(item.Value as Dictionary<string, int>, itemElement);
                    }
                    else if(item.Value.GetType().GetGenericArguments()[0].Name.Equals("String") && item.Value.GetType().GetGenericArguments()[1].Name.Equals("Dictionary`2")) {
                        SerializeDictionary<string, object>(item.Value as Dictionary<string, object>, itemElement);
                    }
                    //lastRootElement.AppendChild(itemElement);
                }
                else if(item.Value.GetType().Name.Equals("List`1")) {
                    if(item.Value is List<Dictionary<string, string>>) {
                        SerializeDictionaryList<string, string>(item.Value as List<Dictionary<string, string>>, itemElement);
                    }
                    else if(item.Value is List<Dictionary<string, object>>) {
                        SerializeDictionaryList<string, object>(item.Value as List<Dictionary<string, object>>, itemElement);
                    }
                }
                lastRootElement.AppendChild(itemElement);
            }
        }

        private static XmlElement ToXml(Dictionary<string, string> sourceDictionary, XmlElement previousElement) {
            foreach(KeyValuePair<string, string> childItem in sourceDictionary) {
                XmlElement itemElement = previousElement.OwnerDocument.CreateElement(Convert.ToString(childItem.Key));
                itemElement.InnerText = Convert.ToString(childItem.Value);
                previousElement.AppendChild(itemElement);
            }

            return previousElement;
        }

        private static XmlElement ToXml(Dictionary<string, int> sourceDictionary, XmlElement previousElement) {
            foreach(KeyValuePair<string, int> childItem in sourceDictionary) {
                XmlElement itemElement = previousElement.OwnerDocument.CreateElement(childItem.Key);
                itemElement.InnerText = Convert.ToString(childItem.Value);
                previousElement.AppendChild(itemElement);
            }
            return previousElement;
        }

        private static void SerializeDictionaryList<Type1, Type2>(List<Dictionary<Type1, Type2>> sourceDictionaryList, XmlElement previousElement) {

            if(sourceDictionaryList is List<Dictionary<string, string>>) {
                List<Dictionary<string, string>> dictList = sourceDictionaryList as List<Dictionary<string, string>>;

                for(int i = 0; i < dictList.Count; i++) {
                    XmlElement childItemElement = previousElement.OwnerDocument.CreateElement(dictList[0]["ElementName"]);
                    ToXml(dictList[i], childItemElement);
                    previousElement.AppendChild(childItemElement);
                }

            }
            //else if(item.Value is List<Dictionary<string, int>>) {
            //    List<Dictionary<string, int>> dictList = item.Value as List<Dictionary<string, int>>;
            //    for(int i = 0; i < dictList.Count; i++) {
            //        XmlElement childItemElement = itemElement.OwnerDocument.CreateElement(dictList[0]["ElementName"]);
            //        ToXml(dictList[i], itemElement);
            //        itemElement.AppendChild(childItemElement);
            //    }
            //}
            else if(sourceDictionaryList is List<Dictionary<string, object>>) {
                List<Dictionary<string, object>> dictList = sourceDictionaryList as List<Dictionary<string, object>>;
                for(int i = 0; i < dictList.Count; i++) {
                    XmlElement childItemElement = previousElement.OwnerDocument.CreateElement(Convert.ToString(dictList[0]["ElementName"]));
                    SerializeDictionary<string, object>(dictList[i], childItemElement);
                    previousElement.AppendChild(childItemElement);
                }
            }
        }

    }
}

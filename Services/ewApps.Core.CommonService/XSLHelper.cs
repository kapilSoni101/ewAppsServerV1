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
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ewApps.Core.CommonService {/// <summary>
                                     /// It provides helper methods for XSL transformation operations.
                                     /// </summary>
    public class XSLHelper {

        #region public methods 
        /// <summary>
        /// Generates required contents by transforming xsl template.
        /// </summary>
        /// <param name="xsltText">XSL string to be transform.</param>
        /// <param name="xmlDoc">XML information used in XSL transformation.</param>
        /// <param name="title">The title.</param>
        /// <param name="xsltParam">Parameter collection used in XSL transformation.</param>
        /// <returns>
        /// Transformed content in form string.
        /// </returns>
        public static string XSLTransform(string xsltText, XmlDocument xmlDoc, out string title, IDictionary<string, string> xsltParam = null) {

            string bodytext;
            title = string.Empty;

            XslCompiledTransform xslt = new XslCompiledTransform(false);
            XsltSettings settings = new XsltSettings(true, true);

            XmlUrlResolver urlResolver = new XmlUrlResolver();
            xslt.Load(new XmlTextReader(new StringReader(xsltText)), settings, urlResolver);

            XPathNavigator xpathnav = xmlDoc.CreateNavigator();

            StringBuilder emailbuilder = new StringBuilder();
            //XmlTextWriter xmlwriter = new XmlTextWriter(new System.IO.StringWriter(emailbuilder));
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
            xmlWriterSetting.NewLineHandling = NewLineHandling.None;
            xmlWriterSetting.Indent = true;
            xmlWriterSetting.ConformanceLevel = ConformanceLevel.Auto;


            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Add additional parameters to xsl arugument list.
                foreach(KeyValuePair<string, string> param in xsltParam) {

                    if(string.IsNullOrEmpty(param.Value) == false) {
                        xslarg.AddParam(param.Key, "", param.Value);
                    }

                }
            }

            using(XmlWriter writer = XmlWriter.Create(new System.IO.StringWriter(emailbuilder), xmlWriterSetting)) {
                xslt.Transform(xpathnav, xslarg, writer, null);
            }

            XmlDocument xemaildoc = new XmlDocument();

            xemaildoc.LoadXml(emailbuilder.ToString());

            if(xemaildoc.SelectSingleNode("//title") != null) {
                title = xemaildoc.SelectSingleNode("//title").InnerText;
            }

            bodytext = emailbuilder.ToString();

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&amp;", "&");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&gt;", ">");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&lt;", "<");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("%3a;", ":");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("%2f;", "/");
            }
            return bodytext;

        }

        /// <summary>
        /// Generates required contents by transforming xsl template.
        /// </summary>
        /// <param name="xslDocumentPath">XSL template document path.</param>
        ///  <param name="title">Title generated in XSL transformation.</param>
        /// <param name="xsltParam">Parameter collection used in XSL transformation.</param>
        /// <returns>Transformed content in form string.</returns>
        public static string XSLTransform(string xslDocumentPath, out string title, IDictionary<string, string> xsltParam = null) {

            string subjecttext = default(string), bodytext = default(string);
            title = "";

            XslCompiledTransform objxslt = new XslCompiledTransform(false);
            objxslt.Load(xslDocumentPath);

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.AppendChild(xmldoc.CreateElement("DocumentRoot"));
            XPathNavigator xpathnav = xmldoc.CreateNavigator();

            StringBuilder emailbuilder = new StringBuilder();


            XsltArgumentList xslarg = new XsltArgumentList();

            // Pass the indivisual variable value as a input parameter
            foreach(KeyValuePair<string, string> item in xsltParam) {
                xslarg.AddParam(item.Key, "", item.Value);
            }

            using(XmlTextWriter xmlwriter = new XmlTextWriter(new System.IO.StringWriter(emailbuilder))) {
                objxslt.Transform(xpathnav, xslarg, xmlwriter, null);
            }


            XmlDocument xemaildoc = new XmlDocument();
            xemaildoc.LoadXml(emailbuilder.ToString());
            XmlNode titlenode = xemaildoc.SelectSingleNode("//title");

            subjecttext = titlenode.InnerText;

            // Replace body to html.
            XmlNode bodynode = xemaildoc.SelectSingleNode("//html");

            bodytext = bodynode.InnerXml;

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("&amp;", "&");
            //  bodytext = bodytext.Replace("&gt;", ">");
            //  bodytext = bodytext.Replace("&lt;", "<");
            //}

            title = subjecttext;

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&lt;br&gt;", "<br>");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&amp;nbsp;", "&nbsp;");
            }

            return bodytext;

        }

        /// <summary>
        /// XSLs the transform.
        /// </summary>
        /// <param name="xsltText">The XSLT text.</param>
        /// <param name="xmlText">The XML text.</param>
        /// <param name="title">The title.</param>
        /// <param name="xsltParam">The XSLT parameter.</param>
        /// <returns>Returns a string type XSLTransform</returns>
        public static string XSLTransform(string xsltText, string xmlText, out string title, IDictionary<string, string> xsltParam = null) {

            string bodytext;

            // if input xml is blank update with dummy xml string.
            if(string.IsNullOrEmpty(xmlText)) {
                xmlText = "<?xml version='1.0' encoding='utf-8' ?><Test></Test>";
            }

            // Prepare input-xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);

            bodytext = XSLTransform(xsltText, doc, out title, xsltParam);

            return bodytext;

        }


        /// <summary>
        /// XSLs the transform using XML string.
        /// </summary>
        /// <param name="xsltText">The XSLT text.</param>
        /// <param name="xmlText">The XML text.</param>
        /// <param name="title">The title.</param>
        /// <param name="xsltParam">The XSLT parameter.</param>
        /// <returns>Returns a srting type value</returns>
        public static string XSLTransformUsingXMLString(string xsltText, string xmlText, out string title, IDictionary<string, string> xsltParam = null) {

            string bodytext;

            // if input xml is blank update with dummy xml string.
            if(string.IsNullOrEmpty(xmlText)) {
                xmlText = "<?xml version='1.0' encoding='utf-8' ?><Test></Test>";
            }


            XslCompiledTransform xslt = new XslCompiledTransform(false);
            XsltSettings settings = new XsltSettings(true, true);

            XmlUrlResolver urlResolver = new XmlUrlResolver();
            xslt.Load(new XmlTextReader(new StringReader(xsltText)), settings, urlResolver);

            // Prepare input-xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);


            StringBuilder emailbuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
            xmlWriterSetting.NewLineHandling = NewLineHandling.None;
            xmlWriterSetting.Indent = true;
            xmlWriterSetting.ConformanceLevel = ConformanceLevel.Auto;


            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Add additional parameters to xsl arugument list.
                foreach(KeyValuePair<string, string> param in xsltParam) {
                    xslarg.AddParam(param.Key, "", param.Value);
                }
            }
            using(XmlWriter writer = XmlWriter.Create(new StringWriter(emailbuilder), xmlWriterSetting)) {
                xslt.Transform(doc, xslarg, writer, null);
            }

            XmlDocument xemaildoc = new XmlDocument();
            xemaildoc.LoadXml(emailbuilder.ToString());
            XmlNode titlenode = xemaildoc.SelectSingleNode("//title");
            title = titlenode.InnerText;

            //XmlNode bodynode = xemaildoc.SelectSingleNode("//body");

            bodytext = emailbuilder.ToString();

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&amp;", "&");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&gt;", ">");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&lt;", "<");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("%3a;", ":");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("%2f;", "/");
            }
            return bodytext;

        }

        /// <summary>
        /// Generates required contents by transforming xsl template.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <param name="xslDocumentPath">XSL template document path.</param>
        /// <param name="title">Title generated in XSL transformation.</param>
        /// <param name="xsltParam">Parameter collection used in XSL transformation.</param>
        /// <returns>
        /// Transformed content in form string.
        /// </returns>
        public static string XSLTransform(XmlDocument xmlDoc, string xslDocumentPath, out string title, IDictionary<string, string> xsltParam = null) {

            string subjecttext = default(string), bodytext = default(string);
            title = "";

            XslCompiledTransform objxslt = new XslCompiledTransform(false);
            objxslt.Load(xslDocumentPath);

            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.AppendChild(xmldoc.CreateElement("DocumentRoot"));
            XPathNavigator xpathnav = xmlDoc.CreateNavigator();

            StringBuilder emailbuilder = new StringBuilder();


            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Pass the indivisual variable value as a input parameter
                foreach(KeyValuePair<string, string> item in xsltParam) {
                    xslarg.AddParam(item.Key, "", item.Value);
                }
            }

            using(XmlTextWriter xmlwriter = new XmlTextWriter(new StringWriter(emailbuilder))) {
                objxslt.Transform(xpathnav, xslarg, xmlwriter, null);
            }
            XmlDocument xemaildoc = new XmlDocument();
            xemaildoc.LoadXml(emailbuilder.ToString());
            XmlNode titlenode = xemaildoc.SelectSingleNode("//title");

            subjecttext = titlenode.InnerText;

            // Replace body to html.
            XmlNode bodynode = xemaildoc.SelectSingleNode("//html");

            bodytext = bodynode.InnerXml;

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("&amp;", "&");
            //  bodytext = bodytext.Replace("&gt;", ">");
            //  bodytext = bodytext.Replace("&lt;", "<");
            //}

            title = subjecttext;

            return bodytext;

        }

        /// <summary>
        /// XSLs the transform to plain text.
        /// </summary>
        /// <param name="xsltText">The XSLT text.</param>
        /// <param name="xsltParam">The XSLT parameter.</param>
        /// <returns>Returns astring type value</returns>
        public static string XSLTransformToPlainText(string xsltText, IDictionary<string, string> xsltParam = null) {

            // Creates with dummy xml string.
            string xmlText = "<?xml version='1.0' encoding='utf-8' ?><Test></Test>";

            // Prepare input-xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);

            XslCompiledTransform xslt = new XslCompiledTransform(false);
            XsltSettings settings = new XsltSettings(true, true);

            XmlUrlResolver urlResolver = new XmlUrlResolver();
            xslt.Load(new XmlTextReader(new StringReader(xsltText)), settings, urlResolver);

            StringBuilder emailbuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
            xmlWriterSetting.NewLineHandling = NewLineHandling.None;
            xmlWriterSetting.Indent = true;
            xmlWriterSetting.ConformanceLevel = ConformanceLevel.Auto;


            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Add additional parameters to xsl arugument list.
                foreach(KeyValuePair<string, string> param in xsltParam) {
                    xslarg.AddParam(param.Key, "", param.Value);
                }
            }

            using(XmlWriter writer = XmlWriter.Create(new StringWriter(emailbuilder), xmlWriterSetting)) {
                xslt.Transform(doc, xslarg, writer, null);
            }

            return emailbuilder.ToString();


        }

        /// <summary>
        /// XSLs the transform by XML string and XSL path.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        /// <param name="xslDocumentPath">The XSL document path.</param>
        /// <param name="title">The title.</param>
        /// <param name="xsltParam">The XSLT parameter.</param>
        /// <returns></returns>
        public static string XSLTransformByXmlStringAndXSLPath(string xmlString, string xslDocumentPath, out string title, IDictionary<string, string> xsltParam = null) {

            string subjecttext = default(string), bodytext = default(string);
            title = string.Empty;

            XslCompiledTransform objxslt = new XslCompiledTransform(false);
            objxslt.Load(xslDocumentPath);

            // Prepare input-xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.AppendChild(xmldoc.CreateElement("DocumentRoot"));
            XPathNavigator xpathnav = xmlDoc.CreateNavigator();

            StringBuilder emailbuilder = new StringBuilder();


            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Pass the indivisual variable value as a input parameter
                foreach(KeyValuePair<string, string> item in xsltParam) {
                    if(string.IsNullOrEmpty(item.Value) == false) {
                        xslarg.AddParam(item.Key, "", Convert.ToString(item.Value));
                    }
                }
            }

            using(XmlTextWriter xmlwriter = new XmlTextWriter(new StringWriter(emailbuilder))) {
                objxslt.Transform(xpathnav, xslarg, xmlwriter, null);
            }
            XmlDocument xemaildoc = new XmlDocument();
            xemaildoc.LoadXml(emailbuilder.ToString());
            XmlNode titlenode = xemaildoc.SelectSingleNode("//title");

            subjecttext = titlenode.InnerText;

            // Replace body to html.
            XmlNode bodynode = xemaildoc.SelectSingleNode("//html");

            bodytext = bodynode.InnerXml;

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("&amp;", "&");
            //  bodytext = bodytext.Replace("&gt;", ">");
            //  bodytext = bodytext.Replace("&lt;", "<");
            //}

            title = subjecttext;

            return bodytext;

        }

        // Transform given XSLT in context of given set of arguments and xml document and 
        // Retrieves the transformed content from the xslt template.


        /// <summary>
        /// Generates required contents by transforming xsl template based on given xml, xsl and tags.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        /// <param name="xslDocumentPath">The XSL document path.</param>
        /// <param name="titleTag">The title tag.</param>
        /// <param name="bodyTag">The body tag.</param>
        /// <param name="title">The title.</param>
        /// <param name="xsltParam">The XSLT parameter.</param>
        /// <returns>Transformed content in form string.</returns>
        public static string XSLTransformByXmlAndTags(string xmlString, string xslDocumentPath, string titleTag, string bodyTag, out string title, IDictionary<string, string> xsltParam = null) {

            XslCompiledTransform objxslt = new XslCompiledTransform(false);
            objxslt.Load(xslDocumentPath);

            // Prepare input-xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.AppendChild(xmldoc.CreateElement("DocumentRoot"));
            XPathNavigator xpathnav = xmlDoc.CreateNavigator();

            StringBuilder emailbuilder = new StringBuilder();


            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Pass the indivisual variable value as a input parameter
                foreach(KeyValuePair<string, string> item in xsltParam) {
                    if(string.IsNullOrEmpty(item.Value) == false) {
                        xslarg.AddParam(item.Key, "", Convert.ToString(item.Value));
                    }
                }
            }

            using(XmlTextWriter xmlwriter = new XmlTextWriter(new StringWriter(emailbuilder))) {
                objxslt.Transform(xpathnav, xslarg, xmlwriter, null);
            }


            XmlDocument xemaildoc = new XmlDocument();
            xemaildoc.LoadXml(emailbuilder.ToString());
            XmlNode titlenode = xemaildoc.SelectSingleNode("//" + titleTag);

            title = string.Empty;
            title = titlenode.InnerText;

            // Replace body to html.
            XmlNode bodynode = xemaildoc.SelectSingleNode("//" + bodyTag);

            string bodytext = string.Empty;

            bodytext = bodynode.InnerXml;

            return bodytext;

        }


        public static string XSLTransformByXslAndXmlString(string xsltText, string xmlText, string titleTag, string bodyTag, out string title, IDictionary<string, string> xsltParam = null) {

            string bodytext;
            title = string.Empty;

            // if input xml is blank update with dummy xml string.
            if(string.IsNullOrEmpty(xmlText)) {
                xmlText = "<?xml version='1.0' encoding='utf-8' ?><Test></Test>";
            }

            // Prepare input-xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);

            XslCompiledTransform xslt = new XslCompiledTransform(false);
            XsltSettings settings = new XsltSettings(true, true);

            XmlUrlResolver urlResolver = new XmlUrlResolver();
            xslt.Load(new XmlTextReader(new StringReader(xsltText)), settings, urlResolver);

            XPathNavigator xpathnav = doc.CreateNavigator();

            StringBuilder emailbuilder = new StringBuilder();
            //XmlTextWriter xmlwriter = new XmlTextWriter(new System.IO.StringWriter(emailbuilder));
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
            xmlWriterSetting.NewLineHandling = NewLineHandling.None;
            xmlWriterSetting.Indent = true;
            xmlWriterSetting.ConformanceLevel = ConformanceLevel.Auto;

            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Add additional parameters to xsl arugument list.
                foreach(KeyValuePair<string, string> param in xsltParam) {
                    if(string.IsNullOrEmpty(param.Value) == false) {
                        xslarg.AddParam(param.Key, "", param.Value);
                    }
                }
            }

            using(XmlWriter writer = XmlWriter.Create(new System.IO.StringWriter(emailbuilder), xmlWriterSetting)) {
                xslt.Transform(xpathnav, xslarg, writer, null);
            }
            XmlDocument xemaildoc = new XmlDocument();
            xemaildoc.LoadXml(emailbuilder.ToString());
            XmlNode titlenode = xemaildoc.SelectSingleNode("//" + titleTag);

            title = string.Empty;
            title = titlenode.InnerText;

            // Replace body to html.
            XmlNode bodynode = xemaildoc.SelectSingleNode("//" + bodyTag);

            bodytext = bodynode.InnerXml;

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&lt;br&gt;", "<br>");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&amp;nbsp;", "&nbsp;");
            }

            return bodytext;

        }



        /// <summary>
        /// XSLs the transform by string and tags.
        /// </summary>
        /// <param name="xsltText">The XSLT text.</param>
        /// <param name="xmlText">The XML text.</param>
        /// <param name="titleTag">The title tag.</param>
        /// <param name="bodyTag">The body tag.</param>
        /// <param name="title">The title.</param>
        /// <param name="xsltParam">The XSLT parameter.</param>
        /// <returns></returns>
        public static string XSLTransformByStringAndTags(string xsltText, string xmlText, string titleTag, string bodyTag, out string title, IDictionary<string, string> xsltParam = null) {
            string bodytext;

            // if input xml is blank update with dummy xml string.
            if(string.IsNullOrEmpty(xmlText)) {
                xmlText = "<?xml version='1.0' encoding='utf-8' ?><Test></Test>";
            }


            XslCompiledTransform xslt = new XslCompiledTransform(false);
            XsltSettings settings = new XsltSettings(true, true);

            XmlUrlResolver urlResolver = new XmlUrlResolver();
            xslt.Load(new XmlTextReader(new StringReader(xsltText)), settings, urlResolver);

            // Prepare input-xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);


            StringBuilder emailbuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
            xmlWriterSetting.NewLineHandling = NewLineHandling.None;
            xmlWriterSetting.Indent = true;
            xmlWriterSetting.ConformanceLevel = ConformanceLevel.Auto;


            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Add additional parameters to xsl arugument list.
                foreach(KeyValuePair<string, string> param in xsltParam) {
                    xslarg.AddParam(param.Key, "", param.Value);
                }
            }

            using(XmlWriter writer = XmlWriter.Create(new StringWriter(emailbuilder), xmlWriterSetting)) {
                xslt.Transform(doc, xslarg, writer, null);
            }


            XmlDocument xemaildoc = new XmlDocument();
            xemaildoc.LoadXml(emailbuilder.ToString());
            XmlNode titlenode = xemaildoc.SelectSingleNode("//" + titleTag);
            title = titlenode.InnerText;

            //XmlNode bodynode = xemaildoc.SelectSingleNode("//body");

            // bodytext = emailbuilder.ToString();

            // Replace body to html.
            XmlNode bodynode = xemaildoc.SelectSingleNode("//" + bodyTag);

            bodytext = bodynode.InnerXml;

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("&amp;", "&");
            //}

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("&gt;", ">");
            //}

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("&lt;", "<");
            //}

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("%3a;", ":");
            //}

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("%2f;", "/");
            //}

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&lt;br&gt;", "<br>");
            }

            if(bodytext.Length > 0) {
                bodytext = bodytext.Replace("&amp;nbsp", "&nbsp");
            }

            //if (bodytext.Length > 0) {
            //  bodytext = bodytext.Replace("&amp;nbsp", "&nbsp");
            //}

            return bodytext;

        }

        #endregion



        public static string XSLTransformByXslAndXmlStringToPlainText(string xsltText, string xmlText,  IDictionary<string, string> xsltParam = null) {

            // if input xml is blank update with dummy xml string.
            if(string.IsNullOrEmpty(xmlText)) {
                xmlText = "<?xml version='1.0' encoding='utf-8' ?><Test></Test>";
            }

            // Prepare input-xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);

            XslCompiledTransform xslt = new XslCompiledTransform(false);
            XsltSettings settings = new XsltSettings(true, true);

            XmlUrlResolver urlResolver = new XmlUrlResolver();
            xslt.Load(new XmlTextReader(new StringReader(xsltText)), settings, urlResolver);

            XPathNavigator xpathnav = doc.CreateNavigator();

            StringBuilder emailbuilder = new StringBuilder();
            //XmlTextWriter xmlwriter = new XmlTextWriter(new System.IO.StringWriter(emailbuilder));
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
            xmlWriterSetting.NewLineHandling = NewLineHandling.None;
            xmlWriterSetting.Indent = true;
            xmlWriterSetting.ConformanceLevel = ConformanceLevel.Auto;

            XsltArgumentList xslarg = new XsltArgumentList();

            if(xsltParam != null) {
                // Add additional parameters to xsl arugument list.
                foreach(KeyValuePair<string, string> param in xsltParam) {
                    if(string.IsNullOrEmpty(param.Value) == false) {
                        xslarg.AddParam(param.Key, "", param.Value);
                    }
                }
            }

            using(XmlWriter writer = XmlWriter.Create(new System.IO.StringWriter(emailbuilder), xmlWriterSetting)) {
                xslt.Transform(xpathnav, xslarg, writer, null);
            }

            return emailbuilder.ToString();
        }
    }
}

<?xml version="1.0" encoding="utf-8"?>


<!--
Subject – <Publisher Co. Name> Business Portal: A/R Invoice(s) data is updated. 
Dear <Business Co. Name>,  
A/R Invoice data with below details is updated on <date & time>: 
New Invoice(s): <count> Updated Invoice(s): < count> 
Updated By: <Business User Name>, 
User ID: <user id>. Please login to the portal to view more details. Your portal details are as below: Sub Domain: <Sub domain> Portal URL: <Business Portal - Payment URL> Regards  Publisher Co. Name> <Copyright text set at the Publisher> You received this email because you are subscribed to Portal Alerts from <Business Co. Name>. Update your email preferences to choose the types of emails you receive.
-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>

  <xsl:param name="newInvoiceCount"/>
  <xsl:param name="updatedInvoiceCount"/>
  <xsl:param name="updatedByName"/>
  <xsl:param name="updatedByNo"/>
  <xsl:param name="updatedOn"/>

  <xsl:param name="publisherName"/>
  <xsl:param name="businessName"/>
  <xsl:param name="subDomain"/>
  <xsl:param name="portalUrl"/>
  <xsl:param name="copyright"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>
          <!--HTML document title-->
          <!--– <Publisher Co. Name> Business Portal: A/R Invoice(s) data is updated.-->
          <xsl:value-of select="$publisherName"/>
          <xsl:text> Business Portal: A/R Invoice(s) data is updated.</xsl:text>
        </title>
        <style type="text/css">
          .a5q {display: none !important;} .a6S {display: none !important;}
        </style>
        <meta name="x-apple-disable-message-reformatting"/>
      </head>

      <body bgcolor="#fff" leftmargin="0" marginwidth="0" topmargin="0" marginheight="0" offset="0" style="background:#fff; -webkit-text-size-adjust: 100% !important;-ms-text-size-adjust: 100% !important; min-height:620px; ">
        <!--PreHeader Text Start-->
        <div style="display: none !important; max-height: 0px; font-size: 0px;   mso-hide: all !important; position: absolute;">
          <div border="0" cellpadding="0" cellspacing="0" height="0" width="500px" style="display:none !important;
           
           mso-hide:all !important;
           font-size:1px;
           color:#E1E1E1;
           line-height:1px;
           max-height:0px;
           max-width:0px;
           opacity:0;height:0px;
            white-space: nowrap;  position: absolute;" >
            <div>
              <span align="center"   valign="top" width="400px;" colspan="0" style="line-height:0px;display:none !important; mso-hide:all;color:#E1E1E1;font-size:0px;height:0px; position: absolute;">
                <div style="height:0px;margin: 3px 45px 0 0;width: 400px;white-space: nowrap; text-overflow: ellipsis;display:none !important;
             position: absolute;">
                  <xsl:text>Dear </xsl:text>
                  <xsl:value-of select="$businessName"/>
                  <xsl:text>, </xsl:text>
                  <xsl:text> A/R Invoice data with below details is updated on </xsl:text>
                  <xsl:value-of select="$updatedOn"/>
                  <xsl:text>: New Invoice(s): </xsl:text>
                  <xsl:value-of select="$newInvoiceCount"/>
                  <xsl:text> Updated Invoice(s): </xsl:text>
                  <xsl:value-of select="$updatedInvoiceCount"/>
                  <xsl:text> Updated By: </xsl:text>
                  <xsl:value-of select="$updatedByName"/>
                  <xsl:text> , User ID: </xsl:text>
                  <xsl:value-of select="$updatedByNo"/>
                  <xsl:text> Please login to the portal to view more details. Your portal details are as below: Sub Domain: </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                    <xsl:text> </xsl:text>

                  <!--<xsl:text> Portal URL: </xsl:text>
                  <xsl:value-of select="$portalUrl"/>-->
                  <xsl:text> Regards, </xsl:text>
                  <xsl:value-of select="$publisherName"/>
                </div>
              </span>
            </div>
          </div>
        </div>
        <!--PreHeader Text End-->

        <!--Main Body Start-->
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td valign="top" align="center" class="textContent">
              <div style="text-align:left;font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;margin-bottom:0;color:#000;line-height:19px;">
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;word-break: break-all;word-break: break-word; margin-top:5px;">
                  <xsl:text>Dear </xsl:text>
                  <b>
                    <xsl:value-of select="$businessName"/>
                  </b>
                  <xsl:text>,</xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> A/R Invoice data with below details is updated on </xsl:text>
                  <xsl:value-of select="$updatedOn"/>
                  <xsl:text>:</xsl:text>
                </p>
                <p>
                  <b>
                    <xsl:text>New Invoice(s): </xsl:text>
                  </b>
                  <xsl:value-of select="$newInvoiceCount"/>
                </p>
                <p>
                  <b>
                    <xsl:text>Updated Invoice(s): </xsl:text>
                  </b>
                  <xsl:value-of select="$updatedInvoiceCount"/>
                </p>
                <p>
                  <b>
                    <xsl:text>Updated By: </xsl:text>
                  </b>
                  <xsl:value-of select="$updatedByName"/>
                  <b>
                    <xsl:text>User ID: </xsl:text>
                  </b>
                  <xsl:value-of select="$updatedByNo"/>
                </p>
                
                <p>
                  <xsl:text> Please login to the portal to view more details. Your portal details are as below: </xsl:text>
                </p>
                <p>
                  <b>
                    <xsl:text>Sub Domain: </xsl:text>
                  </b>
                  <xsl:value-of select="$subDomain"/>
                </p>
                <p>
                  <b>
                    <xsl:text>Portal URL: </xsl:text>
                  </b>
                  <xsl:value-of select="$portalUrl"/>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text>Regards, </xsl:text>
                  <br/>
                  <b>
                    <xsl:value-of select="$publisherName"/>
                  </b>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000; text-align:center">
                  <xsl:value-of select="$copyright"/>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000; text-align:center">
                  <xsl:text>You received this email because you are subscribed to Portal Alerts from </xsl:text>
                  <xsl:value-of select="$businessName"/>
                  <xsl:text>.</xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000; text-align:center">
                  <xsl:text>Update your email preferences to choose the types of emails you receive.</xsl:text>
                </p>
              </div>
            </td>
          </tr>
        </table>
        <!--Main Body End-->
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
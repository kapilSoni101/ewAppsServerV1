<?xml version="1.0" encoding="utf-8"?>


<!--
Subject – <Publisher Co. Name> Business Portal: New user is onboard <App Name> application. 

Dear <Business Co. Name>, You have a new user <New User> onboard <App Name> application on <Publisher Co. Name> 

Business Portal on <date & time>. Please login to the portal to view more details. 

Your portal details are as below: 

Sub Domain: <Sub domain> 
Portal URL: <Business Portal - Payment URL> 

Regards <Publisher Name> <Copyright text set at the Publisher> 

You received this email because you are subscribed to Portal Alerts from <Business Co. Name>. 

Update your email preferences to choose the types of emails you receive.
-->


<!--Not in use-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>

  <xsl:param name="onboardedUserName"/>
  <xsl:param name="paymentAppName"/>
  <xsl:param name="onboardTime"/>
  <xsl:param name="publisherName"/>
  <xsl:param name="businessName"/>
  <xsl:param name="subDomain"/>
  <xsl:param name="portalURL"/>
  <xsl:param name="copyrightText"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>
          <!--HTML document title-->
          <xsl:value-of select="$publisherName"/>
          <xsl:text> Business Portal: New user is onboard </xsl:text>
          <xsl:value-of select="$paymentAppName"/>
          <xsl:text> application.</xsl:text>
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

                  <xsl:text> You have a new user </xsl:text>
                  <xsl:value-of select="$onboardedUserName"/>
                  <xsl:text> onboard  </xsl:text>
                  <xsl:value-of select="$paymentAppName"/>
                  <xsl:text> application on </xsl:text>
                  <xsl:value-of select="$publisherName"/>
                  <xsl:text> Business Portal on </xsl:text>
                  <xsl:value-of select="$onboardTime"/>
                  <xsl:text>. Please login to the portal to view more details. Your portal details are as below: </xsl:text>
                  <xsl:text> Sub Domain:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <!--<xsl:text> Portal URL:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>-->
                  <xsl:text> </xsl:text>
                  <xsl:text> Regards, </xsl:text>
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
                  <xsl:text> You have a new user </xsl:text>
                  <b>
                    <xsl:value-of select="$onboardedUserName"/>
                  </b>
                  <xsl:text> onboard  </xsl:text>
                  <b>
                    <xsl:value-of select="$paymentAppName"/>
                  </b>
                  <xsl:text> application on </xsl:text>
                  <b>
                    <xsl:value-of select="$publisherName"/>
                    <xsl:text> Business Portal</xsl:text>
                  </b>
                  <xsl:text> on </xsl:text>
                  <b>
                    <xsl:value-of select="$onboardTime"/>
                  </b>
                  <xsl:text>.</xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Please login to the portal to view more details. Your portal details are as below: </xsl:text>
                  <br/>
                  <xsl:text> Sub Domain:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$subDomain"/>
                  </b>
                  <br/>
                  <!--<xsl:text> Portal URL:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$portalURL"/>
                  </b>-->
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text>Regards, </xsl:text>
                  <br/>
                  <b>
                    <xsl:value-of select="$publisherName"/>
                  </b>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000; text-align:center">
                  <xsl:value-of select="$copyrightText"/>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000; text-align:center">
                  <xsl:text>You received this email because you are subscribed to Portal Alerts from </xsl:text>
                  <xsl:value-of select="$businessName"/>
                  <xsl:text>.</xsl:text>
                  <br/>
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
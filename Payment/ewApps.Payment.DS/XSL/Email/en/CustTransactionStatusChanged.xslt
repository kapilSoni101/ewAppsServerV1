<?xml version="1.0" encoding="utf-8"?>
<!--
Subject – <Publisher Co. Name> Customer Portal: Status of Transaction <Transaction ID> is updated to <new transaction status>. 
Dear <Customer Co. Name>, Transaction status of an existing transaction with below details is updated on <date & time>. 
Transaction ID: <Transaction status> 
Transaction Amount: <Paid amount with currency type> 
Old Transaction Status: <Old Transaction status> 
New Transaction Status: <New Transaction status> 
Please login to the portal to view more details. Your portal details are as below: 
Sub Domain: <Sub domain> Portal URL: <Customer Portal - Payment URL> 
Regards <Publisher Co. Name> (<Payment> Application) 
<Copyright text set at the Publisher> You received this email because you are subscribed to Portal Alerts from <Business Co. Name>. Update your email preferences to choose the types of emails you receive.
-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>
  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:param name="businessPartnerCompanyName"/>
  <xsl:param name="actionDate"/>
  <xsl:param name="transactionID"/>
  <xsl:param name="transactionAmount"/>
  <xsl:param name="oldTransactionStatus"/>
  <xsl:param name="newTransactionStatus"/>
  <xsl:param name="subDomain"/>
  <xsl:param name="portalURL"/>
  <xsl:param name="applicationName"/>
  <xsl:param name="copyrightText"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>
          <!--HTML document title-->
          <xsl:value-of select="$publisherCompanyName"/>
          <xsl:text> </xsl:text>
          <xsl:text> Customer Portal: Status of Transaction </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$transactionID"/>
          <xsl:text> </xsl:text>
          <xsl:text> is updated to </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$newTransactionStatus"/>
          <xsl:text>. </xsl:text>
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
                  <xsl:value-of select="$businessPartnerCompanyName"/>
                  <xsl:text>, </xsl:text>
                  
                  <xsl:text> Transaction status of an existing transaction with below details is updated on </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$actionDate"/>
                  <xsl:text>. </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:text> Transaction ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionID"/>
                  <xsl:text> Transaction Amount:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="transactionAmount"/>
                  <xsl:text> Old Transaction Status:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$oldTransactionStatus"/>
                  <xsl:text> New Transaction Status:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$newTransactionStatus"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> Please login to the portal to view details. Your Portal details are as below: Sub Domain:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <xsl:text> </xsl:text>
                  <!--<xsl:text>Portal URL: </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>-->
                  <xsl:text> </xsl:text>
                  <xsl:text>Regards, </xsl:text>
                  <xsl:value-of select="$publisherCompanyName"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$copyrightText"/>
                  <xsl:text> You received this email because you are subscribed to Portal Alerts from </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$businessCompanyName"/>
                  <xsl:text>. Update your email preferences to choose the types of emails you receive.</xsl:text>
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
                    <xsl:value-of select="$businessPartnerCompanyName"/>
                  </b>
                  <xsl:text>, </xsl:text>
                </p>

                <!--TODO: ForTesting-->
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Transaction status of an existing transaction with below details is updated on </xsl:text>
                  <xsl:text> </xsl:text>                 
                  <b>
                    <xsl:value-of select="$actionDate"/>
                  </b>
                  <xsl:text>.</xsl:text>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text>Transaction ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionID"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Transaction Amount:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionAmount"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Old Transaction Status:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$oldTransactionStatus"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>New Transaction Status:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$newTransactionStatus"/>
                  <xsl:text>.</xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text> Please login to the portal to view details. Your Portal details are as below: </xsl:text>
                  <br/>
                  <b>
                    <xsl:text> Sub Domain:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <br/>
                  <!--<b>
                    <xsl:text>Portal URL: </xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>-->
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text>Regards, </xsl:text>
                  <br/>
                  <b>
                    <xsl:value-of select="$publisherCompanyName"/>
                  </b>
                  <br/>
                  <xsl:text> (</xsl:text>
                  <xsl:value-of select="$applicationName"/>
                  <xsl:text> Application)</xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000; text-align:center">
                  <xsl:value-of select="$copyrightText"/>
                  <br/>
                  <xsl:text> You received this email because you are subscribed to Portal Alerts from </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$businessCompanyName"/>
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
<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>
  
  <!--Subject – <Publisher Co. Name> Business Portal - New Payment initiated by the <Customer Co. Name>. 
  
Dear <Business Co. Name>, 

Payment with below details initiated against the listed A/R Invoice(s) by <Payee User Type> user, <Payee Name>, User ID <Payee ID>. 

Transaction ID: <Transaction ID> 
Transaction Date: <Transaction Date> 
Transaction Amount: <Paid amount in Business currency type with symbol>
Transaction Status: <Transaction status> 
Transaction Service: <Transaction service> 
Transaction Mode: <Transaction mode> 
Account No. / Card No.: <masked number with last 4 digits visible> 
Customer ID: < Business Partner Co. ID> 
Customer Name: <Business Partner Co. Name>. 

Invoice Details:
Invoice ID Original Received Amount
<same as in receipt> <same as in receipt> 
  
Please login to the portal to view details. Your Portal details are as below:  
Sub Domain: <Sub domain>  
Portal URL: <Business Portal URL with application> 
  
Regards 
<Publisher Co. Name> 
(<Payment> Application) 
  
<Copyright text set at the Publisher> 
You received this email because you are subscribed to Portal Alerts from <Business Co. Name>. 
Update your email preferences to choose the types of emails you receive.-->

  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:param name="businessPartnerCompanyName"/>
  <xsl:param name="userType"/>
  <xsl:param name="dateTime"/>
  <xsl:param name="transactionID"/>
  <xsl:param name="actionDate"/>
  <xsl:param name="transactionAmount"/>
  <xsl:param name="transactionStatus"/>
  <xsl:param name="transactionService"/>
  <xsl:param name="transactionMode"/>
  <xsl:param name="accountNumber"/>
  <xsl:param name="customerId"/>
  <xsl:param name="customerName"/>
  <xsl:param name="userID"/>
  <xsl:param name="userName"/>
  <xsl:param name="subDomain"/>
  <xsl:param name="portalURL"/>
  <xsl:param name="applicationName"/>
  <xsl:param name="copyrightText"/>
  <xsl:param name="userTypeText"/>


  <xsl:template match="/">
    <html>
      <head>
        <title>
          <!--HTML document title-->
          <xsl:value-of select="$publisherCompanyName"/>
          <xsl:text> </xsl:text>
          <xsl:text> Business Portal - New Payment initiated by the </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$businessPartnerCompanyName"/>
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
                  <xsl:value-of select="$businessCompanyName"/>
                  <xsl:text>, </xsl:text>
                  <xsl:text> Payment with below details initiated against the listed Invoice(s) by </xsl:text>                  
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$userName"/>
                  <xsl:text>. Transaction ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionID"/>
                  <xsl:text>. Transaction Date:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$actionDate"/>
                  <xsl:text>. Transaction Amount:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionAmount"/>
                  <xsl:text>. Transaction Status:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionStatus"/>
                  <xsl:text>. Transaction Service:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionService"/>
                  <!--<xsl:text>. Transaction Mode:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionMode"/>-->
                  <xsl:text>. Account No. / Card No.:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$accountNumber"/>
                  <xsl:text>. Customer ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$customerId"/>
                  <xsl:text>. Customer Name:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$businessPartnerCompanyName"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> Please login to the portal to view details. Your Portal details are as below: Sub Domain:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>
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
                    <xsl:value-of select="$businessCompanyName"/>
                  </b>
                  <xsl:text>, </xsl:text>
                </p>

                <!--TODO: ForTesting-->
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Payment with below details initiated against the listed Invoice(s) by </xsl:text>
                  <b><xsl:value-of select="$userTypeText"/></b>
                  <xsl:text> user, </xsl:text>
                  <b><xsl:value-of select="$userName"/></b>
                  <xsl:text>, User ID </xsl:text>
                  <b><xsl:value-of select="$userID"/></b>
                  <xsl:text>.</xsl:text>
                </p> 
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <b>
                    <xsl:text>Transaction ID:</xsl:text>
                  </b>
                  <xsl:value-of select="$transactionID"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Transaction Date:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$actionDate"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Transaction Amount:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionAmount"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Transaction Status:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionStatus"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Transaction Service:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionService"/>
                   <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Transaction Mode:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transactionMode"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Account No. / Card No.:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$accountNumber"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Customer ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$customerId"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Customer Name:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$businessPartnerCompanyName"/>
                  <xsl:text>.</xsl:text>                  
                </p>
                <div style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <div><b>
                  <xsl:text>Invoice Details:</xsl:text>
                  </b>                 
                  </div>
                  <div>
                    <table border="1" cellpadding="0" cellspacing="0" width="100%">
                      <tr>
                        <th>Invoice ID </th>
                        <th>Original Amount</th>
                       <th> Received Amount</th>
                            </tr>
                      <xsl:for-each select="ArrayOfInvoicePaymentDTO/InvoicePaymentDTO">
                                              <tr> 
                                                <td>
                                                <xsl:value-of select="InvoiceId" />
                                              </td>
                                                <td>
                                                  <xsl:value-of select="OrignalAmount" />
                                                </td>
                                                <td>
                                                  <xsl:value-of select="ReceivedAmount" />
                                                </td>
                                              </tr>
                                            </xsl:for-each>
                      <!--<tr>
                        <td>123</td>
                        <td>$120</td>
                        <td>$120</td>
                    </tr>-->
                  </table>
                  
                  </div>
                </div>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text> Please login to the portal to view details. Your Portal details are as below: </xsl:text>
                  <br/>
                  <b>
                    <xsl:text> Sub Domain:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <br/>
                  <b>
                    <xsl:text> Portal URL:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>
                  <br/>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text>Regards, </xsl:text>
                  <br/>
                  <b>
                    <xsl:value-of select="$publisherCompanyName"/>
                  </b>
                  <br/>
                  
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
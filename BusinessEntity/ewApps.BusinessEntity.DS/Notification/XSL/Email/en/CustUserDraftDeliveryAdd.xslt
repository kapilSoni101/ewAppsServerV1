<?xml version="1.0" encoding="utf-8"?>


<!--

Subject – <Publisher Co. Name> Customer Portal: New Draft Delivery <ID> is generated. 

Dear <Customer Co. Name>, 

A New Draft Delivery with below details is generated: 

Draft Delivery ID: <ID> 
Tracking No.: <No.> 
Packaging Slip No.: <Invoice ID> 
Shipment Type: <Type> 
Shipment Plan: <Plan> 
Ship To: <Address>
Total Amount: <Total amount with currency type> 

Please login to the portal to view more details. Your portal details are as below: 
Sub Domain: <Sub domain>
Portal URL: <Customer Portal – Customer Portal URL>

Regards 
<Publisher Co. Name> 

<Copyright text set at the Publisher> You received this email because you are subscribed to Portal Alerts from <Business C


-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>
  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:param name="customerCompanyName"/>
  <xsl:param name="totalAmount"/>
  <xsl:param name="ID"/>
  <xsl:param name="trackingNumber"/>
  <xsl:param name="address"/>
  <xsl:param name="packagingSlipNo"/>
  <xsl:param name="shippingType"/>
  <xsl:param name="shippingPlan"/>
  <xsl:param name="subDomain"/>
  <xsl:param name="portalURL"/>
  <xsl:param name="copyrightText"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>
          <!--HTML document title-->
          <xsl:value-of select="$publisherCompanyName"/>
          <xsl:text> </xsl:text>
          <xsl:text> Customer Portal: New Draft Delivery </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$ID"/>
          <xsl:text> </xsl:text>
          <xsl:text> is generated.  </xsl:text>
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
                  <xsl:value-of select="$customerCompanyName"/>
                  <xsl:text>, </xsl:text>

                  <xsl:text> A New Draft Delivery with below details is generated: </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:text> Draft Delivery ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$ID"/>
                  <xsl:text> Tracking No.:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$trackingNumber"/>                
                  <xsl:text> Packaging Slip No.:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$packagingSlipNo"/>
                  <xsl:text> Shipping Type:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$shippingType"/>
                  <xsl:text> Shipping Plan:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$shippingPlan"/>
                  <xsl:text> Ship To:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$address"/>
                  <xsl:text> Total Amount:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$totalAmount"/>                
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
                    <xsl:value-of select="$customerCompanyName"/>
                  </b>
                  <xsl:text>, </xsl:text>
                </p>

                <!--TODO: ForTesting-->
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> A New </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:text> Draft Delivery </xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> with below details is </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:text> generated</xsl:text>
                  </b>
                  <xsl:text>: </xsl:text>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <b>
                    <xsl:text>Draft Delivery ID:</xsl:text>
                  </b>
                  <xsl:value-of select="$ID"/>

                  <br/>
                  <b>
                    <xsl:text>Tracking No.:</xsl:text>
                  </b>
                  <xsl:value-of select="$trackingNumber"/>
                  <br/>                  
                  <b>
                    <xsl:text>Packaging Slip No.:</xsl:text>
                  </b>
                  <xsl:value-of select="$packagingSlipNo"/>
                  <br/>

                  <b>
                    <xsl:text>Shipping Type:</xsl:text>
                  </b>
                  <xsl:value-of select="$shippingType"/>
                  <br/>
                  <b>
                    <xsl:text>Shipping Plan:</xsl:text>
                  </b>
                  <xsl:value-of select="$shippingPlan"/>
                  <br/>
                  <b>
                    <xsl:text>Ship To:</xsl:text>
                  </b>
                  <xsl:value-of select="$address"/>
                  <br/>
                  <b>
                    <xsl:text>Total Amount:</xsl:text>
                  </b>
                  <xsl:value-of select="$totalAmount"/>                 
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
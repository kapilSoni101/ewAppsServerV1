<?xml version="1.0" encoding="utf-8"?>
   
<!--Subject - <Publisher Co. Name> Business Portal - <application name>: New Delivery is initiated.

Dear <Business Co. Name>,

A new delivery with below details is initiated:

Delivery ID: <Delivery ID>.

Sales Order ID: <Order ID against which delivery was generated>. (if Ad hoc delivery don’t show this item)

Initiated By <Business User who created the Delivery Order>.

Initiated On: <Generated Date and Time of Delivery Order>.

Carrier Service: <carrier service name>.

Service Plan: <carrier service selected plan>.

Freight Charges: <the amount of freight charges>.

Transit Duration: <the number of days in transit from source to destination>.

Expected Pick Up: <the date on which carrier service will pick the package>.

Please login to the portal to view details. Your portal details are as below:

Sub Domain: <Sub domain>

Portal URL: <Customer Portal URL with shipment application>

Regards

<Publisher Co. Name>

<Copyright text set at the Publisher>-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>

  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:param name="applicationName"/>
  <xsl:param name="deliveryID"/>
  <xsl:param name="salesOrderID"/>
  <xsl:param name="initiatedBy"/>
  <xsl:param name="initiatedOn"/> 
  <xsl:param name="carrierService"/>   
  <xsl:param name="servicePlan"/>  
  <xsl:param name="freightCharges"/>  
  <xsl:param name="transitDuration"/>  
  <xsl:param name="expectedPickUp"/>  
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
          <xsl:text> Business Portal - </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$applicationName"/>
          <xsl:text> </xsl:text>
          <xsl:text>: New Delivery is initiated.</xsl:text>         
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
                                       
                  <xsl:text> A new delivery with below details is initiated: Delivery ID:</xsl:text>
                              
                  <xsl:value-of select="$deliveryID"/>
                  <xsl:text>. Sales Order ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$salesOrderID"/>
                  <xsl:text>. Initiated By:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$initiatedBy"/>
                  <xsl:text>. Initiated On:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$initiatedOn"/>
                  <xsl:text>. Carrier Service:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$carrierService"/>
                  <xsl:text>. Service Plan:</xsl:text>                  
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$servicePlan"/>
                  <xsl:text>. Freight Charges:</xsl:text>                  
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$freightCharges"/>
                  <xsl:text>. Transit Duration:</xsl:text>                  
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$transitDuration"/>
                  <xsl:text>. Expected Pick Up:</xsl:text>                  
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$expectedPickUp"/>
                  <xsl:text>. </xsl:text>
                  <xsl:text> Please login to the portal to view details. Your portal details are as below: Sub Domain:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <xsl:text> </xsl:text>
                  <xsl:text>Portal URL: </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>
                  <xsl:text> </xsl:text>
                  <xsl:text>Regards, </xsl:text>
                  <xsl:value-of select="$publisherCompanyName"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$copyrightText"/>
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
                  <xsl:text> A new delivery with below details is initiated: Delivery ID:</xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Delivery ID: </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$deliveryID"/>
                  </b>
                  <xsl:text>.</xsl:text>
               
                  <br/>
                  <xsl:text>Sales Order ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$salesOrderID"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Initiated By:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$initiatedBy"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Initiated On:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$initiatedOn"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Carrier Service:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$carrierService"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Service Plan:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$servicePlan"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>                     
                  <xsl:text>Freight Charges:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$freightCharges"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Transit Duration</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$transitDuration"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <xsl:text>Expected Pick Up:</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$expectedPickUp"/>
                  </b>
                  <xsl:text>.</xsl:text>
                 
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text> Please login to the portal to view details. Your portal details are as below: </xsl:text>
                  <br/>
                  <b>
                    <xsl:text> Sub Domain:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <br/>
                  <b>
                    <xsl:text>Portal URL: </xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>
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
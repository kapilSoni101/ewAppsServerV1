<?xml version="1.0" encoding="utf-8"?>

 
<!--
OS:              Windows-10
Browser:         Chrome Version 0.123
App Version:     1.2
User Name:       Hemendra Swami
User Email:      hswami@eworkplaceapps.com
Phone Number:    <+91 9479439139>
Portal:          Platform/Publisher/Business/Customer/Vendor
Account Name:    <LoggedIn user’s company name, it could be platform, pub, business or customer etc.>
Application:     Payment/CustomerApp/VendorApp/Shipment
Time of Action:  Jan 14, 2020 2:58 PM
Comments:        <Description added on Contact Us form>
 
 
                                                                     <Copyright text set at the Publisher>
                  You received this email because you are subscribed to Portal Alerts from <Business Co. Name>.
-->


<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>
  <xsl:param name="osName"/>
  <xsl:param name="browserName"/>
  <xsl:param name="appVersion"/>
  <xsl:param name="userName"/>
  <xsl:param name="userEmail"/>
<xsl:param name="phoneNumber"/>
<xsl:param name="portal"/>
<xsl:param name="accountName"/>
<xsl:param name="application"/>
<xsl:param name="timeOfAction"/>
<xsl:param name="comments"/>
  <xsl:param name="copyrightText"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>
          <!--HTML document title-->
          <xsl:text>Portal: Contact Us </xsl:text>        
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
                  <xsl:text> OS:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$osName"/>
                  <xsl:text> Browser:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$browserName"/>
                  <xsl:text> App Version:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$appVersion"/>
                  <xsl:text> User Name:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$userName"/>
                  <xsl:text> User Email:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$userEmail"/>
                  <xsl:text> Phone Number:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$phoneNumber"/>
                  <xsl:text> Portal:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portal"/>
                  <xsl:text> Account Name:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$accountName"/>
                  <xsl:text> Application:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$application"/>
                  <xsl:text> Time of Action:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$timeOfAction"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> Comments:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$comments"/>
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
                

                <!--TODO: ForTesting-->
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                   <xsl:text> OS:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$osName"/>
                  <br/>
                  <xsl:text> Browser:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$browserName"/>
                  <br/>
                  <xsl:text> App Version:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$appVersion"/>
                  <br/>
                  <xsl:text> User Name:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$userName"/>
                  <br/>
                  <xsl:text> User Email:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$userEmail"/>
                  <br/>
                  <xsl:text> Phone Number:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$phoneNumber"/>
                  <br/>
                  <xsl:text> Portal:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portal"/>
                  <br/>
                  <xsl:text> Account Name:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$accountName"/>
                  <br/>
                  <xsl:text> Application:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$application"/>
                  <br/>
                  <xsl:text> Time of Action:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$timeOfAction"/>
                  <br/>
                  <xsl:text> </xsl:text>
                  <xsl:text> Comments:  </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$comments"/>
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
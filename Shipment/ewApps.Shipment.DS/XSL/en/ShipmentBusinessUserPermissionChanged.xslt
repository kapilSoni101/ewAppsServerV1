<?xml version="1.0" encoding="utf-8"?>


<!--Subject – <Publisher Co. Name> Business Portal - <Application Name>: Access permissions changed.

Dear <Business User Name>,

< Business User Name who made the change > has changed your access permissions > for <Application Name> application. To view your new permissions, login to the portal and access Business Users.

Regards

<Publisher Co. Name> Team.

<Copyright text set at the Publisher>-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>
  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="businessUserName"/>
  <xsl:param name="businessUserNameChange"/>
  <xsl:param name="applicationName"/>
  <xsl:param name="copyrightText"/>
  <!--businessUserInviteSetPwd-->
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
          <xsl:text>: Access permissions changed.</xsl:text>                 
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
                  <xsl:value-of select="$businessUserName"/>
                  <xsl:text>, </xsl:text>                                  
                  <xsl:value-of select="$businessUserNameChange"/>
                  <xsl:text> </xsl:text>
                  <xsl:text>has changed your access permissions for </xsl:text>
                  <xsl:value-of select="$applicationName"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> application. To view your new permissions, login to the portal and access Business Users. </xsl:text>                 
                  <xsl:text>Regards, </xsl:text>
                  <xsl:value-of select="$publisherCompanyName"/>
                  <xsl:text> Team.</xsl:text>
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
                    <xsl:value-of select="$businessUserName"/>
                  </b>
                  <xsl:text>, </xsl:text>
                </p>

                <!--TODO: ForTesting-->

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <b>
                    <xsl:value-of select="$businessUserNameChange"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text>has changed your access permissions for </xsl:text>
                  <b>
                    <xsl:value-of select="$applicationName"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> application. To view your new permissions, login to the portal and access</xsl:text>
                  <b>
                    <xsl:text> Business Users.</xsl:text>
                  </b>
                </p>


                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text>Regards, </xsl:text>
                  <br/>
                  <b>
                    <xsl:value-of select="$publisherCompanyName"/>
                    <xsl:text> Team.</xsl:text>
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
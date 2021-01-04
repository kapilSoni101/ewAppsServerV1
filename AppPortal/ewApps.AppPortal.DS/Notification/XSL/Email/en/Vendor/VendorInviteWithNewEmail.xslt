<?xml version="1.0" encoding="utf-8"?>
 
<!--Subject - <Publisher Co. Name> <Business Partner Type> Portal - <Application>: New <Business Partner Type> account invitation from <Business Co. Name>.

Dear <Invited User Name>,

<Business Co. Name> hereby invites you to create <Business Partner Co. Name> company account on <Publisher Co. Name> <Business Partner Type> Portal - <Application Name>.

Please click on the below link to access your portal:

<Activation Link to set password screen>

Your Portal details are as below:

Sub Domain: <Sub domain>

Username: <Invited User email id>

Portal URL: <(Business Partner Type) Portal URL with application>

Regards

<Publisher Co. Name>

<Copyright text set at the Publisher >-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>
  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:param name="businessPartnerType"/>
  <xsl:param name="businessPartnerCompanyName"/>
  <xsl:param name="invitedUserName"/>
  <xsl:param name="applicationName"/>
  <xsl:param name="activationLink"/>
  <xsl:param name="subDomain"/>
  <xsl:param name="invitedUserEmailId"/>
  <xsl:param name="portalURL"/>
  <xsl:param name="copyrightText"/>
  <!--businessUserInviteSetPwd-->
  <xsl:template match="/">
    <html>
      <head>
        <title>            
          
          <!--HTML document title-->
          <xsl:value-of select="$publisherCompanyName"/>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$businessPartnerType"/>
          <xsl:text> </xsl:text>
          <xsl:text> Portal - </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$applicationName"/>
          <xsl:text> </xsl:text>
          <xsl:text> : New </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$businessPartnerType"/>
          <xsl:text> </xsl:text>
          <xsl:text> account invitation from </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$businessCompanyName"/>
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
                  <xsl:value-of select="$invitedUserName"/>
                  <xsl:text>, </xsl:text>  
                  
                                    
                  <xsl:value-of select="$businessCompanyName"/>
                  <xsl:text> hereby invites you to create </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$businessPartnerCompanyName"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> company account on </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$publisherCompanyName"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$businessPartnerType"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> Portal - </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$applicationName"/>
                  <xsl:text>. </xsl:text>   
                  <xsl:text> Please click on the below link to access your portal: </xsl:text>
                  <xsl:value-of select="$activationLink"/>
                  <xsl:text> Your Portal details are as below: </xsl:text>
                  <xsl:text> Sub Domain: </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> Username: </xsl:text>
                  <xsl:value-of select="$invitedUserEmailId"/>
                  <xsl:text> </xsl:text>
                  <!--<xsl:text> Portal URL: </xsl:text>
                  <xsl:value-of select="$portalURL"/>-->
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
                    <xsl:value-of select="$invitedUserName"/>
                  </b>
                  <xsl:text>,</xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <b>
                    <xsl:value-of select="$businessCompanyName"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> hereby invites you to create </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$businessPartnerCompanyName"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> company account on </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$publisherCompanyName"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$businessPartnerType"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:text> Portal  </xsl:text>
                  </b>
                  <xsl:text> - </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$applicationName"/>
                  </b>
                  <xsl:text>.</xsl:text>                 
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text> Please click on the below link to access your portal: </xsl:text>                  
                  <br/>
                  <a style="color:#00A091;font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;text-decoration:underline;">
                    <xsl:attribute name="href">
                      <xsl:value-of select="$activationLink"/>
                    </xsl:attribute>
                    <xsl:attribute name="target">_blank</xsl:attribute>
                    <xsl:value-of select="$activationLink"/>
                  </a>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Your Portal details are as below: </xsl:text>
                  <xsl:text> </xsl:text>
                  <br/>
                  <b>
                    <xsl:text>Sub Domain: </xsl:text>
                  </b>
                  <xsl:value-of select="$subDomain"/>
                  <br/>
                  <b>
                    <xsl:text>Username: </xsl:text>
                  </b>
                  <xsl:value-of select="$invitedUserEmailId"/>
                  <br/>
                  <!--<b>
                    <xsl:text>Portal URL: </xsl:text>
                  </b>
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
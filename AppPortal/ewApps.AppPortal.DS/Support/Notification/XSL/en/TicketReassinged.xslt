<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>

  <xsl:param name="publisherName"/>
  <xsl:param name="appName"/>
  <xsl:param name="customerName"/>
  <xsl:param name="customerId"/>
  <xsl:param name="ticketId"/>
  <xsl:param name="companyName"/>
  <xsl:param name="companyId"/>
  <xsl:param name="createdBy"/>
  <xsl:param name="createdOn"/>
  <xsl:param name="title"/>
  <xsl:param name="description"/>
  <xsl:param name="priority"/>
  <xsl:param name="status"/>
  <xsl:param name="activationLink"/>
  <xsl:param name="userType"/>
  <xsl:param name="newAssignedTo"/>
  <xsl:param name="oldAssignedTo"/>
  <xsl:param name="modifiedBy"/>
  <xsl:param name="modifiedOn"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>

          <!--HTML document title-->
          <xsl:value-of select="$publisherName"/>
          <xsl:text> </xsl:text>
          <xsl:choose>
            <xsl:when test="$userType='1'">
              <xsl:text> Publisher Portal </xsl:text>
            </xsl:when>
            <xsl:when test="$userType='2'">
              <xsl:text> Company Portal - </xsl:text>
              <xsl:value-of select="$appName"/>
            </xsl:when>
            <xsl:when test="$userType='3'">
              <xsl:text> Customer Portal - </xsl:text>
              <xsl:value-of select="$appName"/>
            </xsl:when>
          </xsl:choose>
          <xsl:text> ticket : </xsl:text>
          <xsl:value-of select="$ticketId"/>
          <xsl:text> reassigned. </xsl:text>
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
                  <xsl:choose>
                    <xsl:when test="$userType='1'">
                      <xsl:value-of select="$publisherName"/>
                    </xsl:when>
                    <xsl:when test="$userType='2'">
                      <xsl:value-of select="$companyName"/>
                    </xsl:when>
                    <xsl:when test="$userType='3'">
                      <xsl:value-of select="$createdBy"/>
                    </xsl:when>
                  </xsl:choose>
                  <xsl:text>, </xsl:text>
                  <xsl:text> Ticket </xsl:text>
                  <xsl:value-of select="$ticketId"/>
                  <xsl:text> is reassigned to </xsl:text>
                  <xsl:value-of select="$newAssignedTo"/>
                  <xsl:text> from </xsl:text>
                  <xsl:value-of select="$oldAssignedTo"/>
                  <xsl:text> , by </xsl:text>
                  <xsl:value-of select="$modifiedBy"/>
                  <xsl:text> on </xsl:text>
                  <xsl:value-of select="$modifiedOn"/>
                  <xsl:text> Below are the basic details of the ticket:</xsl:text>

                  <xsl:text> Company ID : </xsl:text>
                  <xsl:value-of select="$companyId"/>
                  <xsl:text> Company Name : </xsl:text>
                  <xsl:value-of select="$companyName"/>
                  <xsl:if test="not($customerId='')">
                    <xsl:text> Customer ID : </xsl:text>
                    <xsl:value-of select="$customerId"/>
                    <xsl:text> Customer Name : </xsl:text>
                    <xsl:value-of select="$customerName"/>
                  </xsl:if>
                  <xsl:text> Created By : </xsl:text>
                  <xsl:value-of select="$createdBy"/>
                  <xsl:text> Created On : </xsl:text>
                  <xsl:value-of select="$createdOn"/>
                  <xsl:text> Title : </xsl:text>
                  <xsl:value-of select="$title"/>
                  <xsl:text> Description : </xsl:text>
                  <xsl:value-of select="$description"/>
                  <xsl:text> Priority : </xsl:text>
                  <xsl:value-of select="$priority"/>
                  <xsl:text> Status : </xsl:text>
                  <xsl:value-of select="$status"/>

                  <xsl:text>Please click on the below link to view details:</xsl:text>
                  <xsl:value-of select="$activationLink"/>

                  <xsl:text>Regards, </xsl:text>
                  <xsl:value-of select="$publisherName"/>
                  <xsl:text> Team.</xsl:text>

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
                  <xsl:choose>
                    <xsl:when test="$userType='1'">
                      <xsl:value-of select="$publisherName"/>
                    </xsl:when>
                    <xsl:when test="$userType='2'">
                      <xsl:value-of select="$companyName"/>
                    </xsl:when>
                    <xsl:when test="$userType='3'">
                      <xsl:value-of select="$createdBy"/>
                    </xsl:when>
                  </xsl:choose>
                  <xsl:text>, </xsl:text>
                </p>


                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Ticket </xsl:text>
                  <xsl:value-of select="$ticketId"/>
                  <xsl:text> is reassigned to </xsl:text>
                  <xsl:value-of select="$newAssignedTo"/>
                  <xsl:text> from </xsl:text>
                  <xsl:value-of select="$oldAssignedTo"/>
                  <xsl:text> , by </xsl:text>
                  <xsl:value-of select="$modifiedBy"/>
                  <xsl:text> on </xsl:text>
                  <xsl:value-of select="$modifiedOn"/>
                  <xsl:text> Below are the basic details of the ticket:</xsl:text>
                </p>


                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Company ID : </xsl:text>
                  <xsl:value-of select="$companyId"/>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Company Name : </xsl:text>
                  <xsl:value-of select="$companyName"/>
                </p>
                <xsl:if test="not($customerId='')">
                  <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                    <xsl:text> Customer ID : </xsl:text>
                    <xsl:value-of select="$customerId"/>
                  </p>
                  <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                    <xsl:text> Customer Name : </xsl:text>
                    <xsl:value-of select="$customerName"/>
                  </p>
                </xsl:if>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Created By : </xsl:text>
                  <xsl:value-of select="$createdBy"/>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Created On : </xsl:text>
                  <xsl:value-of select="$createdOn"/>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Title : </xsl:text>
                  <xsl:value-of select="$title"/>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Description : </xsl:text>
                  <xsl:value-of select="$description"/>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Priority : </xsl:text>
                  <xsl:value-of select="$priority"/>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> Status : </xsl:text>
                  <xsl:value-of select="$status"/>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text>Please click on the below link to view details:</xsl:text>
                  <br/>
                  <a style="color:#00A091;font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;text-decoration:underline;">
                    <xsl:attribute name="href">
                      <xsl:value-of select="$activationLink"/>
                    </xsl:attribute>
                    <xsl:attribute name="target">_blank</xsl:attribute>
                    <xsl:value-of select="$activationLink"/>
                  </a>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text>Regards, </xsl:text>
                  <br/>
                  <xsl:value-of select="$publisherName"/>
                  <xsl:text> Team.</xsl:text>
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
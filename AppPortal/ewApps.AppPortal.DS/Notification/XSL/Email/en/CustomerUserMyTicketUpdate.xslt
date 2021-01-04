<?xml version="1.0" encoding="utf-8"?>

<!--

Subject - <Publisher Co. Name> Customer Portal: My Ticket <Ticket ID> of <Payment> application is updated. 

Dear <Customer User Name>, 

My Ticket <Ticket ID> of <Payment> application is updated by <updated by> on <Modified On> for: 

Reassignment: to < New Assigned To >. 
Status: is changed from <Old Status> to <New status>. 
Priority: is changed from <Old Priority> to <New Priority>. 
Comment: <date time>: comments text. 
Attachment: <count> attachments. NOTE – Add Attachments to the email, if any. 

Below are the basic details of the ticket: 
Title: 
Description: 

Please login to the portal to view more details. Your portal details are as below: 
Sub Domain: <Sub domain> 
Portal URL: <Customer Portal - Payment URL> 

Regards Publisher Co. Name> 

<Copyright text set at the Publisher> You received this email because you are subscribed to Portal Alerts from <Business Co. Name>. 

Update your email preferences to choose the types of emails you receive.
-->


<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>
  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:param name="customerCompanyName"/>
  <xsl:param name="customerCompanyID"/>
  <xsl:param name="userName"/>
  <xsl:param name="ticketID"/>
  <xsl:param name="title"/>
  <xsl:param name="description"/>
  <xsl:param name="oldPriority"/>
  <xsl:param name="newPriority"/>
  <xsl:param name="oldStatus"/>
  <xsl:param name="newStatus"/>
  <xsl:param name="dateTime"/>
  <xsl:param name="count"/>
  <xsl:param name="commentsText"/>
  <xsl:param name="modifiedOn"/>
  <xsl:param name="updatedBy"/>
  <xsl:param name="assignedTo"/>
<xsl:param name="oldassignedTo"/>
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
          <xsl:text> Customer Portal: My Ticket </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$ticketID"/>
          <xsl:text> </xsl:text>
          <xsl:text> of </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$applicationName"/>
          <xsl:text> </xsl:text>
          <xsl:text> application is updated.</xsl:text>
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
                  <xsl:value-of select="$userName"/>
                  <xsl:text>, </xsl:text>
                  
                  <xsl:text> My Ticket </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$ticketID"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> of </xsl:text>
                  <xsl:text> </xsl:text>                 
                  <xsl:value-of select="$applicationName"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> application is updated by</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$updatedBy"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> on </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$modifiedOn"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> for: </xsl:text>
                  <xsl:text> </xsl:text>

                  <xsl:if test="($oldassignedTo != $assignedTo)">                
                  <xsl:text> Reassignment: to </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$assignedTo"/>
                  <xsl:text>. </xsl:text>
                  </xsl:if>

                  <xsl:if test="($oldStatus != $newStatus)">
                    <xsl:text> Status: is changed from </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$oldStatus"/>
                  <xsl:text> to </xsl:text>
                  <xsl:value-of select="$newStatus"/>
                  <xsl:text>. </xsl:text>
                  </xsl:if>

 <xsl:if test="($oldPriority != $newPriority)">
                  <xsl:text> Priority: is changed from </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$oldPriority"/>
                  <xsl:text> to </xsl:text>
                  <xsl:value-of select="$newPriority"/>
                  <xsl:text>. </xsl:text>
 </xsl:if>
                  <xsl:if test="($commentsText != '')">
                  <xsl:text> Comment:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$dateTime"/>
                  <xsl:text>: </xsl:text>
                  <xsl:value-of select="$commentsText"/>
                  <xsl:text>. </xsl:text>
                  </xsl:if>

                  <xsl:if test="count &gt;  0">
                  <xsl:text> Attachment(s):</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="count"/>                 
                
                  </xsl:if>   
             
                  <xsl:text> Title:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$title"/>
                  <xsl:text> Description:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$description"/>
                  <xsl:text>. Please login to the portal to view details. Your Portal details are as below: Sub Domain:</xsl:text>
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
                    <xsl:value-of select="$userName"/>
                  </b>
                  <xsl:text>, </xsl:text>
                </p>
                
                <!--TODO: ForTesting-->
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">                 
                  <xsl:text> My Ticket </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$ticketID"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> of </xsl:text>
                  <xsl:text> </xsl:text>                 
                  <b>
                    <xsl:value-of select="$applicationName"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> application is updated by </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$updatedBy"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> on </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$modifiedOn"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> for: </xsl:text>
                </p>


                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
 <xsl:if test="($oldassignedTo != $assignedTo)">  
                  <b>
                    <xsl:text>Reassignment:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> to </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$assignedTo"/>
                  <xsl:text>.</xsl:text>
 </xsl:if>

                  <xsl:if test="($oldStatus != $newStatus)">
                  <br/>
                  <b>
                    <xsl:text>Status:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> is changed from </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$oldStatus"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> to </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$newStatus"/>
                  <xsl:text>.</xsl:text>
                  </xsl:if>

                  <xsl:if test="($oldPriority != $newPriority)">
                  <br/>
                  <b>
                    <xsl:text>Priority:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> is changed from </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$oldPriority"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> to </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$newPriority"/>
                  <xsl:text>.</xsl:text>
                  </xsl:if>

                  <xsl:if test="($commentsText != '')">
                  <br/>
                  <b>
                    <xsl:text>Comment:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$dateTime"/>
                  <xsl:text>: </xsl:text>
                  <xsl:value-of select="$commentsText"/>
                  <xsl:text>.</xsl:text>
                  </xsl:if>

                  <xsl:if test="count &gt;  0">
                  <br/>
                  <b>
                    <xsl:text>Attachment(s):</xsl:text>
                  </b>
                    <xsl:text> </xsl:text>
                    <xsl:value-of select="$count"/>
                    <xsl:text> </xsl:text>                   
</xsl:if>
                  
                </p>
                
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text>  Below are the basic details of the ticket: </xsl:text>
                  <br/>                 
                  <b>
                    <xsl:text>Title:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$title"/>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                    <xsl:text>Description:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$description"/>
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
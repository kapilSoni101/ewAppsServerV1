<?xml version="1.0" encoding="utf-8"?>

<!--Subject - <Publisher Co. Name> Business Portal: Customer Ticket <Ticket ID> of <Customer Co. Name> user for <Payment> application is updated. 

Dear <Business Co. Name>, 

Existing Customer Ticket <Ticket ID> of <Customer Co. Name> user <Payment> application is updated by < Modified By> on <Modified On> for: 

Reassignment: to < New Assigned To >. 
Status: is changed from <Old Status> to <New status>. 
Priority: is changed from <Old Priority> to <New Priority>. 
Comment: <date time>: 
comments text. Attachment(s): <count>. 

Below are the basic details of the ticket: 
Customer ID: 
Customer Name: 
User Name: <created by> 
User ID: 
Contact Email: 
Created On: Title: Description:

Please login to the portal to view more details. Your portal details are as below: 
Sub Domain: <Sub domain> 
Portal URL: <Business Portal - Payment URL> 

Regards  
Publisher Co. Name> 

<Copyright text set at the Publisher> 
You received this email because you are subscribed to Portal Alerts from <Business Co. Name>. 
Update your email preferences to choose the types of emails you receive.-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>
  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="ticketID"/>
  <xsl:param name="customerCompanyName"/>
  <xsl:param name="customerCompanyID"/>
  <xsl:param name="applicationName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:param name="modifiedBy"/>
  <xsl:param name="modifiedOn"/>
  <xsl:param name="newAssignedTo"/>
  <xsl:param name="oldStatus"/>
  <xsl:param name="newStatus"/>
  <xsl:param name="oldPriority"/>
  <xsl:param name="newPriority"/>
  <xsl:param name="dateTime"/>
  <xsl:param name="count"/>
  <xsl:param name="userName"/>
  <xsl:param name="userID"/>
  <xsl:param name="contactEmail"/>
  <xsl:param name="createdOn"/>
  <xsl:param name="title"/>
  <xsl:param name="description"/>
  <xsl:param name="commentsText"/>
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
          <xsl:text> Business Portal: Customer Ticket </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$ticketID"/>
          <xsl:text> </xsl:text>
          <xsl:text> of </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$customerCompanyName"/>
          <xsl:text> </xsl:text>
          <xsl:text> user for </xsl:text>
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
                  <xsl:value-of select="$businessCompanyName"/>
                  <xsl:text>, </xsl:text>
                                    
                  <xsl:text> Existing Customer Ticket </xsl:text>
                  <xsl:value-of select="$ticketID"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> of </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$customerCompanyName"/>
                  <xsl:text> </xsl:text>
                  <xsl:text> user </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$applicationName"/>
                  <xsl:text> </xsl:text>
                  <xsl:text>  application is updated by </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$modifiedBy"/>
                  <xsl:text> </xsl:text>
                  <xsl:text>  on </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$modifiedOn"/>
                  <xsl:text> </xsl:text>
                  <xsl:text>  for: </xsl:text>
                  <xsl:text> </xsl:text>
                  
                  
                  <xsl:text> Reassignment: to</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$newAssignedTo"/>
                  <xsl:text>. Status: is changed from</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$oldStatus"/>
                  <xsl:text> to </xsl:text>
                  <xsl:value-of select="$newStatus"/>
                  <xsl:text>. Priority: is changed from</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$oldPriority"/>
                  <xsl:text> to </xsl:text>
                  <xsl:value-of select="$newPriority"/>
                  <xsl:text>. Comment:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$dateTime"/>
                  <xsl:text>: </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$commentsText"/>
                  <xsl:text>. Attachment(s):</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$count"/>
                  <xsl:text>. Below are the basic details of the ticket: Customer ID: </xsl:text>
                   
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$customerCompanyID"/>
                  <xsl:text>. Customer Name:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$customerCompanyName"/>
                  <xsl:text>. User Name:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$userName"/>
                  <xsl:text>. User ID:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$userID"/>
                  <xsl:text>. Contact Email:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$contactEmail"/>
                  <xsl:text>. Created On:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$createdOn"/>
                  <xsl:text>. Title:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$title"/>
                  <xsl:text>. Description:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$description"/>
                  <xsl:text>. Please login to the portal to view more details. Your portal details are as below: </xsl:text>
                  <xsl:text> Sub Domain:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <!--<xsl:text> Portal URL:</xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>-->
                  <xsl:text> Regards </xsl:text>
                  <xsl:value-of select="$publisherCompanyName"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$copyrightText"/>
                  <xsl:text> You received this email because you are subscribed to Portal Alerts from </xsl:text>
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
                  <xsl:text> Existing Customer Ticket </xsl:text>
                  <b><xsl:value-of select="$ticketID"/></b>
                  <xsl:text> </xsl:text>
                  <xsl:text> of </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$customerCompanyName"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text> user </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$applicationName"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text>  application is updated by </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$modifiedBy"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text>  on </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$modifiedOn"/>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:text>  for: </xsl:text>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <b>
                    <xsl:text>Reassignment: </xsl:text>
                  </b>
                  <xsl:text> to </xsl:text>
                  <b>
                    <xsl:value-of select="$newAssignedTo"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                    <xsl:text>Status:</xsl:text>
                  </b>
                  <xsl:text> is changed from</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$oldStatus"/>
                  </b>
                  <xsl:text> to </xsl:text>
                  <b>
                    <xsl:value-of select="$newStatus"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                    <xsl:text>Priority: </xsl:text>
                  </b>
                  <xsl:text>is changed from</xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$oldPriority"/>
                  </b>
                  <xsl:text> to </xsl:text>
                  <b>
                    <xsl:value-of select="$newPriority"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                    <xsl:text>Comment:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>             
                  <xsl:value-of select="$dateTime"/>                  
                  <xsl:text>: </xsl:text>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$commentsText"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                    <xsl:text>Attachment(s):</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$count"/>
                  </b>
                  <xsl:text>.</xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text>Below are the basic details of the ticket:</xsl:text>
                  <br/>
                  <b>
                      <xsl:text>Customer ID:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$customerCompanyID"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                      <xsl:text>Customer Name:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$customerCompanyName"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                      <xsl:text>User Name</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$userName"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                      <xsl:text>User ID:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$userID"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                      <xsl:text>Contact Email:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$contactEmail"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                      <xsl:text>Created On:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$createdOn"/>
                  </b>
                  <xsl:text>.</xsl:text>
                  <br/>
                  <b>
                      <xsl:text>Title:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$title"/>
                  </b>
                  <xsl:text>.</xsl:text>
                   <br/>
                  <b>
                      <xsl:text>Description:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <b>
                    <xsl:value-of select="$description"/>
                  </b>
                  <xsl:text>.</xsl:text>
                </p>

                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;">
                  <xsl:text> Please login to the portal to view more details. Your portal details are as below: </xsl:text>
                  <br/>
                  <b>
                    <xsl:text>Sub Domain :</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$subDomain"/>
                  <!--<b>
                    <xsl:text>Portal URL:</xsl:text>
                  </b>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="$portalURL"/>-->
                  <br/>
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
                  <xsl:text> You received this email because you are subscribed to Portal Alerts from </xsl:text>
                  <xsl:value-of select="$businessCompanyName"/>
                  <xsl:text>. Update your email preferences to choose the types of emails you receive.</xsl:text>
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
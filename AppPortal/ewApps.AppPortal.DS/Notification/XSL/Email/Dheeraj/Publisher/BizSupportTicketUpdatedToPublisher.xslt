﻿<?xml version="1.0" encoding="utf-8"?>
<!--
Subject - <Publisher Co. Name> Publisher Portal: Existing Ticket <Ticket ID> of <Business / BP Co. Name> user for <Payment> application is updated. 
 
Dear <Publisher Co. Name>, 
 
Existing Ticket <Ticket ID> of <Business / BP Co. Name> user <Payment> application is updated by < Modified By> on <Modified On> for: 
 
Reassignment: to < New Assigned To >. 
Status: is changed from <Old Status> to <New status>. 
Priority:  is changed from <Old Priority> to <New Priority>. 
Comment: <date time>: comments text. 
Attachment(s): <count>. 
 
Below are the basic details of the ticket: 
Business ID:
Business Name:
<BP Type> ID: (if applicable)
<BP Type> Name: (if applicable)
User Name: <created by>
User ID:
Contact Email:
Created On:
Title: 
Description: 
 
Please login to the portal to view more details. Your portal details are as below:
Sub Domain: <Sub domain>
Portal URL: <Publisher Portal URL> 
 
Regards  
<Platform Co. Name> 
 
                                                                    <Copyright text set at the Publisher>
              You received this email because you are subscribed to Portal Alerts from <Publisher Co. Name>.
                                Update your email preferences to choose the types of emails you receive.


-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>
</xsl:stylesheet>

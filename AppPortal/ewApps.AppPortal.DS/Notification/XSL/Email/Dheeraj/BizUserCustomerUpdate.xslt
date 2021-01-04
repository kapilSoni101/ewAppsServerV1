<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
  
  <!--Subject – <Publisher Co. Name> Business Portal: Existing Customer <Customer Co. Name> is updated. 

Dear <Business Co. Name>,  

Existing Customer with below details is updated on <Publisher Co. Name> Business Portal. 

Customer Name: <Customer Co. Name>
Customer ID: < Customer Co. ID>
Updated By: <Business User Name>, User ID: <user id>. 

Please login to the portal to view more details. Your portal details are as below:
Sub Domain: <Sub domain>
Portal URL: <Business Portal - Payment URL> 

Regards  
<Publisher Co. Name> 
(<Payment> Application)

                                                                     <Copyright text set at the Publisher>
               You received this email because you are subscribed to Portal Alerts from <Business Co. Name>.
                                    Update your email preferences to choose the types of emails you receive.-->

  

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>
</xsl:stylesheet>

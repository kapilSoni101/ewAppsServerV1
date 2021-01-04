<?xml version="1.0" encoding="utf-8"?>
<!--
Subject – <Publisher Co. Name> Publisher Portal: Your permissions on Publisher Portal is updated.  
 
Dear <Publisher User Name>,  
 
< Publisher User Name who made the change > has updated your access permissions on Publisher Portal. To view your new permissions, login to the Publisher Portal. 
 
Your portal details are as below:
Sub Domain: <Sub domain>
Portal URL: <Publisher URL> 
 
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

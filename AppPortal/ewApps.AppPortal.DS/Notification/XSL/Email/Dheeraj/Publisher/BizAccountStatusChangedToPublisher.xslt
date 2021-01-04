<?xml version="1.0" encoding="utf-8"?>

<!--
Subject – <Publisher Co. Name> Publisher Portal: <Publisher Co. Name> Business Portal is set to <status> now.
 
Dear <Publisher Co. Name>,  
 
<Publisher User Name> has set <Publisher Co. Name> Business Portal as <status> on <date and time>.
 
Regards  
<Platform Co. Name> 
 
                                                                     <Copyright text set at the Publisher>
               You received this email because you are subscribed to Portal Alerts from <Business Co. Name>.
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

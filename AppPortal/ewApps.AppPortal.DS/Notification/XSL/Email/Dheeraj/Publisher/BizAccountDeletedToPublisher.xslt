<?xml version="1.0" encoding="utf-8"?>
<!--

Subject – <Publisher Co. Name> Publisher Portal: <Business Co. Name> company account has been permanently deleted.
 
Dear <Publisher Co. Name>, 
 
 <Business Co. Name> company account has been permanently deleted by <deleted By> on <date & time>. This Business and its Business Partners won’t be able to access their portals. 
 
Please login to the portal to view more details. Your portal details are as below:
Sub Domain: <Sub domain>
Portal URL: <Publisher Portal URL> 
 
Regards 
<Platform Name> 
 
                                                                 <Copyright text set at the Publisher>
                       You received this email because you are subscribed to Portal Alerts from <Publisher Co. Name>.


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

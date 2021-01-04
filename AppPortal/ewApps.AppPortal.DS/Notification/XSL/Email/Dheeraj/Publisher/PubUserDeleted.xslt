<?xml version="1.0" encoding="utf-8"?>
<!--

Subject – <Publisher Co. Name> Publisher Portal: Publisher User <User name> has been permanently deleted.
 
Dear <Publisher Co. Name>, 
 
Publisher User <User> has been permanently deleted from <Publisher Co. Name> Publisher Portal by <deleted By> on <date & time>. This user won’t be able to access the portal. 
 
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

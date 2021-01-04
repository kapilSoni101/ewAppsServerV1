<?xml version="1.0" encoding="utf-8"?>

<!--
Subject – <Publisher Co. Name> Publisher Portal: New user is onboard Publisher Portal.
 
Dear <Publisher Co. Name>, 
 
You have a new user <New User> onboard on <Publisher Co. Name> Publisher Portal on <date & time>.
 
Please login to the portal to view more details. Your portal details are as below:
Sub Domain: <Sub domain>
Portal URL: <Publisher URL> 
 
Regards 
<Platform Name> 
 
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

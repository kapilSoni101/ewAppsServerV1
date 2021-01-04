<?xml version="1.0" encoding="utf-8"?>
<!--

Subject - <Publisher Co. Name> Publisher Portal: Application <Application Name> is updated.
 
Dear <Publisher Co. Name>,
 
Application <Application Name (not the new one)> with below details is updated by <Updated By> on <Updated On> for:
 
Application Name:  is changed from <Old Name> to <New Name>.
Services enabled: 
1.	Service 1
2.	Service N
Services disabled: 
1.	Service 1
2.	Service N
 
Please login to the Portal to view details:  
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

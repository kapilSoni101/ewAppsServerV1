<?xml version="1.0" encoding="utf-8"?>
<!--

Subject – <Publisher Co. Name> Business Portal: Application(s) access updated for <Business Co. Name>.
 
Dear <Publisher Co. Name>, 
 
Application(s) access updated for <Business Co. Name> by <updated by> on <date & time>.
 
Recently subscribed application(s) details:
1.	<Application Name> with Services <Service 1, Service N> subscribed for <Plan Price> under <Plan Name> activating on <Activation Date>.
2.	<Application Name> with Services <Service 1, Service N> subscribed for <Plan Price> under <Plan Name> activating on <Activation Date>.
N.   <Application Name> with Services <Service 1, Service N> subscribed for <Plan Price> under <Plan Name> activating on <Activation Date>.

 Recently unsubscribed application(s) details:
1.	<Application Name> with Services <Service 1, Service N> subscribed for <Plan Price> under <Plan Name> activating on <Activation Date>.
2.	<Application Name> with Services <Service 1, Service N> subscribed for <Plan Price> under <Plan Name> activating on <Activation Date>.
N.   <Application Name> with Services <Service 1, Service N> subscribed for <Plan Price> under <Plan Name> activating on <Activation Date>.

 
Please login to the portal to view more details. Your portal details are as below:
Sub Domain: <Sub domain>
Portal URL: <Publisher Portal URL> 
 
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

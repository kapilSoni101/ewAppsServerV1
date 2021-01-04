<?xml version="1.0" encoding="utf-8"?>
<!--Not in use-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="onboardedUserName"/>
  <xsl:param name="paymentAppName"/>
  <xsl:param name="onboardTime"/>
  <xsl:param name="appCount"/>

  <xsl:template match="/">
    <xsl:value-of select="$onboardedUserName"/>
    <xsl:text> is a new Business User onboard with </xsl:text>
    <xsl:value-of select="$appCount"/>
    <xsl:text> application access on </xsl:text>
    <xsl:value-of select="$onboardTime"/>
    <xsl:text>.</xsl:text>

    <!--<User name> is a new Business User onboard with <count> applications access on <date & time>.-->
  </xsl:template>
</xsl:stylesheet>
<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="onboardedUserName"/>
  <xsl:param name="appName"/>
  <xsl:param name="onboardTime"/>
  
  <xsl:template match="/">
    <!--<User name> is a new Business User onboard <Payment> application on <date & time>.-->
    <xsl:value-of select="$onboardedUserName"/><xsl:text> is a new Business User onboard </xsl:text> <xsl:value-of select="$appName"/><xsl:text> application on </xsl:text><xsl:value-of select="$onboardTime"/><xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>